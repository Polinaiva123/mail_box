using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Text.Json;
using JsonOptions = Microsoft.AspNetCore.Http.Json.JsonOptions;

namespace App.Pages.Emails
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly JsonSerializerOptions _serializerOptions;

        public IndexModel(IHttpClientFactory clientFactory, AppData appData, IOptions<JsonOptions> jsonOptions)
        {
            _clientFactory = clientFactory;
            Konto = appData.Current;
            _serializerOptions = jsonOptions.Value.SerializerOptions;
        }

        public IEnumerable<Email> Emails { get; private set; }
        public Konto? Konto { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (_clientFactory is null)
                throw new ApplicationException("Wystąpił błąd aplikacji");

            if (Konto is null)
                return RedirectToPage("../Konta/Index");

            var httpClient = _clientFactory?.CreateClient("UslugaWiadomosci");

            var jsonStream = JsonContent.Create<Konto>(Konto, options: _serializerOptions);

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(httpClient!.BaseAddress!, "emails"),
                Content = jsonStream
            };

            var response = await httpClient!.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                var emails = await JsonSerializer.DeserializeAsync<IEnumerable<Email>>(contentStream, _serializerOptions);

                if (emails is null)
                    throw new ApplicationException("Nie udało się pobrać wiadomosci");

                Emails = emails;

                return Page();
            }

            throw new ApplicationException("Nie udało się nawiązać połączenie z usługą");
        }
    }
}
