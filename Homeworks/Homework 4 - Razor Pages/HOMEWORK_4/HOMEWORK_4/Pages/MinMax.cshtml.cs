using HOMEWORK_4.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HOMEWORK_4.Pages
{
    public class MinMaxModel(IOperations operations) : PageModel
    {
        // to store our results (will use it inside the view)
        public int result { get; set; }

        public IActionResult OnGetMin(int n1, int n2=int.MaxValue, int n3=int.MaxValue)
        {
            result = operations.Min(n1, n2, n3);
            return Page();
        }

        public IActionResult OnGetMax(int n1, int n2=int.MinValue, int n3=int.MinValue)
        {
            result = operations.Max(n1, n2, n3);
            return Page();
        }
    }
}
