using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Pages
{
    public class ClearModel : PageModel
    {
        private IHttpClientFactory _clientFactory;
        public bool Cleared { get; private set; }

        public ClearModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var httpClient = _clientFactory.CreateClient("API");

            var response = await httpClient.PostAsync("/clear", null);

            if (response.IsSuccessStatusCode)
            {
                Cleared = true;
                return Page();
            }

            return RedirectToPage("./Error");
        }
    }
}
