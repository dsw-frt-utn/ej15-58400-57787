namespace Dsw2026Ej15.Api.Models;

//Hace esto para evitar tener multiples models, para cada funcionalidad de Doctor
//public record Response();
public record DoctorModel
{
    public record Request(string Name, string LicenseNumber, Guid SpecialityId);

    // Asume por si mismo que no puede ser Nulo, pero si puede estar vacio ("") --> Middlewares q validan
    // De ser Nulo retorna un 400 BadRequest --> Amenos q lo haga nuleable (string?)
}


