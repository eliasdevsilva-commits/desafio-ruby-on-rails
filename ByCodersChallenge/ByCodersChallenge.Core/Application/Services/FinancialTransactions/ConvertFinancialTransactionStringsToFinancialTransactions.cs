using BasePoint.Core.Exceptions;
using BasePoint.Core.Extensions;
using ByCodersChallenge.Core.Application.Services.FinancialTransactions.Interfaces;
using ByCodersChallenge.Core.Domain.Entities;
using ByCodersChallenge.Core.Domain.Enumerators;
using ByCodersChallenge.Core.Shared;
using System.Globalization;

namespace ByCodersChallenge.Core.Application.Services.FinancialTransactions
{
    public class ConvertFinancialTransactionStringsToFinancialTransactions : IConvertFinancialTransactionStringsToFinancialTransactions
    {
        public List<FinancialTransaction> Convert(MemoryStream memoryStream)
        {
            var lines = GetStreamLines(memoryStream);
            var transactions = new List<FinancialTransaction>();

            var emptyLines = lines.Where(x => x.IsEmpty());

            ValidationException.ThrowIfNotEmpty(emptyLines, SharedConstants.ErrorMessages.FileContainsInvalidLines);

            ValidationException.ThrowIfHasDuplicates(lines, SharedConstants.ErrorMessages.FileContainsDuplicatedLines, lineContent => lineContent);

            foreach (var line in lines)
            {
                var transaction = ConvertFromCnabString(line);

                var previousStore = transactions.FirstOrDefault(t => t.Store.Name == transaction.Store.Name);

                if (previousStore is not null)
                    transaction.UpdateStore(previousStore.Store);

                transactions.Add(transaction);
            }

            return transactions;
        }

        private static FinancialTransaction ConvertFromCnabString(string line)
        {
            var transaction = new FinancialTransaction(
                (TransactionType)int.Parse(line.Substring(0, 1)),
                DateTime.ParseExact(
                    $"{line.Substring(1, 4)}-{line.Substring(5, 2)}-{line.Substring(7, 2)}" +
                    " " + $"{line.Substring(42, 2)}:{line.Substring(44, 2)}:{line.Substring(46, 2)}", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                decimal.Parse(line.Substring(9, 10)) / 100,
                line.Substring(19, 11).Trim(),
                line.Substring(30, 12).Trim(),
                new Store(
                    line.Substring(62, 14).Trim(),
                    line.Substring(48, 14).Trim()
               ));

            return transaction;
        }

        private static List<string> GetStreamLines(MemoryStream memoryStream)
        {
            var stringStream = new StreamReader(memoryStream);

            var lines = new List<string>();

            stringStream.BaseStream.Position = 0;

            while (!stringStream.EndOfStream)
            {
                lines.Add(stringStream.ReadLine());
            }

            return lines;
        }
    }
}