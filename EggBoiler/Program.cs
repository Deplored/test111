using System.Net.Http.Json;

using var http = new HttpClient { BaseAddress = new Uri("https://kitchen.example.com/api/") };

async Task Call(string step, HttpMethod method, string path, object? body = null)
{
    Console.WriteLine($"{step}...");
    var req = new HttpRequestMessage(method, path) { Content = body is null ? null : JsonContent.Create(body) };
    (await http.SendAsync(req)).EnsureSuccessStatusCode();
}

await Call("Filling pot with water", HttpMethod.Post, "pot/water", new { liters = 1 });
await Call("Turning on stove", HttpMethod.Put, "stove", new { on = true, power = "high" });
await Call("Waiting for water to boil", HttpMethod.Get, "pot/boiling");
await Call("Adding egg", HttpMethod.Post, "pot/eggs", new { count = 1 });
await Task.Delay(TimeSpan.FromMinutes(7));
await Call("Removing egg", HttpMethod.Delete, "pot/eggs");
await Call("Turning off stove", HttpMethod.Put, "stove", new { on = false });

Console.WriteLine("Egg is boiled!");
