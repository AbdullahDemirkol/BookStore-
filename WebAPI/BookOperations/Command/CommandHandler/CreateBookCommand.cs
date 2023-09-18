﻿using AutoMapper;
using WebAPI.BookOperations.Command.RequestCommandModel;
using WebAPI.Common;
using WebAPI.DataAccess;

namespace WebAPI.BookOperations.Command.CommandHandler
{
    public class CreateBookCommand
    {
        public CreateBookModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateBookCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void Handle()
        {
            var book = _dbContext.Books.FirstOrDefault(p => p.Title == Model.Title);
            if (book is not null || Model is null)
            {
                throw new InvalidOperationException("Kitap zaten mevcut.");
            }
            if (!Enum.IsDefined(typeof(GenreEnum), Model.GenreId))
            {
                throw new InvalidOperationException("Bu tür bulunamadı.");
            }
            //book = new Book();
            book = _mapper.Map<Book>(Model);
            //book.Title = Model.Title;
            //book.PublishDate = Model.PublishDate;
            //book.PageCount = Model.PageCount;
            //book.GenreId = (GenreEnum)Model.GenreId;
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();

        }
    }
}