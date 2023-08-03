using HRSCompute.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRSCompute.Services.Repository
{
    public class NHIFContributionRepository : INHIFContributionRepository
    {
        private readonly Dictionary<Tuple<int, int>, int> contributionTable = new Dictionary<Tuple<int, int>, int>
    {
        { Tuple.Create(0, 5999), 150 },
        { Tuple.Create(6000, 7999), 300 },
        { Tuple.Create(8000, 11999), 400 },
        { Tuple.Create(12000, 14999), 500 },
        { Tuple.Create(15000, 19999), 600 },
        { Tuple.Create(20000, 24999), 750 },
        { Tuple.Create(25000, 29999), 850 },
        { Tuple.Create(30000, 34999), 950 },
        { Tuple.Create(40000, 44999), 1000 },
        { Tuple.Create(45000, 49999), 1100 },
        { Tuple.Create(50000, 59999), 1200 },
        { Tuple.Create(60000, 69999), 1300 },
        { Tuple.Create(70000, 79000), 1400 },
        { Tuple.Create(80000, 89999), 1500 },
        { Tuple.Create(90000, 99999), 1600 },
        { Tuple.Create(100000, int.MaxValue), 1700 },
    };

        public decimal CalculateNHIFContribution(decimal monthlyGrossIncome)
        {
            int nhifContribution = 0;

            foreach (var kvp in contributionTable)
            {
                var incomeRange = kvp.Key;
                var contribution = kvp.Value;

                if (monthlyGrossIncome >= incomeRange.Item1 && monthlyGrossIncome <= incomeRange.Item2)
                {
                    nhifContribution = contribution;
                    break;
                }
            }

            return nhifContribution;
        }
    }
}
