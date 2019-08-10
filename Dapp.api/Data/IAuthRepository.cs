using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    //Starter alltid interface med I
    public interface IAuthRepository
    {
        // register the user
        //and method for the user to  log in to the API
        Task<User> Register(User user, string password); 
         
        Task<User> Login(string username, string password);
        
        //A method to check to see if a user actually already exists - want uniqe usernames
        Task<bool> UserExists(string username);
    }
}