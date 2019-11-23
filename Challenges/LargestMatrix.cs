using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution
{
    public class LargestMatrix
    {

        /*
         * Complete the 'largestMatrix' function below.
         *
         * The function is expected to return an INTEGER.
         * The function accepts 2D_INTEGER_ARRAY arr as parameter.
         */

        private static bool CheckDimension(List<List<int>> arr, int x, int y, int dimension)
        {
            bool r = true;
            var initX = x;
            var initY = y;
            bool bkf = false;

            if ((arr.Count > (x + dimension)) && ((y + dimension) < arr[x].Count))
            {
                for (var xx = x; xx <= (initX + dimension) && !bkf; xx++)
                {
                    for (var yy = y; yy <= (initY + dimension) && !bkf; yy++)
                    {
                        if (arr[xx][yy] != 1)
                        {
                            r = false;
                            bkf = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                r = false;
            }
            return r;
        }
        public static int Main(List<List<int>> arr)
        {
            int maxSizeDim = 0;
            for (var x = 0; x < arr.Count; x++)
            {
                for (var y = 0; y < arr[x].Count; y++)
                {
                    if (arr[x][y] == 1)
                    {
                        int dim = 1;
                        var cnt = true;
                        do
                        {
                            cnt = CheckDimension(arr, x, y, dim);
                            dim = cnt ? dim + 1 : dim;
                        } while (cnt);
                        maxSizeDim = dim > maxSizeDim ? dim : maxSizeDim;
                    }
                }
            }
            return maxSizeDim;
        }
    }

}
