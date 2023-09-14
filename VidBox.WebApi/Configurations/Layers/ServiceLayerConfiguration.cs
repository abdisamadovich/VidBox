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
using Amazon.Runtime;
using VidBox.Service.Interfaces.Adminstrators;
using VidBox.Service.Interfaces.Users;
using VidBox.Service.Services.Adminstrator;
using VidBox.Service.Services.Users;
using VidBox.Service.Interfaces.Videos;
using VidBox.Service.Services.Videos;

namespace VidBox.WebApi.Configurations.Layers;

public static class ServiceLayerConfiguration
{
    public static void ConfigureServiceLayer(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IFileService, FileService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddSingleton<ISmsSender, SmsSender>();
        builder.Services.AddScoped<IPaginator, Paginator>();
        builder.Services.AddScoped<IAdminstratorService, AdminstratorService>();
        builder.Services.AddScoped<IIdentityService, IdentityService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IAdminAuthService, AdminAuthService>();
        builder.Services.AddScoped<IVideoService, VideoService>();
    }
}
