using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Text.Json;
using JsonOptions = Microsoft.AspNetCore.Http.Json.JsonOptions;

namespace App.Pages.Emails
{
    public class NewModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly JsonSerializerOptions _serializerOptions;

        public NewModel(IHttpClientFactory clientFactory, AppData appData, IOptions<JsonOptions> jsonOptions)
        {
            _clientFactory = clientFactory;
            Konto = appData.Current;
            _serializerOptions = jsonOptions.Value.SerializerOptions;
        }

        public Konto? Konto { get; private set; }

        [BindProperty]
        public Email Email { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (_clientFactory is null)
                throw new ApplicationException("Wystąpił błąd aplikacji");

            if (Konto is null)
                return RedirectToPage("../Konta/Index");

            var httpClient = _clientFactory?.CreateClient("UslugaWiadomosci");

            var jsonStream = JsonContent.Create<Email>(Email, options: _serializerOptions);

            var response = await httpClient!.PostAsync("/emails", jsonStream);

            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                var email = await JsonSerializer.DeserializeAsync<Email>(contentStream, _serializerOptions);

                if (email is null)
                    throw new ApplicationException("Nie udało się pobrać dane utworzonej wiadomości");

                return RedirectToPage("../Emails/Index");
            }

            throw new ApplicationException("Nie udało się nawiązać połączenie z usługą");
        }
    }
}
