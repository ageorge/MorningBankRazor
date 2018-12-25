using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MorningBankRazor.Models;

namespace MorningBankRazor.Pages.Loans
{
    public class DetailsModel : PageModel
    {
        private readonly MorningBankRazor.Models.MyBankContext _context;

        public DetailsModel(MorningBankRazor.Models.MyBankContext context)
        {
            _context = context;
        }

        public Loan Loan { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Loan = await _context.Loan.FirstOrDefaultAsync(m => m.LoanId == id);

            if (Loan == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
