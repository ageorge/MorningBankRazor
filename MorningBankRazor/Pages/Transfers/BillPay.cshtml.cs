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
using MorningBankRazor.Models;

namespace MorningBankRazor.Pages.Transfers
{
    //[Authorize]
    public class BillPayModel : PageModel
    {
        private readonly MorningBankRazor.Models.MyBankContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TransactionHistories transactionHistories { get; set; }

        public CheckingAccounts checkingacct { get; set; }

        public BillPayModel(MorningBankRazor.Models.MyBankContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            transactionHistories = new TransactionHistories();

            var userName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;

            checkingacct = _context.CheckingAccounts.FirstOrDefault(c => c.Username == userName);
             
        }

        //List<CheckingAccounts> cacct = null;
        //public List<BillPayment> bills = null;
        public BillPayment bill { get; set; }
 
        public IActionResult OnGet(int? id)
        {
            if(id != null)
            {
                bill = _context.BillPayment.FirstOrDefault(b => b.BillId == id);
                transactionHistories.Amount = (decimal)bill.Amount;
                transactionHistories.SavingAccountNumber = bill.BillId;
                bill.Status = "PAID";
                _context.BillPayment.Update(bill);
            }

            if (checkingacct != null)
            {
                checkingacct.Balance -= transactionHistories.Amount;
                _context.CheckingAccounts.Update(checkingacct);
                transactionHistories.CheckingAccountNumber = checkingacct.CheckingAccountNumber;
            }

            transactionHistories.TransactionFee = 0;
            transactionHistories.TransactionTypeId = 102;

            _context.TransactionHistories.Add(transactionHistories);
            
            _context.SaveChanges();

            return Page();
        } 

        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }
             
        //    //_context.TransactionHistories.Add(transactionHistories);
        //    //await _context.SaveChangesAsync(); 

        //    return RedirectToPage("./Bills");
        //}
    }
}