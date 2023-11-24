using Kanban.Models;

namespace Kanban.Repository;

public interface IUsuarioRepository
{    
    public List<Usuario> Get();
    public Usuario GetById(int id);
    public Usuario Post(Usuario nuevoUsuario);
    public bool Uptadte(int id, Usuario nuevoUsuario);
    public bool Delete(int id);
}