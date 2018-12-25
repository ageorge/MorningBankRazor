using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MorningBankRazor.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;

namespace MorningBankRazor.Pages.Admin
{
    public class CreateModel : PageModel
    {
        private readonly MorningBankRazor.Models.MyBankContext _context;
        IServiceProvider _serviceProvider;

        public CreateModel(MorningBankRazor.Models.MyBankContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }


        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AspNetRoles AspNetRoles { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var RoleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = _serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            IdentityResult roleResult;
            var roleCheck = await RoleManager.RoleExistsAsync(AspNetRoles.Name);
            if (!roleCheck)
            {
                roleResult = await RoleManager.CreateAsync(new IdentityRole(AspNetRoles.Name));
            }
            //_context.AspNetRoles.Add(AspNetRoles);
            //await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}