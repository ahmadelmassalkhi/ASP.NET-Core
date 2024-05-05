using HOMEWORK_4.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HOMEWORK_4.Pages
{
    public class SumMulModel(IOperations operations) : PageModel
    {
        // to store our results (will use it inside the view)
        public int result { get; set; }
        
        public IActionResult OnGetSum(int n1, int n2=0, int n3=0)
        {
            result = operations.Sum(n1, n2, n3);
            return Page();
        }

        public IActionResult OnGetMul(int n1, int n2=1, int n3=1)
        {
            result = operations.Multiply(n1, n2, n3);
            return Page();
        }
    }
}
