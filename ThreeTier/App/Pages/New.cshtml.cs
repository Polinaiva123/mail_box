using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Pages
{
    public class NewModel : PageModel
    {
        private IHttpClientFactory _clientFactory;

        public bool Sent { get; private set; }

        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public string From { get; set; }
        [BindProperty]
        public string To { get; set; }
        [BindProperty]
        public string Title { get; set; }
        [BindProperty]
        public string Text { get; set; }

        public NewModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var httpClient = _clientFactory.CreateClient("API");

            var data = new Dictionary<string, string>()
            {
                { "from", From },
                { "to", To },
                { "name", Name },
                { "title", Title },
                { "text", Text },
            };

            var response = await httpClient.PostAsJsonAsync("/email/new", data);

            if (response.IsSuccessStatusCode)
            {
                Sent = true;
                return Page();
            }

            return RedirectToPage("./Error");
        }
    }
}
