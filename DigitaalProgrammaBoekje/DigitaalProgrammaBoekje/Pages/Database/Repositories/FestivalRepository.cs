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
        var festivals = connection.Query<Festival>(sql, new {festival_id});
        return festivals;
    }
    
    public void AddFestival(String Name, DateOnly Date, string Logo, int User)
    {
        //Voeg een festival toe
        string sql = @"
                INSERT INTO festival (festival_naam, festival_datum, festival_logo, gebruiker_id) 
                VALUES (@Name, @Date, @Logo, @User)";

        using var connection = GetConnection();
        connection.Query<Festival>(sql, new{Name, Date, Logo, User});
    }
    
    public void DeleteFestival(int Festival_id)
    {
        //Verwijdert een bepaald festival
        string sql = @"DELETE FROM Festival WHERE festival_id = @Festival_id";

        using var connection = GetConnection(); 
        connection.Query(sql, new { Festival_id });
    }
    
    public void UpdateFestival(int Id, String Name, DateOnly Date, string Logo)
    {
        //Hier kan je de velden van een festivallen aanpassen.
        string sql = @"
                UPDATE Stripboek SET 
                    festival_naam = @Name,
                    festival_datum = @Date,
                    festival_logo = @Logo,
                WHERE festival_id = @Id;";
            
        using var connection = GetConnection();
        connection.Query<Festival>(sql, new{Id, Name, Date, Logo});
    }
}