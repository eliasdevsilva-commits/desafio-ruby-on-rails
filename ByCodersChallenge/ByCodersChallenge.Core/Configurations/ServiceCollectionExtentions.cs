using ByCodersChallenge.Core.Application.Dtos.FinancialTransactions;
using ByCodersChallenge.Core.Application.Dtos.Validators.FinancialTransactions;
using ByCodersChallenge.Core.Application.Services.FinancialTransactions;
using ByCodersChallenge.Core.Application.Services.FinancialTransactions.Interfaces;
using ByCodersChallenge.Core.Application.UseCases.FinancialTransactions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ByCodersChallenge.Core.Configurations
{
    public static class ServiceCollectionExtentions
    {
        public static void MapUseCases(this IServiceCollection service)
        {
            service.AddSingleton<ImportFinancialTransactionsUseCase>();
        }

        public static void MapServices(this IServiceCollection service)
        {
            service.AddSingleton<IConvertFinancialTransactionStringsToFinancialTransactions, ConvertFinancialTransactionStringsToFinancialTransactions>();
        }

        public static void MapValidations(this IServiceCollection service)
        {
            service.AddSingleton<IValidator<ImportFinancialTransactionsInput>, ImportFinancialTransactionsInputValidator>();
        }
    }
}