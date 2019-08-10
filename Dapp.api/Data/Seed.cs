using System.Collections.Generic;
using DatingApp.API.Models;
using Newtonsoft.Json;

namespace DatingApp.API.Data
{
    public class Seed
    {
        private readonly DataContext _context;
        public Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedUsers()
        {
            /*teksten med brukere er lagret i userData */
            var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
            /*serialiser dette til objects*/
            var users = JsonConvert.DeserializeObject<List<User>>(userData);
            /* bruker en loop til å lese alt sammen og lagre det inni databasen */
            foreach (var user in users)
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash("password", out passwordHash, out passwordSalt);
            
                user.PassHash = passwordHash; /*hver bruker har en unik passwordHash */
                user.PassSalt = passwordSalt;
                user.Username = user.Username.ToLower(); /*Brukernavn i ToLower() */

                _context.Users.Add(user); /*legger til user i Users collection */
            }
            /*lagre tilbake til databasen */
            _context.SaveChanges();
        }

        private void CreatePasswordHash(string password, out byte[] passHash, out byte[] passSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                //setter password salt og hash
                passSalt = hmac.Key;// randomly genererte .Key
                //gir ComputeHash() hva den leter etter når det gjelder passord
                passHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            
        }
    }
}