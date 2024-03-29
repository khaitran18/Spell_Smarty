﻿using AutoMapper;
using SpellSmarty.Domain.Interfaces;
using SpellSmarty.Infrastructure.Data;
using SpellSmarty.Infrastructure.Repositories;

namespace SpellSmarty.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SpellSmartyContext _context;
        private readonly IMapper _mapper;
        private IVideoRepository _videoRepository;
        private IAccountRepository _accountRepository;
        private IVideoStatRepository _videoStatRepository;
        private IGenreRepository _genreRepository;
        private IFeedBackRepository _feedBackRepository;
        private IVideoGenreRepository _videoGenreRepository;
        public UnitOfWork(SpellSmartyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IVideoRepository VideosRepository => _videoRepository ??= new VideoRepository(_context,_mapper);
        public IVideoStatRepository VideoStatRepository => _videoStatRepository ??= new VideoStatRepository(_context, _mapper);
        public IAccountRepository AccountRepository => _accountRepository ??= new AccountRepository(_context, _mapper);
        public IGenreRepository GenreRepository => _genreRepository ??= new GenreRepository(_context, _mapper);
        public IFeedBackRepository FeedBackRepository => _feedBackRepository ??= new FeedBackRepository(_context, _mapper);
        public IVideoGenreRepository VideoGenreRepository => _videoGenreRepository ??= new VideoGenreRepository(_context, _mapper);
        public void Dispose()
        {
            _context.Dispose();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
