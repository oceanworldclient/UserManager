using System.Text.Json.Serialization;
using UserManager.Server.Converter;

namespace UserManager.Server.Model;

public class ShopContent
{
    [JsonConverter(typeof(StringToIntConverter))]
    [JsonPropertyName("bandwidth")]
    public int Bandwidth { get; set; }
    
    [JsonConverter(typeof(StringToIntConverter))]
    [JsonPropertyName("class")]
    public int Class { get; set; }
    
    [JsonConverter(typeof(StringToIntConverter))]
    [JsonPropertyName("class_expire")]
    public int ClassExpire { get; set; }
    
    [JsonConverter(typeof(StringToIntConverter))]
    [JsonPropertyName("speedlimit")]
    public int SpeedLimit { get; set; }
    
    [JsonConverter(typeof(StringToIntConverter))]
    [JsonPropertyName("connector")]
    public int Connector { get; set; }
    
}