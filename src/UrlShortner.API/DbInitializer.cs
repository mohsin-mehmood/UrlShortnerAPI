using UrlShortner.Infrastructure.Data;

namespace UrlShortner.API
{
    public static class DbInitializer
    {

        public static bool Initialize(AppDBContext context)
        {
            return context.Database.EnsureCreated();
        }
    }
}
