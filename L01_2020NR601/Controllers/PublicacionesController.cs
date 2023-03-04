using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using WEB_API.Models;
using Microsoft.EntityFrameworkCore;
using L01_2020NR601.Models;

namespace L01_2020NR601.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class PublicacionesController : Controller
    {
        private readonly blogContext _blogConext;

        public PublicacionesController(blogContext equiposContext)
        {
            _blogConext = equiposContext;
        }

        //crear
        [HttpGet]
        [Route("getall")]
        public IActionResult ObtenerPublicaciones()
        {
            List <publicaciones> ListadoPublicaciones = (from e in _blogConext.publicaciones select e).ToList();
            if (ListadoPublicaciones.Count == 0)
            {
                return NotFound();
            }
            return Ok(ListadoPublicaciones);
        }
        //agregar
        [HttpPost]
        [Route("add")]
        public IActionResult Crear([FromBody] publicaciones publicacionesNuevo)
        {

            try
            {
                _blogConext.publicaciones.Add(publicacionesNuevo);
                  


                _blogConext.SaveChanges();

                return Ok(publicacionesNuevo);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        //Actualizar
        [HttpPut]
        [Route("actualizar")]

        public IActionResult actualizarPublicacion(int id, [FromBody] publicaciones publicacionModificar)
        {
            publicaciones? publicacionExiste = (from e in _blogConext.publicaciones
                                             where e.publicacionId == id
                                             select e).FirstOrDefault();
            if (publicacionExiste == null)
                return NotFound();

            publicacionExiste.titulo = publicacionModificar.titulo;
            publicacionExiste.descripcion = publicacionModificar.descripcion;
            publicacionExiste.usuarioId = publicacionModificar.usuarioId;

            _blogConext.Entry(publicacionExiste).State = EntityState.Modified;
            _blogConext.SaveChanges();

            return Ok(publicacionExiste);
        }

        //Delete
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult eliminarPublicacion(int id)
        {
            publicaciones? publicacionExiste = (from e in _blogConext.publicaciones
                                             where e.publicacionId == id
                                             select e).FirstOrDefault();
            if (publicacionExiste == null)
                return NotFound();

            _blogConext.Attach(publicacionExiste);
            _blogConext.Remove(publicacionExiste);
            _blogConext.SaveChanges();

            return Ok(publicacionExiste);


        }

        //filtro por usaurio
        [HttpGet]
        [Route("getbyid/{id}")]

        public IActionResult get(int id)
        {

            publicaciones? unPublicacion = (from e in _blogConext.publicaciones
                                         where e.usuarioId == id
                                         select e).FirstOrDefault();
            if (unPublicacion == null)
            {
                return NotFound();
            }
            return Ok(unPublicacion);
        }


    }
}
