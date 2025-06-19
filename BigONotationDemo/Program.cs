using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Console.WriteLine("Big O Notation Eksempler:\n");

        // O(1)
        Console.WriteLine("O(1) - Konstant tid:");
        int[] numbers = { 1, 2, 3, 4, 5 };
        Console.WriteLine(GetFirst(numbers));

        // O(log n)
        Console.WriteLine("\nO(log n) - Binary Search:");
        Console.WriteLine(BinarySearch(numbers, 4));

        // O(n)
        Console.WriteLine("\nO(n) - Lineær søgning:");
        Console.WriteLine(Contains(numbers, 3));

        // O(n²)
        Console.WriteLine("\nO(n²) - Print alle par:");
        PrintAllPairs(numbers);

        // O(2^n)
        Console.WriteLine("\nO(2^n) - Fibonacci:");
        Console.WriteLine(Fibonacci(5));

        // O(n!) - Permutationer
        Console.WriteLine("\nO(n!) - Permutationer:");
        Permute(new List<int> { 1, 2, 3 });
    }

    // O(1)
    static int GetFirst(int[] arr)
    {
        return arr[0];
    }

    // O(log n)
    static int BinarySearch(int[] arr, int target)
    {
        int left = 0, right = arr.Length - 1;
        while (left <= right)
        {
            int mid = (left + right) / 2;
            if (arr[mid] == target)
                return mid;
            if (arr[mid] < target)
                left = mid + 1;
            else
                right = mid - 1;
        }
        return -1;
    }

    // O(n)
    static bool Contains(int[] arr, int target)
    {
        foreach (int number in arr)
        {
            if (number == target)
                return true;
        }
        return false;
    }

    // O(n²)
    static void PrintAllPairs(int[] arr)
    {
        foreach (var a in arr)
        {
            foreach (var b in arr)
            {
                Console.WriteLine($"{a}, {b}");
            }
        }
    }

    // O(2^n)
    static int Fibonacci(int n)
    {
        if (n <= 1) return n;
        return Fibonacci(n - 1) + Fibonacci(n - 2);
    }

    // O(n!)
    static void Permute(List<int> nums, int start = 0)
    {
        if (start == nums.Count)
        {
            Console.WriteLine(string.Join(",", nums));
            return;
        }

        for (int i = start; i < nums.Count; i++)
        {
            (nums[start], nums[i]) = (nums[i], nums[start]); // swap
            Permute(nums, start + 1);
            (nums[start], nums[i]) = (nums[i], nums[start]); // swap tilbage
        }
    }
}
