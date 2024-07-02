using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUDOperations.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using CRUDOperations.Data;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace CRUDOperations.Controllers;

//[Route("[controller]")]
public class AccountController : Controller
    {
         private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

         [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if a user with the provided username and password exists in the database
                var user = await _context.Users.FirstOrDefaultAsync(u => u.username == model.Username && u.password == model.Password);

                if (user != null)
                {
                    // Authentication successful, create claims
                    // For simplicity, in a real-world scenario, you would typically use ASP.NET Core Identity or similar frameworks for authentication

                    // Example: Setting a claim for username
                    var claims = new[] { new Claim(ClaimTypes.Name, user.username) };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                   // Sign in the user
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    // Redirect to a secure area or home page
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid username or password");
            }

            return View(model);
        }

         [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Perform sign-out using the default authentication scheme (CookieAuthenticationDefaults.AuthenticationScheme)
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirect to the home page or another appropriate page after logout
            return RedirectToAction("Index", "Home");
        }

        // [HttpPost]
        // public async Task<IActionResult> _LoginPartial()
        // {
        //     // Perform sign-out using the default authentication scheme (CookieAuthenticationDefaults.AuthenticationScheme)
        //     await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        //     // Redirect to the home page or another appropriate page after logout
        //     return RedirectToAction("Index", "Home");
       // }
}
