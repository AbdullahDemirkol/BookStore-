using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.BookOperations.Queries.QueryHandler.GetBook;
using WebAPI.Application.BookOperations.Queries.Validator;
using WebAPI.Application.GenreOperations.Queries.QueryHandler.GetGenre;
using WebAPI.Application.GenreOperations.Queries.Validator;

namespace WebAPI.UnitTests.Application.GenreOperations.Queries.Validator
{
    public class GetGenreByIdQueryValidatorTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidGenreIdsAreGiven_InvalidOperationException_ShouldBeError(int id)
        {
            //Arrange(Hazırlık)
            GetGenreByIdQuery query = new GetGenreByIdQuery(null, null);
            query.GenreId = id;

            //Act (Çalıştırma)
            GetGenreByIdQueryValidator validator = new GetGenreByIdQueryValidator();
            var result = validator.Validate(query);

            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
