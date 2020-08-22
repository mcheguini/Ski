using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Ski {
    class Program {
        //Global Vars
        private const int maxElevation = 1500; // limit value
        static int m = 0, n = 0; // matrix dimension
        private static int dS = 0; // Aux var to compute difference in Slope find maximum slope ()
        private static int len = 0, lenAux = 0; // to find maximum length
        private static bool north = false, east = false, south = false, west = false; // boolean to check if we move to any direction
        private static List<int> auxL = new List<int> (); // temp list to hold the search path results
        private static List<int> resultL = new List<int> (); //hold the result of the search (Longest path)
        static void Main (string[] args) {
            int[, ] map = readData (); // Read matrix data form the text file
            //Console.WriteLine ("Original Map:"); // debug
            //mapPrint (map, m, n); // debug
            bool[, ] binMap = logicMapMaker (map, m, n);
            //Console.WriteLine ("Logic Map:");
            mapPrint (binMap, m, n);
            for (int i = 0; i < m; ++i) { // Traverse all elements in the matrix Rows
                for (int j = 0; j < n; ++j) { // Columns
                    findRoute (i, j, map[i, j], binMap, map); // Resursion for each element in the matrix
                }
            }
            int[] result = resultL.ToArray ();
            Console.WriteLine (String.Join (",", result));
            Console.WriteLine ("Max length: " + (resultL.Count+1));
            Console.WriteLine ("Max length 2nd way: " + lenAux);
            Console.WriteLine ("Slope: " + dS);
        }
        private static void findRoute (int i, int j, int ele, bool[, ] logicMap, int[, ] map) {
            len++; // count length
            int c = map[i, j]; // aux current processing elevation
            auxL.Add (c); // save current processing data into the list
            if (i > 0) { // Check where I want to go its good Area NORTH BOUNDRY
                if ((c > map[i - 1, j]) && logicMap[i - 1, j]) { // if the northen element is less and its true
                    findRoute (i - 1, j, ele, logicMap, map);
                    // Console.Write(" N ");
                    north = true;
                } else north = false;
            } else north = false;
            if (j < (n - 1)) { // EAST BOUNDRY
                if ((c > map[i, j + 1]) && logicMap[i, j + 1]) { // Check the condition and logic Map
                    // Console.Write(" E ");
                    findRoute (i, j + 1, ele, logicMap, map);
                    east = true;
                } else east = false;
            } else east = false;
            if (i < (m - 1)) { // SOUTH BOUNDRY
                if ((c > map[i + 1, j]) && logicMap[i + 1, j]) {
                    // Console.Write(" S ");
                    findRoute (i + 1, j, ele, logicMap, map);
                    south = true;
                } else south = false;
            } else south = false;
            if (j > 0) { // WEST BOUNDRY
                if ((c > map[i, j - 1]) && logicMap[i, j - 1]) {
                    // Console.Write(" W ");
                    findRoute (i, j - 1, ele, logicMap, map);
                    west = true;
                } else west = false;
            } else west = false;
            if (auxL.Count > 0) { // If there is element in list list is not empty
                if (resultL.Count < auxL.Count) { // Find the Maximum route Path OJO its workind only if there is no NODE
                    resultL.Clear ();
                    auxL.ForEach ((item) => { // make a copy
                        resultL.Add (item); // this array will carry the element elevation of the longest path
                    });
                }
                int[] array = auxL.ToArray ();
                //Console.WriteLine (String.Join (",", array) + "\t L: " + auxL.Count); // Debug
                //auxL.Clear (); //??????
            }
            if (!north && !east && !south && !west) auxL.Clear (); // No where to go!!!!!!
            // Second method to find Max Length and Slope (its more practical)
            if (len > lenAux) { // First: Maximize the lengt
                lenAux = len; // find Maximum length
                dS = ele - map[i, j]; //find the slope
            }
            if ((ele - c) > dS) { // Second Maximize the slope
                dS = (ele - c); // find Maximum Slope
            }
            len--; // compensate
        }

        // Desc: split string into the array
        // INPUT: string and sep character
        // OUTPUT: int array
        private static int[] stringToIntArray (string str, char sep) {
            string[] aux = str.Split (sep);
            int[] result = new int[aux.Length]; // create an aux Int Array same length
            for (int i = 0; i < result.Length; ++i) {
                int j;
                string s = aux[i];
                if (int.TryParse (s, out j)) result[i] = j;
            }
            return result;
        }
        // Desc: Read raw data from the text file and set the map matrix
        // OUTPUT: Map(Matrix)
        private static int[, ] readData () {
            string fileName = "4x4.txt"; // File Name
            string path = System.IO.Directory.GetCurrentDirectory () + "/" + fileName; // Full path to the File Name I think In windows / will change to \
            //Console.WriteLine(path); // Debug purpose
            String input = File.ReadAllText (path); // Read all the Data from the text File save it into a 
            String[] array = input.Split ('\n'); // Read the whole text into the string 
            //List<int> auxL = new List<int> (); // 
            int[] dimention = stringToIntArray (array[0], ' '); // Read Matrix dimension form the first Row
            m = dimention[0];
            n = dimention[1]; // Store the dimention into the variable 
            int[, ] map = new int[m, n]; // create empty map matrix
            for (int i = 1; i < array.GetLength (0); i++) { // Read raw data pass to the matrix 
                int[] row = stringToIntArray (array[i], ' ');
                for (int j = 0; j < row.Length; j++) {
                    map[i - 1, j] = row[j];
                }
            }
            return map;
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