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
    public class ComentariosController : Controller
    {
        private readonly blogContext _blogConext;

        public ComentariosController(blogContext equiposContext)
        {
            _blogConext = equiposContext;
        }

        
        [HttpGet]
        [Route("getall")]
        public IActionResult Obtenercomentarios()
        {
            List<comentarios> ListadoComentarios = (from e in _blogConext.comentarios select e).ToList();
            if (ListadoComentarios.Count == 0)
            {
                return NotFound();
            }
            return Ok(ListadoComentarios);
        }

        [HttpGet]
        [Route("getbyid/{id}")]

        public IActionResult get(int id)
        {

            comentarios? unComentario = (from e in _blogConext.comentarios
                                         where e.publicacionId == id
                                         select e).FirstOrDefault();
            if (unComentario == null)
            {
                return NotFound();
            }
            return Ok(unComentario);
        }

        
        [HttpPost]
        [Route("add")]
        public IActionResult Crear([FromBody] comentarios comentarioNuevo)
        {

            try
            {
                _blogConext.comentarios.Add(comentarioNuevo);
                _blogConext.SaveChanges();

                return Ok(comentarioNuevo);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        
        [HttpPut]
        [Route("actualizar")]

        public IActionResult actualizarEquipo(int id, [FromBody] comentarios comentarioModificar)
        {
            comentarios? comentarioExiste = (from e in _blogConext.comentarios
                                     where e.cometarioId == id
                                     select e).FirstOrDefault();
            if (comentarioExiste == null)
                return NotFound();

            comentarioExiste.comentario = comentarioModificar.comentario;
            comentarioExiste.publicacionId = comentarioModificar.publicacionId;
            comentarioExiste.usuarioId = comentarioModificar.usuarioId;

            _blogConext.Entry(comentarioExiste).State = EntityState.Modified;
            _blogConext.SaveChanges();

            return Ok(comentarioExiste);
        }
       
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult eliminarEquipo(int id)
        {
            comentarios? comentarioExiste = (from e in _blogConext.comentarios
                                     where e.cometarioId == id
                                     select e).FirstOrDefault();
            if (comentarioExiste == null)
                return NotFound();

            _blogConext.Attach(comentarioExiste);
            _blogConext.Remove(comentarioExiste);
            _blogConext.SaveChanges();

            return Ok(comentarioExiste);


        }

    }
   
}
