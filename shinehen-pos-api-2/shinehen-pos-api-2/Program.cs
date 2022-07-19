using System;
using Newtonsoft.Json;

using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace shinehen_pos_api_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Barimt barimt = new Barimt();
            Stock stock = new Stock();
            Excel excel = new Excel();
            bool isReading = false;
            int row = 1;
            Console.WriteLine(PosAPI.sendData());
            Console.WriteLine(PosAPI.checkApi());
            Console.WriteLine(PosAPI.getInformation());
            string timeStamp = excel.GetTimestamp(DateTime.Now);

            while (isReading)
            {
                row++;
                Range excelRow = excel.readExcel(row);
                if (excelRow[11].Value == null) //K merchantId
                {
                    if (excelRow[4].Value == null) //D amount
                    {
                        Console.WriteLine("Error: Empty input");
                        isReading = false;
                        break;
                    }
                    else
                    {
                        barimt.amount = string.Format("{0:N2}", excelRow[4].Value).Replace(",", ""); //D
                        barimt.vat = "0.00";
                        barimt.cashAmount = barimt.amount;
                        barimt.nonCashAmount = "0.00";
                        barimt.cityTax = "0.00";
                        barimt.districtCode = Convert.ToString(excelRow[8].Value); //H
                        barimt.posNo = "1000";
                        barimt.billType = "1";
                        barimt.billIdSuffix = Convert.ToString(excelRow[10].Value); //J
                        barimt.returnBillId = null;
                        barimt.taxType = "3";
                        barimt.invoiceId = null;
                        barimt.branchNo = Convert.ToString(excelRow[9].Value); //I
                        barimt.date = excelRow[3].Value.ToString("yyyy-MM-dd hh:mm:ss"); //C

                        stock.code = Convert.ToString(excelRow[5].Value); //E
                        stock.name = excelRow[6].Value; //F
                        stock.measureUnit = "1";
                        stock.qty = "1.00";
                        stock.unitPrice = barimt.amount;
                        stock.totalAmount = barimt.amount;
                        stock.cityTax = "0.00";
                        stock.vat = "0.00";
                        stock.barCode = Convert.ToString(excelRow[7].Value); //G
                        barimt.stocks.Add(stock);
                        var json = JsonConvert.SerializeObject(barimt, Formatting.Indented);
                        Console.WriteLine($"---------------Input-{row - 1}-------------");
                        //Console.WriteLine(json);
                        //Console.WriteLine("-----------------------------------");
                        //Console.WriteLine("\n");
                        Barimt deserializedBarimt = JsonConvert.DeserializeObject<Barimt>(json);

                        var outputJson = PosAPI.put(json);
                        //Console.WriteLine(outputJson);
                        var JsonObject = JObject.Parse(outputJson);
                        List<string> outputs = new List<string>();
                        // Lottery don't return if amount is lower than 1
                        double amountDouble = 0;
                        double.TryParse(barimt.amount, out amountDouble);
                        if (amountDouble > 1)
                        {
                            if (JsonObject["success"].ToString() == "False")
                            {
                                outputs.Add(JsonObject["success"].ToString());
                                outputs.Add(JsonObject["errorCode"].ToString());
                                outputs.Add(JsonObject["message"].ToString());
                                Console.WriteLine(timeStamp);
                                excel.writeExcel(row, outputs, false, timeStamp, "");
                            }
                            else
                            {
                                outputs.Add(JsonObject["merchantId"].ToString());
                                outputs.Add(JsonObject["billId"].ToString());
                                outputs.Add(JsonObject["lottery"].ToString());
                                outputs.Add(JsonObject["internalCode"].ToString());
                                outputs.Add(JsonObject["qrData"].ToString());
                                outputs.Add(JsonObject["success"].ToString());
                                excel.writeExcel(row, outputs, true, timeStamp, JsonObject["date"].ToString());
                            }
                        }
                    }
                }
            }
            excel.exitExcel();
            Console.ReadLine();
        }
    }
}
