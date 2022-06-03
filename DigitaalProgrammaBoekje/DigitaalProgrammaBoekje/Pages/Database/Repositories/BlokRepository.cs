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
    
    public IEnumerable<Blok> GetFestival(int festival_id)
    {
        //Haalt alles op van een bepaald blok
        string sql = "SELECT * FROM Blok WHERE festival_id = @festival_id ORDER BY bloknummer";

        using var connection = GetConnection();
        var blok = connection.Query<Blok>(sql, new {festival_id});
        return blok;
    }
    
    public void AddBlok(int Fest_id, TimeSpan Starttime, char Type, int Number, string Text)
    {
        //Voeg een blok toe
        string sql = @"
                INSERT INTO Blok (festival_id, begintijd, blok_type, bloknummer, tekstvak) 
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
    
    public IEnumerable<Blok> GetAll()
    {
        string sql = @"select * from blok b 
                inner join festival f on b.festival_id = f.festival_id
                left join orkestgroep o on b.blok_id = o.blok_id ORDER BY bloknummer";

        using var connection = GetConnection();
        var products = connection.Query<Blok, Festival, Orkestgroep, Blok>(sql, (Blok, Festival, Orkestgroep) =>
        {
            Blok.Orkestgroep = Orkestgroep;
            Blok.Festival = Festival;
            return Blok;
        }, splitOn: "Festival_id, Orkest_id" );
        return products;
    }

    public void ChangeRankingDown(int Bloknummer, int Blok_id, int Festival_id)
    {
        int BlokIncrease = Bloknummer + 1;
        
        string sql = @"Update blok SET 
                bloknummer = bloknummer -1
                WHERE festival_id = @Festival_id and bloknummer LIKE " + BlokIncrease + ""; 
            string sql2 = @"
                UPDATE blok SET 
                bloknummer = bloknummer +1 
                WHERE blok_id = @Blok_id";
        using var connection = GetConnection();
        connection.Query(sql, new{Bloknummer, Festival_id, BlokIncrease});
        connection.Query(sql2, new{Blok_id});
    }
    
    public void ChangeRankingUp(int Bloknummer, int Blok_id, int Festival_id)
    {
        int BlokDecrease = Bloknummer - 1;
        string sql = @"Update blok SET 
                bloknummer = bloknummer +1
                WHERE festival_id = @Festival_id and bloknummer LIKE " + BlokDecrease + @"";
        string sql2 = @"
                UPDATE blok SET 
                bloknummer = bloknummer -1 
                WHERE blok_id = @Blok_id";
        using var connection = GetConnection();
        connection.Query(sql, new{Bloknummer, Festival_id, BlokDecrease});
        connection.Query(sql2, new{Blok_id});
    }

    public string Text(int Blok_id)
    {
        string sql = @"SELECT tekstvak FROM blok WHERE blok_id = @Blok_id";
        using var connection = GetConnection();
        string Text = connection.ExecuteScalar<string>(sql, new {Blok_id });
        return Text;
    }
}