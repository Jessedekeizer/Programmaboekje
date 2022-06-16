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
    public IEnumerable<Bedrijf> GetAllbedrijf()
    {
        //Haalt alles op de bedrijven
        string sql = "SELECT * FROM Bedrijf";

        using var connection = GetConnection();
        var bedrijfen = connection.Query<Bedrijf>(sql);
        return bedrijfen;
    }
    public IEnumerable<Bedrijf> Getbedrijf(int Bedrijf_id)
    {
        //Haalt alles op van een bepaald bedrijf
        string sql = "SELECT * FROM Bedrijf WHERE bedrijf_id = @Bedrijf_id";

        using var connection = GetConnection();
        var bedrijfen = connection.Query<Bedrijf>(sql, new {Bedrijf_id});
        return bedrijfen;
    }
    
    public int AddBedrijf(string Name, string Link)
    {
        //Voegt een bedrijf toe
        string sql = @"
                INSERT INTO bedrijf (bedrijf_naam, websitelink) 
                VALUES (@Name, @Link); SELECT LAST_INSERT_ID()";

        using var connection = GetConnection();
       int bedrijf_id = connection.ExecuteScalar<int>(sql, new {Name, Link});
       return bedrijf_id;
    }
    
    public void DeleteBedrijf(int Bedrijf_id)
    {
        //Verwijdert een bepaald bedrijf
        string sql = @"DELETE FROM bedrijf WHERE bedrijf_id = @Bedrijf_id";

        using var connection = GetConnection();
        connection.Query(sql, new {Bedrijf_id});
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
    
    public IEnumerable<Bedrijf> UpdateLinkBedrijf(int bedrijf_id, string LinkUpd)
    {
        //Hier kan je de websitelink van een bedrijf aanpassen.
        string sql = @"
                UPDATE bedrijf SET 
                   websitelink = @LinkUpd
                WHERE bedrijf_id = @Bedrijf_id;";

        using var connection = GetConnection();
        var Link = connection.Query<Bedrijf>(sql, new {bedrijf_id, LinkUpd});
        return Link;
    }
    
    public IEnumerable<Bedrijf> UpdateNaamBedrijf(int bedrijf_id, string NaamUpd)
    {
        //Hier kan je de naam van een bedrijf aanpassen.
        string sql = @"
                UPDATE bedrijf SET 
                   bedrijf_naam = @NaamUpd
                WHERE bedrijf_id = @Bedrijf_id;";

        using var connection = GetConnection();
        var Naam = connection.Query<Bedrijf>(sql, new {bedrijf_id, NaamUpd});
        return Naam;
    }
    
    public IEnumerable<Bedrijf> Get1Bedrijf(int Bedrijf_id)
    {
        //Hier haal je een bepaalt bedrijf op
        string sql = @"SELECT * FROM bedrijf WHERE bedrijf_id = @Bedrijf_id";
        using var connection = GetConnection();
        var bedrijf = connection.Query<Bedrijf>(sql, new {Bedrijf_id});
        return bedrijf;
    }
}