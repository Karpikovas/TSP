using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP
{
    class Program
    {
        /*private static int[,] prices = new int[6, 6] {   { -2,+4,+4,+5,+4,+3},  //1
                                                         { +2,-2,+7,+1,+1,+6},  //2
                                                         { +2,+3,-2,+9,+4,+5},  //3
                                                         { +1,+3,+2,-2,+3,+1},  //4
                                                         { +7,+4,+1,+1,-2,+4},  //5
                                                         { +2,+3,+4,+7,+9,-2} };//6

                                                         */
        private static int[,] prices = new int[4, 4] {   { -2, +5, +11, +9},  //1
                                                         { 10,-2,+8,+7},  //2
                                                         { +7,+14,-2,+8},  //3
                                                         { +12,+6,+15,-2},  //4
                                                         };//6
        static void Main(string[] args)
        {
            Salesman salesman = new Salesman(prices);
            Console.WriteLine(salesman.GetWay());

            Console.ReadKey();
        }
    }
}
