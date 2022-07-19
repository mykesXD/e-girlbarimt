﻿using System;
using Newtonsoft.Json;

using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace goodsec_pos_api
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Excel excel = new Excel();
            bool isReading = false; 
            int row = 1;
            Console.WriteLine(PosAPI.sendData());
            //Console.WriteLine(PosAPI.checkApi());
            //Console.WriteLine(PosAPI.getInformation());
            //Console.WriteLine(PossAPI.returnBill("{\"returnBillId\" : \"000005208173000220623001000100310\",\"date\" : \"2022-06-27 11:32:04\"}"));
            
            string timeStamp = excel.GetTimestamp(DateTime.Now);

            /*Microsoft.Office.Interop.Excel.Application excel1 = new Microsoft.Office.Interop.Excel.Application();
            Workbook wBook;
            Worksheet wSheet;
            string filePath = "c:\\Users\\ztuws\\Documents\\BarimtExcel\\butsaah.xlsx";
            wBook = excel1.Workbooks.Open(filePath);
            wSheet = wBook.Worksheets[1];
            bool isReading1 = true;

            while (isReading1)
            {
                row++;
                Range cells = wSheet.Range[$"A{row}", $"F{row}"];
                if (cells[2].Value == null)
                {
                    Console.WriteLine("Error: Empty input");
                    isReading1 = false;
                    wBook.Save();
                    wBook.Close();
                    excel1.Quit();
                    break;
                }
                else
                {
                    string resultStr = PosAPI.returnBill("{\"returnBillId\" : \"" + cells[2].Value + "\",\"date\" : \"" + cells[5].Value + "\"}");
                    Console.WriteLine(cells[2].Value + "-" + resultStr);
                    excel1.DisplayAlerts = false;
                    wSheet.Cells[6][row].Value = resultStr;
                }*/



            while (isReading) {
                row++;
                Range excelRow = excel.readExcel(row);
                if (excelRow[12].Value == null)
                {
                    if (excelRow[5].Value == null)
                    {
                        Console.WriteLine("Error: Empty input");
                        isReading = false;
                        break;
                    }
                    else
                    {
                        Barimt barimt = new Barimt();
                        Stock stock = new Stock();
                        barimt.amount = string.Format("{0:N2}", excelRow[5].Value).Replace(",","");
                        barimt.vat = "0.00";
                        barimt.cashAmount = barimt.amount;
                        barimt.nonCashAmount = "0.00";
                        barimt.cityTax = "0.00";
                        barimt.districtCode = Convert.ToString(excelRow[9].Value);
                        barimt.posNo = "1000";
                        //barimt.customerNo = Convert.ToString(excelRow[2].Value);
                        barimt.billType = "1";
                        barimt.billIdSuffix = Convert.ToString(excelRow[11].Value);
                        barimt.returnBillId = null;
                        barimt.taxType = "3";
                        barimt.invoiceId = null;
                        //barimt1.reportMonth = "2022-04";
                        barimt.branchNo = Convert.ToString(excelRow[10].Value);
                        barimt.date = excelRow[1].Value.ToString("yyyy-MM-dd hh:mm:ss");

                        stock.code = Convert.ToString(excelRow[6].Value);
                        stock.name = excelRow[7].Value;
                        stock.measureUnit = "1";
                        stock.qty = "1.00";
                        stock.unitPrice = barimt.amount;
                        stock.totalAmount = barimt.amount;
                        stock.cityTax = "0.00";
                        stock.vat = "0.00";
                        stock.barCode = Convert.ToString(excelRow[8].Value);
                        barimt.stocks.Add(stock);
                        var json = JsonConvert.SerializeObject(barimt, Formatting.Indented);
                        Console.WriteLine($"---------------Input-{row - 1}-------------");
                        Console.WriteLine(json);
                        Console.WriteLine("-----------------------------------");
                        Console.WriteLine("\n");
                        Barimt deserializedBarimt = JsonConvert.DeserializeObject<Barimt>(json);
                        /*
                        Console.WriteLine($"--------------Output-{row-1}-------------");
                        Console.WriteLine(PosAPI.put(json));
                        Console.WriteLine("-----------------------------------");
                        Console.WriteLine("\n");
                        */
                        var outputJson = PosAPI.put(json);
                        Console.WriteLine(outputJson);
                        var JsonObject = JObject.Parse(outputJson);
                        List<string> outputs = new List<string>();
                        Console.WriteLine("WROOOONG");
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
            //Console.WriteLine(PosAPI.sendData());
            //Console.WriteLine(PosAPI.put(json));
            //Console.WriteLine(PosAPI.checkApi());
            //Console.WriteLine(PosAPI.getInformation());
            excel.exitExcel();
            Console.ReadLine();
        }
    }
}
