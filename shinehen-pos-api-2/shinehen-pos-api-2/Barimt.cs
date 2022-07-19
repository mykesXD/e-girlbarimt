using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shinehen_pos_api_2
{
    class Barimt
    {
        public string amount { get; set; }
        public string vat { get; set; }
        public string cashAmount { get; set; }
        public string nonCashAmount { get; set; }
        public string cityTax { get; set; }
        public string districtCode { get; set; }
        public string posNo { get; set; }
        public string customerNo { get; set; }
        public string billType { get; set; }
        public string billIdSuffix { get; set; }
        public string returnBillId { get; set; }
        public string taxType { get; set; }
        public string invoiceId { get; set; }
        public string reportMonth { get; set; }
        public string branchNo { get; set; }
        public string date { get; set; }

        public List<Stock> stocks = new List<Stock>();
        Stock stock1 = new Stock();
    }
}
