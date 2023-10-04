using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Application.BookOperations.Queries.QueryHandler.GetBook;
using WebAPI.Application.BookOperations.Queries.Validator;

namespace WebAPI.UnitTests.Application.BookOperations.Queries.Validator
{
    public class GetBookByIdQueryValidatorTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenInvalidBookIdIsGiven_InvalidOperationException_ShouldBeError(int id)
        {
            //Arrange(Hazırlık)
            GetBookByIdQuery query = new GetBookByIdQuery(null,null);
            query.Id = id;

            //Act (Çalıştırma)
            GetBookByIdQueryValidator validator = new GetBookByIdQueryValidator();
            var result= validator.Validate(query);

            //Assert (Doğrulama)
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
