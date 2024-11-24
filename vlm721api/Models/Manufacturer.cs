namespace vlm721api.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public Manufacturer(string name, string imagePath, int id = 0)
        {
            Id = id;
            Name = name;
            ImagePath = imagePath;
        }
    }
}
