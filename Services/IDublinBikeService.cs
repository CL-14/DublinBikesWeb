using DublinBikesWeb.Models;

namespace DublinBikesWeb.Services
{
    public interface IDublinBikeService // Interface that defines the contract for the DublinBikeService class, which retrieves bike station data from the JCDecaux API.
    {
        Task<IEnumerable<DublinBikesData>> GetDublinBikesDataAsync();
    }
}
