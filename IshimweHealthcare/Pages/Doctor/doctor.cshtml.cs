using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IshimweHealthcare.Pages.Doctor
{
    //[Authorize(Policy = "MustBeADoctor")]
    public class doctorModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
