using System.Data;
using Dapper;
using DigitaalProgrammaBoekje.Pages.Database;
using DigitaalProgrammaBoekje.Pages.Database.Models;

namespace DigitaalProgrammaBoekje.Pages.Database.Repositories;

public class FestivalRepository
{
    private IDbConnection GetConnection()
    {
        return new DbUtils().Connect();
    }
    
    public IEnumerable<Festival> Get(int festival_id)
    {
        //Haalt alles op van een bepaald festival
        string sql = "SELECT * FROM Festival WHERE Festival_id = @festival_id";

        using var connection = GetConnection();
        var festival = connection.Query<Festival>(sql, new {festival_id});
        return festival;
    }
    
    public IEnumerable<Festival> FestivalsGet()
    {
        string sql = @"SELECT * FROM festival
        ORDER BY festival_id DESC";

        using var connection = GetConnection();
        var festivals = connection.Query<Festival>(sql, new {});
        return festivals;
    }
    
    public void AddFestival(string Name, string Location, DateTime Date, string Logo, int User)
    {
        //Voeg een festival toe
        string sql = @"
                INSERT INTO festival (festival_naam, festival_locatie, festival_datum, festival_logo, gebruiker_id) 
                VALUES (@Name,@Location, @Date, @Logo, @User)";

        using var connection = GetConnection();
        connection.Query<Festival>(sql, new{Name, Location, Date, Logo, User});
    }
    
    public void AddTestFestival(string Name, string Location, DateTime Date, string Logo)
    {
        //Voeg een festival toe
        string sql = @"
                INSERT INTO festival (festival_naam, festival_locatie, festival_datum, festival_logo) 
                VALUES (@Name,@Location, @Date, @Logo)";

        using var connection = GetConnection();
        connection.Query<Festival>(sql, new{Name, Location, Date, Logo});
    }
    
    public void DeleteFestival(int Festival_id)
    {
        //Verwijdert een bepaald festival
        string sql = @"DELETE FROM Festival WHERE festival_id = @Festival_id";

        using var connection = GetConnection(); 
        connection.Query(sql, new { Festival_id });
    }
    
    public void UpdateFestival(int Id, string Name, string Location, DateOnly Date, string Logo)
    {
        //Hier kan je de velden van een festivallen aanpassen.
        string sql = @"
                UPDATE festival SET 
                    festival_naam = @Name,
                    festival_datum = @Date,
                    festival_logo = @Logo,
                WHERE festival_id = @Id;";
            
        using var connection = GetConnection();
        connection.Query<Festival>(sql, new{Id, Name, Location, Date, Logo});
    }
}