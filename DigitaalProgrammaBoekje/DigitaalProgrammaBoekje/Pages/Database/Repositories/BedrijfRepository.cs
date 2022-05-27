using System.Data;
using Dapper;
using DigitaalProgrammaBoekje.Pages.Database;
using DigitaalProgrammaBoekje.Pages.Database.Models;

namespace DigitaalProgrammaBoekje.Pages.Database.Repositories;

public class BedrijfRepository
{
    private IDbConnection GetConnection()
    {
        return new DbUtils().Connect();
    }
    
    public IEnumerable<Bedrijf> Get(int bedrijf_id)
    {
        //Haalt alles op van een bepaald bedrijf
        string sql = "SELECT * FROM Bedrijf WHERE Bedrijf_id = @bedrijf_id";

        using var connection = GetConnection();
        var bedrijfen = connection.Query<Bedrijf>(sql, new {bedrijf_id});
        return bedrijfen;
    }
    
    public void AddBedrijf(string Name, string Link)
    {
        //Voegt een jurylid toe
        string sql = @"
                INSERT INTO bedrijf (bedrijf_naam, websitelink) 
                VALUES (@Name, @Link)";

        using var connection = GetConnection();
        connection.Query<Bedrijf>(sql, new {Name, Link});
    }
    
    public void DeleteBedrijf(int bedrijf_id)
    {
        //Verwijdert een bepaald bedrijf
        string sql = @"DELETE FROM bedrijf WHERE bedrijf_id = @Bedrijf_id";

        using var connection = GetConnection();
        connection.Query(sql, new {bedrijf_id});
    }
    
    public void UpdateBedrijf(int Id, string Name, string Link)
    {
        //Hier kan je de velden van een bedrijf aanpassen.
        string sql = @"
                UPDATE bedrijf SET 
                   bedrijf_naam = @Name,
                    websitelink = @Bio
                WHERE bedrijf_id = @Id;";

        using var connection = GetConnection();
        connection.Query<Bedrijf>(sql, new {Id, Name, Link});
    }
}