namespace SGB.Domain.Entities;

public class Borrowing
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public  Customer Customer  { get; set; } = null!;

    public int CopyId { get; set; }

    public Copy Copy  { get; set; } = null!;

    public DateTime BorrowDate { get; set; }

    public DateTime LimitDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public decimal AccumulatedFee { get; set; }
}
