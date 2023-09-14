using System.Text.Json.Serialization;

namespace HackerNewsApi.Models;

public class Story : Base
{
    [JsonIgnore]
    public uint Id { get; set; }

    public int CommentCount { get; set; }
    public string PostedBy { get; set; }

    public string Uri { get; set; }
}