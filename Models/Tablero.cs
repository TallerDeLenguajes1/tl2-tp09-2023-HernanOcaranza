namespace Kanban.Models;

public class Tablero
{
    public int Id { get; set; }
    public int IdUsuarioPropietario { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public Tablero(int id, int idUsuario, string nombre, string descripcion)
    {
        Id = id;
        IdUsuarioPropietario = idUsuario;
        Nombre = nombre;
        Descripcion = descripcion;
    }
}