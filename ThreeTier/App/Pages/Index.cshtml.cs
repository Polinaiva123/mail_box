using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace App.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public IEnumerable<IDictionary<string, string>> Mails { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        private async Task<IActionResult> FetchEmails()
        {
            var httpClient = _httpClientFactory?.CreateClient("API");
            var response = await httpClient.GetAsync("/emails");

            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                if (contentStream.Length > 0)
                {
                    var emails = await JsonSerializer.DeserializeAsync<IEnumerable<IDictionary<string, string>>>(contentStream);
                    Mails = emails;
                }
                else
                {
                    Mails = new List<IDictionary<string, string>>();
                }

                return Page();
            }

            return RedirectToPage("./Error");
        }

        public async Task<IActionResult> OnGetAsync()
        {
            return await FetchEmails();
        }
    }
}