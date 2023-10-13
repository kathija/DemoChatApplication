using ChatWebApplication.ViewModel;
using JwtTokenAuthenticationManager.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace ChatWebApplication.Pages.UserManagement
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        public LoginModel()
        {

        }
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public LoginViewModel LoginViewModel { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            AuthenticationResponse responseModel = new AuthenticationResponse();
            if (!ModelState.IsValid || LoginViewModel == null)
            {
                return Page();
            }

            var json = JsonConvert.SerializeObject(LoginViewModel);
            HttpContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync("http://localhost:8004/gateway/account/login", stringContent))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        responseModel = JsonConvert.DeserializeObject<AuthenticationResponse>(apiResponse);    
                        
                        return RedirectToPage("./Chat/Index");
                    }                    
                }
            }
            ModelState.AddModelError("Error", responseModel.Message);
            return RedirectToPage("./Login");
        }
    }
}
