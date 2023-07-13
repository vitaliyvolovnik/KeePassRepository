using BLL.Services;
using BLL.Services.Interfaces;
using DAL.Context;
using DAL.Repositories;
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
            collection.AddDbContext<KeyPassContext>(x => x.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=KeyPassDb;Integrated Security=True;Connect Timeout=30;Encrypt=False"));

            //repositories
            collection.AddTransient<UserRepository>();
            collection.AddTransient<FolderRepository>();
            collection.AddTransient<NoteRepository>();
            collection.AddTransient<CollectionRepository>();


        }
    }
}
