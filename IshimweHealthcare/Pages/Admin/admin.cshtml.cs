using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IshimweHealthcare.Pages.Admin
{

    //[Authorize(Policy = "AdminOnly")]
    public class adminModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
