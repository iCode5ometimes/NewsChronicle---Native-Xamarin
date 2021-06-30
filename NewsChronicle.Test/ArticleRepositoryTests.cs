using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using Moq;
using NewsChronicle.Data.Interfaces;
using NewsChronicle.Data.Model;
using NewsChronicle.Data.Repositories;
using NewsChronicle.Data.Services;
using NUnit.Framework;

namespace NewsChronicle.Test
{
    public class ArticleRepositoryTests
    {
        private readonly Mock<IDBRepository<Article>> _articleRepoMock = new Mock<IDBRepository<Article>>();
        private IFixture _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();

            _fixture.Customize(new AutoMoqCustomization() { ConfigureMembers = true });
        }

        [Test]
        public async Task AddNewRecordAsyncShouldReturnTrueWhenInsertingANonNullObject()
        {
            // Arrange
            var record = _fixture.Create<Article>();
            _articleRepoMock.Setup(c => c.AddNewRecordAsync(record)).ReturnsAsync(true);
            _fixture.Inject(_articleRepoMock);
            var sut = _fixture.Create<ArticleRepository>();

            // Act
            var result = await sut.AddNewRecordAsync(record);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task AddNewRecordAsyncShouldReturnFalseWhenInsertingANullObject()
        {
            // Arrange
            Article record = null;
            _articleRepoMock.Setup(c => c.AddNewRecordAsync(record)).ReturnsAsync(false);
            _fixture.Inject(_articleRepoMock);
            var sut = _fixture.Create<ArticleRepository>();

            // Act
            var result = await sut.AddNewRecordAsync(record);

            // Assert
            result.Should().BeFalse();
        }
    }
}