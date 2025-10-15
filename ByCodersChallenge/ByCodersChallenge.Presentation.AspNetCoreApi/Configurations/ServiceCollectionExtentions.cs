namespace ByCodersChallenge.Presentation.AspNetCoreApi.Configurations
{
    public static class ServiceCollectionExtentions
    {
        public static void MapHttpContextAccessor(this IServiceCollection service)
        {
            service.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}