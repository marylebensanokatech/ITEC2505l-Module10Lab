using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VirtualPetAdoption.Models;

namespace VirtualPetAdoption.Pages
{
    public class QuizModel : PageModel
    {
        // Add the database context
        private readonly PetAdoptionContext _context;

        //Add the page model with the database context - so the page (html page) can be updated
        //with data from the database
        public QuizModel(PetAdoptionContext context)
        {
            _context = context;
        }

        //Bind properties persist the form data after the user add their name and their energy preference
        [BindProperty]
        public string Name { get; set;}

        [BindProperty]
        public int EnergyPreference { get; set; }

        // Need to add on get method, but there is nothing we need to do to display the page because
        //it is straight HTML - no interactivity
        public void OnGet()
        {
            
        }

        // onPost method is called when the form submit button is clicked
        //This method contains the logic for displaying a pet to the user
        public async Task<IActionResult> OnPostAsync()
        {
            // Get a list of pets from the database using the db context
            var pets =  await _context.Pets.ToListAsync();

            //Declare a variable to store the pet that is the best match and a variable
            //to help calculate the best pet
            Pet bestMatch = null;
            int smallestDifference = int.MaxValue;

            //Find the best pet match by looping through the pets from the database
            // and comparing their energy levels to the user's level
            foreach (var pet in pets)
            {
                //Calculate the difference between the user's level and the pet's level of energy
                int difference = Math.Abs(pet.EnergyLevel = EnergyPreference);

                // Test if the difference is the smallest one so far
                if (difference < smallestDifference)
                {
                    //update the variable
                    smallestDifference = difference;
                    bestMatch = pet;
                } //end if
            }//end method

            // returns the redirect to the results page
            return RedirectToPage("./Results", new {petId = bestMatch.Id, userName = Name});
        }
    }
}