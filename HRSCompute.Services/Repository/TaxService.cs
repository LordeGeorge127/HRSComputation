using HRSCompute.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSCompute.Services.Repository
{
    public class TaxService : ITaxRepository
    {
        // Tax rates and bands for the 2021/2022 tax year (example)
        private const decimal RateBand1 = 24000m;
        private const decimal RateBand2 = 40000m;
        private const decimal RateBand3 = 60000m;

        private const decimal TaxRate1 = 0.1m;
        private const decimal TaxRate2 = 0.25m;
        private const decimal TaxRate3 = 0.3m;

        public decimal taxRate { get; private set; }

        private decimal tax;
       

        public decimal TaxAmount(decimal totalAmount)
        {
            decimal taxableIncome = totalAmount;

            // Check if the taxable income is below the first tax band
            if (taxableIncome <= 24000)
            {
                taxRate = .1m;
                tax = taxableIncome * taxRate;
            }

            // Check if the taxable income is within the second tax band
            if (taxableIncome <= 40000)
            {
                taxRate = .25m;
                // Calculate tax in the first band           
                // Calculate tax in the second band
               
                tax = (24000 * .1m) + ((taxableIncome - 24000) * taxRate);
            }

            // Check if the taxable income is within the third tax band
            if (taxableIncome <= RateBand3)
            {
                taxRate = .3m;
                // Calculate tax in the first band
                // Calculate tax in the second band
                // Calculate tax in the third band

                tax = (24000 * .1m) + ((40000 - 24000) * .25m) + ((taxableIncome - 40000) * taxRate);
            }

            // Calculate tax for incomes above the third tax band

            decimal taxBand1 = RateBand1 * TaxRate1;
            decimal taxBand2 = (RateBand2 - RateBand1) * TaxRate2;
            decimal taxBand3 = (RateBand3 - RateBand2) * TaxRate3;
            decimal remainingIncomeAboveBand3 = taxableIncome - RateBand3;
            decimal taxBand4 = remainingIncomeAboveBand3 * TaxRate3;

            return taxBand1 + taxBand2 + taxBand3 + taxBand4;
        }
    }
}

