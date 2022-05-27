using System.Data;
using Dapper;
using DigitaalProgrammaBoekje.Pages.Database;
using DigitaalProgrammaBoekje.Pages.Database.Models;
namespace DigitaalProgrammaBoekje.Pages.Database.Repositories;

public class BlokRepository
{
    private IDbConnection GetConnection()
    {
        return new DbUtils().Connect();
    }
    
    public IEnumerable<Blok> Get(int blok_id)
    {
        //Haalt alles op van een bepaald blok
        string sql = "SELECT * FROM Blok WHERE Blok_id = @blok_id";

        using var connection = GetConnection();
        var blok = connection.Query<Blok>(sql, new {blok_id});
        return blok;
    }
    
    public void AddBlok(int Fest_id, DateTime Starttime, string Type, int Number, string Text)
    {
        //Voeg een blok toe
        string sql = @"
                INSERT INTO Blok (festival_id, begintijd, type, bloknummer, tekstvak) 
                VALUES (@Fest_id, @Starttime, @Type, @Number, @Text)";

        using var connection = GetConnection();
        connection.Query<Blok>(sql, new{Fest_id, Starttime, Type, Number, Text});
    }
    
    public void DeleteBlok(int Blok_id)
    {
        //Verwijdert een bepaald blok
        string sql = @"DELETE FROM Blok WHERE blok_id = @Blok_id";

        using var connection = GetConnection(); 
        connection.Query(sql, new { Blok_id });
    }
    
    public void UpdateBlok(int Blok_id, DateTime Starttime, string Type, int Number, string Text)
    {
        //Hier kan je de velden van een blokken aanpassen.
        string sql = @"
                UPDATE Blok SET 
                    begintijd = @Starttime,
                    type = @Type,
                    bloknummer = @Number,
                    tekstvak = @Text,
                WHERE blok_id = @Blok_id;";
            
        using var connection = GetConnection();
        connection.Query<Blok>(sql, new{Blok_id, Starttime, Type, Number, Text});
    }
}