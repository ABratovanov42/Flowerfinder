using System.Text.Json;

namespace Flowerfinder.Services
{
    // One species guess from the identifier, best first
    public record PlantGuess(string ScientificName, string? CommonName, double Score);

    /// <summary>
    /// Identifies a flower photo via the PlantNet API (https://my.plantnet.org).
    /// With ApiKey "demo" it returns a canned result after a realistic pause,
    /// so the identify page can be used before an API key exists.
    /// </summary>
    public class PlantNetService
    {
        private readonly HttpClient _http;
        private readonly string _apiKey;
        private readonly ILogger<PlantNetService> _log;

        public PlantNetService(HttpClient http, IConfiguration config, ILogger<PlantNetService> log)
        {
            _http = http;
            _apiKey = config["PlantNet:ApiKey"] ?? "";
            _log = log;
        }

        public bool IsConfigured => !string.IsNullOrWhiteSpace(_apiKey);
        public bool IsDemo => _apiKey == "demo";

        public async Task<List<PlantGuess>?> IdentifyAsync(Stream image, string fileName, string contentType, CancellationToken ct = default)
        {
            if (!IsConfigured) return null;

            if (IsDemo)
            {
                // pretend to think, then answer like the real API would
                await Task.Delay(1400, ct);
                return new List<PlantGuess>
                {
                    new("Rosa gallica", "Gallic rose", 0.86),
                    new("Paeonia lactiflora", "Chinese peony", 0.07),
                    new("Dahlia pinnata", "Garden dahlia", 0.03)
                };
            }

            using var form = new MultipartFormDataContent();
            var file = new StreamContent(image);
            file.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
            form.Add(file, "images", fileName);
            form.Add(new StringContent("flower"), "organs");

            var url = "https://my-api.plantnet.org/v2/identify/all?api-key=" + Uri.EscapeDataString(_apiKey) + "&lang=en";

            HttpResponseMessage response;
            try
            {
                response = await _http.PostAsync(url, form, ct);
            }
            catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException)
            {
                _log.LogWarning(ex, "PlantNet request failed");
                return null;
            }

            if (!response.IsSuccessStatusCode)
            {
                _log.LogWarning("PlantNet returned {Status}", (int)response.StatusCode);
                return null;
            }

            using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync(ct));
            if (!doc.RootElement.TryGetProperty("results", out var results)) return null;

            var guesses = new List<PlantGuess>();
            foreach (var r in results.EnumerateArray())
            {
                var species = r.GetProperty("species");
                var sci = species.GetProperty("scientificNameWithoutAuthor").GetString();
                if (string.IsNullOrWhiteSpace(sci)) continue;

                string? common = null;
                if (species.TryGetProperty("commonNames", out var names) && names.GetArrayLength() > 0)
                    common = names[0].GetString();

                guesses.Add(new PlantGuess(sci, common, r.GetProperty("score").GetDouble()));
                if (guesses.Count >= 5) break;
            }
            return guesses;
        }
    }
}
