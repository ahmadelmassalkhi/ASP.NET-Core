using CVInfoApp.Models;
using CVInfoApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HOMEWORK_5.Pages
{
    public class AddCVModel(ICVService _service) : PageModel
    {
        [BindProperty]
        public CVBindModel Cv { get; set; } = new CVBindModel();

        // View properties
        public string? SuccessMessage = null;
        public List<String> Skills = ["Java", "Python", "ASP"];
		public List<SelectListItem> Nationalities = 
        [
            new SelectListItem(){Value="Lebanon", Text="Lebanon"},
            new SelectListItem(){Value="Germany", Text="Germany"},
            new SelectListItem(){Value="USA", Text="USA"},
            new SelectListItem(){Value="France", Text="France"}
        ];

        public async Task<IActionResult> OnPostAsync(string[] checkedSkills)
        {
            if(checkedSkills.Length != 0) Cv.Skills = [.. checkedSkills];

            // verify
            if (Cv.Verify1 + Cv.Verify2 != Cv.VerifyTotal)
            {
                ModelState.AddModelError(
                    "VerificationError", 
                    $"{Cv.Verify1} + {Cv.Verify2} != {Cv.VerifyTotal}");
            }

            // go back incase of validation failure
            if (!ModelState.IsValid) return Page();

            // attempt to add CV
            try {
                 await _service.AddCV(Cv);
                 SuccessMessage = "Your CV was added successfully !";
            } catch(ArgumentException ex) {
                ModelState.AddModelError("ArgumentException", ex.Message);
                return Page();
            }

            return RedirectToPage("CVs");
        }
    }
}
