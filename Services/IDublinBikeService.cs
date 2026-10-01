using DublinBikesWeb.Models;

namespace DublinBikesWeb.Services
{
    public interface IDublinBikeService
    {
        Task<IEnumerable<DublinBikesData>> GetDublinBikesDataAsync();
    }
}
