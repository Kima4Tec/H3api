using H3api.Controllers;
using H3api.Entities;
using H3api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;
using Xunit.Abstractions;

namespace H3Test
{

    public class TestAuthorWithNSubstitute
    {
        private readonly ITestOutputHelper _output;

        public TestAuthorWithNSubstitute(ITestOutputHelper output)
        {
            _output = output;
        }
        /// <summary>
        /// Testing the GetThemAllAsync method in AuthorsController
        /// This method should return a list of authors from the repository 
        /// and return it as an OkObjectResult HTTP 200.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetAuthors_ReturnsExpectedAuthors()
        {
            // Arrange
            var repo = Substitute.For<IAuthorRepository>();
            var fakeAuthors = new List<Author>
        {
            new Author { Id = 1, FirstName = "fornavn1", LastName = "efternavn1" },
            new Author { Id = 2, FirstName = "fornavn2", LastName = "efternavn2" }
        };

            repo.GetAllAsync().Returns(fakeAuthors);

            var controller = new AuthorsController(repo);

            // Act
            var result = await controller.GetThemAllAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var authors = Assert.IsType<List<Author>>(okResult.Value);
            Assert.Equal(2, authors.Count);
        }

        /// <summary>
        /// Testing the creation of a new author using the AddAsync method in the repository.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CreateFakeAuthors()
        {
            var repo = Substitute.For<IAuthorRepository>();
            var fakeAuthors = new List<Author>
        {
            new Author { Id = 1, FirstName = "fornavn1", LastName = "efternavn1" },
            new Author { Id = 2, FirstName = "fornavn2", LastName = "efternavn2" }
        };
            var newFakeAuthor = new Author { Id = 3, FirstName = "NyBent", LastName = "NyHansen" };

            // simulate adding a new author
            await repo.AddAsync(newFakeAuthor);
            fakeAuthors.Add(newFakeAuthor);

            // Tell nsubstitute to return the updated list when GetAllAsync is called
            repo.GetAllAsync().Returns(fakeAuthors);

            var controller = new AuthorsController(repo);
            var result = await controller.GetThemAllAsync();

            var okResultat = result.Result as OkObjectResult;
            var myAuthors = okResultat.Value as List<Author>;
            foreach (var author in myAuthors)
            {
                _output.WriteLine($"Id: {author.Id}, Navn: {author.FirstName} {author.LastName}");
            }

            Assert.Equal(3, myAuthors.Count);
        }

        /// <summary>
        /// Testing a simple Get request to retrieve all authors.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task SimpelGetTest()
        {
            var repo = Substitute.For<IAuthorRepository>();

            repo.GetAllAsync().Returns(new List<Author>
                {
                new Author { Id = 1, FirstName = "Test", LastName = "Forfatter" }
                });
            var controller = new AuthorsController(repo);
            var result = await controller.GetThemAllAsync();
            var okResultat = result.Result as OkObjectResult;
            var myAuthors = okResultat.Value as List<Author>;

            foreach (var author in myAuthors)
            {
                _output.WriteLine($"Id: {author.Id}, Navn: {author.FirstName} {author.LastName}");
            }

        }
    }
}
