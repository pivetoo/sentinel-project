using Isopoh.Cryptography.Argon2;

namespace Sentinel.Domain.Services.Security
{
    public class Argon2HashingService
    {
        public string GerarHash(string senha)
        {
            return Argon2.Hash(senha);
        }

        public bool ValidarHash(string hash, string senha)
        {
            return Argon2.Verify(hash, senha);
        }
    }
}
