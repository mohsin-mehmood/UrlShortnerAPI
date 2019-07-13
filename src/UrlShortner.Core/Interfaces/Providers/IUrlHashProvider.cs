namespace UrlShortner.Core.Interfaces.Providers
{
    public interface IUrlHashProvider
    {
        string GenerateHash(long urlId);
    }
}
