using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP
{
    class Salesman
    {
        private int[,] matrix;
        //= new int [7, 7];
        private int[] coords = new int[3];
        private List<int[]> points = new List<int[]>();
        private int step = 0;
        public Salesman(int[,] matrix)
        {
            int MatrixSize = matrix.GetLength(0) + 1;
            this.matrix = new int[MatrixSize, MatrixSize];
            for (int i = 0; i < this.matrix.GetLength(0); i++)
            {
                this.matrix[0, i] = i;
                this.matrix[i, 0] = i;
            }
            for (int i = 1; i < this.matrix.GetLength(0); i++)
            {
                for (int j = 1; j < this.matrix.GetLength(0); j++)
                {
                    this.matrix[i, j] = matrix[i - 1, j - 1];
                }
            }
        }
        public int[,] Matrix
        {
            get { return matrix; }
            set { this.matrix = value; }
        }
        private int[] GetMinInRow()
        {
            int[] result = new int[matrix.GetLength(0)];
            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                int min = Int32.MaxValue;
                for (int j = 1; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] < min && matrix[i, j] != -2) min = matrix[i, j];
                }
                result[i] = min;
            }
            return result;
        }
        private int[] GetMinInCol()
        {
            int[] result = new int[matrix.GetLength(0)];
            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                int min = Int32.MaxValue;
                for (int j = 1; j < matrix.GetLength(1); j++)
                {
                    if (matrix[j, i] < min && matrix[j, i] != -2) min = matrix[j, i];
                }
                result[i] = min;
            }
            return result;
        }
        private void DecInRow(int[] rowConsts)
        {
            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                for (int j = 1; j < matrix.GetLength(0); j++)
                {
                    if (matrix[i, j] != -2) matrix[i, j] -= rowConsts[i];
                }
            }
        }
        private void DecInCol(int[] colConsts)
        {
            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                for (int j = 1; j < matrix.GetLength(0); j++)
                {
                    if (matrix[j, i] != -2) matrix[j, i] -= colConsts[i];
                }
            }
        }
        private int[] GetCurrentCoords()
        {
            int[] result = new int[3]; //0-i, 1-j, 2-Coef. sum
            int max = Int32.MinValue;
            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                for (int j = 1; j < matrix.GetLength(0); j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        matrix[i, j] = -2;
                        int[] thisRowConsts = GetMinInRow();
                        int[] thisColConsts = GetMinInCol();
                        if (thisRowConsts[i] + thisColConsts[j] > max)
                        {
                            max = thisRowConsts[i] + thisColConsts[j];
                            result[0] = matrix[i, 0];
                            result[1] = matrix[0, j];
                            result[2] = thisRowConsts.Sum() + thisColConsts.Sum();
                        }
                        matrix[i, j] = 0;
                    }
                }
            }
            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                for (int j = 1; j < matrix.GetLength(0); j++)
                {
                    if (matrix[i, 0] == result[0] && matrix[0, j] == result[1])
                    {
                        matrix[i, j] = -2;
                    }
                    if (matrix[i, 0] == result[1] && matrix[0, j] == result[0])
                    {
                        matrix[i, j] = -2;
                    }

                }
            }
            points.Add(result);
            step++;
            return result;
        }
        private void DeleteByCoords(int[] coords)
        {
            int[,] result = new int[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1];
            for (int i = 0; i < result.GetLength(0); i++)
            {
                if (matrix[i, 0] < coords[0])
                    for (int j = 0; j < result.GetLength(0); j++)
                    {
                        if (matrix[0, j] < coords[1])
                            result[i, j] = matrix[i, j];
                        if (matrix[0, j] >= coords[1])
                            result[i, j] = matrix[i, j + 1];
                    }
                if (matrix[i, 0] >= coords[0])
                    for (int j = 0; j < result.GetLength(0); j++)
                    {
                        if (matrix[0, j] < coords[1])
                            result[i, j] = matrix[i + 1, j];
                        if (matrix[0, j] >= coords[1])
                            result[i, j] = matrix[i + 1, j + 1];
                    }
            }
            matrix = result;
        }
        private void Reduction()
        {
            while (step <= 5)
            {
                DecInRow(GetMinInRow());
                DecInCol(GetMinInCol());
                DeleteByCoords(GetCurrentCoords());
            }
        }
        public string GetWay()
        {
            Reduction();
            string result =  points[0][0] + " --> " + points[0][1];
            int[] currentPoint = points[0];
            for (int i = 1; i < points.Count; i++)
            {
                int[] nextPoint = GetNextPoint(currentPoint);
                result += " --> " + nextPoint[1];
                currentPoint = nextPoint;
            }
            result += "\n";
            return result;
        }
        private int[] GetNextPoint(int[] currentPoint)
        {
            foreach (int[] point in points)
            {
                if (currentPoint[1] == point[0]) return point;
            }
            return null;
        }
    }
}
