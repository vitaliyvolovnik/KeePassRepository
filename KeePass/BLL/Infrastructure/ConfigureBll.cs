using BLL.Services;
using BLL.Services.Interfaces;
using DAL.Context;
using DAL.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure
{
    public class ConfigureBll
    {

        public static void Configure(ServiceCollection collection, string connectionString)
        {


            var dbContextBuider = new DbContextOptionsBuilder<KeyPassContext>();
            dbContextBuider.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=KeyPassDb;Integrated Security=True;Connect Timeout=30;Encrypt=False");

            collection.AddTransient<KeyPassContext>();
            collection.AddSingleton(dbContextBuider.Options);

            //repositories
            collection.AddTransient<UserRepository>();
            collection.AddTransient<FolderRepository>();
            collection.AddTransient<NoteRepository>();
            collection.AddTransient<CollectionRepository>();

            //models
            collection.AddSingleton<User>(new User());


        }
    }
}
