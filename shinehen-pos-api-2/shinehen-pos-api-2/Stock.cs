using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shinehen_pos_api_2
{
    class Stock
    {
        public string code { get; set; }
        public string name { get; set; }
        public string measureUnit { get; set; }
        public string qty { get; set; }
        public string unitPrice { get; set; }
        public string totalAmount { get; set; }
        public string cityTax { get; set; }
        public string vat { get; set; }
        public string barCode { get; set; }
    }
}
