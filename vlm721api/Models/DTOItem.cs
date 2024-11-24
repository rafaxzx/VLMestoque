namespace vlm721api.Models
{
    public class DTOItem
    {
        public int Id { get; set; }
        public string CodIntern { get; set; } = string.Empty;
        public string ItemName { get; set; } = string.Empty;
        public string ItemSpecification { get; set; } = string.Empty;
        public int ManufacturerId { get; set; }
        public int UDCNumber { get; set; }
        public string LocationTray { get; set; } = string.Empty;
        public int MinStock { get; set; }
        public int MaxStock { get; set; }

        public DTOItem(string codIntern, string itemName, string itemSpecification,
            int manufacturerId, int uDCNumber, string locationTray,
            int minStock, int maxStock)
        {
            CodIntern = codIntern;
            ItemName = itemName;
            ItemSpecification = itemSpecification;
            ManufacturerId = manufacturerId;
            UDCNumber = uDCNumber;
            LocationTray = locationTray;
            MinStock = minStock;
            MaxStock = maxStock;
        }
    }
}
