using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
namespace goodsec_pos_api
{
    class Excel
    {
        Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
        Workbook wBook;
        Worksheet wSheet;
        string filePath = "c:\\Users\\ztuws\\Documents\\BarimtExcel\\oruulah-last.xlsx";
        public String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }
        //  ...later on in the code
        public void exitExcel()
        {
            excel.Quit();
        }
        public Range readExcel(int row)
        {
            wBook = excel.Workbooks.Open(filePath);
            wSheet = wBook.Worksheets[1];
            Range cells = wSheet.Range[$"A{row}", $"R{row}"];
            return cells;
        }
        
        public void writeExcel(int row,List<string> list,bool success,string timeStamp, string date)
        {

            if(row == 2)
            {
                filePath = "c:\\Users\\ztuws\\Documents\\BarimtExcel\\oruulah-last.xlsx";
                wBook = excel.Workbooks.Open(filePath);
                wSheet = wBook.Worksheets[1];
                Range cells = wSheet.Range[$"L{row}", $"R{row}"];
                if (success == false)
                {
                    cells = wSheet.Range[$"Q{row}", $"S{row}"];
                }
                else
                {
                    wSheet.Cells[20][row].Value = date;

                }
                string savePath = $"c:\\Users\\ztuws\\Documents\\BarimtExcel\\Ebarimt-{timeStamp}.xlsx";
                string[] arr = list.ToArray();
                cells.set_Value(XlRangeValueDataType.xlRangeValueDefault, arr);
                excel.DisplayAlerts = false;
                wBook.SaveAs(savePath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing);
                wBook.Close();
                excel.Quit();

            }
            else
            {
                filePath = $"c:\\Users\\ztuws\\Documents\\BarimtExcel\\Ebarimt-{timeStamp}.xlsx";
                wBook = excel.Workbooks.Open(filePath);
                wSheet = wBook.Worksheets[1];
                Range cells = wSheet.Range[$"L{row}", $"R{row}"];
                if (success == false)
                {
                    cells = wSheet.Range[$"Q{row}", $"S{row}"];
                }
                else
                {
                    wSheet.Cells[20][row].Value = date;
                }

                string savePath = $"c:\\Users\\ztuws\\Documents\\BarimtExcel\\Ebarimt-{timeStamp}.xlsx";
                string[] arr = list.ToArray();
                cells.set_Value(XlRangeValueDataType.xlRangeValueDefault, arr);
                excel.DisplayAlerts = false;
                wBook.SaveAs(savePath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing);
                wBook.Close();
                excel.Quit();
                
            }
        }
        
    }
}
