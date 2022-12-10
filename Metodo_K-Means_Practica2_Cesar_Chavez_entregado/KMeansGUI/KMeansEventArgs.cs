using KMeansProject;
using System.Collections.Generic;
//clase constructora donde declaramos las dos listas con sus geter y seter 
/*
*Cesar Chave Zamorano
*Sistemas Expertos
*Programa desarrollado
*ICO: Ingenieria en computacion
*/
namespace KMeansGUI
{
    public class KMeansEventArgs
    {
        private List<Centro> _centroidList;
        public List<Centro> CentroidList
        {
            get { return _centroidList; }
        }
        
        private double[][] _dataset;
        public double[][] Dataset
        {
            get { return _dataset; }
        }

        public KMeansEventArgs(List<Centro> centroidList,double[][] dataset)
        {
            _centroidList = centroidList;
            _dataset = dataset;
        }
    }
}
