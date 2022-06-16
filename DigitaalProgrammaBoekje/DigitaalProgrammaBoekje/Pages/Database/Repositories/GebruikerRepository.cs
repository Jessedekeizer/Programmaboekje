using System.Data;
using Dapper;
using DigitaalProgrammaBoekje.Pages.Database;
using DigitaalProgrammaBoekje.Pages.Database.Models;

namespace DigitaalProgrammaBoekje.Pages.Database.Repositories;

public class GebruikerRepository
{
    private IDbConnection GetConnection()
    {
        return new DbUtils().Connect();
    }

    public void DeleteOrganisator(int Gebruiker_id)
    {
        //verwijdert gebruiker 
        string sql = @"DELETE FROM Gebruikers WHERE gebruiker_id = @gebruiker_id";
        
        using var connection = GetConnection();
        connection.Query(sql, new { Gebruiker_id });
    }

    public void AddUser(string Username, string Email, string Password, char Functie, int Telefoonnummer,
        string Dirigent, int LedenAantal)
    {
        string sql = @" INSERT INTO gebruikers (naam, email, wachtwoord, functie, telefoonnummer, dirigent, leden_aantal)
                        VALUES (@Username, @Email, @Password, @Functie, @Telefoonnummer, @Dirigent, @LedenAantal)";
        using var connection = GetConnection();
        connection.Query<Gebruiker>(sql, new{Username, Email, Password, Functie, Telefoonnummer, Dirigent, LedenAantal});
    }
    public void AddOrganisator(string Username, string Password)
    {
        string sql = @" INSERT INTO gebruikers (naam, wachtwoord, functie)
                        VALUES (@Username, @Password, 'o')";
        using var connection = GetConnection();
        connection.Query<Gebruiker>(sql, new{Username, Password});
    }
    
    public bool checkUsername(string Username)
    {
        string sql = "SELECT COUNT(gebruiker_id) FROM gebruikers WHERE naam = @Username";
            
        using var connection = GetConnection();
        bool amount = connection.ExecuteScalar<bool>(sql, new{Username});
        return amount;
    }
    public bool checkEmail(string Email)
    {
        string sql = "SELECT COUNT(gebruiker_id) FROM gebruikers WHERE email = @Email";
            
        using var connection = GetConnection();
        bool amount = connection.ExecuteScalar<bool>(sql, new{Email});
        return amount;
    }
    
    public int Gebruiker_ID (string Username)
    {
        string sql = @"SELECT gebruiker_id FROM gebruikers WHERE naam = @Username";
        using var connection = GetConnection();
        int Gebruiker_ID = connection.ExecuteScalar<int>(sql, new {Username});
        return Gebruiker_ID;
    }
    
    public string GetPassword(int Gebruiker_id)
    {
        string sql = @"SELECT wachtwoord FROM gebruikers WHERE gebruiker_id = @Gebruiker_id";
        using var connection = GetConnection();
        string amount = connection.ExecuteScalar<string>(sql, new {Gebruiker_id});
        return amount;
    }

    public char GetUserRole(int gebruiker_id)
    {
        string sql = @"SELECT functie FROM gebruikers WHERE Gebruiker_id = @gebruiker_id";
        using var connection = GetConnection();
        char functie = connection.ExecuteScalar<char>(sql, new {gebruiker_id});
        return functie;
    }
    
    public IEnumerable<Gebruiker> GetUser(int Gebruiker_id)
    {
        string sql = @"SELECT * FROM gebruikers WHERE gebruiker_id = @Gebruiker_id";
        using var connection = GetConnection();
        var gebruiker = connection.Query<Gebruiker>(sql, new {Gebruiker_id});
        return gebruiker;
    }
    public IEnumerable<Gebruiker> GetOrganisators()
    {
        string sql = @"SELECT * FROM gebruikers WHERE functie = 'o'";
        using var connection = GetConnection();
        var organisators = connection.Query<Gebruiker>(sql);
        return organisators;
    }
    public string UpdatePassword(int Gebruiker_id, string PasswordUpd)
    {
        string sql = @"UPDATE gebruikers SET wachtwoord = @PasswordUpd WHERE gebruiker_id = @Gebruiker_id";
        using var connection = GetConnection();
        connection.Execute(sql, new {Gebruiker_id, PasswordUpd});
        return "Succes";
        
    }
    
    public int UpdateUsername(int Gebruiker_id, string UsernUpdate)
    {
        if (!checkUsername(UsernUpdate))
        {
            string sql = @"UPDATE gebruikers SET naam = @UsernUpdate WHERE gebruiker_id = @Gebruiker_id";
            using var connection = GetConnection();
            connection.Execute(sql, new {Gebruiker_id, UsernUpdate});
            return 0;
        }

        return 2;
    }
    
    public string UpdateEmail(int Gebruiker_id, string EmailUpd)
    {
        string sql = @"UPDATE gebruikers SET email = @EmailUpd WHERE gebruiker_id = @Gebruiker_id";
        using var connection = GetConnection();
        string email = connection.ExecuteScalar<string>(sql, new {Gebruiker_id, EmailUpd});
        return email;
    }
    
    public string UpdateDirigent(int Gebruiker_id, string DirigentUpd)
    {
        string sql = @"UPDATE gebruikers SET dirigent = @DirigentUpd WHERE gebruiker_id = @Gebruiker_id";
        using var connection = GetConnection();
        string dirigent = connection.ExecuteScalar<string>(sql, new {Gebruiker_id, DirigentUpd});
        return dirigent;
    }
    
    public string UpdateNummer(int Gebruiker_id, string NummerUpd)
    {
        string sql = @"UPDATE gebruikers SET telefoonnummer = @NummerUpd WHERE gebruiker_id = @Gebruiker_id";
        using var connection = GetConnection();
        string nummer = connection.ExecuteScalar<string>(sql, new {Gebruiker_id, NummerUpd});
        return nummer;
    }
}