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

    public void DeleteGebruiker(int Gebruiker_id)
    {
        //verwijdert gebruiker 
        string sql = @"DELETE FROM Gebruiker WHERE gebruiker_id = @gebruiker_id";
        
        using var connection = GetConnection();
        connection.Query(sql, new { Gebruiker_id });
    }
    
    public void AddUser(string Username, string Email, string Password, char Functie, int Telefoonnummer, string Dirigent)
    {
        string sql = @" INSERT INTO gebruikers (naam, email, wachtwoord, functie, telefoonnummer, dirigent)
                        VALUES (@Username, @Email, @Password, @Functie, @Telefoonnummer, @Dirigent)";
        using var connection = GetConnection();
        connection.Query<Gebruiker>(sql, new{Username, Email, Password, Functie, Telefoonnummer, Dirigent});
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
    
}