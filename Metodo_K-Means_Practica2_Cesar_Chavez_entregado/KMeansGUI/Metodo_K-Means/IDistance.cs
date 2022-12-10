namespace KMeansProject
{/*
*Cesar Chave Zamorano
*Sistemas Expertos
*Programa desarrollado
*ICO: Ingenieria en computacion
*/
    //como accede a los metodos Run mediante una interfaz que puede ser variable desde otros aplicativos (el codigo idDistancia)
    public interface IDistance
    {
        double Run(double[] array1, double[] array2);
    }
}
