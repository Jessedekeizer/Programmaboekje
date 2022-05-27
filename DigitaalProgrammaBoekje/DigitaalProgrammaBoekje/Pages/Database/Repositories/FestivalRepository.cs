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
        //Haalt alles op van een bepaald stripboek
        string sql = "SELECT * FROM Festival WHERE Festival_id = @festival_id";

        using var connection = GetConnection();
        var stripboek = connection.Query<Festival>(sql, new {festival_id});
        return stripboek;
    }
}