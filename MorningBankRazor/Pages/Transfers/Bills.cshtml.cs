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

namespace MorningBankRazor.Pages.Transfers
{
    [Authorize]
    public class BillsModel : PageModel
    {
        private readonly MorningBankRazor.Models.MyBankContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public BillsModel(MorningBankRazor.Models.MyBankContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IList<BillPayment> BillPayment { get;set; }

        public async Task OnGetAsync()
        {
            var userName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;

            BillPayment = await _context.BillPayment
                .Where(b => b.Username == userName)
                .ToListAsync();
        }
    }
}
