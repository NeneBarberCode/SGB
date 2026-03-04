namespace SGB.Domain.Entities;

public class Copy
{
    public int Id { get; set; }

    public int BookId { get; set; }
    
    public Book Book { get; set; } = null!;

    public bool Available { get; set; } = true;

    public ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
}
