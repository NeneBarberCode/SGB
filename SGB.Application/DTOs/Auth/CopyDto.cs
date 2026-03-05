namespace SGB.Application.DTOs.Auth;
public class CopyDto
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public string BookTitle { get; set; } = string.Empty;
    public bool Available { get; set; }
}
