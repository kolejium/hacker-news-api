using HackerNewsApi.Converters;
using System.Text.Json.Serialization;

namespace HackerNewsApi.Models;

public class Base
{
    #region [ Properties ]

    public int Score { get; set; }
    [JsonConverter(typeof(TicksToDateTimeConverter))]
    public DateTime Time { get; set; }

    public string Title { get; set; }

    #endregion
}