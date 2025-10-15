using BasePoint.Core.Application.Cqrs.QueryProviders;
using BasePoint.Core.Application.Dtos.Input;
using BasePoint.Core.Application.Dtos.Validators;
using BasePoint.Core.Application.UseCases;
using ByCodersChallenge.Core.Application.Dtos.FinancialServices;
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
            service.AddSingleton<GetPaginatedResultsUseCase<IListItemOutputCqrsQueryProvider<FinancialTransactionListItemOutput>, FinancialTransactionListItemOutput>>();
        }

        public static void MapServices(this IServiceCollection service)
        {
            service.AddSingleton<IConvertFinancialTransactionStringsToFinancialTransactions, ConvertFinancialTransactionStringsToFinancialTransactions>();
        }

        public static void MapValidations(this IServiceCollection service)
        {
            service.AddSingleton<IValidator<ImportFinancialTransactionsInput>, ImportFinancialTransactionsInputValidator>();
            service.AddSingleton<IValidator<GetPaginatedResultsInput>, GetPaginatedResultsInputValidator>();
        }
    }
}