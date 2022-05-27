using System.Data;
using Dapper;
using DigitaalProgrammaBoekje.Pages.Database;
using DigitaalProgrammaBoekje.Pages.Database.Models;

namespace DigitaalProgrammaBoekje.Pages.Database.Repositories;

public class GebruikerRepository
{
    private IDbConnection GetConnection()
    {
        return new DbUtils().Connect();
    }

    public void DeleteGebruiker(int Gebruiker_id)
    {
        //verwijdert gebruiker 
        string sql = @"DELETE FROM Gebruiker WHERE gebruiker_id = @gebruiker_id";
        
        using var connection = GetConnection();
        connection.Query(sql, new { Gebruiker_id });
    }
    
}