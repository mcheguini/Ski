using System;

namespace Ski
{
    class Program
    {
        static void Main(string[] args)
        {
                        string fileName = "4x4.txt"; // File Name
            string path = System.IO.Directory.GetCurrentDirectory() + "/" + fileName; // Full path to the File Name I think In windows / will change to \
            //Console.WriteLine(path); // Debug purpose
            String input = File.ReadAllText(path); // Read all the Data from the text File save it into a 
            String[] array = input.Split('\n');
            List<int> auxL = new List<int> ();
            int[] dimention = tointarray(array[0], ' ');
            int m = dimention[0], n = dimention[1];
            Console.WriteLine("m: " + m + " n: " + n);
            for (int i =1 ; i<array.GetLength(0); i++){
                //Console.WriteLine(array[i]);
                //string[] row = array[i].Split(' ');
                // List<int> numberList = array[i].SplitToIntList(' ');
                int[] ia = tointarray(array[i], ' ');
                for (int j =0 ; j<ia.Length; j++){
                    Console.Write(ia[j] + "\t");
                }
                Console.WriteLine("");
        }
                private static int[] tointarray(string value, char sep)
{
    string[] sa = value.Split(sep);
    int[] ia = new int[sa.Length];
    for (int i = 0; i < ia.Length; ++i)
    {
        int j;
        string s = sa[i];
        if (int.TryParse(s, out j))
        {
            ia[i] = j;
        }
    }
    return ia;
}
    }
}
