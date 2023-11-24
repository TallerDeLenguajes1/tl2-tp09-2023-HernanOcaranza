using Kanban.Models;
using Kanban.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Controllers;

[ApiController]
[Route("v1/api/usuario")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;
    public UsuarioController()
    {
        _usuarioRepository = new UsuarioRepository();
    }

    [HttpGet]
    public ActionResult<List<Usuario>> GetUsuarios()
    {
        var res = _usuarioRepository.Get();
        return Ok(res);
    }

    [HttpGet("{id}")]
    public ActionResult<Usuario> GetUsuarioById(int id)
    {
        var res = _usuarioRepository.GetById(id);
        if (res == null) return NotFound();
        return Ok(res);
    }

    [HttpPost]
    public ActionResult<Usuario> PostUsuario(Usuario nuevoUsuario)
    {
        var res = _usuarioRepository.Post(nuevoUsuario);
        if(res == null) return BadRequest();
        return Ok(res);
    }

    [HttpPut]
    public ActionResult UpdateUsuario(int id, Usuario nuevoUsuario)
    {
        var res = _usuarioRepository.Uptadte(id, nuevoUsuario);
        if (!res) return NotFound();
        return Ok();
    }

    [HttpDelete]
    public ActionResult DeleteUsuario(int id)
    {
        var res = _usuarioRepository.Delete(id);
        if (!res) return NotFound();
        return Ok();
    }
}
