using BasePoint.Core.UnitOfWork;
using BasePoint.Core.UnitOfWork.Interfaces;
using ByCodersChallenge.Core.Application.Dtos.FinancialTransactions;
using ByCodersChallenge.Core.Application.Services.FinancialTransactions.Interfaces;
using ByCodersChallenge.Core.Application.UseCases.FinancialTransactions;
using ByCodersChallenge.Core.Domain.Entities;
using ByCodersChallenge.Core.Domain.Enumerators;
using ByCodersChallenge.Core.Domain.Repositories.Interfaces.FinancialTransactions;
using ByCodersChallenge.Core.Domain.Repositories.Interfaces.Stores;
using ByCodersChallenge.Core.Tests.Application.Dtos.Builders.Stores;
using ByCodersChallenge.Core.Tests.Domain.Entities.Builders.FinancialTransactions;
using ByCodersChallenge.Core.Tests.Domain.Entities.Builders.Stores;
using FluentAssertions;
using FluentValidation;
using Moq;
using Xunit;

namespace ByCodersChallenge.Core.Tests.Application.UseCases.FinancialTransactions
{
    public class ImportFinancialTransactionsUseCaseTests
    {
        private readonly ImportFinancialTransactionsUseCase _useCase;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IConvertFinancialTransactionStringsToFinancialTransactions> _convertFinancialTransactionStringsToFinancialTransactions;
        private readonly Mock<IStoreRepository> _storeRepository;
        private readonly Mock<IFinancialTransactionRepository> _financialTransactionRepository;
        private readonly Mock<IValidator<ImportFinancialTransactionsInput>> _validator;

        public ImportFinancialTransactionsUseCaseTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _validator = new Mock<IValidator<ImportFinancialTransactionsInput>>();
            _convertFinancialTransactionStringsToFinancialTransactions = new Mock<IConvertFinancialTransactionStringsToFinancialTransactions>();
            _storeRepository = new Mock<IStoreRepository>();
            _financialTransactionRepository = new Mock<IFinancialTransactionRepository>();

            _useCase = new ImportFinancialTransactionsUseCase(
                _validator.Object,
                _financialTransactionRepository.Object,
                _convertFinancialTransactionStringsToFinancialTransactions.Object,
                _storeRepository.Object,
                _unitOfWork.Object);
        }

        [Fact]
        public async Task Execute_EverythingIsOk_ReturnsSuccess()
        {
            var memoryStream = new MemoryStream();

            memoryStream.WriteByte(1);

            var input = new ImportFinancialTransactionsInputBuilder()
                .WithMemoryStream(memoryStream)
                .Build();

            _unitOfWork.Setup(x => x.SaveChangesAsync())
                .ReturnsAsync(new UnitOfWorkResult(true, "Commands executed With success."));

            var financialTransaction = new FinancialTransactionBuilder()
                .WithType(TransactionType.Credit)
                .WithOccurrenceDate(DateTime.Now)
                .WithValue(100m)
                .WithCPF("55641815063")
                .WithCard("1234****6678")
                .WithStore(new StoreBuilder().Build())
                .Build();

            var transactions = new List<FinancialTransaction>() { financialTransaction };

            _convertFinancialTransactionStringsToFinancialTransactions.Setup(x => x.Convert(It.IsAny<MemoryStream>()))
                .Returns(transactions);

            _storeRepository.Setup(x => x.GetStoreByName(It.IsAny<string>()))
                .ReturnsAsync(null as Store);

            var output = await _useCase.ExecuteAsync(input);

            output.HasErros.Should().BeFalse();

            _unitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
            _convertFinancialTransactionStringsToFinancialTransactions.Verify(x => x.Convert(It.IsAny<MemoryStream>()), Times.Once);
            _storeRepository.Verify(x => x.GetStoreByName(It.IsAny<string>()), Times.Exactly(1));
        }

        [Fact]
        public async Task Execute_EverythingIsOkAndStoreAlreadyExists_ReturnsSuccess()
        {
            var input = new ImportFinancialTransactionsInputBuilder()
                .Build();

            _unitOfWork.Setup(x => x.SaveChangesAsync())
                .ReturnsAsync(new UnitOfWorkResult(true, "Commands executed With success."));

            var transactions = new List<FinancialTransaction>() { new FinancialTransactionBuilder().Build() };

            _convertFinancialTransactionStringsToFinancialTransactions.Setup(x => x.Convert(It.IsAny<MemoryStream>()))
                .Returns(transactions);

            _storeRepository.Setup(x => x.GetStoreByName(It.IsAny<string>()))
                .ReturnsAsync(new StoreBuilder().Build());

            var output = await _useCase.ExecuteAsync(input);

            output.HasErros.Should().BeFalse();

            _unitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
            _convertFinancialTransactionStringsToFinancialTransactions.Verify(x => x.Convert(It.IsAny<MemoryStream>()), Times.Once);
            _storeRepository.Verify(x => x.GetStoreByName(It.IsAny<string>()), Times.Exactly(1));
        }

        [Fact]
        public async Task Execute_WhenStoreRepositoryThrowsException_ReturnsError()
        {
            var input = new ImportFinancialTransactionsInputBuilder()
                .Build();

            var transactions = new List<FinancialTransaction>() { new FinancialTransactionBuilder().Build() };

            _convertFinancialTransactionStringsToFinancialTransactions.Setup(x => x.Convert(It.IsAny<MemoryStream>()))
                .Returns(transactions);

            _storeRepository.Setup(x => x.GetStoreByName(It.IsAny<string>()))
                .ThrowsAsync(new Exception("999;Error Test"));

            var output = await _useCase.ExecuteAsync(input);

            output.HasErros.Should().BeTrue();

            _unitOfWork.Verify(x => x.SaveChangesAsync(), Times.Never);
            _convertFinancialTransactionStringsToFinancialTransactions.Verify(x => x.Convert(It.IsAny<MemoryStream>()), Times.Once);
            _storeRepository.Verify(x => x.GetStoreByName(It.IsAny<string>()), Times.Exactly(1));
        }
    }
}