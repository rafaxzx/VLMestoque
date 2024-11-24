using System.ComponentModel.DataAnnotations;

namespace vlm721api.Models
{
    public class Item
    {
        public int Id { get; set; }
        [StringLength(9, ErrorMessage = "O código tem mais de 9 caracteres")]
        public string CodIntern { get; set; } = string.Empty;
        [StringLength(50, ErrorMessage = "O código tem mais de 50 caracteres")]
        public string ItemName { get; set; } = string.Empty;
        [StringLength(100, ErrorMessage = "O código tem mais de 100 caracteres")]
        public string ItemSpecification { get; set; } = string.Empty;
        [Range(0, int.MaxValue, ErrorMessage = "Número para ID inválido")]
        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; }
        [Range(1, 300, ErrorMessage = "Número do prateleira precisa ser entre 1 e 300")]
        public int UDCNumber { get; set; }
        public string LocationTray { get; set; } = string.Empty;
        [Range(0, 100, ErrorMessage = "O valor deve ser entre 0 e 100")]
        public int MinStock { get; set; }
        [Range(0, 100, ErrorMessage = "O valor deve ser entre 0 e 100")]
        public int MaxStock { get; set; }

        public Item(string codIntern, string itemName, string itemSpecification,
            int manufacturerId, int uDCNumber, string locationTray,
            int minStock, int maxStock, Manufacturer manufacturer)
        {
            CodIntern = codIntern;
            ItemName = itemName;
            ItemSpecification = itemSpecification;
            ManufacturerId = manufacturerId;
            UDCNumber = uDCNumber;
            LocationTray = locationTray;
            MinStock = minStock;
            MaxStock = maxStock;
            Manufacturer = manufacturer;
        }
    }
}
