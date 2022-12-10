using KMeansGUI;
using System;
using System.Collections.Generic;
using System.Threading;
//
namespace KMeansProject
{
    public delegate void OnUpdateProgress(object sender, KMeansEventArgs eventArgs);
    public class KMeans
    {
        /*
*Cesar Chave Zamorano
*Sistemas Expertos
*Programa desarrollado
*ICO: Ingenieria en computacion
*/
        private IDistance _distance;
        private int _k;

        public event OnUpdateProgress UpdateProgress;
        protected virtual void OnUpdateProgress(KMeansEventArgs eventArgs)
        {
            //actualizacion los datos hasta que el evento del centro se cumpla  el programa revisa si ya esta en el centro y si no se genera un evento del tipo kmeansEvenArg
            //cuando ya no encuentro nuevos eventos ya no actualiza
            if (UpdateProgress != null)
                UpdateProgress(this, eventArgs);
            Thread.Sleep(1500);
        }

        public KMeans(int k, IDistance distance)
        {//constructor de kmeans donde se inicializan las variables desde k hasta la interfaz distancia
            _k = k;
            _distance = distance;
        }

        public Centro[] Run(double[][] dataSet)
        {//metodo que se va  acorrer donde llegan los datasets que se genera anteriormente y aqui es el llenado
            List<Centro> centroidList = new List<Centro>();
            for (int i=0;i<_k;i++)
            {
                Centro centroid = new Centro(dataSet,coloresC_min_max.centroidColors[i]);
                centroidList.Add(centroid);
            }
            //mandamos un evento de actualizacion
            OnUpdateProgress(new KMeansEventArgs(centroidList,dataSet));
            while (true)//bucle infinito hasta que se estan llenado las listas d epuntos
            {
                foreach (Centro centroid in centroidList)
                    centroid.Reiniciar();

                for (int i = 0; i < dataSet.GetLength(0); i++)
                {
                    double[] point = dataSet[i];
                    int closestIndex = -1;
                    double minDistance = Double.MaxValue;
                    for (int k = 0; k < centroidList.Count; k++)
                    {
                        double distance = _distance.Run(centroidList[k].Array, point);
                        if (distance < minDistance)
                        {
                            closestIndex = k;
                            minDistance = distance;
                        }
                    }
                    centroidList[closestIndex].addPoint(point);
                }
             
                foreach (Centro centroid in centroidList)
                    centroid.MoveCentroid();
                //mandamos nuevamente a actualizar 
                OnUpdateProgress(new KMeansEventArgs(centroidList, null));
                //Revisamos si existen cambios y finalizamos el bucle cuando no existan ams cambios
                bool hasChanged = false;
                foreach (Centro centroid in centroidList)
                    if (centroid.movimiento_canbios())
                    {
                        hasChanged = true;
                        break;
                    }
                if (!hasChanged)
                    break;
            }
            return centroidList.ToArray();
        }
    }
}
