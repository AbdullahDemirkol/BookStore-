﻿using WebAPI.DataAccess;

namespace WebAPI.Application.GenreOperations.Commands.CommandHandler
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }
        private readonly BookStoreDbContext _dbContext;
        public DeleteGenreCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
            var genre=_dbContext.Genres.FirstOrDefault(g=>g.Id==GenreId);
            if (genre is null)
            {
                throw new InvalidOperationException("Kitap Türü Bulunamadı");
            } 
            _dbContext.Genres.Remove(genre);
            _dbContext.SaveChanges();
        }
    }
}
