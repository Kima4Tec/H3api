using H3api.Controllers;
using H3api.Data;
using H3api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Serialization;
using Xunit;

namespace H3Test
{
    public class TestAuthor
    {
            private ApplicationDbContext GetInMemoryDbContext()
            {
                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(databaseName: "TestDb")
                    .Options;

                var context = new ApplicationDbContext(options);

                // Tilføj testdata hvis nødvendigt
                context.Authors.AddRange(new List<Author>
            {
                new Author { Id = 1, FirstName = "fornavn1", LastName = "efternavn1" },
                new Author { Id = 2, FirstName = "fornavn2", LastName = "efternavn2" },
            });
                context.SaveChanges();

                return context;
            }

            [Fact]
            public async Task GetAuthors_ReturnsListOfAuthors()
            {
                // Arrange
                var context = GetInMemoryDbContext();
                var controller = new AuthorsController(context);

                // Act
                var result = await controller.GetAuthors();

            // Assert

            /*-------------------- Assert 1: Tjekker at returtypen er ActionResult<IEnumerable<Author>> ------------------*/

            var actionResult = Assert.IsType<ActionResult<IEnumerable<Author>>>(result);

            /*-------------------- Assert 2: Tjekker at værdien er en List<Author> ------------------*/

            var authors = Assert.IsType<List<Author>>(actionResult.Value);

            /*-------------------- Assert 3: Tjekker at listen har 2 elementer ------------------*/

            Assert.Equal(2, authors.Count);

            /*-------------------- Assert 4: Tjekker specifikke egenskaber på første forfatter ------------------*/

            var author = authors.FirstOrDefault(i => i.Id == 1);
            Assert.NotNull(author);
            Assert.Equal("fornavn1", author.FirstName);
        }
        }
    }

