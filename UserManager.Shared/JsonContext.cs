using System.Text.Json.Serialization;
using UserManager.Shared.Response;

namespace UserManager.Shared;

[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Default, PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
[JsonSerializable(typeof(UserDto))]
[JsonSerializable(typeof(List<UserDto>))]
[JsonSerializable(typeof(List<BoughtDto>))]
[JsonSerializable(typeof(List<ShopDto>))]
[JsonSerializable(typeof(BaseResult))]
[JsonSerializable(typeof(LoginResult))]
[JsonSerializable(typeof(RegisterResult))]
[JsonSerializable(typeof(TokenDto))]
public partial class JsonContext: JsonSerializerContext
{
    
}