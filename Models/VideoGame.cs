namespace Week4Lab.Models
{
    //Defines a VideoGame structure.

    public class VideoGame
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Platform { get; set; }
        public double Price { get; set; }
    }
}
