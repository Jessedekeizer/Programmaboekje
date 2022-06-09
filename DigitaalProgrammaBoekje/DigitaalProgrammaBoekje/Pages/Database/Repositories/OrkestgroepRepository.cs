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
    
    
    
    public void AddOrkestgroep(string Musiclist, string Orkestname,int Divisie, int Number,
        int orkest_id, int Blok_id, int Fest_id, TimeSpan Starttime, char Type)
    {
        if (orkest_id == 0)
        {
            Number = 1 + new BlokRepository().CheckRanking(Fest_id);
            string sql = @"
                INSERT INTO Blok (festival_id, begintijd, blok_type, bloknummer) 
                VALUES (@Fest_id, @Starttime, @Type, @Number); SELECT LAST_INSERT_ID()";

            using var connection = GetConnection();
            int blok_id = connection.QuerySingle<int>(sql, new {Fest_id, Starttime, Type, Number});
            
            sql = @"
                INSERT INTO Orkestgroep (blok_id, muziekstukken, orkestnaam, divisie) 
                VALUES (@blok_id, @Musiclist, @Orkestname,@Divisie)";
            connection.Query<Orkestgroep>(sql, new{blok_id, Musiclist, Orkestname, Divisie});
        }
        else
        {
            UpdateOrkestgroep(orkest_id, Musiclist, Orkestname, Divisie, Number, Blok_id, Starttime);
        }
        
    }

    public int CheckIfOrkestgroep(int Blok_id)
    {
        string sql = @"SELECT orkest_id FROM orkestgroep WHERE blok_id =@Blok_id";
        using var connection = GetConnection();
        int check = connection.ExecuteScalar<int>(sql, new { Blok_id});
        return check;
    }
    
    public void DeleteOrkestgroep(int Orkest_id, int Blok_id, int BLoknummer)
    {
        
        string sql1 = @"Update blok SET 
                        bloknummer = bloknummer - 1
                        WHERE bloknummer > @Bloknummer ;
DELETE FROM Orkestgroep WHERE orkest_id = @Orkest_id;
                        DELETE FROM Blok WHERE blok_id = @Blok_id";
        
        using var connection = GetConnection(); 
        connection.Query(sql1, new { Orkest_id, Blok_id, BLoknummer });
        
        
    }
    
    public void UpdateOrkestgroep(int Orkest_id, string Musiclist, string Orkestname, int Divisie, int Number, int Blok_id, TimeSpan Starttime)
    {
        //Hier kan je de velden van een Orkestgroepen aanpassen.
        string sql = @"
                UPDATE Orkestgroep SET 
                    muziekstukken = @Musiclist,
                    orkestnaam = @Orkestname,
                    cijfer = @Number,
                    divisie = @Divisie
                WHERE orkest_id = @Orkest_id;
                UPDATE blok SET 
                begintijd = @Starttime
                WHERE blok_id = @Blok_id";
        
            
        using var connection = GetConnection();
        connection.Query<Orkestgroep>(sql, new{Orkest_id, Musiclist, Orkestname, Number, Divisie, Blok_id, Starttime});
    }

    public void UpdateCijfer(int Orkest_id, int Number)
    {
        string sql = @"UPDATE orkestgroep SET cijfer = @Number WHERE orkest_id = @Orkest_id";
        using var connection = GetConnection();
        connection.Query(sql, new {Orkest_id, Number });
    }
}