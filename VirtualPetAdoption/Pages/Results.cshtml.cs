using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VirtualPetAdoption.Models;
 
 namespace VirtualPetAdoption.Pages
{
    public class ResultsModel : PageModel
    {
        // Add the database context
        private readonly PetAdoptionContext _context;

        // Create the page model using the database context
        public ResultsModel(PetAdoptionContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string UserName { get; set;}  //user name that was entered into the form

        [BindProperty(SupportsGet = true)]
        public int PetId {get; set; }
        // Create a pet object
        public Pet Pet {get; set;}

        //OnGet method that is called when a Get HTTP request is sent from the user's computer to the server
        public async Task<IActionResult> OnGetAsync()
        {
            //Set up redirects to the other pages from this one

            //If the pet ID <= 0 we'll send the user back to the home page
            if (PetId <= 0)
            {
                return RedirectToPage("./Index");
            }

            // Get the pet form the database context and populate the Pet object we created earlier
            Pet = await _context.Pets.FindAsync(PetId);

            // Test if the pet is null (if something is wrong ) and redirect the user to the home page
            if (Pet == null)
            {
                return RedirectToPage("./Index");
            }

            //Return the page 
            return Page();
        }
    }
}