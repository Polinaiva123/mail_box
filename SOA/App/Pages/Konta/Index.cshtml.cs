using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Microsoft.Extensions.Options;
using JsonOptions = Microsoft.AspNetCore.Http.Json.JsonOptions;


namespace App.Pages.Konta
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly AppData _appData;
        private readonly JsonSerializerOptions _serializerOptions;

        public IndexModel(IHttpClientFactory clientFactory, AppData appData, IOptions<JsonOptions> jsonOptions)
        {
            _clientFactory = clientFactory;
            _appData = appData;
            _serializerOptions = jsonOptions.Value.SerializerOptions;
        }

        public IEnumerable<Konto> Konta { get; private set; }

        [BindProperty]
        public Konto? NewKonto { get; set; }

        [BindProperty]
        public int? SelectedKontoId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (_appData.Current is not null)
                return RedirectToPage("../Emails/Index");

            Konta = await FetchKonta();

            return Page();
        }

        public async Task<IEnumerable<Konto>> FetchKonta()
        {
            if (_clientFactory is null)
                throw new ApplicationException("Wystąpił błąd aplikacji");

            var httpClient = _clientFactory?.CreateClient("UslugaKont");

            var response = await httpClient!.GetAsync("/konta");
            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                var konta = await JsonSerializer.DeserializeAsync<IEnumerable<Konto>>(contentStream, _serializerOptions);

                if (konta is null)
                    throw new ApplicationException("Nie udało się pobrać dane kont");

                return konta;
            }

            throw new ApplicationException("Nie udało się nawiązać połączenie z usługą");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (SelectedKontoId is not null)
            {
                Konta = await FetchKonta();
                var konto = Konta.Where(k => k.Id == SelectedKontoId).FirstOrDefault();

                if (konto is null)
                    throw new ApplicationException("Nie udało się odnaleźć wybrane konto");

                _appData.Current = konto;

                return RedirectToPage("../Emails/Index");
            }
            else if (NewKonto is not null)
            {
                if (_clientFactory is null)
                    throw new ApplicationException("Wystąpił błąd aplikacji");

                var httpClient = _clientFactory?.CreateClient("UslugaKont");

                var jsonStream = JsonContent.Create<Konto>(NewKonto, options: _serializerOptions);

                var response = await httpClient!.PostAsync("/konta", jsonStream);

                if (response.IsSuccessStatusCode)
                {
                    var contentStream = await response.Content.ReadAsStreamAsync();
                    var k = await JsonSerializer.DeserializeAsync<Konto>(contentStream, _serializerOptions);

                    if (k is null)
                        throw new ApplicationException("Nie udało się pobrać dane utworzonego konta");

                    _appData.Current = k;

                    return RedirectToPage("../Emails/Index");
                }

                throw new ApplicationException("Nie udało się nawiązać połączenie z usługą");
            }

            throw new ApplicationException("Wystąpił błąd aplikacji");
        }
    }
}
