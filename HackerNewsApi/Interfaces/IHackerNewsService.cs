using HackerNewsApi.Models;

namespace HackerNewsApi.Interfaces;

/// <summary>
///     Defines methods for work with Hacker News
/// </summary>
public interface IHackerNewsService
{
    /// <summary>
    ///     Get best stories
    /// </summary>
    /// <param name="count">amount of best stories</param>
    /// <returns>list of stories</returns>
    Task<List<Story>> GetBestStoriesAsync(int count);

    /// <summary>
    ///     Get best story by id
    /// </summary>
    /// <param name="storyId">unique identifier</param>
    /// <returns>story</returns>
    Task<Story> GetStoryDetailsAsync(int storyId);
}