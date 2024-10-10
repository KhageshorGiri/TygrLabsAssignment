using System.Text.Json;

namespace TygrLabs.Shared.Helpers;

public static class JsonFileHelper
{
    public static void WriteToFile<T>(string filePath, T data)
    {
        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
    }

    public static T ReadFromFile<T>(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return default;
        }

        var json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<T>(json);
    }
}
