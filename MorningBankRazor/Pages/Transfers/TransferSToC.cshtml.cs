﻿using System;
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
    public class TransferSToCModel : PageModel
    {
        private readonly MorningBankRazor.Models.MyBankContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public TransferSToCModel(MorningBankRazor.Models.MyBankContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;

            //var userName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;

            //cacct = _context.CheckingAccounts.FirstOrDefault(c => c.Username == userName); 
            //sacct = _context.SavingAccounts.FirstOrDefault(s => s.Username == userName);
        }

        public decimal? checkingBalance { get; set; }
        public decimal? savingBalance { get; set; }

        CheckingAccounts cacct { get; set; }
        SavingAccounts sacct { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;

            cacct = await _context.CheckingAccounts.FirstOrDefaultAsync(c => c.Username == userName);
            sacct = await _context.SavingAccounts.FirstOrDefaultAsync(s => s.Username == userName);

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
            var userName = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;

            cacct = await _context.CheckingAccounts.FirstOrDefaultAsync(c => c.Username == userName);
            sacct = await _context.SavingAccounts.FirstOrDefaultAsync(s => s.Username == userName);
            if (cacct != null)
            {
                cacct.Balance += transactionHistories.Amount;
                _context.CheckingAccounts.Update(cacct);
                transactionHistories.CheckingAccountNumber = cacct.CheckingAccountNumber;
            }

            if (sacct != null)
            {
                sacct.Balance -= (transactionHistories.Amount + transactionHistories.TransactionFee);
                _context.SavingAccounts.Update(sacct);
                transactionHistories.SavingAccountNumber = sacct.SavingAccountNumber;
            }

            transactionHistories.TransactionFee = 5.0M;
            transactionHistories.TransactionTypeId = 101;

            _context.TransactionHistories.Add(transactionHistories);

            await _context.SaveChangesAsync();

            return RedirectToPage("./TransactionHistories");
        }
    }
}