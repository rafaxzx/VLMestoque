using vlm721api.Models;

namespace vlm721api.Repositories
{
    public interface IItemRepository : IDisposable
    {
        List<Item> GetAll();
        Item GetById(int id);
        void Add(DTOItem item);
        void Update(DTOItem item, int id);
        void Delete(int id);
    }
}
