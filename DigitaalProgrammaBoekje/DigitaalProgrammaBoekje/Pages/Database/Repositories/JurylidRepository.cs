using System.Data;
using Dapper;
using DigitaalProgrammaBoekje.Pages.Database;
using DigitaalProgrammaBoekje.Pages.Database.Models;

namespace DigitaalProgrammaBoekje.Pages.Database.Repositories;

public class JurylidRepository
{
    private IDbConnection GetConnection()
    {
        return new DbUtils().Connect();
    }

    public IEnumerable<Jurylid> Get(int jury_id)
    {
        //Haalt alles op van een bepaald jurylid
        string sql = "SELECT * FROM Jurylid WHERE Jury_id = @jury_id";

        using var connection = GetConnection();
        var juryleden = connection.Query<Jurylid>(sql, new {jury_id});
        return juryleden;
    }

    public void AddJurylid(string Name, string Bio)
    {
        //Voegt een jurylid toe
        string sql = @"
                INSERT INTO jurylid (jury_naam, jury_bio) 
                VALUES (@Name, @Bio)";

        using var connection = GetConnection();
        connection.Query<Jurylid>(sql, new {Name, Bio});
    }

    public void DeleteFestival(int jury_id)
    {
        //Verwijdert een bepaald jurylid
        string sql = @"DELETE FROM Jurylid WHERE jury_id = @Jury_id";

        using var connection = GetConnection();
        connection.Query(sql, new {jury_id});
    }

    public void UpdateJurylid(int Id, string Name, string Bio)
    {
        //Hier kan je de velden van een jurylid aanpassen.
        string sql = @"
                UPDATE Jurylid SET 
                   jury_naam = @Name,
                    jury_bio = @Bio
                WHERE jury_id = @Id;";

        using var connection = GetConnection();
        connection.Query<Jurylid>(sql, new {Id, Name, Bio});
    }
}