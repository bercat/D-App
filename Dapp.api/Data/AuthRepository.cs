using System;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<User> Login(string username, string password)
        {
            // a variabel to store a used room 
            //and will need to go out to our database
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            
            if (user == null)
                return null;
            
            if(!VerifyPasswordHash(password, user.PassHash, user.PassSalt))
                return null;
            //kan returnere user, fordi brukeren har gått igjennom som true
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passHash, byte[] passSalt)
        {
            //plasserer the key som parameter, og nøkkelen her er passSalt
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passSalt))
            {
                //computedHash er lik som passHash i CreatePasswordHash()
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i <computedHash.Length; i++)
                {
                    if (computedHash[i] != passHash[i]) return false;
                }
            }
            return true; //om alle elementene i arrayen matcher, return true
        }

        public async Task<User> Register(User user, string password)
        {
            //disse verdiene lagres i byte[]
            byte[] passHash, passSalt;
            CreatePasswordHash(password, out passHash, out passSalt);

            user.PassHash = passHash;
            user.PassSalt = passSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        //returnerer ikke noe i denen metoden 
        private void CreatePasswordHash(string password, out byte[] passHash, out byte[] passSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                //setter password salt og hash
                passSalt = hmac.Key;// randomly genererte .key
                //gir ComputeHash() hva den leter etter når det gjelder passord
                passHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            
        }

        public async Task<bool> UserExists(string username)
        {
            //bruker expression fordi vi vil sammenligne username med 
            //hvem som helst annen bruker i databasen
            if(await _context.Users.AnyAsync(x => x.Username == username))
                
                return true; //betyr den har funnet en match i databasen

            return false;// om ikke returneres false
        }
    }
}