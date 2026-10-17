using Incidencias.API.Dto;
using Incidencias.API.Interfaces;
using Incidencias.API.Repositorios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Incidencias.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ComentariosController : CustomBaseController
    {
        private readonly INotaIncidenciaRepository _notaIncidenciaRepository;
        public ComentariosController(INotaIncidenciaRepository notaIncidenciaRepository) 
        {
            _notaIncidenciaRepository = notaIncidenciaRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ComentarioDto comentarioDto)
        {
            var usuarioLogueado = GetUserId();

            await _notaIncidenciaRepository.CreateAsync(new Models.NotaIncidencia()
            {
                IncidenciaId = comentarioDto.IncidenciaId,
                Nota= comentarioDto.Nota,
                CreadoPorId= new Guid(usuarioLogueado), 
                FechaNota= DateTime.UtcNow
            });

            return Ok();
        }

        [HttpGet("{incidenciaId}/{skip}/{take}")]
        public async Task<IActionResult> Get(int incidenciaId, int skip, int take)
        {
            var comentarios = await _notaIncidenciaRepository.GetNotasByIncidenciaIdAsync(incidenciaId, skip, take);
            return Ok(comentarios);
        }

        [HttpGet("getbyId/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _notaIncidenciaRepository.GetById(id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _notaIncidenciaRepository.Delete(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ComentarioDto comentarioDto)
        {
            var existingComentario = await _notaIncidenciaRepository.GetById(id);
            if (existingComentario == null)
            {
                return NotFound();
            }

            // Actualizar las propiedades de la entidad
            existingComentario.Nota = comentarioDto.Nota;
            await _notaIncidenciaRepository.Update(existingComentario);
            return Ok(existingComentario);
        }
    }

  
}
