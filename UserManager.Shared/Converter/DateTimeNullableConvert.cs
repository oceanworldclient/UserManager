using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace UserManager.Shared.Converter;

public class DateTimeNullableConvert : JsonConverter<DateTime?>
{
    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var readOnlySpan = reader.GetString();
        return readOnlySpan == null ? DateTime.Now : DateTime.ParseExact(readOnlySpan, "yyyy-MM-dd HH:mm:ss", DateTimeFormatInfo.CurrentInfo);
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value?.ToString("yyyy-MM-dd HH:mm:ss"));
    }
}