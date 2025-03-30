using Fleximinded.Core.Parts.CLI;
using System.Security.Cryptography;
using Fleximinded.Core.Extensions;
using FlexiMinded.Core.Security;
using System.Text;

namespace Syntra.Security.Encrypt
{
    public class HashFunctions : ICliExecutable
    {
        public string Name { get=>"Syntra's Great Hashing"; }
        public string Description { get => "Let's give it a great hash, yeah!"; }

        public bool Execute(ICliRuntime owner, ICliCommand cmd)
        {
            string? source = null;
            string? alg = null;

            switch(cmd.Command)
            {

                case "hash":
                    if(cmd.ContainsOption("alg"))
                    {
                        alg = cmd.FindOption("alg")?.Value;

                    }
                    if(cmd.ContainsOption("src"))
                    {
                        source = cmd.FindOption("src")?.Value;
                    }
                    if(source == null || alg == null)
                    {
                        Console.WriteLine("Please provide a source and an algorithm");
                        return false;
                    }
                    return Hash(source, alg);
                case "password":
                    if((cmd.RawCommandParameter?.Length > 0))
                    {
                        source = cmd.RawCommandParameter;
                        return HashAsPassword(source);
                    }
                    break;
                default:
                    return false;
            }
            return false;
        }
        private bool Hash(string source, string alg)
        {
            byte[] data = null;
            if(File.Exists(source))
            {
                data = File.ReadAllBytes(source);
            }
            else {
                data = Encoding.Unicode.GetBytes(source);
            }
            if(data == null)
            {
                return false;
            }
            using HashAlgorithm hasher = alg switch
            {
                "md5" => MD5.Create(),
                "sha1" => SHA1.Create(),
                "sha256" => SHA256.Create(),
                "sha512" => SHA512.Create(),
                _ => SHA256.Create()
            };
            byte[] hash = hasher.ComputeHash(data);
            Console.WriteLine($"Computed hash bytes:\t\t{hash.ToHexString()}");
            Console.WriteLine($"Computed hash as Base64:\t{Convert.ToBase64String(hash)}");
            return true;
        }
        private bool HashAsPassword(string source) {
            byte[]? salt = /*Get 16 first bytes from the password hash in my table else */ null;
            //if password hash is not in my table:
            salt ??=RandomNumberGenerator.GetBytes(16);
            HashAlgorithmName alg = HashAlgorithmName.SHA256;   
            Rfc2898DeriveBytes passwordHash = new(Encoding.Unicode.GetBytes(source), Encoding.Unicode.GetBytes("salt"), 100911,alg);
            var result = passwordHash.GetBytes(32);
            Console.WriteLine($"Computed hash bytes:\t\t{result.ToHexString()}");
            Console.WriteLine($"Computed hash as Base64:\t{Convert.ToBase64String(result)}");
            Console.WriteLine();
            Console.WriteLine($"Stored in my user table:\t\t{salt.ToHexString()}{result.ToHexString()}");
            return true;
        }
    }
}
