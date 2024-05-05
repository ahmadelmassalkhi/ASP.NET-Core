using HOMEWORK_4.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HOMEWORK_4.Pages
{
    public class LcdGcdModel(IOperations operations) : PageModel
    {
        // to store our results (will use it inside the view)
        public int result { get; set; }

        public IActionResult OnGetLcm(int n1, int n2)
        {
            result = operations.LCM(n1, n2);
            return Page();
        }

        public IActionResult OnGetGcd(int n1, int n2)
        {
            result = operations.GCD(n1, n2);
            return Page();
        }
    }
}
