using VidBox.DataAccess.Interfaces.Adminstrators;
using VidBox.DataAccess.Interfaces.Categories;
using VidBox.DataAccess.Interfaces.Users;
using VidBox.DataAccess.Repositories.Adminstrators;
using VidBox.DataAccess.Repositories.Categories;
using VidBox.DataAccess.Repositories.Users;

namespace VidBox.WebApi.Configurations.Layers;

public static class DataAccessConfiguration
{
    public static void ConfigureDataAccess(this WebApplicationBuilder builder)
    {
        //-> DI containers, IoC containers
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IAdminstratorRepository, AdminstratorRepository>();
        //     builder.Services.AddScoped<IVideoRepository,VideoRepository>();
    }
}
