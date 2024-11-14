using vlm721api.Models;

namespace vlm721api.Repositories
{
    public interface IManufacturerRepository
    {
        List<Manufacturer> GetAll();
        Manufacturer GetById(int id);
        void Add(Manufacturer manufacturer);
        void Update(Manufacturer manufacturer, int id);
        void Delete(int id);
    }
}
