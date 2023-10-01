﻿using WebAPI.DataAccess;

namespace WebAPI.Application.GenreOperations.Commands.CommandHandler
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }
        private readonly IBookStoreDbContext _dbContext;
        public DeleteGenreCommand(IBookStoreDbContext dbContext)
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
