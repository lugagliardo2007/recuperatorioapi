using System.Text.Json.Serialization;

public class Origen
{
    [JsonPropertyName("name")] public string Name { get; set; } = "";
}

public class Personaje
{
    [JsonPropertyName("id")] public int Id { get; set; } 
    [JsonPropertyName("name")] public string Name { get; set; } = "";
    [JsonPropertyName("status")] public string Status { get; set; } = "";
    [JsonPropertyName("species")] public string Species { get; set; } = "";
    [JsonPropertyName("type")] public string Type { get; set; } = "";
    [JsonPropertyName("Origin")] public Origen Origin { get; set; } = new();
}

public class RespuestaApi
{
    [JsonPropertyName("results")] public List<Personaje> Results { get; set; } = new();
}