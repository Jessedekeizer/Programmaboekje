using System.Data;
using Dapper;
using DigitaalProgrammaBoekje.Pages.Database;
using DigitaalProgrammaBoekje.Pages.Database.Models;
namespace DigitaalProgrammaBoekje.Pages.Database.Repositories;

public class Meld_aanRepository
{
    private IDbConnection GetConnection()
    {
        return new DbUtils().Connect();
    }
    
    public void AddToFestival(int Festival_id, int Gebruiker_id)
    {
        //Query voor het toevoegen van een Aanmelding(of het gelezen is en de notes)
        string sql = @"INSERT INTO Meld_aan(festival_id, gebruiker_id)
            VALUES (@Festival_id, @Gebruiker_id)";

            using var connection = GetConnection();
            connection.Query(sql, new {Festival_id, Gebruiker_id});
    }
    public void RemoveFromFestival(int Festival_id, int Gebruiker_id)
    {
        //Query voor het verwijderen van een Aanmelding(of het gelezen is en de notes)
        string sql = @"DELETE FROM Meld_aan
            WHERE festival_id = @Festival_id AND gebruiker_id  = @Gebruiker_id";

        using var connection = GetConnection();
        connection.Query(sql, new {Festival_id, Gebruiker_id});
    }
    public void ShowFestivalMeld_aan(int Festival_id)
    {
        
        string sql = @"SELECT Gb.naam FROM Gebruikers Gb
        LEFT JOIN Meld_aan M ON M.gebruiker_id = Gb.gebruiker_id
        WHERE M.festival_id = @Festival_id";
            
            
        using var connection = GetConnection();
        connection.Query(sql, new {Festival_id});
    }
}