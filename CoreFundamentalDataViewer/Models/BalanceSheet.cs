using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreFundamentalDataViewer.Models
{
    public class BalanceSheet
    {
        public int Company { get; set; }

        public string FilingName { get; set; }

        public string FilingDate { get; set; }

        public decimal WorkingCapital { get; set; }

    }
}
