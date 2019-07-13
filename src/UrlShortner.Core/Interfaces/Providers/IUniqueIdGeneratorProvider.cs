using System.Threading.Tasks;

namespace UrlShortner.Core.Interfaces.Providers
{
    public interface IUniqueIdGeneratorProvider
    {
        Task<long> GenerateNextIdAsync();
    }
}
