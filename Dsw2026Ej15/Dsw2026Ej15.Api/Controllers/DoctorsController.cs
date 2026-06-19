using System.Collections;
using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers;

[ApiController]
[Route("api")]
public class DoctorsController : ControllerBase
{
    private readonly IPersistence _persistence;

    public DoctorsController(IPersistence persistence)
    {
        _persistence = persistence;
    }

    // representa un acceso determinado a un recurso determinda a partir de una URI y un verbo
    // Siempre EndPoints asincronos, devolver un Task<IActionResult> o IActionResult
    [HttpPost("doctors")]
    public async Task<IActionResult> CreateDoctor(DoctorModel.Request request)
    {
        if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.LicenseNumber))
        {
            return BadRequest("Nombre y Numero de Licencia son Requeridos");
        }

        var speciality = _persistence.GetSpecialityById(request.SpecialityId);
        if (speciality is null)
        {
            return BadRequest("Especialidad no encontrada");
        }

        var doctor = new Doctor(request.Name, request.LicenseNumber, speciality);
        _persistence.SaveDoctor(doctor);

        return Created(); // devuelve un 201
    }


    [HttpGet("doctors")]
    public async Task<IActionResult> GetAllDoctors()
    {
        var doctores = _persistence.GetDoctores();
        return Ok(doctores);
    }

  

}


