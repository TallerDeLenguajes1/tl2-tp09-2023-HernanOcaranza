using System.Data.SqlClient;
using System.Data.SQLite;
using Kanban.Models;

namespace Kanban.Repository;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly string _connectionString = "Data Source=DB/kanban.db;Cache=Shared";
    public UsuarioRepository()
    {
        
    }

    private int getLastId()
    {
        int id = 0;
        string queryString = "SELECT MAX(id_usuario) FROM usuario;";
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            SQLiteCommand command = new SQLiteCommand(queryString, connection);
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    id = Convert.ToInt16(reader[0]);
                }
            }
            connection.Close();
        }
        return id;
    }

    public List<Usuario> Get()
    {
        var usuarios = new List<Usuario>();
        string queryString = "SELECT * FROM usuario;";
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            SQLiteCommand command = new SQLiteCommand(queryString, connection);
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var id = Convert.ToInt16(reader["id_usuario"]);
                    var nombre = reader["nombre"].ToString();
                    var usuario = new Usuario(id, nombre);
                    usuarios.Add(usuario);
                }
            }
            connection.Close();
        }
        return usuarios;
    }

    public Usuario GetById(int id)
    {        
        Usuario usuario = null;
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM usuario WHERE id_usuario = @id;";
            command.Parameters.Add(new SQLiteParameter("@id", id));
            using(SQLiteDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    var idUsuario = Convert.ToInt16(reader["id_usuario"]);
                    var nombre = reader["nombre"].ToString();
                    usuario = new Usuario(idUsuario, nombre);
                }
            }
            connection.Close();
        }
        return usuario;
    }

    public Usuario Post(Usuario nuevoUsuario)
    {
        Usuario usuario = null;
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO usuario VALUES (@id, @nombre);";
            int id = getLastId() + 1;            
            command.Parameters.Add(new SQLiteParameter("@id", id));
            command.Parameters.Add(new SQLiteParameter("@nombre", nuevoUsuario.Nombre));
            command.ExecuteNonQuery();
            connection.Close();
            usuario = GetById(id);
        }
        return usuario;
    }

    public bool Uptadte(int id, Usuario nuevoUsuario)
    {
        var res = false;
        if(GetById(id) == null) return res;
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE usuario SET nombre = @nombre WHERE id_usuario = @id;";            
            command.Parameters.Add(new SQLiteParameter("@nombre", nuevoUsuario.Nombre));
            command.Parameters.Add(new SQLiteParameter("@id", id));
            command.ExecuteNonQuery();
            res = true;
            connection.Close();
        }        
        return res;
    }

    public bool Delete(int id)
    {
        var res = false;
        if(GetById(id) == null) return res;
        using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM usuario WHERE id_usuario = @id;";            
            command.Parameters.Add(new SQLiteParameter("@id", id));
            command.ExecuteNonQuery();
            res = true;
            connection.Close();
        }
        return res;
    }
}