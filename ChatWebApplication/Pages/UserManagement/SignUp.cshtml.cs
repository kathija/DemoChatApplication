using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ChatWebApplication.ViewModel;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace ChatWebApplication.Pages.UserManagement
{
    [AllowAnonymous]
    public class SignUpModel : PageModel
    {

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public SignUpViewModel SignUpViewModel { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            string apiResponse;
            if (!ModelState.IsValid || SignUpViewModel == null)
            {
                return Page();
            }

            var json = JsonConvert.SerializeObject(SignUpViewModel);
            HttpContent stringContent = new StringContent(json, Encoding.UTF8, "application/json"); 
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync("http://localhost:8004/gateway/account/register", stringContent))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToPage("./Login");
        }
    }
}
