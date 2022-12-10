
using System;

namespace KMeansProject
{
    public class Distancia : IDistance
    {//codigo de run llena los areglos y regresa la raiz de la distancia
        public double Run(double[] array1, double[] array2)
        {
            double resultado = 0;
            for (int i = 0; i < array1.Length; i++)
            {
                resultado += Math.Pow(array1[i] - array2[i], 2);
            }
            return Math.Sqrt(resultado);
        }
    }
}
