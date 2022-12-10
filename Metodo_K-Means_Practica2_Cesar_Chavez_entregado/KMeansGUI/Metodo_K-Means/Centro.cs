using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
/*
*Cesar Chave Zamorano
*Sistemas Expertos
*Programa desarrollado
*ICO: Ingenieria en computacion
*/
namespace KMeansProject
{
    public class Centro
    {
        private double[] _array;
        public double[] Array
        {
            get { return _array; }
        }

        private Color collor;

        public void DrawMe(PaintEventArgs e)
        {
            //crear lapiz
            //Pen blackPen = new Pen(Color.Black, 3);
            Graphics g = e.Graphics;
            //dibujo loa centroides
            g.FillEllipse(new SolidBrush(collor), (float)_array[0], (float)_array[1], 10, 10);
            foreach (double[] point in list_puntos_cercanos)
            {
                //e.Graphics.DrawLine(new Pen(collor, 2.0f), (float)point[0], (float)point[1], 10, 10);
                g.DrawEllipse(new Pen(collor, 2.0f), (float)point[0], (float)point[1], 1, 1);
            }
        }

        private List<double[]> list_puntos_antigua;
        private List<double[]> list_puntos_cercanos;
        public void addPoint(double[] closestArray)
        {
            list_puntos_cercanos.Add(closestArray);
        }

        private static Random random = new Random();

        public Centro(double[][] dataSet, Color color)
        {
            collor = color;
            List<Tuple<double, double>> minMaxPoints = coloresC_min_max.GetMinMaxPoints(dataSet);
            _array = new double[minMaxPoints.Count];
            int i = 0;
            foreach (Tuple<double, double> tuple in minMaxPoints)
            {
                double num_min = tuple.Item1;
                double num_max = tuple.Item2;
                double elemto = random.NextDouble() * (num_max - num_min) + num_min;
                _array[i] = elemto;
                i++;
            }
            list_puntos_antigua = new List<double[]>();
            list_puntos_cercanos = new List<double[]>();
        }

        public void MoveCentroid()
        {
            List<double> resultVector = new List<double>();
            if (list_puntos_cercanos.Count == 0) return;
            for (int j = 0; j < list_puntos_cercanos[0].GetLength(0); j++)
            {
                double sum = 0.0;
                for (int i = 0; i < list_puntos_cercanos.Count; i++)
                {
                    sum += list_puntos_cercanos[i][j];
                }
                sum /= list_puntos_cercanos.Count;
                resultVector.Add(sum);
            }
            _array = resultVector.ToArray();
        }

        public bool movimiento_canbios()
        {
            bool result = true;
            if (list_puntos_antigua.Count != list_puntos_cercanos.Count) return true;
            if (list_puntos_antigua.Count == 0 || list_puntos_cercanos.Count == 0) return false;
            for (int i = 0; i < list_puntos_cercanos.Count; i++)
            {
                double[] punto_antiguo = list_puntos_antigua[i];
                double[] act_punto = list_puntos_cercanos[i];
                for (int j = 0; j < punto_antiguo.Length; j++)
                    if (punto_antiguo[j] != act_punto[j])
                    {
                        result = false;
                        break;
                    }
            }
            return !result;
        }

        public void Reiniciar()
        {
            list_puntos_antigua = coloresC_min_max.Clone(list_puntos_cercanos);
            //si se mueve el centroide genera el color del los puntos
            list_puntos_cercanos.Clear();
        }

        public override string ToString()
        {
            return String.Join(",", _array);
        }
    }
}
