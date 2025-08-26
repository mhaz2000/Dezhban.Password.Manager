using ClosedXML.Excel;
using Dezhban.Core.Entities;
using Dezhban.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Dezhban.ApplicationServices.Services.Users
{
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordRepository _repository;

        public PasswordService(IPasswordRepository repository)
        {
            _repository = repository;
        }

        public async Task UpdatePasswordAsync(PasswordModel model)
        {
            var password = await _repository.GetAsync(c => c.Id == model.Id);
            password.Title = model.Title;
            password.Password = model.Password;
            password.Username = model.Username;
            password.AdditionalData = model.AdditionalData ?? "";

            await _repository.UpdateAsync(password);
        }
        public async Task AddPasswordAsync(PasswordModel model)
        {
            await _repository.AddAsync(model);
        }

        public async Task DeletePasswordAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<List<PasswordModel>> GetPasswordsAsync()
        {
            return await _repository.GetMany(_ => true).ToListAsync();
        }

        public async Task<MemoryStream> GetPasswordsExcelBackupFileAsync()
        {
            var passwords = await _repository.GetMany(_ => true).ToListAsync();

            using var workbook = new XLWorkbook();
            var ws = workbook.AddWorksheet("Passwords");

            // Header row
            ws.Cell(1, 1).Value = "Title";
            ws.Cell(1, 2).Value = "Username";
            ws.Cell(1, 3).Value = "Password";
            ws.Cell(1, 4).Value = "Additional Data";

            // Make header bold and add background color
            var headerRange = ws.Range(1, 1, 1, 4);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
            headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            int row = 2;
            foreach (var p in passwords)
            {
                ws.Cell(row, 1).Value = p.Title;
                ws.Cell(row, 2).Value = p.Username;
                ws.Cell(row, 3).Value = p.Password;
                ws.Cell(row, 4).Value = p.AdditionalData;
                row++;
            }

            // Auto-fit columns
            ws.Columns().AdjustToContents();

            // Freeze the header row
            ws.SheetView.FreezeRows(1);

            // Save to MemoryStream
            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0; // reset position for reading
            return stream;
        }
    }
}
