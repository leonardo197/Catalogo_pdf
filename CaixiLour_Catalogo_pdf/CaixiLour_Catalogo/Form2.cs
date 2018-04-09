using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;
using System.IO;
namespace CaixiLour_Catalogo
{
    
    public partial class Form2 : Form
    {
        int maxrows;
        // SqlConnection cnn = new SqlConnection("Data Source=192.168.1.2,1433; Network Library=DBMSSOCN;Initial Catalog=catalogo; User ID=admin;Password=caixilour");
        SqlConnection cnn = new SqlConnection("Data Source=192.168.2.39,1433; Network Library=DBMSSOCN;Initial Catalog=Catalgo; User ID=admin;Password=caixilour1");  //coneção

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da_tab;

            string sql_string;
            DataTable dat_tab_tab;

            pb_foto_1.BackgroundImage = null;
            pb_foto_2.BackgroundImage = null;
            pb_foto_3.BackgroundImage = null;
            pb_foto_4.BackgroundImage = null;
            pb_foto_5.BackgroundImage = null;
            pb_foto_6.BackgroundImage = null;


            //caso erro de ligua
            if (Form1.st == "Assistências")
            {
                sql_string = "select * from Assistencias where ID_" + Form1.st + "='" + Form1.id + "'";
            }
            else
            {
                sql_string = "select * from " + Form1.st + " where ID_" + Form1.st + "='" + Form1.id + "'";
            }
            //ligar tab

            cnn.Open();
            da_tab = new SqlDataAdapter(sql_string, cnn);
            dat_tab_tab = new System.Data.DataTable();
            da_tab.Fill(dat_tab_tab);
            maxrows = dat_tab_tab.Rows.Count;
            cnn.Close();
            if (maxrows==1)
            {
            Byte[] fotos = (byte[])dat_tab_tab.Rows[0]["Imagem"];
            MemoryStream ms = new MemoryStream(fotos);
            Image fotos_s = Image.FromStream(ms);
            if (Form1.st == "Portas")
            {
                Byte[] fotos1 = (byte[])dat_tab_tab.Rows[0]["Imagem1"];
                MemoryStream ms1 = new MemoryStream(fotos1);
                Image fotos_s1 = Image.FromStream(ms1);
                Byte[] fotos2 = (byte[])dat_tab_tab.Rows[0]["Imagem2"];
                MemoryStream ms2 = new MemoryStream(fotos2);
                Image fotos_s2 = Image.FromStream(ms2);
                Byte[] fotos3 = (byte[])dat_tab_tab.Rows[0]["Imagem3"];
                MemoryStream ms3 = new MemoryStream(fotos3);
                Image fotos_s3 = Image.FromStream(ms3);
                Byte[] fotos4 = (byte[])dat_tab_tab.Rows[0]["Imagem4"];
                MemoryStream ms4 = new MemoryStream(fotos4);
                Image fotos_s4 = Image.FromStream(ms4);
                Byte[] fotos5 = (byte[])dat_tab_tab.Rows[0]["Imagem5"];
                MemoryStream ms5 = new MemoryStream(fotos5);
                Image fotos_s5 = Image.FromStream(ms5);
                Byte[] fotos6 = (byte[])dat_tab_tab.Rows[0]["Imagem6"];
                MemoryStream ms6 = new MemoryStream(fotos6);
                Image fotos_s6 = Image.FromStream(ms6);
                pb_foto_1.BackgroundImage = fotos_s1;
                pb_foto_2.BackgroundImage = fotos_s2;
                pb_foto_3.BackgroundImage = fotos_s3;
                pb_foto_4.BackgroundImage = fotos_s4;
                pb_foto_5.BackgroundImage = fotos_s5;
                pb_foto_6.BackgroundImage = fotos_s6;
            }
            pb_foto.BackgroundImage = fotos_s;
            pb_foto_0.BackgroundImage = fotos_s;
            }
        }

        private void pb_foto_5_Click(object sender, EventArgs e)
        {
            pb_foto.BackgroundImage = pb_foto_5.BackgroundImage;
        }
        private void pb_foto_6_Click(object sender, EventArgs e)
        {
            pb_foto.BackgroundImage = pb_foto_6.BackgroundImage;
        }
        private void pb_foto_1_Click(object sender, EventArgs e)
        {
            pb_foto.BackgroundImage = pb_foto_1.BackgroundImage;
        }
        private void pb_foto_2_Click(object sender, EventArgs e)
        {
            pb_foto.BackgroundImage = pb_foto_2.BackgroundImage;
        }
        private void pb_foto_3_Click(object sender, EventArgs e)
        {
            pb_foto.BackgroundImage = pb_foto_3.BackgroundImage;
        }
        private void pb_foto_4_Click(object sender, EventArgs e)
        {
            pb_foto.BackgroundImage = pb_foto_4.BackgroundImage;
        }
        private void pb_foto_0_Click(object sender, EventArgs e)
        {
            pb_foto.BackgroundImage = pb_foto_0.BackgroundImage;
        }
    }


 }


        