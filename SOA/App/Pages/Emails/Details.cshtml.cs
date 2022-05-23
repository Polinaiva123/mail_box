using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System.Text.Json;
using JsonOptions = Microsoft.AspNetCore.Http.Json.JsonOptions;


namespace App.Pages.Emails
{
    public class DetailsModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly JsonSerializerOptions _serializerOptions;

        public DetailsModel(IHttpClientFactory clientFactory, AppData appData, IOptions<JsonOptions> jsonOptions)
        {
            _clientFactory = clientFactory;
            Konto = appData.Current;
            _serializerOptions = jsonOptions.Value.SerializerOptions;
        }

        public Konto? Konto { get; private set; }
        public Email Email { get; private set; } = null!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (_clientFactory is null)
                throw new ApplicationException("Wystąpił błąd aplikacji");

            if (Konto is null)
                return RedirectToPage("../Konta/Index");

            var httpClient = _clientFactory?.CreateClient("UslugaWiadomosci");

            var response = await httpClient!.GetAsync($"/emails/{id}");
            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                var emails = await JsonSerializer.DeserializeAsync<Email>(contentStream, _serializerOptions);

                if (emails is null)
                    throw new ApplicationException($"Nie udało się pobrać wiadomość [ Id: {id} ]");

                Email = emails;

                return Page();
            }

            throw new ApplicationException("Nie udało się nawiązać połączenie z usługą");
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (_clientFactory is null)
                throw new ApplicationException("Wystąpił błąd aplikacji");

            if (Konto is null)
                return RedirectToPage("../Konta/Index");

            var httpClient = _clientFactory?.CreateClient("UslugaWiadomosci");

            var response = await httpClient!.DeleteAsync($"/emails/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }

            throw new ApplicationException("Nie udało się nawiązać połączenie z usługą");
        }
    }
}
