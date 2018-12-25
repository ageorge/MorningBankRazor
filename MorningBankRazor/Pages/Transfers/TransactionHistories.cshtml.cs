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
    public class TransactionHistoriesModel : PageModel
    {
        private readonly MorningBankRazor.Models.MyBankContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public TransactionHistoriesModel(MorningBankRazor.Models.MyBankContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IList<TransactionHistories> transactionHistories { get;set; }

        public async Task OnGetAsync()
        {
            var userName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;

            List<CheckingAccounts> cacct = null; 

            cacct = await _context.CheckingAccounts 
                .OrderBy(c => c.CheckingAccountNumber)
                .Where(c => c.Username == userName)
                .ToListAsync();
            
            transactionHistories = await _context.TransactionHistories
                .Include(t => t.TransactionType)
                .Where(t => t.CheckingAccountNumber == cacct[0].CheckingAccountNumber) 
                .ToListAsync();
            
        }
    }
}
