using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MorningBankRazor.Models;

namespace MorningBankRazor.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class AddUserModel : PageModel
    {
        private readonly MorningBankRazor.Models.MyBankContext _context;
        public AddUserModel(IServiceProvider serviceProvider, MorningBankRazor.Models.MyBankContext context)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }
        IServiceProvider _serviceProvider;

        [BindProperty]
        public List<string> RoleId { get; set; }
        public List<RoleInfo> RolesList { get; set; } // will be bound to checkboxes

        [BindProperty]
        public UserInfo UInfo { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            UInfo = new UserInfo();
            RolesList = await _context.AspNetRoles.Select(m => new RoleInfo
            {
                RoleId = m.Id,
                RoleName = m.Name
            }).ToListAsync<RoleInfo>();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var RoleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = _serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            //add role to the database received in BindProperty
            var user = new IdentityUser { UserName = UInfo.Email, Email = UInfo.Email };
            var result = await UserManager.CreateAsync(user, UInfo.Password);
            if (result.Succeeded)
            {
                // add roles for user
                foreach (string roleid in RoleId)
                {
                    var role = _context.AspNetRoles.FirstOrDefault<AspNetRoles>(m => roleid == m.Id);
                    await UserManager.AddToRoleAsync(user, role.Name);
                }
            }
            return RedirectToPage("./Index");
        }

        private bool AspNetRolesExists(string id)
        {
            return _context.AspNetRoles.Any(e => e.Id == id);
        }
    }
}