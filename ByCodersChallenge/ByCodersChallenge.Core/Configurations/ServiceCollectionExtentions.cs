using ByCodersChallenge.Core.Application.Dtos.Stores;
using ByCodersChallenge.Core.Application.Dtos.Validators.Stores;
using ByCodersChallenge.Core.Application.Services.Stores;
using ByCodersChallenge.Core.Application.Services.Stores.Interfaces;
using ByCodersChallenge.Core.Application.UseCases.Stores;
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