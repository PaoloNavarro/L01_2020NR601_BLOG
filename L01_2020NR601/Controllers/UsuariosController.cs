using Microsoft.AspNetCore.Mvc;
using WEB_API.Models;
using Microsoft.AspNetCore.Mvc;
using WEB_API.Models;
using Microsoft.EntityFrameworkCore;
using L01_2020NR601.Models;

namespace L01_2020NR601.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsuariosController : Controller
    {
        private readonly blogContext _blogConext;
        public UsuariosController(blogContext equiposContext)
        {
            _blogConext = equiposContext;
        }

        //crear
        [HttpGet]
        [Route("getall")]
        public IActionResult ObtenerUsuarios()
        {
            List<usuarios> ListadoUsuarios = (from e in _blogConext.usuarios select e).ToList();
            if (ListadoUsuarios.Count == 0)
            {
                return NotFound();
            }
            return Ok(ListadoUsuarios);
        }
        //agregar
        [HttpPost]
        [Route("add")]
        public IActionResult Crear([FromBody] usuarios usuarioNuevo)
        {

            try
            {
                _blogConext.usuarios.Add(usuarioNuevo);


                _blogConext.SaveChanges();

                return Ok(usuarioNuevo);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        //Actualizar
        [HttpPut]
        [Route("actualizar")]

        public IActionResult actualizarUsuario(int id, [FromBody] usuarios usuarioModificar)
        {
            usuarios? usuarioExiste = (from e in _blogConext.usuarios
                                                where e.usuarioID == id
                                                select e).FirstOrDefault();
            if (usuarioExiste == null)
                return NotFound();

            usuarioExiste.rolID = usuarioModificar.rolID;
            usuarioExiste.nombreUsuario = usuarioModificar.nombreUsuario;
            usuarioExiste.clave = usuarioModificar.clave;
            usuarioExiste.nombre = usuarioModificar.nombre;
            usuarioExiste.apellido = usuarioModificar.apellido;




            _blogConext.Entry(usuarioExiste).State = EntityState.Modified;
            _blogConext.SaveChanges();

            return Ok(usuarioExiste);
        }

        //Delete
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult eliminarUsuarios(int id)
        {
            usuarios? usuarioExiste = (from e in _blogConext.usuarios
                                           where e.usuarioID == id
                                           select e).FirstOrDefault();
            if (usuarioExiste == null)
                return NotFound();

            _blogConext.Attach(usuarioExiste);
            _blogConext.Remove(usuarioExiste);
            _blogConext.SaveChanges();

            return Ok(usuarioExiste);


        }

        //nombre y apellido
        [HttpGet]
        [Route("find")]
        [HttpGet]
        [Route("getbynombre/{nombre}/{apellido}")]
        public IActionResult getNombreApellido(string nombre, string apellido)
        {
            usuarios? usuario = (from e in _blogConext.usuarios
                                where e.nombre == nombre && e.apellido == apellido
                                select e).FirstOrDefault();

            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpGet]
        [Route("getbyrol/{rol}")]
        public IActionResult getUserRol(int rol)
        {
            List<usuarios> userListRol = (from e in _blogConext.usuarios
                                         where e.rolID == rol
                                         select e).ToList();

            if (userListRol.Any())
            {
                return Ok(userListRol);
            }

            return NotFound();
        }






    }
}
