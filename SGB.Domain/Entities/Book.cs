namespace SGB.Domain.Entities;

public class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;
    
    public string ISBN { get; set; } = string.Empty;

    public ICollection<Copy> Copies { get; set; } = new List<Copy>();
}
