namespace SGB.Domain.Entities;

public class Empleado
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string Rol { get; set; } = "Bibliotecario";

    public bool Active { get; set; } = true;

}
