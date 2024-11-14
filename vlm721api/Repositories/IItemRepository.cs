using vlm721api.Models;

namespace vlm721api.Repositories
{
    public interface IItemRepository
    {
        List<Item> GetAll();
        Item GetById(int id);
        void Add(DTOItem item);
        void Update(Item item, int id);
        void Delete(int id);
    }
}
