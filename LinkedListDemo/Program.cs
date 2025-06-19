using System;

// Denne klasse repræsenterer en enkelt node i en kædeliste
class Node
{
    public int Value;    // Værdien der gemmes i noden
    public Node Next;    // Reference til næste node i kæden

    public Node(int value)
    {
        Value = value;
        Next = null;     // Når noden oprettes, peger den ikke på nogen
    }
}

// Denne klasse repræsenterer selve kædelisten (linked list)
class LinkedList
{
    private Node head;   // Reference til første node i listen

    // Tilføjer en node i slutningen af listen
    public void AddLast(int value)
    {
        Node newNode = new Node(value); // Opret ny node

        if (head == null) // Hvis listen er tom
        {
            head = newNode; // Gør den nye node til første node
            return;
        }

        // Gå igennem listen til vi når den sidste node
        Node current = head;
        while (current.Next != null)
        {
            current = current.Next;
        }

        current.Next = newNode; // Tilføj ny node til slutningen
    }

    // Udskriver hele listen til konsollen
    public void PrintList()
    {
        Node current = head; // Start fra første node

        while (current != null)
        {
            Console.Write($"{current.Value} -> "); // Udskriv node
            current = current.Next; // Gå videre til næste node
        }

        Console.WriteLine("null"); // Slutningen af listen
    }

    // Tjekker om en bestemt værdi findes i listen
    public bool Contains(int value)
    {
        Node current = head;

        while (current != null)
        {
            if (current.Value == value) // Hvis værdien findes
                return true;

            current = current.Next; // Fortsæt til næste node
        }

        return false; // Hvis vi når slutningen uden fund
    }

    // Fjerner den første node med den angivne værdi
    public void Remove(int value)
    {
        if (head == null) return; // Hvis listen er tom

        if (head.Value == value)
        {
            head = head.Next; // Fjern første node ved at hoppe videre
            return;
        }

        Node current = head;

        // Find noden før den vi vil fjerne
        while (current.Next != null && current.Next.Value != value)
        {
            current = current.Next;
        }

        // Hvis vi fandt en matchende node
        if (current.Next != null)
        {
            current.Next = current.Next.Next; // "spring over" noden
        }
    }
}

// Programklasse med Main-metode – vores startpunkt
class Program
{
    static void Main(string[] args)
    {
        // Opretter en ny kædeliste
        LinkedList list = new LinkedList();

        Console.WriteLine("🔹 Tilføjer noder til kæden:");
        list.AddLast(10);
        list.AddLast(20);
        list.AddLast(30);

        // Udskriver listen: 10 -> 20 -> 30 -> null
        list.PrintList();

        Console.WriteLine("\n🔎 Søger efter værdien 20:");
        Console.WriteLine(list.Contains(20) ? "✅ Fundet!" : "❌ Ikke fundet");

        Console.WriteLine("\n🗑️ Fjerner værdi 20 fra listen:");
        list.Remove(20);

        // Udskriver listen igen: 10 -> 30 -> null
        list.PrintList();
    }
}
