namespace SGB.Application.DTOs.Auth
{
    public class BorrowingResponseDto
    {
        public int Id { get; set; }
        public string Customer { get; set; } = "";
        public string Book { get; set; } = "";
        public DateTime BorrowDate { get; set; }
        public DateTime LimitDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal AccumulatedFee { get; set; }
    }
}
