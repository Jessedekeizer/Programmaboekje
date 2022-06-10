using System.Data;
using Dapper;
using DigitaalProgrammaBoekje.Pages.Database;
using DigitaalProgrammaBoekje.Pages.Database.Models;
namespace DigitaalProgrammaBoekje.Pages.Database.Repositories;

public class SponsortRepository
{
    private IDbConnection GetConnection()
    {
        return new DbUtils().Connect();
    }
    public IEnumerable<Sponsort> GetSponsors()
    {
        string sql = @"SELECT * FROM Sponsort";
        
        using var connection = GetConnection();
        var sponsorts = connection.Query<Sponsort>(sql);
        return sponsorts;
    }
    public void AddSponsor(int Bedrijf_id, int Festival_id, string Foto_link)
    {
        string sql = @"INSERT INTO Sponsort(bedrijf_id, festival_id, foto_link)
            VALUES (@Bedrijf_id, @Festival_id, @Foto_link)";

        using var connection = GetConnection();
        connection.Query(sql, new {Bedrijf_id, Festival_id, Foto_link});
    }
    
    public void RemoveSponsor(int Bedrijf_id, int Festival_id)
    {
        string sql = @"DELETE FROM Sponsort
            WHERE bedrijf_id = @Bedrijf_id AND festival_id = @Festival_id";

        using var connection = GetConnection();
        connection.Query(sql, new {Bedrijf_id, Festival_id});
    }

    public void ShowSponsor(int Bedrijf_id)
    {
        string sql = @"SELECT foto_link FROM Sponsort
        WHERE bedrijf_id = @Bedrijf_id";
        
        using var connection = GetConnection();
        connection.Query(sql, new {Bedrijf_id}); 
    }
}