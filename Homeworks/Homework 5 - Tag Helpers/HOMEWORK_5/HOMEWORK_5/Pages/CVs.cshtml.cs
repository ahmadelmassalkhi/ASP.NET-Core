using CVInfoApp.Models;
using CVInfoApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HOMEWORK_5.Pages
{
    public class CVsModel(ICVService _service) : PageModel
    {
        public List<CV> CVs { get; set; } = [];

		public IActionResult OnGet()
        {
            CVs = _service.GetAllCVs();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(long id)
        {
            await _service.DeleteCVAsync(id);
            return OnGet();
        }
    }
}
