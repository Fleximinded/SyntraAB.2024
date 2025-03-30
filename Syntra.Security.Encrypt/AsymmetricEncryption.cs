using Fleximinded.Core.Extensions;
using Fleximinded.Core.Parts.CLI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Syntra.Security.Encrypt
{
    public static class AsymmetricExtensions
    {
        public static string ToJson(this AsymmetricKey key)
        {
            return JsonSerializer.Serialize(key);
        }
        public static void ToJsonFile(this AsymmetricKey key, string jsonFile, bool? isPrivate = null)
        {
            bool oldVal = key.IsPrivate;
            key.IsPrivate = isPrivate != null ? isPrivate.Value : oldVal;
            File.WriteAllText(jsonFile, key.ToJson());
            key.IsPrivate = oldVal;

        }
        public static AsymmetricKey? FromJson(this string jsonData)
        {
            return JsonSerializer.Deserialize<AsymmetricKey>(jsonData);
        }
        public static AsymmetricKey? FromJsonFile(this string jsonFile)
        {
            if(File.Exists(jsonFile))
            {
                return JsonSerializer.Deserialize<AsymmetricKey>(File.ReadAllText(jsonFile));
            }
            return null;
        }
    }
    public class AsymmetricKey
    {
        public bool IsPrivate { get; set; } = false;
        public string Public { get; set; } = default!;
        [JsonIgnore]
        public string? PrivateSecret { get; set; }
        public string? Private { get => IsPrivate ? PrivateSecret : null; set => PrivateSecret = value; }

        static public AsymmetricKey Generate()
        {
            using RSA rsa = RSA.Create(2048);
            return new AsymmetricKey()
            {
                Public = rsa.ExportRSAPublicKeyPem(),
                PrivateSecret = rsa.ExportRSAPrivateKeyPem(),
                IsPrivate = true
            };
        }

    }
    public class AsymmetricEncryption : ICliExecutable
    {
        public string Name { get => "Syntra's Great Asymmetric encryptor"; }
        public string Description { get => "Let's give it a great encryption, yeah do it symmetric!"; }

        public bool Execute(ICliRuntime owner, ICliCommand cmd)
        {

            string? source = null;
            string? dest = null;
            string? key = null;

            switch(cmd.Command)
            {

                case "encrypt.asymmetric":
                    if(cmd.ContainsOption("src"))
                    {
                        source = cmd.FindOption("src")?.Value;
                    }
                    if(cmd.ContainsOption("dest"))
                    {
                        dest = cmd.FindOption("dest")?.Value;
                    }
                    if(cmd.ContainsOption("key"))
                    {
                        key = cmd.FindOption("key")?.Value;
                    }
                    else {
                        key = $"{dest}.key";
                    }
                    if(source == null || key == null || dest == null || !File.Exists(source))
                    {
                        Console.WriteLine("Please provide a source and an algorithm");
                        return false;
                    }
                    return EncryptFile(source, key, dest);
                case "decrypt.asymmetric":
                    if(cmd.ContainsOption("src"))
                    {
                        source = cmd.FindOption("src")?.Value;
                    }
                    if(cmd.ContainsOption("dest"))
                    {
                        dest = cmd.FindOption("dest")?.Value;
                    }
                    if(cmd.ContainsOption("key"))
                    {
                        key = cmd.FindOption("key")?.Value;
                    }
                    else
                    {
                        key = $"{source}.key";
                    }
                    if(source == null || key == null || dest == null || !File.Exists(source) || !File.Exists(key))
                    {
                        Console.WriteLine("Please provide a source and an algorithm");
                        return false;
                    }
                    return DecryptFile(source, key, dest);
            }



            return false;
        }

        private bool DecryptFile(string source, string keyFile, string dest)
        {
            try
            {
                byte[] data = null;
                if(File.Exists(source))
                {
                    data = File.ReadAllBytes(source);
                }
                if(data == null)
                {
                    return false;
                }
                var key = keyFile.FromJsonFile();
                if(key == null || key.Private.IsNullOrWhitespace())
                {
                    return false;
                }
                using RSA rsa = RSA.Create();
                rsa.ImportFromPem(key.Private);
                byte[] decrypted = rsa.Decrypt(data, RSAEncryptionPadding.OaepSHA256);
                File.WriteAllBytes(dest, decrypted);
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;

        }

        private bool EncryptFile(string source, string keyFile, string dest)
        {
            try
            {
                byte[] data = null;
                if(File.Exists(source))
                {
                    data = File.ReadAllBytes(source);
                }
                if(data == null)
                {
                    return false;
                }
                var key = AsymmetricKey.Generate();
                key.ToJsonFile(keyFile);
                using RSA rsa = RSA.Create();
                rsa.ImportFromPem(key.Public);
                byte[] encrypted = rsa.Encrypt(data, RSAEncryptionPadding.OaepSHA256);
                File.WriteAllBytes(dest, encrypted);
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
    }
}
