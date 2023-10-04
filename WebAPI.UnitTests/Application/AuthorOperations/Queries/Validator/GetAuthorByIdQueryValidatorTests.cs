using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.AuthorOperations.Queries.QueryHandler.GetAuthor;
using WebAPI.Application.AuthorOperations.Queries.Validator;
using WebAPI.Application.GenreOperations.Queries.QueryHandler.GetGenre;
using WebAPI.Application.GenreOperations.Queries.Validator;

namespace WebAPI.UnitTests.Application.AuthorOperations.Queries.Validator
{
    public class GetAuthorByIdQueryValidatorTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidAuthorIdsAreGiven_InvalidOperationException_ShouldBeError(int id)
        {
            //Arrange(Hazırlık)
            GetAuthorByIdQuery query = new GetAuthorByIdQuery(null, null);
            query.AuthorId = id;

            //Act (Çalıştırma)
            GetAuthorByIdQueryValidator validator = new GetAuthorByIdQueryValidator();
            var result = validator.Validate(query);

            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
