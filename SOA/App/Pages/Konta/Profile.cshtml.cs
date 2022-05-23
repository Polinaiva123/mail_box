using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Pages.Konta
{
    public class ProfileModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly AppData _appData;

        public ProfileModel(IHttpClientFactory clientFactory, AppData appData)
        {
            _clientFactory = clientFactory;
            Konto = appData.Current;
            _appData = appData;
        }

        public Konto? Konto { get; private set; }

        [BindProperty]
        public string Action { get; set; }

        public IActionResult OnGet()
        {
            if (Konto is null)
                return RedirectToPage("./Index");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            switch (Action)
            {
                case "deleteKonto":
                    if (Konto is null)
                        return RedirectToPage("./Index");

                    if (_clientFactory is null)
                        throw new ApplicationException("Wystąpił błąd aplikacji");

                    var httpClient = _clientFactory?.CreateClient("UslugaKont");

                    var response = await httpClient!.DeleteAsync($"/konta/{Konto.Id}");
                    if (response.IsSuccessStatusCode)
                    {
                        _appData.Current = null;
                        return RedirectToPage("./Index");
                    }

                    throw new ApplicationException("Nie udało się nawiązać połączenie z usługą");

                case "logout":
                    _appData.Current = null;
                    return RedirectToPage("./Index");

                default:
                    throw new ApplicationException("Wystąpił błąd aplikacji");
            }
            
        }
    }
}
