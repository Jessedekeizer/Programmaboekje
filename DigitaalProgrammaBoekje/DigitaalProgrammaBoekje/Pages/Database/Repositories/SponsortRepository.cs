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
    public IEnumerable<Sponsort> GetSponsors(int Festival_id)
    {
        string sql = @"SELECT * FROM Sponsort S 
        INNER JOIN Bedrijf B ON S.bedrijf_id = B.bedrijf_id
        WHERE festival_id = @Festival_id";
        
        using var connection = GetConnection();
        var sponsorts = connection.Query<Sponsort>(sql, new{Festival_id});
        return sponsorts;
    }
    public void AddSponsor(int Bedrijf_id, int Festival_id, string Foto_link, string Name, string Link)
    {
        if (Bedrijf_id == 0)
        {
           Bedrijf_id = new BedrijfRepository().AddBedrijf(Name, Link);
        }

        string sql1 = @"SELECT count(*) FROM sponsort WHERE bedrijf_id = @Bedrijf_id AND festival_id = @Festival_id ";
        using var connection = GetConnection();
        bool CheckRecord = connection.ExecuteScalar<bool>(sql1, new {Bedrijf_id, Festival_id });
        if (!CheckRecord)
        {
            string sql2 = @"INSERT INTO Sponsort(bedrijf_id, festival_id, foto_link)
            VALUES (@Bedrijf_id, @Festival_id, @Foto_link)";
            connection.Query(sql2, new {Bedrijf_id, Festival_id, Foto_link});
        }
        else
        {
            UpdateSponsor(Bedrijf_id, Festival_id, Foto_link);
        }
    }
    
    public void RemoveSponsor(int Bedrijf_id, int Festival_id)
    {
        string sql = @"DELETE FROM Sponsort
            WHERE bedrijf_id = @Bedrijf_id AND festival_id = @Festival_id";

        using var connection = GetConnection();
        connection.Query(sql, new {Bedrijf_id, Festival_id});
    }
    
    public void UpdateSponsor(int Bedrijf_id, int Festival_id, string Foto_link)
    {
        string sql = @"Update sponsort (foto_link)
                        SET (@Foto_link) WHERE bedrijf_id = @Bedrijf_id AND festival_id = @Festival_id";
        
        using var connection = GetConnection();
        connection.Query(sql, new {Bedrijf_id, Festival_id, Foto_link}); 
    }

    public void ShowSponsor(int Bedrijf_id)
    {
        string sql = @"SELECT foto_link FROM Sponsort
        WHERE bedrijf_id = @Bedrijf_id";
        
        using var connection = GetConnection();
        connection.Query(sql, new {Bedrijf_id}); 
    }
}