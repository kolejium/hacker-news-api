namespace HackerNewsApi.Models;

public class HackerNewsStory : Base
{
    #region [ Properties ]

    public string? By { get; set; }

    public uint Id { get; set; }

    public IEnumerable<int>? Kids { get; set; }

    public string? Url { get; set; }

    #endregion
}