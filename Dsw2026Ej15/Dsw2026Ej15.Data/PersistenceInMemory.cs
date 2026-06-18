using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Dsw2026Ej15.Data.Dtos;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dsw2026Ej15.Data;

public class PersistenceInMemory : IPersistence
{
    private List<Doctor> Doctores = [];
    private List<Speciality> Especialidades = [];
    

    public PersistenceInMemory()
    {
        LoadSpecialities();
    }

    private void LoadSpecialities()
    {
        try
        {
            string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "Sources", "specialities.json");
            string json = File.ReadAllText(jsonPath);
            var specialities = JsonSerializer.Deserialize<List<SpecialityDto>>(json,
                new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true
                }) ?? [];
            Especialidades = [.. specialities.Select(s => new Speciality(s.Name, s.Description, s.Id))];
        }
        catch (Exception)
        {

        }
    }

    public Speciality? GetSpecialityById(Guid id)
    {
        return Especialidades.Find(s => s.Id == id);
    }
    public void SaveDoctor(Doctor doctor)
    {
        Doctores.Add(doctor);
    }
    public IEnumerable<Doctor> GetDoctores()
    {
        return Doctores;
    }

}
