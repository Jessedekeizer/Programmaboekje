using System.Data;
using Dapper;
using DigitaalProgrammaBoekje.Pages.Database;
using DigitaalProgrammaBoekje.Pages.Database.Models;
namespace DigitaalProgrammaBoekje.Pages.Database.Repositories;

public class Neemt_deelRepository
{
    private IDbConnection GetConnection()
    {
        return new DbUtils().Connect();
    }
    
    public void AddJuryFestival(int Jury_id, int Festival_id)
    {
        string sql = @"INSERT INTO Neemt_deel(jury_id, festival_id)
            VALUES (@Jury_id, @Festival_id)";

        using var connection = GetConnection();
        connection.Query(sql, new {Jury_id, Festival_id});
    }
    
    public void RemoveJuryFestival(int Jury_id, int Festival_id)
    {
        string sql = @"DELETE FROM Neemt_deel
            WHERE jury_id = @Jury_id AND festival_id = @Festival_id";

        using var connection = GetConnection();
        connection.Query(sql, new {Jury_id, Festival_id});
    }

    public void ShowJuryFestival(int Festival_id)
    {
        string sql = @"SELECT Jl.jury_naam FROM Jurylid Jl
        LEFT JOIN Neemt_deel N ON N.jury_id = Jl.jury_id
        WHERE N.festival_id = @Festival_id";
            
            
        using var connection = GetConnection();
        connection.Query(sql, new {Festival_id});
    }
}