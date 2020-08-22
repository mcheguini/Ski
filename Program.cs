using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Ski {
    class Program {
        //Global Vars
        private const int maxElevation = 1500; // limit value
        // private const int m = 4,
        //     n = 4;
        // static int[, ] map = new int[m, n] { { 4, 8, 7, 3 }, { 2, 5, 9, 3 }, { 6, 3, 2, 5 }, { 4, 4, 1, 6 }
        // };
        private static int dS = 0; // Aux var to compute difference in Slope find maximum slope ()
        private static int len = 0, lenAux = 0; // to find maximum length
        private static bool north = false, east = false, south = false, west = false; // boolean to check if we move to any direction
        private static List<int> auxL = new List<int> (); // temp list to hold the search path results
        private static List<int> resultL = new List<int> (); //hold the result of the search (Longest path)
        static void Main (string[] args) {
            string fileName = "4x4.txt"; // File Name
            string path = System.IO.Directory.GetCurrentDirectory () + "/" + fileName; // Full path to the File Name I think In windows / will change to \
            //Console.WriteLine(path); // Debug purpose
            String input = File.ReadAllText (path); // Read all the Data from the text File save it into a 
            String[] array = input.Split ('\n'); // Read the whole text into the string 
            //List<int> auxL = new List<int> (); // 
            int[] dimention = stringToIntArray (array[0], ' '); // Read Matrix dimension form the first Row
            int m = dimention[0], n = dimention[1]; // Store the dimention into the variable 
int[,] map = new int[m,n]; // create empty map matrix


            for (int i = 1; i < array.GetLength (0); i++) { // Read raw data pass to the matrix 
                int[] row = stringToIntArray (array[i], ' ');
                for (int j = 0; j < row.Length; j++) {
                    map[i-1,j] = row[j];
                }
            }
            Console.WriteLine("")
            mapPrint(map,m,n); // debug
        }
        private static int[] stringToIntArray (string str, char sep) {
            string[] aux = str.Split(sep);
            int[] result = new int[aux.Length]; // create an aux Int Array same length
            for (int i = 0; i < result.Length; ++i) {
                int j;
                string s = aux[i];
                if (int.TryParse (s, out j)) result[i] = j;
            }
            return result;
        }

        // Desc: Construct the T/F based on the original matrix and range [0-1500]
        // INPUT: Original Matrix
        // Output: LogicMap 
        private static bool[, ] logicMapMaker (int[, ] map, int m, int n) {
            bool[, ] goodArea = new bool[m, n];
            for (int i = 0; i < m; i++) {
                for (int j = 0; j < n; j++) {
                    if ((map[i, j] >= 0) && (map[i, j] <= maxElevation)) goodArea[i, j] = true;
                    else goodArea[i, j] = false;
                }
            }
            return goodArea;
        }

        // Desc: Print a Matrix (Map)
        // INPUT: Maxtix, Row, Column
        private static void mapPrint (int[, ] map, int m, int n) {
            for (int i = 0; i < m; i++) {
                for (int j = 0; j < n; j++) {
                    Console.Write (map[i, j] + "\t");
                }
                Console.WriteLine ("");
            }
        }

        // Desc: Print a Matrix (Map) Polymorph for Bool
        // INPUT: Maxtix, Row, Column
        private static void mapPrint (bool[, ] map, int m, int n) {
            for (int i = 0; i < m; i++) {
                for (int j = 0; j < n; j++) {
                    Console.Write (map[i, j] + "\t");
                }
                Console.WriteLine ("");
            }
        }
    }
}