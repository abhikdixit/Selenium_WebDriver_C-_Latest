using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections;

public class WebOrder_TestData
{
    public static IEnumerable LoginData()
    {
        yield return new TestCaseData("Tester", "test");
        yield return new TestCaseData("Tester", "test");
        yield return new TestCaseData("Tester", "test");
        yield return new TestCaseData("Tester", "test");
    }

    public static IEnumerable OrderTestData()
    {
        yield return new TestCaseData("1", "5", "Andrew V", "123 Main St", "Washington", "DC", "20010", "123456789", "12/24", "New order has been successfully added.");
        yield return new TestCaseData("0", "0", "Andrew V", "123 Main St", "Washington", "DC", "20010", "123456789", "12/24", "Quantity must be greater than zero.");
        yield return new TestCaseData("1", "5", "", "123 Main St", "Washington", "DC", "20010", "123456789", "12/24", "Field 'Customer name' cannot be empty.");
        yield return new TestCaseData("1", "5", "Andrew V", "123 Main St", "Washington", "DC", "", "123456789", "12/24", "Field 'Zip' cannot be empty.");
        yield return new TestCaseData("1", "5", "Andrew V", "123 Main St", "Washington", "DC", "ff", "123456789", "12/24", "Invalid format. Only digits allowed.");
        yield return new TestCaseData("1", "5", "Andrew V", "123 Main St", "Washington", "DC", "20010", "ff", "12/24", "Invalid format. Only digits allowed.");
        yield return new TestCaseData("1", "5", "Andrew V", "123 Main St", "Washington", "DC", "20010", "123456789", "ff/ff", "Invalid format. Required format is mm/yy.");
    }

    public static IEnumerable CreateOrderTestData()
    {
        yield return new TestCaseData("MyMoney", "5", "Minh", "123 Main St", "Dallas", "75000", "Visa", "123456789", "12/23", "valid");
        yield return new TestCaseData("MyMoney", "5", "Nguyen", "123 Main St", "Dallas", "75000", "MasterCard", "123456789", "12/23", "valid");
        yield return new TestCaseData("MyMoney", "", "Minh", "123 Main St", "Dallas", "75000", "Visa", "123456789", "12/23", "empty_quantity");
        yield return new TestCaseData("MyMoney", "0", "Minh", "123 Main St", "Dallas", "75000", "Visa", "123456789", "12/23", "invalid_quantity");
        yield return new TestCaseData("MyMoney", "5", "", "123 Main St", "Dallas", "75000", "Visa", "123456789", "12/23", "empty_name");
        yield return new TestCaseData("MyMoney", "5", "Minh", "", "Dallas", "75000", "Visa", "123456789", "12/23", "empty_street");
        yield return new TestCaseData("MyMoney", "5", "Minh", "123 Main St", "", "75000", "Visa", "123456789", "12/23", "empty_city");
        yield return new TestCaseData("MyMoney", "5", "Minh", "123 Main St", "Dallas", "", "Visa", "123456789", "12/23", "empty_zip");
        yield return new TestCaseData("MyMoney", "5", "Minh", "123 Main St", "Dallas", "abcde", "Visa", "123456789", "12/23", "invalid_zip");
        yield return new TestCaseData("MyMoney", "5", "Minh", "123 Main St", "Dallas", "75000", "", "123456789", "12/23", "empty_card");
        yield return new TestCaseData("MyMoney", "5", "Minh", "123 Main St", "Dallas", "75000", "Visa", "", "12/23", "empty_cardnr");
        yield return new TestCaseData("MyMoney", "5", "Minh", "123 Main St", "Dallas", "75000", "Visa", "abcdef", "12/23", "invalid_cardnr");
        yield return new TestCaseData("MyMoney", "5", "Minh", "123 Main St", "Dallas", "75000", "Visa", "123456789", "", "empty_expdate");
        //yield return new TestCaseData("MyMoney", "5", "Minh", "123 Main St", "Dallas", "75000", "Visa", "123456789", "abcdef", "invalid_date");
    }

    public static IEnumerable<object[]> ReadDataFromExcel()
    {
        string filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestDataFile", "WebOrder_Login_TestData.xlsx");
        List<object[]> loginData = new List<object[]>();
        IWorkbook workbook;
        using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            if (filePath.EndsWith(".xls"))
                workbook = new HSSFWorkbook(file);
            else
                workbook = new XSSFWorkbook(file);
            //IWorkbook workbook = new XSSFWorkbook(file);
            ISheet sheet = workbook.GetSheet("Login");

            if (sheet == null)
            {
                throw new Exception("Sheet 'Login' not found in the Excel file.");
            }

            // Read header row to find the column index of "uname" and "upass"
            IRow headerRow = sheet.GetRow(0);
            if (headerRow == null)
            {
                throw new Exception("Excel sheet is empty.");
            }

            int unameIndex = -1;
            int upassIndex = -1;

            for (int i = 0; i < headerRow.LastCellNum; i++)
            {
                string columnName = headerRow.GetCell(i)?.ToString().Trim().ToLower();
                if (columnName == "uname") unameIndex = i;
                if (columnName == "upass") upassIndex = i;
            }

            if (unameIndex == -1 || upassIndex == -1)
            {
                throw new Exception("Column names 'uname' and 'upass' not found in Excel.");
            }

            // Read data rows based on column indices
            for (int i = 1; i < sheet.PhysicalNumberOfRows; i++)  // Skipping header row
            {
                IRow row = sheet.GetRow(i);
                if (row == null) continue;

                string username = row.GetCell(unameIndex)?.ToString() ?? "";
                string password = row.GetCell(upassIndex)?.ToString() ?? "";

                loginData.Add(new object[] { username, password });
            }
        }
        return loginData;
    }

}