using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MorningBankRazor.Models;

namespace MorningBankRazor.Pages.Transfers
{
    [Authorize]
    public class TransferCToSModel : PageModel
    {
        private readonly MorningBankRazor.Models.MyBankContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public TransferCToSModel(MorningBankRazor.Models.MyBankContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

            var userName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;

            cacct = _context.CheckingAccounts.FirstOrDefault(c => c.Username == userName);

            sacct = _context.SavingAccounts.FirstOrDefault(s => s.Username == userName);
        }

        public decimal? checkingBalance { get; set; }
        public decimal? savingBalance { get; set; }

        CheckingAccounts cacct { get; set; }
        SavingAccounts sacct { get; set; }

        public IActionResult OnGet()
        { 
            
            if (cacct != null)
            {
                checkingBalance = cacct.Balance;
            }

            if (sacct != null)
            {
                savingBalance = sacct.Balance;
            }

            return Page();
        }

        [BindProperty]
        public TransactionHistories transactionHistories { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            
            if(cacct != null)
            {
                cacct.Balance -= transactionHistories.Amount; 
                _context.CheckingAccounts.Update(cacct);
                transactionHistories.CheckingAccountNumber = cacct.CheckingAccountNumber;
            }
            
            if(sacct != null)
            {
                sacct.Balance += transactionHistories.Amount; 
                _context.SavingAccounts.Update(sacct);
                transactionHistories.SavingAccountNumber = sacct.SavingAccountNumber;
            } 

            transactionHistories.TransactionFee = 0;
            transactionHistories.TransactionTypeId = 100;

            _context.TransactionHistories.Add(transactionHistories);

            await _context.SaveChangesAsync();

            return RedirectToPage("./TransactionHistories");
        }
    }
}