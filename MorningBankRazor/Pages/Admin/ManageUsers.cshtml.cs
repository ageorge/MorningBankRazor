using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MorningBankRazor.Models;

namespace MorningBankRazor.Pages.Admin
{
    public class ManageUsersModel : PageModel
    {
        private readonly MorningBankRazor.Models.MyBankContext _context;

        public ManageUsersModel(MorningBankRazor.Models.MyBankContext context)
        {
            _context = context;
        }

        public IList<UserVM> UserList { get; set; }

        public async Task OnGetAsync()
        {
            var res = await _context.AspNetUsers.ToListAsync();
            UserList = (from item in res
                        select new UserVM
                        {
                            UserName = item.UserName,
                            Email = item.Email,
                            Id = item.Id
                        }).ToList<UserVM>();
        }
    }
}
