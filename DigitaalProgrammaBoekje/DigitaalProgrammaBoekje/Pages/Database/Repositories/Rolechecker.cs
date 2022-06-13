using System.Diagnostics;
using DigitaalProgrammaBoekje.Helpers;
using Microsoft.AspNetCore.Mvc;
using Ubiety.Dns.Core;

namespace DigitaalProgrammaBoekje.Pages.Database.Repositories;

public class Rolechecker
{
    private const string SESSION_KEY = SessionConstant.Gebruiker_ID;

    private ISession _session;

    public Rolechecker(ISession session)
    {
        _session = session;
    }

    
    
    
    public bool Loged_in()
    {
        var User_id = _session.GetString(SESSION_KEY);
        if (User_id == null)
            return false;
        else
            return true;
    }
    
    public bool checkUser()
    {
        bool answer = true;
        var User_id = _session.GetString(SESSION_KEY);
         switch(new GebruikerRepository().GetUserRole(Convert.ToInt16(User_id)))
         {
             case 'u':
             answer = true;
              break;
             case 'a':
                 answer = false;
                 break;
             case 'o':
                 answer = false;
                 break;

         }
         
        return answer;
    }
    public bool checkOrganisator()
    {
        bool answer = false;
        var User_id = _session.GetString(SESSION_KEY);
        switch(new GebruikerRepository().GetUserRole(Convert.ToInt16(User_id)))
        {
            case 'u':
                answer = false;
                break;
            case 'a':
                answer = false;
                break;
            case 'o':
                answer = true;
                break;

        }
         
        return answer;
    }
    public bool checkAdmin()
    {
        bool answer = false;
        var User_id = _session.GetString(SESSION_KEY);
        switch(new GebruikerRepository().GetUserRole(Convert.ToInt16(User_id)))
        {
            case 'u':
                answer = false;
                break;
            case 'a':
                answer = true;
                break;
            case 'o':
                answer = false;
                break;

        }
         
        return answer;
    }
}