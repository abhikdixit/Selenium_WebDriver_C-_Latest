using System;
using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NUnit.Framework;

namespace WebOrderTests
{
    public class ReadExcel
    {
        private static ISheet ExcelWSheet;
        private static IWorkbook ExcelWBook;

        // Method to read test data from an Excel file
        public string[,] GetExcelData(string fileName, string sheetName)
        {
            string[,] arrayExcelData;

            using (FileStream ExcelFile = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                ExcelWBook = new XSSFWorkbook(ExcelFile);
                ExcelWSheet = ExcelWBook.GetSheet(sheetName);

                int totalNoOfRows = ExcelWSheet.PhysicalNumberOfRows - 1;
                int totalNoOfCols = ExcelWSheet.GetRow(0).PhysicalNumberOfCells;

                arrayExcelData = new string[totalNoOfRows, totalNoOfCols];

                for (int i = 0; i < totalNoOfRows; i++)
                {
                    IRow row = ExcelWSheet.GetRow(i + 1);
                    for (int j = 0; j < totalNoOfCols; j++)
                    {
                        arrayExcelData[i, j] = row.GetCell(j)?.ToString() ?? string.Empty;
                    }
                }
            }

            return arrayExcelData;
        }
    }
}
