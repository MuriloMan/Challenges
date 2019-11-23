using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;
using System.Net;

class SolutionTest
{
    public static void Main(string[] args)
    {

        int result = Solution.LargestMatrix.Main(new List<List<int>> {
            new List<int>{ 0,0,0,0,0,0},
            new List<int>{ 0,0,0,0,0,0},
            new List<int>{ 0,0,0,0,0,0},
            new List<int>{ 0,0,0,0,0,0},
            new List<int>{ 1,1,1,1,1,1,1},
            new List<int>{ 1,1,1,1,1,1,1},
            new List<int>{ 1,1,1,1,1,1,1,0,0,0,0},
            new List<int>{ 1,1,1,1,1,1,1,0,0,0,0},
            new List<int>{ 1,1,1,1,1,1,1,0,0,0,0},
            new List<int>{ 1,1,1,1,1,1,1,0,0,0,0},
            new List<int>{ 1,1,1,1,1,1,1,0,0,0,0}
        });
        List<int> a = new List<int> { 1, 2, 3, 6, 4, 15, 1, 4, 14 };
        List<int> b = new List<int> { 3, 3, 1, 2, 12, 1, 15, 3, 13 };

        if (Solution.AngryAnimals2.AngryAnimals(15, a, b) != 60)
        {
            throw new Exception("Errado");
        };
        Solution.OpenClosePrices.Main("1-January-2000", "29-December-2030", "Monday");
    }
}