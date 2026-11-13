var api = new ApiController();

foreach (var p in await api.Buscar(especie: "human", estado: "alive"))
    api.AgregarACache(p);

foreach (var kv in api.ContarPorEstado())
    Console.WriteLine($"{kv.Key}: {kv.Value}");

api.Matar(1);

foreach (var kv in api.ContarPorEstado())
    Console.WriteLine($"{kv.Key}: {kv.Value}");

foreach (var p in api.FiltrarPorOrigen("Earth"))
    Console.WriteLine($"{p.Name} viene de {p.Origin.Name}");