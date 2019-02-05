using System;
using System.IO;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace GenerarFitxers
{
    public partial class FrmXifrasio : Form
    {
        public FrmXifrasio()
        {
            InitializeComponent();
        }

        public String[] lletres = new String[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        public String[] crearLletres(int numeroFitxer)  
        {
            String rutaFi = "C:\\Users\\admin\\Desktop\\Ficheros\\lletres"+numeroFitxer+".txt";
            Random obj = new Random();
            String[] lletresRandom = new String[1000000];
            
            String codi = "";
            StreamWriter file = new StreamWriter(rutaFi, true);
                for (int x = 0; x < lletresRandom.Length; x++)
                {
                    int randomNumero = obj.Next(0,25);
                    codi = lletres[randomNumero];
                    file.Write(codi);
                    lletresRandom[x] = codi;
                }
                file.Close();
            return lletresRandom;
        }
        private void crearFitxers() {
            String[] lletresRandom = new String[16000000];
            for (int i = 1; i< 5; i++) {
               lletresRandom = crearLletres(i);
                XifrarLLetraNum(lletresRandom, i);
            }
        }
        public string[] CodiLletra()
        {
            SqlConnection connexxion = new SqlConnection();
            connexxion.ConnectionString = ConfigurationManager.ConnectionStrings["RepublicSystemConnectionString"].ConnectionString;
            connexxion.Open();

            DataSet dtsCli = new DataSet();

            string query = "SELECT Numbers from InnerEncryptionData where IdInnerEncryption = 24"; // and b.Day = '" + Convert.ToDateTime(DateTime.Today).ToString("yyyy-MM-dd") + "'";
           
            SqlDataAdapter adapter = new SqlDataAdapter(query, connexxion);
            adapter.Fill(dtsCli);

            string[] LletraCodi = new string[26];

            for (int i = 0; i < LletraCodi.Length; i++)
            {
                
                LletraCodi[i] = dtsCli.Tables[0].Rows[i][0].ToString();
                
            }

            return LletraCodi;
        }

        private void XifrarLLetraNum(string [] crearLletres, int z)
        {
            string[] codiLletra = new string[26];

            bool verifica;
            string rutaFitxer = "C:\\Users\\admin\\Desktop\\Ficheros\\Arxiu"+z+".txt";
            codiLletra = CodiLletra();
            StreamWriter XifratNums = new StreamWriter(rutaFitxer);
            for (int i = 0; i < crearLletres.Length; i++)
            {
                verifica = false;

                for (int x = 0; x < lletres.Length && verifica == false; x++)
                {
                    if (crearLletres[i].Equals(lletres[x]))
                    {
                        XifratNums.Write(codiLletra[x]);
                        verifica = true;
                    }
                }
            }

            XifratNums.Close();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            crearFitxers();
            MessageBox.Show("Has generat una moguda que flipas");
        }

    }
}
