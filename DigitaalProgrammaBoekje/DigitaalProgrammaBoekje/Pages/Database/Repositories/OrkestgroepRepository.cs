using System.Data;
using Dapper;
using DigitaalProgrammaBoekje.Pages.Database;
using DigitaalProgrammaBoekje.Pages.Database.Models;
namespace DigitaalProgrammaBoekje.Pages.Database.Repositories;

public class OrkestgroepRepository
{
    private IDbConnection GetConnection()
    {
        return new DbUtils().Connect();
    }
    
    public IEnumerable<Orkestgroep> Get(int Orkest_id)
    {
        //Haalt alles op van een bepaald Orkestgroep
        string sql = "SELECT * FROM Orkestgroep WHERE orkest_id = @Orkest_id";

        using var connection = GetConnection();
        var Orkestgroep = connection.Query<Orkestgroep>(sql, new {Orkest_id});
        return Orkestgroep;
    }
    
    
    
    public void AddOrkestgroep(int Blok_id, string Musiclist, string Orkestname, int Number)
    {
        //Voeg een Orkestgroep toe
        string sql = @"
                INSERT INTO Orkestgroep (blok_id, muziekstukken, orkestnaam, cijfer) 
                VALUES (@Blok_id, @Musiclist, @Orkestname, @Number)";

        using var connection = GetConnection();
        connection.Query<Orkestgroep>(sql, new{Blok_id, Musiclist, Orkestname, Number});
    }
    
    public void DeleteOrkestgroep(int Orkest_id, int Blok_id)
    {
        
        string sql1 = @"DELETE FROM Orkestgroep WHERE orkest_id = @Orkest_id";
        string sql2 = @"DELETE FROM Blok WHERE blok_id = @Blok_id";

        using var connection = GetConnection(); 
        connection.Query(sql1, new { Orkest_id });
        connection.Query(sql2, new { Blok_id });
        
    }
    
    public void UpdateOrkestgroep(int Orkest_id, string Musiclist, string Orkestname, int Number)
    {
        //Hier kan je de velden van een Orkestgroepen aanpassen.
        string sql = @"
                UPDATE Orkestgroep SET 
                    muziekstukken = @Musiclist,
                    orkestnaam = @Orkestname,
                    cijfer = @Number,
                WHERE orkest_id = @Orkest_id;";
            
        using var connection = GetConnection();
        connection.Query<Orkestgroep>(sql, new{Orkest_id, Musiclist, Orkestname, Number});
    }
}