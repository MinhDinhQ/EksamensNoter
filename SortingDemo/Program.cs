using System;

class Program
{
    static void Main()
    {
        // 🔢 Opret et array med nogle tilfældige tal
        int[] data = { 5, 3, 8, 4, 2 };

        Console.WriteLine("🎲 Originalt array:");
        PrintArray(data);

        // 💥 BUBBLE SORT – langsom men enkel
        Console.WriteLine("\n🫧 Bubble Sort:");
        int[] bubble = (int[])data.Clone(); // Clone, så vi ikke ændrer originalen
        BubbleSort(bubble);
        PrintArray(bubble);

        // 🧩 INSERTION SORT – god til næsten-sorterede arrays
        Console.WriteLine("\n🧩 Insertion Sort:");
        int[] insertion = (int[])data.Clone();
        InsertionSort(insertion);
        PrintArray(insertion);

        // 🎯 SELECTION SORT – finder det mindste og flytter det frem
        Console.WriteLine("\n🎯 Selection Sort:");
        int[] selection = (int[])data.Clone();
        SelectionSort(selection);
        PrintArray(selection);

        // 🔀 MERGE SORT – hurtig, rekursiv og meget brugt
        Console.WriteLine("\n🔀 Merge Sort:");
        int[] merge = (int[])data.Clone();
        merge = MergeSort(merge);
        PrintArray(merge);

        // ⚡ QUICK SORT – meget effektiv, bruges i praksis
        Console.WriteLine("\n⚡ Quick Sort:");
        int[] quick = (int[])data.Clone();
        QuickSort(quick, 0, quick.Length - 1);
        PrintArray(quick);

        // 🧠 INDGEBYGGET SORTERING – .NETs egen optimerede metode
        Console.WriteLine("\n🧠 Array.Sort():");
        int[] builtin = (int[])data.Clone();
        Array.Sort(builtin);
        PrintArray(builtin);
    }

    // 📤 Udskriver indholdet af et array som tekst
    static void PrintArray(int[] arr)
    {
        Console.WriteLine(string.Join(", ", arr));
    }

    // 🫧 BUBBLE SORT – bytter naboer hvis de står forkert
    static void BubbleSort(int[] arr)
    {
        for (int i = 0; i < arr.Length - 1; i++)
        {
            // Gennemgår arrayet igen og igen
            for (int j = 0; j < arr.Length - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    // Bytter hvis forkert rækkefølge
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }
    }

    // 🧩 INSERTION SORT – indsætter elementer ét ad gangen på rigtig plads
    static void InsertionSort(int[] arr)
    {
        for (int i = 1; i < arr.Length; i++)
        {
            int key = arr[i];  // Den værdi der skal "indsættes"
            int j = i - 1;

            // Flytter større tal til højre
            while (j >= 0 && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                j--;
            }

            // Sætter key ind på rigtig plads
            arr[j + 1] = key;
        }
    }

    // 🎯 SELECTION SORT – finder det mindste element og flytter det forrest
    static void SelectionSort(int[] arr)
    {
        for (int i = 0; i < arr.Length - 1; i++)
        {
            int minIndex = i;

            // Find det mindste tal i resten af arrayet
            for (int j = i + 1; j < arr.Length; j++)
            {
                if (arr[j] < arr[minIndex])
                {
                    minIndex = j;
                }
            }

            // Bytter det mindste med første element i dette loop
            int temp = arr[i];
            arr[i] = arr[minIndex];
            arr[minIndex] = temp;
        }
    }

    // 🔀 MERGE SORT – opdeler, sorterer, samler igen
    static int[] MergeSort(int[] arr)
    {
        if (arr.Length <= 1)
            return arr; // Enkelt element er allerede sorteret

        int mid = arr.Length / 2;

        // 🪓 Deler arrayet i to halvdele
        int[] left = MergeSort(arr[..mid]);     // venstre halvdel
        int[] right = MergeSort(arr[mid..]);    // højre halvdel

        // 🔗 Fletter de to sorterede halvdele
        return Merge(left, right);
    }

    // Sammenfletter to sortede arrays til ét
    static int[] Merge(int[] left, int[] right)
    {
        int[] result = new int[left.Length + right.Length];
        int i = 0, l = 0, r = 0;

        // Går igennem begge arrays og fletter dem i rækkefølge
        while (l < left.Length && r < right.Length)
        {
            if (left[l] < right[r])
                result[i++] = left[l++];
            else
                result[i++] = right[r++];
        }

        // Tilføjer resterende elementer hvis nogle er tilbage
        while (l < left.Length)
            result[i++] = left[l++];

        while (r < right.Length)
            result[i++] = right[r++];

        return result;
    }

    // ⚡ QUICK SORT – vælger en "pivot", opdeler og sorterer rekursivt
    static void QuickSort(int[] arr, int low, int high)
    {
        if (low < high)
        {
            int pivotIndex = Partition(arr, low, high);  // Find pivot og del arrayet
            QuickSort(arr, low, pivotIndex - 1);         // Sorter venstre del
            QuickSort(arr, pivotIndex + 1, high);        // Sorter højre del
        }
    }

    // 📌 Partition: flytter alt mindre end pivot til venstre
    static int Partition(int[] arr, int low, int high)
    {
        int pivot = arr[high]; // Brug sidste element som pivot
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (arr[j] < pivot)
            {
                i++;
                (arr[i], arr[j]) = (arr[j], arr[i]); // Swap
            }
        }

        (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]); // Placer pivot korrekt
        return i + 1;
    }
}
