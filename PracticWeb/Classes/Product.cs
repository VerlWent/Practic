namespace PracticeWeb.Classes
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public DateOnly DateAdded { get; set; }
        public int? CountInStock { get; set; }
        public string Category { get; set; }

    }
}
