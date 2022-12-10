using KMeansProject; //utilizar para el análisis de datos.
using System;
using System.Collections.Generic; //para utiliza List
using System.ComponentModel; 
//using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Xml.Linq;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;


/*  boton de generar clases
 *  separar clases por punto, esta como general
 * Cesar Chavez Zamorano
 * ICO
 * UAEMEX
 * Practica2Ñ Metodo de K-Means
 */
namespace KMeansGUI
{
    public partial class Form1 : Form
    {
        private KMeans objKMeans;
        public int atractores = 10;
        //private double[][] dataset;
        private List<double[]> Lista_conj_datos;
        private BackgroundWorker objBackgroundWorker;
        KMeansEventArgs kmeansEA;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            //button2.Enabled = false;
            //generar puntos random
            Random objRandom = new Random();
            Lista_conj_datos = new List<double[]>();
            for (int i = 0; i < (int)num_arri_abaj.Value; i++)  //generar 10 muestras
            {
                double[] point = new double[2];
                for (int j = 0; j < 2; j++) //bucle generar centros
                {   
                    point[j] = coloresC_min_max.GenerateRandomDouble(objRandom, 0, 400);//posicion de los centroides generados
                }
                Lista_conj_datos.Add(point);
            }
            //colocar numero de centroides
            button2.Enabled = true;
            objKMeans = new KMeans(atractores, new Distancia());
            picImage.Invalidate();
            objBackgroundWorker = new BackgroundWorker();
            objBackgroundWorker.WorkerReportsProgress = true;
            objBackgroundWorker.DoWork += ObjBackgroundWorker_DoWork;
            objBackgroundWorker.RunWorkerCompleted += ObjBackgroundWorker_RunWorkerCompleted;
            objBackgroundWorker.ProgressChanged += ObjBackgroundWorker_ProgressChanged;
            objBackgroundWorker.RunWorkerAsync(Lista_conj_datos.ToArray());
        }

        private void ObjBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            System.Console.WriteLine("El progreso esta cambiando");
            kmeansEA = e.UserState as KMeansEventArgs;
            if (kmeansEA != null)
            {
                foreach (Centro centroid in kmeansEA.CentroidList)
                {
                    System.Console.WriteLine("Centro: " + centroid.ToString());
                    Console.WriteLine("Numero de atractores: " + atractores);
                    Console.WriteLine("Numero de puntos: " + (int)num_arri_abaj.Value);
                    picImage.Invalidate();
                    Thread.Sleep(100);
                }
            }
        }

        private void ObjBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Centro[] result = e.Result as Centro[];
            Console.WriteLine("Finalisando Proceso");
            button1.Enabled = true;
        }

        private void ObjBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            double[][] inputDataset = e.Argument as double[][];
            objKMeans.UpdateProgress += (x, y) => {
                objBackgroundWorker.ReportProgress(0, y);
            };
            e.Result = objKMeans.Run(inputDataset);
        }

        private void picImage_Paint(object sender, PaintEventArgs e)
        {
            if (kmeansEA == null || kmeansEA.CentroidList == null) return;
            foreach (Centro centroid in kmeansEA.CentroidList)
                centroid.DrawMe(e);
            Thread.Sleep(100);
            if (kmeansEA.Dataset == null) return;
            Graphics g = e.Graphics;
            foreach (double[] point in kmeansEA.Dataset) //enumera los elementos de una colección y ejecuta su cuerpo para cada elemento de la colección
            {
                //generando primeros puntos
                g.DrawEllipse(new Pen(Color.Black, 2.0f), (float)point[0], (float)point[1], 2, 2);
                //Thread.Sleep(1);
            }
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void num_arri_abaj_valorCambiar(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //boton de salir
        private void button3_Click(object sender, EventArgs e)
        {
            //El usuario quiere salir de la aplicación. Cierra todo
            Application.Exit();
            //System.Environment.Exit(1);
        }

        //boton de reinicio
        private void button4_Click(object sender, EventArgs e)
        {
            Application.EnableVisualStyles();
            DialogResult dr = MessageBox.Show("Seguro decea reiniciar...", "Reinicio del tablero", MessageBoxButtons.YesNo);
            switch (dr)
            {
                case DialogResult.No:
                    MessageBox.Show("Ok, Reanudando...");
                break;

                case DialogResult.Yes:
                    Application.Restart();
                break;
            }
        }
        //boton para detener
        private void button5_Click(object sender, EventArgs e)
        {
            Worker w = new Worker();
            Thread t = new Thread(new ThreadStart(w.DoWork));
            w.Stopping = true;
            //Thread.Sleep(100000);
        }
        
        class Worker
        {
            public bool Stopping { get; set; }
            //detener proceso
            public void DoWork()
            {
                while (!Stopping)
                {
                    System.Diagnostics.Debug.WriteLine(DateTime.Now);
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            //string num_arri_abaj = textBox2.Text; 
            //textBox2.Text = 100.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            atractores += 1;
            //button2.Enabled = false;
            //generar puntos random
            Random objRandom = new Random();
           // Lista_conj_datos = new List<double[]>();

            //for (int i = 0; i < (int)num_arri_abaj.Value; i++)  //generar 10 muestras
            //{
            //    double[] point = new double[2];
            //    for (int j = 0; j < 2; j++) //bucle generar centros
            //    {
            //        point[j] = coloresC_min_max.GenerateRandomDouble(objRandom, 0, 400);//posicion de los centroides generados
            //    }
            //    Lista_conj_datos.Add(point);
            //}

            //colocar numero de centroides
            button2.Enabled = true;
            objKMeans = new KMeans(atractores, new Distancia());
            picImage.Invalidate();
            objBackgroundWorker = new BackgroundWorker();
            objBackgroundWorker.WorkerReportsProgress = true;
            objBackgroundWorker.DoWork += ObjBackgroundWorker_DoWork;
            objBackgroundWorker.RunWorkerCompleted += ObjBackgroundWorker_RunWorkerCompleted;
            objBackgroundWorker.ProgressChanged += ObjBackgroundWorker_ProgressChanged;
            objBackgroundWorker.RunWorkerAsync(Lista_conj_datos.ToArray());

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            /*
            double puntos;
            
            puntos = Convert.ToDouble(textBox1.Text);*/
        }

        private void picImage_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
