using Fleximinded.Core.Extensions;
using Fleximinded.Core.Parts.CLI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Syntra.Security.Encrypt
{
    public class SymmetricEncryption : ICliExecutable
    {
        public string Name { get => "Syntra's Great Symmetric encryptor"; }
        public string Description { get => "Let's give it a great encryption, yeah do it symmetric!"; }

        public bool Execute(ICliRuntime owner, ICliCommand cmd)
        {

            string? source = null;
            string? dest = null;
            string? pwd = null;

            switch(cmd.Command)
            {

                case "encrypt.symmetric":
                    if(cmd.ContainsOption("src"))
                    {
                        source = cmd.FindOption("src")?.Value;
                    }
                    if(cmd.ContainsOption("dest"))
                    {
                        dest = cmd.FindOption("dest")?.Value;
                    }
                    if(cmd.ContainsOption("pwd"))
                    {
                        pwd = cmd.FindOption("pwd")?.Value;
                    }
                    if(source == null || pwd?.Length < 5 || dest == null || !File.Exists(source))
                    {
                        Console.WriteLine("Please provide a source and an algorithm");
                        return false;
                    }
                    return EncryptFile(source, pwd, dest);
                case "decrypt.symmetric":
                    if(cmd.ContainsOption("src"))
                    {
                        source = cmd.FindOption("src")?.Value;
                    }
                    if(cmd.ContainsOption("dest"))
                    {
                        dest = cmd.FindOption("dest")?.Value;
                    }
                    if(cmd.ContainsOption("pwd"))
                    {
                        pwd = cmd.FindOption("pwd")?.Value;
                    }
                    if(source == null || pwd?.Length < 5 || dest == null || !File.Exists(source))
                    {
                        Console.WriteLine("Please provide a source and an algorithm");
                        return false;
                    }
                    return DecryptFile(source, pwd, dest);
            }



            return false;
        }
        private bool DecryptFile(string input, string? pwd, string output)
        {
            try
            {
                if(!File.Exists(input) || pwd?.Length < 5 || output.IsNullOrWhitespace())
                {
                    return false;
                }
                FileStream fsInput = File.OpenRead(input);
                byte[] salt = new byte[32];
                fsInput.Read(salt, 0, salt.Length);
                byte[] iv = new byte[16];
                fsInput.Read(iv, 0, iv.Length);
                byte[] key = new Rfc2898DeriveBytes(Encoding.Unicode.GetBytes(pwd), salt, 100904, HashAlgorithmName.SHA256).GetBytes(32);
                Aes aes = Aes.Create();
                aes.Key = key;
                aes.IV = iv;
                aes.Padding = PaddingMode.PKCS7;
                CryptoStream fsCrypto = new(fsInput, aes.CreateDecryptor(), CryptoStreamMode.Read);
                using FileStream fsOutput = File.Create(output);
                fsCrypto.CopyTo(fsOutput);
            } catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
            return File.Exists(output);
        }
        private bool EncryptFile(string source, string? pwd, string dest)
        {
            try
            {
                if(!File.Exists(source))
                {
                    return false;
                }
                using FileStream fsInput = File.OpenRead(source);
                using FileStream fsOutput = File.Create(dest);
                if(pwd.IsNullOrWhitespace() || dest.IsNullOrWhitespace())
                {
                    return false;
                }
                byte[] salt = RandomNumberGenerator.GetBytes(32);
                byte[] key = new Rfc2898DeriveBytes(Encoding.Unicode.GetBytes(pwd), salt, 100904, HashAlgorithmName.SHA256).GetBytes(32);
                byte[] iv = RandomNumberGenerator.GetBytes(16);
                fsOutput.Write(salt, 0, salt.Length);
                fsOutput.Write(iv, 0, iv.Length);
                using SymmetricAlgorithm aes = Aes.Create();
                aes.Key = key;
                aes.IV = iv;
                aes.Padding = PaddingMode.PKCS7;
                using CryptoStream fsCrypto = new(fsOutput, aes.CreateEncryptor(), CryptoStreamMode.Write);
                fsInput.CopyTo(fsCrypto);
            } catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
            return File.Exists(dest);
        }
    }

}
