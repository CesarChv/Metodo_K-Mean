using System;
using System.Collections.Generic;
using System.Drawing;

namespace KMeansProject
{
    public static class coloresC_min_max
    {
        public static Color[] centroidColors;

        static coloresC_min_max()
        {
            //definiar centros
            centroidColors = new Color[11];

            centroidColors[0] = Color.Green;
            centroidColors[1] = Color.Orange;
            centroidColors[2] = Color.Blue;
            centroidColors[3] = Color.Red;
            centroidColors[4] = Color.Black;
            centroidColors[5] = Color.Gray;
            centroidColors[6] = Color.Violet;
            centroidColors[7] = Color.BlueViolet;
            centroidColors[8] = Color.Purple;
            centroidColors[9] = Color.DarkOrange;
            centroidColors[10] = Color.BlueViolet;
        }

        public static List<double[]> Clone(List<double[]> array)
        {//como se clonann
            List<double[]> resultList = new List<double[]>();
            foreach (double[] tempArray in array)
            {
                double[] newArray = new double[tempArray.Length];
                for (int i = 0; i < tempArray.Length; i++)
                    newArray[i] = tempArray[i];
                resultList.Add(newArray);
            }
            return resultList;
        }

        public static List<Tuple<double, double>> GetMinMaxPoints(double[][] dataset)
        {//definicion de los puntos maximos y minimos del nuestor dataset
            List<Tuple<double, double>> result = new List<Tuple<double, double>>();

            for (int j = 0; j < dataset[0].GetLength(0); j++)
            {
                double min = Double.MaxValue;
                double max = Double.MinValue;
                for (int i = 0; i < dataset.Length; i++)
                {
                    double elemento = dataset[i][j];
                    if (elemento < min)
                        min = elemento;
                    if (elemento > max)
                        max = elemento;
                }
                result.Add(new Tuple<double, double>(min, max));
            }
            return result;
        }
        //genera un doble random para los 
        public static double GenerateRandomDouble(Random random, double minimum, double maximum)
        {
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}
