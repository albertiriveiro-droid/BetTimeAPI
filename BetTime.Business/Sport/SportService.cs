using BetTime.Models;
using BetTime.Data;
using Microsoft.EntityFrameworkCore;

namespace BetTime.Business;


public class SportService:ISportService
{

private readonly ISportRepository _repository;

public SportService(ISportRepository repository)
    {
      _repository= repository;  
    }

public Sport CreateSport(SportCreateDTO sportCreateDTO)
{
    if (_repository.SportNameExists(sportCreateDTO.Name))
        throw new InvalidOperationException("A sport with this name already exists.");

    var sport = new Sport(sportCreateDTO.Name);

    try
    {
        _repository.AddSport(sport);
    }
    catch (DbUpdateException)
    {
       
        throw new InvalidOperationException("A sport with this name already exists.");
    }

    return sport;
}

public IEnumerable<Sport> GetAllSports()
    {
   return _repository.GetAllSports();
   
    }

public Sport GetSportById(int sportId)
    {

var sport= _repository.GetSportById(sportId);
 if (sport == null)
        {
        throw new KeyNotFoundException($"Deporte con ID {sportId} no encontrado");
        }   
        return sport;   
    }

public void DeleteSport(int sportId)
    {
     var sport= _repository.GetSportById(sportId);
     if(sport== null)
        {
     throw new KeyNotFoundException($"Deporte con ID {sportId} no encontrado");
        }
     _repository.DeleteSport(sport);   
    }

public void UpdateSport(int id, SportUpdateDTO sportUpdateDTO)
{
    var sport = _repository.GetSportById(id)
        ?? throw new KeyNotFoundException($"Sport with ID {id} not found");

    if (!string.IsNullOrWhiteSpace(sportUpdateDTO.Name) && sportUpdateDTO.Name != sport.Name)
    {
        if (_repository.SportNameExists(sportUpdateDTO.Name))
            throw new InvalidOperationException("A sport with this name already exists.");

        sport.Name = sportUpdateDTO.Name.Trim();
    }

    try
    {
        _repository.UpdateSport(sport);
    }
    catch (DbUpdateException)
    {
       
        throw new InvalidOperationException("A sport with this name already exists.");
    }
}
}