using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kommivoyajer.Helpers
{
    class AdjacencyMatrix
    {
        //расстояния между городами
        static double[,] tempMatrix;
        //индексы городов формируют искомый путь
        public int[] Path;

        public static double[,] PointToMatrix(List<Point> Cities) //из точек в матрицу
        {
            double[,] matrix = new double[,] {};
            try
            {
                if (Cities.Count < 4)
                {
                    throw new ArgumentException("Number of cities shouldn't be less than 4");
                }

                int size = Cities.Count;
                matrix = new double[size, size];
                for (int i = 0; i <= Cities.Count - 1; i++)
                {
                    for (int j = 0; j <= Cities.Count - 1; j++)
                    {
                        if (i == j)
                            matrix[i, j] = 0;
                        else
                        {
                            double distance = Math.Sqrt((Cities[j].X - Cities[i].X)*(Cities[j].X - Cities[i].X) +
                                                        (Cities[j].Y - Cities[i].Y)*(Cities[j].Y - Cities[i].Y));
                            matrix[i, j] = distance;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            tempMatrix = matrix;
            return matrix;
        }

        public static IEnumerable<int> GetVertices(double[,] mtrx)
        {
            List<int> vertList = new List<int>();
            IEnumerable<int> verticesEnum;

            for (int i = 0; i < mtrx.GetLongLength(1); i++)
            {
                vertList.Add(i);
            }
            return verticesEnum = vertList.AsEnumerable();
        }
    }
}
