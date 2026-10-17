using Incidencias.API.Dto;
using Incidencias.API.Interfaces;
using Incidencias.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Incidencias.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IncidenciasController : CustomBaseController
    {
        private readonly IIncidenciasRepository _incidenciasRepository;
        public IncidenciasController(IIncidenciasRepository incidenciasRepository)
        {
            _incidenciasRepository = incidenciasRepository;
        }


        [HttpPost]
        [Authorize(Roles = "Admin, Usuario")]
        public async Task<IActionResult> Create(IncidenciaDto incidencia)
        {

            var usuarioLogueado = GetUserId();

            await _incidenciasRepository.CreateAsync(new Incidencia()
            {
                Titulo = incidencia.Titulo,
                Descripcion = incidencia.Descripcion,
                Estado = incidencia.Estado,
                Prioridad = incidencia.Prioridad,

                CreadoPorId = new Guid(usuarioLogueado)
            });

            return Ok(incidencia);
        }

        [HttpGet("{skip}/{take}")]
        public async Task<IActionResult> Get(int skip, int take)
        {
            var incidencias = await _incidenciasRepository.GetAsync(skip, take);
            return Ok(incidencias);
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var incidencia = await _incidenciasRepository.GetById(id, c => c.CreadoPor, c => c.Notas);

            if (incidencia == null)
            {
                return NotFound();
            }

            return Ok(incidencia);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _incidenciasRepository.Delete(id);
            if (!result)
            {
                return NotFound();
            }
            return  Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, IncidenciaDto incidencia)
        {
            var existingIncidencia = await _incidenciasRepository.GetById(id);
            if (existingIncidencia == null)
            {
                return NotFound();
            }

            // Actualizar las propiedades de la entidad
            existingIncidencia.Titulo = incidencia.Titulo;
            existingIncidencia.Descripcion = incidencia.Descripcion;
            existingIncidencia.Estado = incidencia.Estado;
            existingIncidencia.Prioridad = incidencia.Prioridad;

            await _incidenciasRepository.Update(existingIncidencia);
            return Ok(existingIncidencia);
        }


    }
}
