using ByCodersChallenge.Core.Domain.Enumerators;

namespace ByCodersChallenge.Core.Extensions
{
    public static class TransactionTypeExtension
    {
        public static decimal GetSignal(this TransactionType transactionType)
        {
            return transactionType switch
            {
                Domain.Enumerators.TransactionType.Debit => 1,
                Domain.Enumerators.TransactionType.Boleto => -1,
                Domain.Enumerators.TransactionType.Financing => -1,
                Domain.Enumerators.TransactionType.Credit => 1,
                Domain.Enumerators.TransactionType.LoanReceipt => 1,
                Domain.Enumerators.TransactionType.Sales => 1,
                Domain.Enumerators.TransactionType.TEDReceipt => 1,
                Domain.Enumerators.TransactionType.DOCReceipt => 1,
                Domain.Enumerators.TransactionType.Rent => -1,
                _ => throw new ArgumentOutOfRangeException(nameof(transactionType), transactionType, null)
            };
        }

        public static decimal GetSignedValue(this TransactionType transactionType, decimal value)
        {
            return value * transactionType.GetSignal();
        }
    }
}