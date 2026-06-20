namespace Dsw2026Ej15.Api.Models;


public record DoctorModel
{
    public record Request(string Name, string LicenseNumber, Guid SpecialityId);
    public record Response(Guid Id, string Name, string LicenseNumber, string NameSpeciality);

    // Asume por si mismo que no puede ser Nulo, pero si puede estar vacio ("") --> Middlewares q validan
    // De ser Nulo retorna un 400 BadRequest --> Amenos q lo haga nuleable (string?)
}


