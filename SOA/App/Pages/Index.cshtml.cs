using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace App.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly AppData _appData;

    public IndexModel(ILogger<IndexModel> logger, AppData appData)
    {
        _logger = logger;
        _appData = appData;
    }

    public IActionResult OnGet()
    {
        if (_appData.Current is null)
            return RedirectToPage("./Konta/Index");
        else
            return RedirectToPage("./Emails/Index");
    }
}
