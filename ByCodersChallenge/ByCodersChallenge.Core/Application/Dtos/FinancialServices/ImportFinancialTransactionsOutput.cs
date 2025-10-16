namespace ByCodersChallenge.Core.Application.Dtos.FinancialTransactions
{
    public class ImportFinancialTransactionsOutput
    {
        public ImportFinancialTransactionsOutput(bool result)
        {
            Result = result;
        }
        public bool Result { get; }
    }
}
