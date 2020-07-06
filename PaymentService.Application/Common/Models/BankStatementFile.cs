namespace PaymentService.Application.Common.Models
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Collections.Generic;

    using PaymentService.Application.Common.Exceptions;

    public class BankStatementFile
    {
        public BankStatementFile(string path, DateTimeOffset importDate)
        {
            this.FilePath = path ?? throw new ArgumentNullException(nameof(path));

            this.FileName = this.ConstructFileNameFromDate(importDate);
        }

        private string FilePath { get; }

        private string FileName { get; }

        private string FullPath => Path.Combine(FilePath, FileName);

        private string ProcessedFullPath => Path.Combine(FilePath, $"_processed_{FileName}");

        private BankStatement ReadRow(string row)
        {
            string[] cells = row.Split(",");
            return new BankStatement(accountNumber: cells[3], amountAsString: cells[4], accountingDateAsIsoDateString: cells[2]);
        }

        private string ConstructFileNameFromDate(DateTimeOffset importDate) =>
            $"bankStatements_{importDate.Year}_{importDate.Month}_{importDate.Day}.csv";

        public bool Exists() => File.Exists(FullPath);

        public List<BankStatement> Read()
        {
            try
            {
                return File.ReadAllLines(FullPath).Skip(1).Select(ReadRow).ToList();
            }
            catch (FileNotFoundException ex) { throw new BankStatementsFileNotFound(ex); }
            catch (IOException ex) { throw new BankStatementsFileReadingError(ex); }
        }

        public void MarkProcessed() => File.Copy(FullPath, ProcessedFullPath);
    }
}
