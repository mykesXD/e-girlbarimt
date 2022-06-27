using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
namespace eBarimt
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Barimt barimt1 = new Barimt();
            Stock stock1 = new Stock();
            Barimt barimt2 = new Barimt();
            Stock stock2 = new Stock();

            string reg = PosAPI.callFunction("toReg", "РУ13291018");

            stock1.code = "10";
            stock1.name = "Данс нээх хураамж";
            stock1.measureUnit = "1";
            stock1.qty = "1.00";
            stock1.unitPrice = "7500.00";
            stock1.totalAmount = "7500.00";
            stock1.cityTax = "0.00";
            stock1.vat = "0.00";
            stock1.barCode = "7152";
            barimt1.stocks.Add(stock1);
            barimt1.amount = "7500.00";
            barimt1.vat = "0.00";
            barimt1.cashAmount = "7500.00";
            barimt1.nonCashAmount = "0.00";
            barimt1.cityTax = "0.00";
            barimt1.districtCode = "17";
            barimt1.posNo = "5705";
            barimt1.customerNo = "11239010";
            barimt1.billType = "1";
            barimt1.billIdSuffix = "226303";
            barimt1.returnBillId = null;
            barimt1.taxType = "3";
            barimt1.invoiceId = null;
            //barimt1.reportMonth = "2022-04";
            barimt1.branchNo = "570";
            barimt1.date = "2022-06-15 11:45:50";


            stock2.code = "10";
            stock2.name = "Данс нээх хураамж";
            stock2.measureUnit = "1";
            stock2.qty = "1.00";
            stock2.unitPrice = "7500.00";
            stock2.totalAmount = "7500.00";
            stock2.cityTax = "0.00";
            stock2.vat = "0.00";
            stock2.barCode = "7152";
            barimt2.stocks.Add(stock1);
            barimt2.amount = "7500.00";
            barimt2.vat = "0.00";
            barimt2.cashAmount = "7500.00";
            barimt2.nonCashAmount = "0.00";
            barimt2.cityTax = "0.00";
            barimt2.districtCode = "17";
            barimt2.posNo = "5702";
            barimt2.customerNo = "13291018";
            barimt2.billType = "1";
            barimt2.billIdSuffix = "226301";
            barimt2.returnBillId = null;
            barimt2.taxType = "3";
            barimt2.invoiceId = null;
            //barimt2.reportMonth = "2022-04";
            barimt2.branchNo = "570";
            barimt2.date = "2022-04-06 11:45:50";


            string json = JsonConvert.SerializeObject(barimt1, Formatting.Indented);
            //Console.WriteLine(json);
            Barimt deserializedBarimt = JsonConvert.DeserializeObject<Barimt>(json);

            //Console.WriteLine(PosAPI.sendData());
            //Console.WriteLine(PosAPI.put(json));
            //Console.WriteLine(PosAPI.checkApi());
            //Console.WriteLine(PosAPI.getInformation());
            Console.ReadLine();
        }
    }
}