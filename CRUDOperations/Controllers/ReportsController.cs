using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using CRUDOperations.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;


using CRUDOperations.Models;
using CRUDOperations.Assetmanager;

public class ReportsController : Controller
{
    private readonly ApplicationDbContext _context; // Replace with your DbContext

    public ReportsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult GenerateExcelReport()
    {
        List<UsersEntity> customers = _context.Users.ToList(); // Retrieve data from database

        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Customers");

            // Headers
            worksheet.Cells["A1"].Value = "ID";
            worksheet.Cells["B1"].Value = "Name";
            worksheet.Cells["C1"].Value = "Email";
            worksheet.Cells["D1"].Value = "Phone";

            // Data
            int row = 2;
            foreach (var customer in customers)
            {
                worksheet.Cells[row, 1].Value = customer.user_id;
                worksheet.Cells[row, 2].Value = customer.username;
                worksheet.Cells[row, 3].Value = customer.email;
                worksheet.Cells[row, 4].Value = customer.password;
                row++;
            }

            // Auto fit columns
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            // Convert the package to a byte array
            byte[] fileBytes = package.GetAsByteArray();

            // Return the Excel file as a FileStreamResult
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "CustomersReport.xlsx");
        }
    }

    public IActionResult GenerateExcelReport2()
    {
        List<Asset> customers = _context.Assets.ToList(); // Retrieve data from database

        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Customers");

            // Headers
            worksheet.Cells["A1"].Value = "Employee_Id";
            worksheet.Cells["B1"].Value = "Asset_name";
            worksheet.Cells["C1"].Value = "Make_Company";
            worksheet.Cells["D1"].Value = "Value";
            worksheet.Cells["E1"].Value = "Date_of_assign";
            worksheet.Cells["F1"].Value = "Date_of_req";


            // Data
            int row = 2;
            foreach (var customer in customers)
            {
                worksheet.Cells[row, 1].Value = customer.Employee_Id;
                worksheet.Cells[row, 2].Value = customer.Asset_name;
                worksheet.Cells[row, 3].Value = customer.Make_Company;
                worksheet.Cells[row, 4].Value = customer.Value;
                worksheet.Cells[row, 5].Value = customer.Date_of_assign;
                worksheet.Cells[row, 6].Value = customer.Date_of_req;
                row++;
            }

            // Auto fit columns
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            // Convert the package to a byte array
            byte[] fileBytes = package.GetAsByteArray();

            // Return the Excel file as a FileStreamResult
            return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AssetReport.xlsx");
        }
    }
}
