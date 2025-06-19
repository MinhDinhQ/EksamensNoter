using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    // En ren funktion – samme input giver altid samme output
    static int Square(int x) => x * x;

    // En højereordens funktion – tager en funktion som parameter
    static List<int> Map(List<int> list, Func<int, int> func)
    {
        return list.Select(func).ToList();
    }

    static void Main()
    {
        var numbers = new List<int> { 1, 2, 3, 4, 5 };

        // Brug af højereordensfunktion (Map) og ren funktion (Square)
        var squared = Map(numbers, Square);

        Console.WriteLine("Originale tal: " + string.Join(", ", numbers));
        Console.WriteLine("Kvadrater: " + string.Join(", ", squared));

        // Brug af lambda-funktion direkte
        var evens = numbers.Where(n => n % 2 == 0).ToList();
        Console.WriteLine("Lige tal: " + string.Join(", ", evens));

        // Eksempel på funktion som værdi
        Func<int, int> triple = x => x * 3;
        var tripled = numbers.Select(triple).ToList();
        Console.WriteLine("Gange 3: " + string.Join(", ", tripled));
    }
}