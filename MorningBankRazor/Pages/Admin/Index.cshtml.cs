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
    public class IndexModel : PageModel
    {
        private readonly MorningBankRazor.Models.MyBankContext _context;

        public IndexModel(MorningBankRazor.Models.MyBankContext context)
        {
            _context = context;
        }

        public IList<AspNetRoles> AspNetRoles { get;set; }

        public async Task OnGetAsync()
        {
            AspNetRoles = await _context.AspNetRoles.ToListAsync();
        }
    }
}
