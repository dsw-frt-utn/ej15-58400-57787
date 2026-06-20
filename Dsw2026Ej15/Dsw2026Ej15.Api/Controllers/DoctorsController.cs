using System.Collections;
using Dsw2026Ej15.Api.Exceptions;
using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers;

[ApiController]
[Route("api/doctors")]
public class DoctorsController : ControllerBase
{
    private readonly IPersistence _persistence;

    public DoctorsController(IPersistence persistence)
    {
        _persistence = persistence;
    }

    // representa un acceso determinado a un recurso determinda a partir de una URI y un verbo
    // Siempre EndPoints asincronos, devolver un Task<IActionResult> o IActionResult
    [HttpPost]
    public async Task<IActionResult> CreateDoctor(DoctorModel.Request request)
    {
        if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.LicenseNumber))
        {
            throw new ValidationException("Nombre y Numero de Licencia son Requeridos");
        }

        var speciality = _persistence.GetSpecialityById(request.SpecialityId);
        if (speciality is null)
        {
            throw new ValidationException("Especialidad no encontrada");
        }

        var doctor = new Doctor(request.Name, request.LicenseNumber, speciality);
        _persistence.SaveDoctor(doctor);

        return Created(); // devuelve un 201
    }


    [HttpGet]
    public async Task<IActionResult> GetAllDoctors()
    {
        var doctores = _persistence.GetDoctores();
        return Ok(doctores);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDoctorById(Guid id)
    {
        var _doctor = _persistence.GetByIdDoctor(id);

        if (_doctor == null || !_doctor.IsActive)
        {
            return NotFound("El medico no existe o no esta activo");
        }

        var response = new DoctorModel.Response(
            _doctor.Id,
            _doctor.Name,
            _doctor.LicenseNumber,
            _doctor.Speciality!.Name
        );

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDoctor(Guid id)
    {
        var _doctor = _persistence.GetByIdDoctor(id);

        if (_doctor == null || !_doctor.IsActive)
        {
            return NotFound("El medico no existe o no esta activo"); 
        }


        _persistence.DeleteDoctor(_doctor);

        return NoContent(); 
    }



}


