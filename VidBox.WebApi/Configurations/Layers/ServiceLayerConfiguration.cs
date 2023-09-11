using VidBox.Service.Interfaces.Auth;
using VidBox.Service.Interfaces.Categories;
using VidBox.Service.Interfaces.Common;
using VidBox.Service.Services.Categories.Layers;
using VidBox.Service.Services.Categories;
using VidBox.Service.Interfaces;
using VidBox.Service.Services.Notification;
using VidBox.Service.Services.Auth;
using VidBox.Service.Interfaces.AdminAuth;
using VidBox.Service.Services.Commons;
using VidBox.Service.Services.AdminAuth;
using VidBox.Service.Services.Identity;
using VidBox.Service.Interfaces.Identity;

namespace VidBox.WebApi.Configurations.Layers;

public static class ServiceLayerConfiguration
{
    public static void ConfigureServiceLayer(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        //builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IFileService, FileService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IPaginator, Paginator>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<ISmsSender, SmsSender>();
        builder.Services.AddScoped<IAuthAdminService, AuthAdminService>();
        builder.Services.AddScoped<ITokenAdminService, TokenAdminService>();
        builder.Services.AddScoped<IIdentityService, IdentityService>();
    }
}
