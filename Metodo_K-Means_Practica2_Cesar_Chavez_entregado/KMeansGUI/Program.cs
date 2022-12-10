using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
//metodo main 
/*
*Cesar Chave Zamorano
*Sistemas Expertos
*Programa desarrollado
*ICO: Ingenieria en computacion
*/
namespace KMeansGUI
{
    static class Program
    {
        //El principal punto de entrada para la aplicación.
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
