﻿using BLL.Services;
using DAL.Context;
using DAL.Repositories;
using DAL.Repositories.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Infrastructure
{
    public class ConfigureBll
    {

        public static void Configure(ServiceCollection collection, string connectionString, string salt)
        {

            var user = new User();

            //models


            collection.AddSingleton<User>(user);

            //db
            var dbContextBuider = new DbContextOptionsBuilder<KeyPassContext>();
            dbContextBuider.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=KeyPassDb;Integrated Security=True;Connect Timeout=30;Encrypt=False");

            collection.AddTransient<KeyPassContext>();
            collection.AddSingleton(dbContextBuider.Options);

            //repositories
            collection.AddTransient<IUserRepository, UserRepository>();
            collection.AddTransient<IFolderRepository, FolderRepository>();
            collection.AddTransient<INoteRepository, NoteRepository>();
            collection.AddTransient<ICollectionRepository, CollectionRepository>();


            //service
            collection.AddTransient<CryptographyService>(sp => new CryptographyService(salt, user));

        }
    }
}
