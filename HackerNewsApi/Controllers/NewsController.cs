using HackerNewsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HackerNewsApi.Controllers;

/// <summary>
///     News controller
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class NewsController : ControllerBase
{
    #region [ Variables ]

    private readonly IHackerNewsService _service;

    #endregion

    #region [ Constructors ]

    /// <summary>
    ///     Create NewsController instance
    /// </summary>
    /// <param name="service">service of hacker news</param>
    public NewsController(IHackerNewsService service)
    {
        _service = service;
    }

    #endregion

    /// <summary>
    ///     Get best stories from foreign api of Hacker News
    /// </summary>
    /// <param name="count">amount of news/articles/stories</param>
    /// <returns>enumerable of stories</returns>
    [HttpGet]
    public async Task<IActionResult> Get(int count)
    {
        return Ok(await _service.GetBestStoriesAsync(count));
    }
}