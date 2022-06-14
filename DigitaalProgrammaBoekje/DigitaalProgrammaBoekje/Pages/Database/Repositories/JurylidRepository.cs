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

    public IEnumerable<Jurylid> GetAllJury()
    {
        //Haalt alles op van een bepaald jurylid
        string sql = @"SELECT * FROM Jurylid";

        using var connection = GetConnection();
        var juryleden = connection.Query<Jurylid>(sql);
        return juryleden;
    }

    public IEnumerable<Jurylid> GetJury(int festival_id)
    {
        //Haalt alles op van een bepaald jurylid
        string sql = @"SELECT * FROM Jurylid j
        left join neemt_deel nd on j.jury_id = nd.jury_id 
        WHERE festival_id = @Festival_id
        ORDER BY j.jury_id DESC";

        using var connection = GetConnection();
        var juryleden = connection.Query<Jurylid>(sql, new {festival_id});
        return juryleden;
    }

    public void AddJurylid(int Jury_id, string Name, string Bio, string Foto, int Festival_id)
    {
        if (Jury_id != 0)
        {
            UpdateJurylid(Jury_id, Name, Bio, Foto);
        }
        else
        {
            string sql = @"
                INSERT INTO jurylid (jury_naam, jury_bio, jury_foto) 
                VALUES (@Name, @Bio, @Foto); SELECT LAST_INSERT_ID()";

            using var connection = GetConnection();
            Jury_id = connection.QuerySingle<int>(sql, new {Name, Bio, Foto});

            sql = @"
                INSERT INTO neemt_deel (jury_id, festival_id) 
                VALUES (@Jury_id, @Festival_id)";
            connection.Query(sql, new {Jury_id, Festival_id});
        }
    }

    public void AddExistingjury(int Jury_id, int Festival_id)
    {
        string sql = @"
                INSERT INTO neemt_deel (jury_id, festival_id) 
                VALUES (@Jury_id, @Festival_id)";
        using var connection = GetConnection();
        connection.Query(sql, new {Jury_id, Festival_id});
    }

    public void DeleteJury(int Jury_id)
    {
        //Verwijdert een bepaald jurylid
        string sql = @"DELETE FROM Jurylid WHERE jury_id = @Jury_id";

        using var connection = GetConnection();
        connection.Query(sql, new {Jury_id});
    }

    public void UnassignJury(int Jury_id)
    {
        //Verwijdert een bepaald jurylid
        string sql = @"DELETE FROM neemt_deel WHERE jury_id = @Jury_id";

        using var connection = GetConnection();
        connection.Query(sql, new {Jury_id});
    }

    public void UpdateJurylid(int Id, string Name, string Bio, string Foto)
    {
        //Hier kan je de velden van een jurylid aanpassen.
        string sql = @"
                UPDATE Jurylid SET 
                   jury_naam = @Name,
                    jury_bio = @Bio,
                    jury_foto = @Foto
                WHERE jury_id = @Id;";

        using var connection = GetConnection();
        connection.Query<Jurylid>(sql, new {Id, Name, Bio, Foto});
    }

    public IEnumerable<Jurylid> Get1Jury(int Jury_id)
    {
        string sql = @"SELECT * FROM jurylid WHERE jury_id = @Jury_id";
        using var connection = GetConnection();
        var gebruiker = connection.Query<Jurylid>(sql, new {Jury_id});
        return gebruiker;
    }
}