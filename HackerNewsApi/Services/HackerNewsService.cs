using HackerNewsApi.Interfaces;
using HackerNewsApi.Models;

using Microsoft.Extensions.Caching.Memory;

namespace HackerNewsApi.Services;

public class HackerNewsService : IHackerNewsService
{
    #region [ Variables ]

    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;
    private readonly ILogger<HackerNewsService> _logger;

    #endregion

    #region [ Constructors ]

    public HackerNewsService(HttpClient httpClient, IMemoryCache cache, ILogger<HackerNewsService> logger)
    {
        _httpClient = httpClient;
        _cache = cache;
        _logger = logger;
    }

    #endregion

    /// <inheritdoc cref="IHackerNewsService.GetBestStoriesAsync"/>
    public async Task<List<Story>> GetBestStoriesAsync(int count)
    {
        var bestStories = await GetBestStoriesWithDetailsAsync(count);

        bestStories.Sort((x, y) => y.Score.CompareTo(x.Score));

        _logger.LogDebug($"Best stories amount: {bestStories.Count} - Requested: {count}");

        return bestStories;
    }

    private async Task<List<int>> GetBestStoryIdsAsync()
    {
        if (!_cache.TryGetValue("BestStoryIds", out List<int> bestStoryIds))
        {
            var response = await _httpClient.GetAsync("https://hacker-news.firebaseio.com/v0/beststories.json");
            response.EnsureSuccessStatusCode();

            bestStoryIds = await response.Content.ReadFromJsonAsync<List<int>>();

            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
            };

            _cache.Set("BestStoryIds", bestStoryIds, cacheEntryOptions);

        }

        _logger.Log(LogLevel.Debug, $"Amount of best stories id: {bestStoryIds.Count}");

        return bestStoryIds;
    }

    /// <inheritdoc cref="IHackerNewsService.GetStoryDetailsAsync"/>
    public async Task<Story> GetStoryDetailsAsync(int storyId)
    {
        var cacheKey = $"StoryDetails_{storyId}";
        if (_cache.TryGetValue(cacheKey, out Story cachedStory))
        {
            return cachedStory;
        }

        var response = await _httpClient.GetAsync($"https://hacker-news.firebaseio.com/v0/item/{storyId}.json");
        response.EnsureSuccessStatusCode();

        var model = await response.Content.ReadFromJsonAsync<HackerNewsStory>();
        var story = new Story
        {
            Id = model.Id,
            CommentCount = model.Kids?.Count() ?? 0,
            PostedBy = model.By ?? string.Empty,
            Score = model.Score,
            Time = model.Time,
            Title = model.Title,
            Uri = model.Url ?? string.Empty
        };

        var cacheEntryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
        };

        _logger.LogDebug($"New cached story details key: {cacheKey}");

        _cache.Set(cacheKey, story, cacheEntryOptions);

        return story;
    }

    private async Task<List<Story>> GetBestStoriesWithDetailsAsync(int count)
    {
        if (count <= 0)
            return new List<Story>();

        var bestStoryIds = await GetBestStoryIdsAsync();
        var bestStories = new List<Story>();
        var min = Math.Min(count, bestStoryIds.Count);

        for (var i = 0; i < min; i++)
        {
            var storyDetails = await GetStoryDetailsAsync(bestStoryIds[i]);
            bestStories.Add(storyDetails);
        }

        return bestStories;
    }
}