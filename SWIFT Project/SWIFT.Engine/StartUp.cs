using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SWIFT.Entities;
using SWIFT.Main;
using System;
using System.IO;

namespace SWIFT.Engine
{
    public class StartUp
    {
        private readonly IConfiguration configuration;

        public StartUp(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //db
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   this.configuration.GetConnectionString(@"Server=.;Database=SWIFT;Trusted_Connection=True;MultipleActiveResultSets=true")));


            ////automapper
            //services.AddAutoMapper(typeof(ApplicationProfile).Assembly);

            ////services
            //services.AddTransient<ISeedService, SeedService>();
            //services.AddTransient<ISeedProductsService, SeedProductsService>();
            //services.AddTransient<IProductService, ProductService>();
            //services.AddTransient<IRoomService, RoomService>();
            //services.AddTransient<IImageService, ImageService>();
            //services.AddTransient<ICartService, CartService>();
            //services.AddTransient<ICommentService, CommentService>();
            //services.AddTransient<IUsersService, UsersService>();
            //services.AddTransient<IRecipeService, RecipeService>();
            //services.AddTransient<IPostService, PostService>();
            //services.AddTransient<IContactService, ContactService>();

            //services.AddDatabaseDeveloperPageExceptionFilter();

            //services.AddControllersWithViews();
            //services.AddRazorPages();
        }
        static void Main(string[] args)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            string mainDirectory = Directory.GetParent(currentDirectory).Parent.Parent.Parent.FullName;
            Console.WriteLine(mainDirectory);
            var dirInfo = new DirectoryInfo(mainDirectory);
            var files = dirInfo.GetFiles("*.txt");
            foreach (var file in files)
            {
                var strResult = string.Empty;
                using (StreamReader streamReader = File.OpenText(file.ToString()))
                {
                    strResult = streamReader.ReadToEnd();
                    var parser = new Parser();
                    var messagesInfo = parser.SeparateTxtFile(strResult.Trim());
                    var textHeader = new TextHeader(messagesInfo["TextBlock"]);
                    Console.WriteLine(textHeader.TextTags["20"]);

                }
            }


        }
    }
}

