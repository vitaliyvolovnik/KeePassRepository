using BLL.Infrastructure;
using BLL.Services;
using BLL.Services.Interfaces;
using KeePass.View;
using KeePass.View.Pages;
using KeePass.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace KeePass
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public static IServiceProvider ServiceProvider;

        public App()
        {
            ServiceCollection collection = new();
            ConfigureService(collection);
            ServiceProvider = collection.BuildServiceProvider();
        }


        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var wind = ServiceProvider.GetService<MainWindow>();
            wind.Show();

        }
        private void ConfigureService(ServiceCollection collection)
        {
            //Windows
            collection.AddTransient<MainWindow>();
            collection.AddTransient<AuthorizeWindow>();

            //Pages
            //collection.AddTransient<LoginPage>();
            //collection.AddTransient<RegisterPage>();
            collection.AddTransient<ExplorerPage>();

            //ViewModel
            collection.AddTransient<CollectionViewModel>();
            collection.AddTransient<ExplorerViewModel>();



            //Services
            collection.AddTransient<FolderSerivce>();
            collection.AddTransient<IFolderService, FolderSerivce>();
            collection.AddTransient<ICollectionService, FolderSerivce>();

            collection.AddTransient<NoteService>();
            collection.AddTransient<INoteService, NoteService>();

            collection.AddTransient<UserService>();
            collection.AddTransient<IUserService, UserService>();


            string aesKey = ConfigurationManager.AppSettings["AES_KEY"];
            string salt = ConfigurationManager.AppSettings["PASSWORD_SALT"];
            collection.AddTransient<CryptographyService>(sp => new CryptographyService(aesKey, salt));

            ConfigureBll.Configure(collection, ConfigurationManager.ConnectionStrings["MCSQLConnectionString"].ConnectionString);
        }
    }
}
