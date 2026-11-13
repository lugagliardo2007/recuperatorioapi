using System.Net.Http.Headers;
using System.Net.Http.Json;

public class ApiController
{
    private readonly HttpClient _http = new() { BaseAddress = new Uri("https://rickandmortyapi.com/api/")};
    private readonly List<Personaje> _cache = new();

    public async Task<Personaje?> BuscarPorId(int id)
    {
        var resp = await _http.GetAsync($"character/{id}");

        if (!resp.IsSuccessStatusCode) return null;
        return await resp.Content.ReadFromJsonAsync<Personaje>();
    }

    public async Task<List<Personaje>> Buscar(string? nombre = null, string? especie = null, string? estado = null)
    {
        var filtros = new List<string>();
        if (!string.IsNullOrEmpty(nombre)) filtros.Add($"name={nombre}");
        if (!string.IsNullOrEmpty(especie)) filtros.Add($"species={especie}");
        if (!string.IsNullOrEmpty(estado)) filtros.Add($"status={estado}");    
        
        var resp = await _http.GetAsync("character/?" + string.Join("&", filtros));
        if(!resp.IsSuccessStatusCode) return new();
        var data = await resp.Content.ReadFromJsonAsync<RespuestaApi>();
        return data?.Results ?? new();
    }

    public void AgregarACache (Personaje p)
    {
        if(!_cache.Any(x => x.Id == p.Id)) _cache.Add(p);
    }

    public List<Personaje> FiltrarPorOrigen(string lugar) =>
        _cache.Where(p => p.Origin.Name.Contains(lugar, StringComparison.OrdinalIgnoreCase)).ToList();
    public Dictionary<string, int> ContarPorEstado() =>
        _cache.GroupBy(p => p.Status.ToLower()).ToDictionary(g => g.Key, g => g.Count());
    public List<Personaje> PorEspecie(string especie) =>
        _cache.Where(p => p.Species.Equals(especie, StringComparison.OrdinalIgnoreCase)).ToList();
    public bool Matar(int id)
    {
        var p = _cache.FirstOrDefault(x => x.Id == id);
        if(p == null) return false;
        p.Status = "Dead";
        return true;
    }
}