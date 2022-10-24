using System.Text.Json.Serialization;

namespace UserManager.Shared;

[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Default, PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(UserDto))]
[JsonSerializable(typeof(List<BoughtDto>))]
[JsonSerializable(typeof(List<ShopDto>))]
public partial class JsonContext: JsonSerializerContext
{
    
}