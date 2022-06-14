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
    
    public IEnumerable<Festival> GetAll()
    {
        //Haalt alles op van een bepaald festival
        string sql = "SELECT * FROM Festival";
        using var connection = GetConnection();
        var festival = connection.Query<Festival>(sql);
        return festival;
    }
    
    public IEnumerable<Festival> GetFestivalUser(int User_id)
    {
        //Haalt alles op van een bepaald festival
        string sql = "SELECT * FROM Festival WHERE gebruikers_id = @User_id";

        using var connection = GetConnection();
        var festival = connection.Query<Festival>(sql, new {User_id});
        return festival;
    }
    
    public IEnumerable<Festival> FestivalsGetYears()
    {
        string sql = "SELECT * FROM Festival GROUP BY YEAR(Festival_datum)";

        using var connection = GetConnection();
        var festival = connection.Query<Festival>(sql);
        return festival;
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
    
    public void UpdateFestival(int Id, string Name, string Location, DateTime Date, string Logo)
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

    public IEnumerable<Festival> Getfestival(DateTime Jaar)
    {
        //Haalt alles op van een bepaald festival
        string sql = "SELECT * FROM Festival WHERE YEAR(festival_datum) = YEAR(@Jaar)";

        using var connection = GetConnection();
        var festival = connection.Query<Festival>(sql, new {Jaar});
        return festival;
    }
    
    public IEnumerable<Festival> GetCurrentfestival(int Festival_id)
    {
        //Haalt alles op van een bepaald festival
        string sql = "SELECT * FROM Festival WHERE festival_id = @Festival_id";

        using var connection = GetConnection();
        var festival = connection.Query<Festival>(sql, new {Festival_id});
        return festival;
    }

    
    
        

}