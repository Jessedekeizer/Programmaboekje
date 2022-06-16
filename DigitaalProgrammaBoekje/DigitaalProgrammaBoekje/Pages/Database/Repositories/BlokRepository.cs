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

    public void AddBlok(int Blok_id, int Fest_id, TimeSpan Starttime, char Type, string Text)
    {
        //Voegt een blok toe en als die al bestaat, dan update hij de bestaande
        if (Blok_id != 0)
        {
            UpdateBlok(Blok_id, Starttime, Type, Text);
        }
        else
        {
            int Number = 1 + CheckRanking(Fest_id);
            string sql = @"
                INSERT INTO Blok (festival_id, begintijd, blok_type, bloknummer, tekstvak) 
                VALUES (@Fest_id, @Starttime, @Type, @Number, @Text)";

            using var connection = GetConnection();
            connection.Query<Blok>(sql, new {Fest_id, Starttime, Type, Number, Text});
        }
    }
    
    public void DeleteBlok(int Blok_id)
    {
        //Verwijdert een bepaald blok
        string sql = @"DELETE FROM Blok WHERE blok_id = @Blok_id";

        using var connection = GetConnection(); 
        connection.Query(sql, new { Blok_id });
    }
    
    public void UpdateBlok(int Blok_id, TimeSpan Starttime, char Type, string Text)
    {
        //Hier kan je de velden van een blokken aanpassen.
        string sql = @"
                UPDATE Blok SET 
                    begintijd = @Starttime,
                    blok_type = @Type,
                    tekstvak = @Text
                WHERE blok_id = @Blok_id;";
            
        using var connection = GetConnection();
        connection.Query<Blok>(sql, new{Blok_id, Starttime, Type, Text});
    }
    
    public IEnumerable<Blok> GetAll(int Festival_id)
    {
        //Haalt alle blokken op
        string sql = @"select * from blok b 
                inner join festival f on b.festival_id = f.festival_id
                left join orkestgroep o on b.blok_id = o.blok_id WHERE b.festival_id = @Festival_id  ORDER BY bloknummer";

        using var connection = GetConnection();
        var products = connection.Query<Blok, Festival, Orkestgroep, Blok>(sql, (Blok, Festival, Orkestgroep) =>
        {
            Blok.Orkestgroep = Orkestgroep;
            Blok.Festival = Festival;
            return Blok;
        }, new{Festival_id} , splitOn: "Festival_id, Orkest_id");
        return products;
    }

    public void ChangeRankingDown(int Bloknummer, int Blok_id, int Festival_id)
    {
        //Verplaatst een blok 1 plek naar onder
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
        //Verplaatst een blok 1 plek omhoog
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

    public int CheckRanking (int Festival_id)
    {
        //bekijkt op welke plek een bepaald blok staat
        string sql = @"SELECT MAX(bloknummer) FROM blok WHERE festival_id = @Festival_id";
        using var connection = GetConnection();
        int Check = connection.ExecuteScalar<int>(sql, new {Festival_id });
        return Check;
    }
}