﻿using GameBook.Infrastructure.Repositories;

namespace GameBook.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IGameRepository GameRepository { get; }
        ICountryRepository CountryRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
