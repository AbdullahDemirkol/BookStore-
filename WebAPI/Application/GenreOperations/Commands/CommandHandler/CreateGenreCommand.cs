﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using WebAPI.Application.GenreOperations.Commands.RequestCommandModel;
using WebAPI.DataAccess;
using WebAPI.Entity.Concrete;

namespace WebAPI.Application.GenreOperations.Commands.CommandHandler
{
    public class CreateGenreCommand
    {
        public CreateGenreModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateGenreCommand(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public void Handle()
        {
            var genre = _dbContext.Genres.FirstOrDefault(g=>g.Name==Model.Name);
            if (genre is not null)
            {
                throw new InvalidOperationException("Kitap Türü Zaten Mevcut");
            }
            genre = _mapper.Map<Genre>(Model);
            _dbContext.Genres.Add(genre);
            _dbContext.SaveChanges();
        }
    }
}