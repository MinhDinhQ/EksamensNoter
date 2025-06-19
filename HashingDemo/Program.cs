using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main()
    {
        // 📦 Eksempel på brug af Dictionary (hash-table)
        Dictionary<string, string> users = new Dictionary<string, string>();

        // 🔐 Gemmer brugere med deres hashed password
        users["Alice"] = HashPassword("mypassword123");
        users["Bob"] = HashPassword("bobspassword");

        Console.WriteLine("🔍 Indtast brugernavn:");
        string username = Console.ReadLine();

        Console.WriteLine("🔑 Indtast adgangskode:");
        string password = Console.ReadLine();

        // 🧪 Tjekker om brugeren findes
        if (users.ContainsKey(username))
        {
            string storedHash = users[username];
            string inputHash = HashPassword(password);

            // ✅ Sammenlign hashet input med hashet kode
            if (storedHash == inputHash)
                Console.WriteLine("✅ Adgangskode korrekt!");
            else
                Console.WriteLine("❌ Forkert adgangskode.");
        }
        else
        {
            Console.WriteLine("❌ Bruger findes ikke.");
        }

        // 🧬 Vis hvordan et hash faktisk ser ud
        Console.WriteLine("\nEksempel på SHA256 hash:");
        Console.WriteLine(HashPassword("Eksempel123"));
    }

    // 🔄 Funktion til at lave SHA256 hash af en adgangskode
    static string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            // Konverterer password til bytes
            byte[] bytes = Encoding.UTF8.GetBytes(password);

            // Generer hash
            byte[] hash = sha256.ComputeHash(bytes);

            // Konverterer hash til en hex-string (læsbar form)
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hash)
            {
                sb.Append(b.ToString("x2")); // x2 = to-cifret hex
            }

            return sb.ToString();
        }
    }
}
