using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MorningBankRazor.Models;

namespace MorningBankRazor.Pages.Loans
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly MorningBankRazor.Models.MyBankContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel(MorningBankRazor.Models.MyBankContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IList<Loan> Loan { get;set; }

        public async Task OnGetAsync()
        {
            var userName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;

            Loan = await _context.Loan
                .Where(l => l.UserName == userName)
                .ToListAsync();
            
        }
    }
}
