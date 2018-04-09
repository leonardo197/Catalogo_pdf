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
using iTextSharp.text;
using iTextSharp.text.pdf;

using System.Diagnostics;


namespace CaixiLour_Catalogo
{
    public partial class Form1 : Form
    {
        //permicao de uso
        public static Boolean fot { get; set; }
        public static Boolean eliminar_r { get; set; }
        public static Boolean mod_r { get; set; }
        public static Boolean criar_r { get; set; }
        public static Boolean criar_m_r_u { get; set; }

        //adapters exclusivos
        int maxrows;
        int iii;
        int ii;
        Double x1;
        Boolean b = false;
        Boolean nr = true;
        //Double id;
        //string st = "portas";
        SqlCommand cmd;
        SqlDataAdapter da_tab;
        PictureBox[] pb_array = new PictureBox[1];
        //int maxrows;
        SqlConnection cnn = new SqlConnection("Data Source=192.168.1.2,1433; Network Library=DBMSSOCN;Initial Catalog=catalogo; User ID=admin;Password=caixilour");//casa
        //SqlConnection cnn = new SqlConnection("Data Source=192.168.2.39,1433; Network Library=DBMSSOCN;Initial Catalog=Catalgo; User ID=admin;Password=caixilour1");  //coneção
        string sql_string_pdf;
        string sql_string;
        string sql_string1;
        //select ... from
        //formes
        Form2 form2 = new Form2();
        Form3 form3 = new Form3();


        public static Double id { get; set; }
        public static string st { get; set; }

        //DataSet dat_set_tab;
        public static DataTable dat_tab_tab { get; set; }


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sql_string = "select * from portas";
            st = "Portas";
            pre_pes_tb();
            pes_cor();
            BT_admin.BackColor = Color.White;
            BT_utilizador.BackColor = Color.YellowGreen;
            panel_pes.Enabled = true;
            panel_menu.Enabled = true;
            panel_admin.Visible = false;
            panel10.Visible = false;
            panel_menu.Location = new Point(222, 9);
            panel_menu.Size = new Size(809, 760);
            cb_tabela_admin.Text = st;
            novo_re_admin();
            panel_pes_portas.Visible = true;
        }
        public void pes_tb()
        {
            ////ligar tab
            //SqlConnection cnn = new SqlConnection("Data Source=192.168.1.2,1433; Network Library=DBMSSOCN;Initial Catalog=catalogo; User ID=admin;Password=caixilour");//casa

            SqlConnection cnn = new SqlConnection("Data Source=192.168.2.39,1433; Network Library=DBMSSOCN;Initial Catalog=Catalgo; User ID=admin;Password=caixilour1");  //coneção           
            //
            cnn.Open();
            da_tab = new SqlDataAdapter(sql_string, cnn);
            dat_tab_tab = new System.Data.DataTable();
            da_tab.Fill(dat_tab_tab);
            maxrows = dat_tab_tab.Rows.Count;
            cnn.Close();

            PictureBox[] pb_array = new PictureBox[maxrows];//array de ing
            int n;
            int i = 0;
            int x = 0;

            panel_menu.Controls.Clear();

            for (n = 0; n <= maxrows - 1; n++)
            {
                // comverte byte ei img
                Byte[] fotos = (byte[])dat_tab_tab.Rows[n]["Imagem"];
                MemoryStream ms = new MemoryStream(fotos);
                System.Drawing.Image fotos_s = System.Drawing.Image.FromStream(ms);
                //cria pb no panel_menu
                x++;
                pb_array[n] = new PictureBox();
                pb_array[n].Location = new Point(16 + ((x - 1) * 155), 10 + (146 * i));
                pb_array[n].Size = new Size(133, 140);
                pb_array[n].SizeMode = PictureBoxSizeMode.Zoom;
                //pb_array[n].Anchor  = (AnchorStyles.Left | AnchorStyles.Right); ;
                pb_array[n].Image = fotos_s;
                pb_array[n].Name = Convert.ToString(n);
                this.Controls.Add(panel_menu);
                pb_array[n].Click += new EventHandler(this.click_fotos_Click);
                panel_menu.Controls.Add(pb_array[n]);
                x1 =Convert.ToDouble(panel_menu.Size.Width)/161 ;
               // x1 = 1000 / 161;
                Math.Floor(x1);
                if (x == Math.Floor(x1))
                {
                    x = 0;
                    i = i + 1;
                }
            }
            panel_menu.AutoScroll = true;
            pes_1_foto_inf();
            preencher_admim();
        }
        private void pes_cor()
        {
            b = false;
            if (rb_portas.Checked == true)
            {
                sql_string1 = "select DISTINCT Cor from portas";
            }
            if (rb_estores.Checked == true)
            {
                sql_string1 = "select DISTINCT Cor from estores ";
            }
            if (rb_puxadores.Checked == true)
            {
                sql_string1 = "select DISTINCT Cor from puxadores";
            }
            if (rb_complemetos.Checked == true)
            {
                sql_string1 = "select DISTINCT Cor from complementos";
            }
            //ligar tab
            //SqlConnection cnn = new SqlConnection("Data Source=192.168.1.2,1433; Network Library=DBMSSOCN;Initial Catalog=catalogo; User ID=admin;Password=caixilour");//casa
            SqlConnection cnn = new SqlConnection("Data Source=192.168.2.39,1433; Network Library=DBMSSOCN;Initial Catalog=Catalgo; User ID=admin;Password=caixilour1");  //coneção
            cnn.Open();
            da_tab = new SqlDataAdapter(sql_string1, cnn);
            dat_tab_tab = new System.Data.DataTable();
            da_tab.Fill(dat_tab_tab);
            //maxrows = dat_tab_tab.Rows.Count;
            cnn.Close();
            if (rb_portas.Checked == true)
            {
                cb_cor_portas.Items.Clear();
                maxrows = dat_tab_tab.Rows.Count;
                for (int i = 0; i < maxrows; i++)
                {
                    cb_cor_portas.Items.Add(Convert.ToString(dat_tab_tab.Rows[i]["Cor"]));
                }
                cb_cor_portas.Items.Add("TODAS");
                cb_cor_portas.Text = Convert.ToString("TODAS");
            }
            if (rb_estores.Checked == true)
            {
                cb_cor_estores.Items.Clear();
                maxrows = dat_tab_tab.Rows.Count;
                for (int i = 0; i < maxrows; i++)
                {
                    cb_cor_estores.Items.Add(Convert.ToString(dat_tab_tab.Rows[i]["Cor"]));
                }
                cb_cor_estores.Items.Add("TODAS");
                cb_cor_estores.Text = Convert.ToString("TODAS");
            }
            if (rb_puxadores.Checked == true)
            {
                cb_cor_puxadores.Items.Clear();
                maxrows = dat_tab_tab.Rows.Count;
                for (int i = 0; i < maxrows; i++)
                {
                    cb_cor_puxadores.Items.Add(Convert.ToString(dat_tab_tab.Rows[i]["Cor"]));
                }
                cb_cor_puxadores.Items.Add("TODAS");
                cb_cor_puxadores.Text = Convert.ToString("TODAS");
            }
            if (rb_complemetos.Checked == true)
            {
                cb_cor_complementos.Items.Clear();
                maxrows = dat_tab_tab.Rows.Count;
                for (int i = 0; i < maxrows; i++)
                {
                    cb_cor_complementos.Items.Add(Convert.ToString(dat_tab_tab.Rows[i]["Cor"]));
                }
                cb_cor_complementos.Items.Add("TODAS");
                cb_cor_complementos.Text = Convert.ToString("TODAS");
            }
            if (rb_assitencias.Checked != true)
            {
                cb_cor_admin.Items.Clear();
                maxrows = dat_tab_tab.Rows.Count;
                for (int i = 0; i < maxrows; i++)
                {
                    cb_cor_admin.Items.Add(Convert.ToString(dat_tab_tab.Rows[i]["Cor"]));
                }
                //cb_cor_admin.Items.Add("");
                //cb_cor_admin.Text = Convert.ToString(""); 
            }

            b = true;
        }
        private void pre_pes_tb()
        {
            //parte grafica
            panel_pes_complementos.Visible = false;
            panel_pes_portas.Visible = false;
            panel_pes_puxadores.Visible = false;
            panel_pes_estores.Visible = false;

            //verifica a tab para abrir
            //if (rb_portas.Checked == true)
            //{
            //    panel_pes_portas.Visible = true;
            //    sql_string = "select * from portas";
            //}
            //if (rb_estores.Checked == true)
            //{
            //    panel_pes_estores.Visible = true;
            //    sql_string = "select * from estores";
            //}
            //if (rb_puxadores.Checked == true)
            //{
            //    panel_pes_puxadores.Visible = true;
            //    sql_string = "select * from puxadores";
            //}
            //if (rb_complemetos.Checked == true)
            //{
            //    panel_pes_complementos.Visible = true;
            //    sql_string = "select * from complementos";
            //}
            //if (rb_assitencias.Checked == true)
            //{
            //    panel_pes_assitencias.Visible = true;
            //    sql_string = "select * from assistencias";
            //}
            pes_tb();
        }
        public void pes_familia()
        {
            b = false;
            if (rb_estores.Checked == true)
            {
                sql_string1 = "select DISTINCT Família from estores";
            }
            if (rb_puxadores.Checked == true)
            {
                sql_string1 = "select DISTINCT Família from puxadores";
            }
            if (rb_complemetos.Checked == true)
            {
                sql_string1 = "select DISTINCT Família from complementos";
            }
            if (rb_assitencias.Checked == true)
            {
                sql_string1 = "select DISTINCT Família from assistencias";
            }
            //ligar tab
            //SqlConnection cnn = new SqlConnection("Data Source=192.168.1.2,1433; Network Library=DBMSSOCN;Initial Catalog=catalogo; User ID=admin;Password=caixilour");//casa
            SqlConnection cnn = new SqlConnection("Data Source=192.168.2.39,1433; Network Library=DBMSSOCN;Initial Catalog=Catalgo; User ID=admin;Password=caixilour1");  //coneção
            //cnn.Open();
            da_tab = new SqlDataAdapter(sql_string1, cnn);
            dat_tab_tab = new System.Data.DataTable();
            da_tab.Fill(dat_tab_tab);
            //maxrows = dat_tab_tab.Rows.Count;
            cnn.Close();
            cb_familia_admin.Items.Clear();
            maxrows = dat_tab_tab.Rows.Count;
            for (int i = 0; i < maxrows; i++)
            {
                cb_familia_assitencias.Items.Add(Convert.ToString(dat_tab_tab.Rows[i]["Família"]));
                cb_familia_admin.Items.Add(Convert.ToString(dat_tab_tab.Rows[i]["Família"]));
            }
            cb_familia_admin.Items.Add("");
            cb_familia_admin.Text = Convert.ToString("");
            if (rb_estores.Checked == true)
            {
                cb_familia_estores.Items.Clear();
                maxrows = dat_tab_tab.Rows.Count;
                for (int i = 0; i < maxrows; i++)
                {
                    cb_familia_estores.Items.Add(Convert.ToString(dat_tab_tab.Rows[i]["Família"]));
                }
                cb_familia_estores.Items.Add("TODAS");
                cb_familia_estores.Text = Convert.ToString("TODAS");
            }
            if (rb_puxadores.Checked == true)
            {
                cb_familia_puxadores.Items.Clear();
                maxrows = dat_tab_tab.Rows.Count;
                for (int i = 0; i < maxrows; i++)
                {
                    cb_familia_puxadores.Items.Add(Convert.ToString(dat_tab_tab.Rows[i]["Família"]));
                }
                cb_familia_puxadores.Items.Add("TODAS");
                cb_familia_puxadores.Text = Convert.ToString("TODAS");
            }
            if (rb_complemetos.Checked == true)
            {
                cb_familia_complementos.Items.Clear();
                maxrows = dat_tab_tab.Rows.Count;
                for (int i = 0; i < maxrows; i++)
                {
                    cb_familia_complementos.Items.Add(Convert.ToString(dat_tab_tab.Rows[i]["Família"]));
                }
                cb_familia_complementos.Items.Add("TODAS");
                cb_familia_complementos.Text = Convert.ToString("TODAS");
            }
            if (rb_assitencias.Checked == true)//assitencias
            {
                cb_familia_assitencias.Items.Clear();
                maxrows = dat_tab_tab.Rows.Count;
                for (int i = 0; i < maxrows; i++)
                {
                    cb_familia_assitencias.Items.Add(Convert.ToString(dat_tab_tab.Rows[i]["Família"]));
                }
                cb_familia_assitencias.Items.Add("TODAS");
                cb_familia_assitencias.Text = Convert.ToString("TODAS");
            }
            b = true;
        }
        public void pes_1_foto_inf()
        {
            int i = 0;
            if (dat_tab_tab.Rows.Count != 0)
            {
                {
                    Byte[] fotos = (byte[])dat_tab_tab.Rows[i]["Imagem"];
                    MemoryStream ms = new MemoryStream(fotos);
                    System.Drawing.Image fotos_s = System.Drawing.Image.FromStream(ms);
                    pb_foto_i_0.BackgroundImage = fotos_s;
                    if (rb_portas.Checked == true)
                    {
                        Byte[] fotos1 = (byte[])dat_tab_tab.Rows[i]["Imagem1"];
                        MemoryStream ms1 = new MemoryStream(fotos1);
                        System.Drawing.Image fotos_s1 = System.Drawing.Image.FromStream(ms1);
                        Byte[] fotos2 = (byte[])dat_tab_tab.Rows[i]["Imagem2"];
                        MemoryStream ms2 = new MemoryStream(fotos2);
                        System.Drawing.Image fotos_s2 = System.Drawing.Image.FromStream(ms2);
                        Byte[] fotos3 = (byte[])dat_tab_tab.Rows[i]["Imagem3"];
                        MemoryStream ms3 = new MemoryStream(fotos3);
                        System.Drawing.Image fotos_s3 = System.Drawing.Image.FromStream(ms3);
                        Byte[] fotos4 = (byte[])dat_tab_tab.Rows[i]["Imagem4"];
                        MemoryStream ms4 = new MemoryStream(fotos4);
                        System.Drawing.Image fotos_s4 = System.Drawing.Image.FromStream(ms4);
                        Byte[] fotos5 = (byte[])dat_tab_tab.Rows[i]["Imagem5"];
                        MemoryStream ms5 = new MemoryStream(fotos5);
                        System.Drawing.Image fotos_s5 = System.Drawing.Image.FromStream(ms5);
                        Byte[] fotos6 = (byte[])dat_tab_tab.Rows[i]["Imagem6"];
                        MemoryStream ms6 = new MemoryStream(fotos6);
                        System.Drawing.Image fotos_s6 = System.Drawing.Image.FromStream(ms6);
                        pb_foto_1.BackgroundImage = fotos_s1;
                        pb_foto_2.BackgroundImage = fotos_s2;
                        pb_foto_3.BackgroundImage = fotos_s3;
                        pb_foto_4.BackgroundImage = fotos_s4;
                        pb_foto_5.BackgroundImage = fotos_s5;
                        pb_foto_6.BackgroundImage = fotos_s6;
                        pb_foto_i_1.BackgroundImage = fotos_s1;
                        pb_foto_i_2.BackgroundImage = fotos_s2;
                        pb_foto_i_3.BackgroundImage = fotos_s3;
                        pb_foto_i_4.BackgroundImage = fotos_s4;
                        pb_foto_i_5.BackgroundImage = fotos_s5;
                        pb_foto_i_6.BackgroundImage = fotos_s6;
                    }


                    nr = false;//novo registo negado
                    pb_foto.BackgroundImage = fotos_s;

                    id = Convert.ToDouble(dat_tab_tab.Rows[i][0]);
                    l_id.Text = Convert.ToString(dat_tab_tab.Rows[i]["ID"]);
                    tb_id_admin.Text = Convert.ToString(dat_tab_tab.Rows[i]["ID"]);
                    l_ref.Text = Convert.ToString(dat_tab_tab.Rows[i]["Referência"]);
                    tb_referencia_admin.Text = Convert.ToString(dat_tab_tab.Rows[i]["Referência"]);
                    l_descricao.Text = Convert.ToString(dat_tab_tab.Rows[i]["descrição"]);
                    rtb_descriçao_admin.Text = Convert.ToString(dat_tab_tab.Rows[i]["descrição"]);
                    if (rb_portas.Checked == true)
                    {
                        l_preco1.Text = Convert.ToString(dat_tab_tab.Rows[i]["Preço"]) + "€";
                    }
                    l_preco.Text = Convert.ToString(dat_tab_tab.Rows[i]["Preço"]) + "€";
                    tb_perco_admin.Text = Convert.ToString(dat_tab_tab.Rows[i]["Preço"]);
                    if (rb_portas.Checked != true)
                    {
                        l_familia.Text = Convert.ToString(dat_tab_tab.Rows[i]["Família"]);
                        cb_familia_admin.Text = l_familia.Text;
                    }
                    if (rb_assitencias.Checked != true)
                    {
                        l_cor.Text = Convert.ToString(dat_tab_tab.Rows[i]["cor"]);
                        cb_cor_admin.Text = l_cor.Text;
                    }

                    if (rb_portas.Checked == true)
                    {
                        l_tipo_grellha.Text = Convert.ToString(dat_tab_tab.Rows[i]["Tipo_Grelha"]);
                        cb_tipo_de_grelha_admin.Text = l_tipo_grellha.Text;

                        if (Convert.ToString(dat_tab_tab.Rows[i]["Vidro"]) == "True")
                        {
                            panel_d_portas.Visible = true;
                            rb_vidro_s.Checked = true;
                            rb_admin_vidro_sim.Checked = true;
                        }
                        else
                        {
                            rb_vidro_nao.Checked = true;
                            rb_admin_vidro_nao.Checked = true;
                        }
                        if (Convert.ToString(dat_tab_tab.Rows[i]["Grelha"]) == "True")
                        {
                            rb_grelha_sim.Checked = true;
                            rb_admin_grelha_sim.Checked = true;

                        }
                        else
                        {
                            rb_grelha_nao.Checked = true;
                            rb_admin_grelha_nao.Checked = true;
                        }
                    }
                    else
                    {
                        panel_d_portas.Visible = false;
                    }
                }
            }
            else
            {
                pb_foto.BackgroundImage = CaixiLour_Catalogo.Properties.Resources.vazio;
                l_id.Text = "";
                l_ref.Text = "";
                l_descricao.Text = "";
                l_cor.Text = "";
                l_preco.Text = "€";
                if (rb_portas.Checked == true)
                {
                    l_tipo_grellha.Text = "";
                    panel_d_portas.Visible = true;
                }
                else
                {
                    panel_d_portas.Visible = false;
                }
            }
        }
        public void click_fotos_Click(object sender, EventArgs e)
        {
            //if (rb_portas.Checked == true)
            //{
            //    // panel_pes_portas.Visible = true;
            //    sql_string = "select * from portas";
            //}
            //if (rb_estores.Checked == true)
            //{
            //    //panel_pes_estores.Visible = true;
            //    sql_string = "select * from estores";
            //}
            //if (rb_puxadores.Checked == true)
            //{
            //    //panel_pes_puxadores.Visible = true;
            //    sql_string = "select * from puxadores";
            //}
            //if (rb_complemetos.Checked == true)
            //{
            //    //panel_pes_complementos.Visible = true;
            //    sql_string = "select * from complementos";
            //}
            //if (rb_assitencias.Checked == true)
            //{
            //    //panel_pes_complementos.Visible = true;
            //    sql_string = "select * from assistencias";
            //}
            //SqlConnection cnn = new SqlConnection("Data Source=192.168.1.2,1433; Network Library=DBMSSOCN;Initial Catalog=catalogo; User ID=admin;Password=caixilour");//casa
            SqlConnection cnn = new SqlConnection("Data Source=192.168.2.39,1433; Network Library=DBMSSOCN;Initial Catalog=Catalgo; User ID=admin;Password=caixilour1");  //coneção//
            //ligar tab
           cnn.Open();
            da_tab = new SqlDataAdapter(sql_string, cnn);
            dat_tab_tab = new System.Data.DataTable();
            da_tab.Fill(dat_tab_tab);
            maxrows = dat_tab_tab.Rows.Count;
            cnn.Close();
            var Pictur = sender as PictureBox;
            for (int i = 0; i < maxrows; i++)
            {
                if (Pictur != null && Pictur.Name == Convert.ToString(i))
                {
                    Byte[] fotos = (byte[])dat_tab_tab.Rows[i]["Imagem"];
                    MemoryStream ms = new MemoryStream(fotos);
                    System.Drawing.Image fotos_s = System.Drawing.Image.FromStream(ms);
                    pb_foto_i_0.BackgroundImage = fotos_s;
                    if (rb_portas.Checked == true)
                    {
                        Byte[] fotos1 = (byte[])dat_tab_tab.Rows[i]["Imagem1"];
                        MemoryStream ms1 = new MemoryStream(fotos1);
                        System.Drawing.Image fotos_s1 = System.Drawing.Image.FromStream(ms1);
                        Byte[] fotos2 = (byte[])dat_tab_tab.Rows[i]["Imagem2"];
                        MemoryStream ms2 = new MemoryStream(fotos2);
                        System.Drawing.Image fotos_s2 = System.Drawing.Image.FromStream(ms2);
                        Byte[] fotos3 = (byte[])dat_tab_tab.Rows[i]["Imagem3"];
                        MemoryStream ms3 = new MemoryStream(fotos3);
                        System.Drawing.Image fotos_s3 = System.Drawing.Image.FromStream(ms3);
                        Byte[] fotos4 = (byte[])dat_tab_tab.Rows[i]["Imagem4"];
                        MemoryStream ms4 = new MemoryStream(fotos4);
                        System.Drawing.Image fotos_s4 = System.Drawing.Image.FromStream(ms4);
                        Byte[] fotos5 = (byte[])dat_tab_tab.Rows[i]["Imagem5"];
                        MemoryStream ms5 = new MemoryStream(fotos5);
                        System.Drawing.Image fotos_s5 = System.Drawing.Image.FromStream(ms5);
                        Byte[] fotos6 = (byte[])dat_tab_tab.Rows[i]["Imagem6"];
                        MemoryStream ms6 = new MemoryStream(fotos6);
                        System.Drawing.Image fotos_s6 = System.Drawing.Image.FromStream(ms6);
                        pb_foto_1.BackgroundImage = fotos_s1;
                        pb_foto_2.BackgroundImage = fotos_s2;
                        pb_foto_3.BackgroundImage = fotos_s3;
                        pb_foto_4.BackgroundImage = fotos_s4;
                        pb_foto_5.BackgroundImage = fotos_s5;
                        pb_foto_6.BackgroundImage = fotos_s6;
                        pb_foto_i_1.BackgroundImage = fotos_s1;
                        pb_foto_i_2.BackgroundImage = fotos_s2;
                        pb_foto_i_3.BackgroundImage = fotos_s3;
                        pb_foto_i_4.BackgroundImage = fotos_s4;
                        pb_foto_i_5.BackgroundImage = fotos_s5;
                        pb_foto_i_6.BackgroundImage = fotos_s6;
                    }

                    nr = false;//novo registo negado
                    pb_foto.BackgroundImage = fotos_s;

                    id = Convert.ToDouble(dat_tab_tab.Rows[i][0]);
                    l_id.Text = Convert.ToString(dat_tab_tab.Rows[i]["ID"]);
                    tb_id_admin.Text = Convert.ToString(dat_tab_tab.Rows[i]["ID"]);
                    l_ref.Text = Convert.ToString(dat_tab_tab.Rows[i]["Referência"]);
                    tb_referencia_admin.Text = Convert.ToString(dat_tab_tab.Rows[i]["Referência"]);
                    l_descricao.Text = Convert.ToString(dat_tab_tab.Rows[i]["descrição"]);
                    rtb_descriçao_admin.Text = Convert.ToString(dat_tab_tab.Rows[i]["descrição"]);
                    if (rb_portas.Checked == true)
                    {
                        l_preco1.Text = Convert.ToString(dat_tab_tab.Rows[i]["Preço"]) + "€";
                    }
                    l_preco.Text = Convert.ToString(dat_tab_tab.Rows[i]["Preço"]) + "€";
                    tb_perco_admin.Text = Convert.ToString(dat_tab_tab.Rows[i]["Preço"]);
                    if (rb_portas.Checked != true)
                    {
                        l_familia.Text = Convert.ToString(dat_tab_tab.Rows[i]["Família"]);
                        cb_familia_admin.Text = l_familia.Text;
                    }
                    if (rb_assitencias.Checked != true)
                    {
                        l_cor.Text = Convert.ToString(dat_tab_tab.Rows[i]["cor"]);
                        cb_cor_admin.Text = l_cor.Text;
                    }

                    if (rb_portas.Checked == true)
                    {
                        l_tipo_grellha.Text = Convert.ToString(dat_tab_tab.Rows[i]["Tipo_Grelha"]);
                        cb_tipo_de_grelha_admin.Text = l_tipo_grellha.Text;

                        if (Convert.ToString(dat_tab_tab.Rows[i]["Vidro"]) == "True")
                        {
                            panel_d_portas.Visible = true;
                            rb_vidro_s.Checked = true;
                            rb_admin_vidro_sim.Checked = true;
                        }
                        else
                        {
                            rb_vidro_nao.Checked = true;
                            rb_admin_vidro_nao.Checked = true;
                        }
                        if (Convert.ToString(dat_tab_tab.Rows[i]["Grelha"]) == "True")
                        {
                            rb_grelha_sim.Checked = true;
                            rb_admin_grelha_sim.Checked = true;

                        }
                        else
                        {
                            rb_grelha_nao.Checked = true;
                            rb_admin_grelha_nao.Checked = true;
                        }
                    }
                    else
                    {
                        panel_d_portas.Visible = false;
                    }
                }
            }
        }
        //admin
        public void novo_re_admin()
        {
            nr = true;
            cb_cor_admin.Text = null;
            cb_familia_admin.Text = null;
            cb_tipo_de_grelha_admin.Text = null;
            tb_id_admin.Text = null;
            tb_perco_admin.Text = null;
            tb_referencia_admin.Text = null;
            rtb_descriçao_admin.Text = null;
            rb_admin_vidro_sim.Checked = false;
            rb_admin_vidro_nao.Checked = false;
            rb_admin_grelha_sim.Checked = false;
            rb_admin_grelha_nao.Checked = false;
            pb_foto_i_0.BackgroundImage = null;
            pb_foto_i_1.BackgroundImage = CaixiLour_Catalogo.Properties.Resources.vazio;
            pb_foto_i_2.BackgroundImage = CaixiLour_Catalogo.Properties.Resources.vazio;
            pb_foto_i_3.BackgroundImage = CaixiLour_Catalogo.Properties.Resources.vazio;
            pb_foto_i_4.BackgroundImage = CaixiLour_Catalogo.Properties.Resources.vazio;
            pb_foto_i_5.BackgroundImage = CaixiLour_Catalogo.Properties.Resources.vazio;
            pb_foto_i_6.BackgroundImage = CaixiLour_Catalogo.Properties.Resources.vazio;
        }
        private void pes_utilizador()
        {
            if (NOME_Utelirador.Text != "" && PASSE_Utelirador.Text != "")
            {
                sql_string = "select count(*) from utilizador where Nome='" + NOME_Utelirador.Text + "'and Passe='" + PASSE_Utelirador.Text + "'";//

                ////ligar tab
                //SqlConnection cnn = new SqlConnection("Data Source=192.168.1.2,1433; Network Library=DBMSSOCN;Initial Catalog=catalogo; User ID=admin;Password=caixilour");//casa
                SqlConnection cnn = new SqlConnection("Data Source=192.168.2.39,1433; Network Library=DBMSSOCN;Initial Catalog=Catalgo; User ID=admin;Password=caixilour1");  //coneção              
               cnn.Open();
                da_tab = new SqlDataAdapter(sql_string, cnn);
                dat_tab_tab = new System.Data.DataTable();
                da_tab.Fill(dat_tab_tab);
                //maxrows = dat_tab_tab.Rows.Count;

                //l_cor.Text = Convert.ToString(dat_tab_tab.Rows[0][0]);
                if ("1" == Convert.ToString(dat_tab_tab.Rows[0][0]))
                {
                    sql_string = "select * from utilizador where Nome='" + NOME_Utelirador.Text + "'and Passe='" + PASSE_Utelirador.Text + "'";//
                    da_tab = new SqlDataAdapter(sql_string, cnn);
                    dat_tab_tab = new System.Data.DataTable();
                    da_tab.Fill(dat_tab_tab);
                    //
                    fot = Convert.ToBoolean(dat_tab_tab.Rows[0]["Fotocopiar"]);
                    eliminar_r = Convert.ToBoolean(dat_tab_tab.Rows[0]["Eliminar_Registos"]);
                    mod_r = Convert.ToBoolean(dat_tab_tab.Rows[0]["Modificar_Registros"]);
                    criar_m_r_u = Convert.ToBoolean(dat_tab_tab.Rows[0]["Criar_Utilizadores"]);
                    criar_r = Convert.ToBoolean(dat_tab_tab.Rows[0]["Criar_Registos"]);
                    BT_fotocopiar.Enabled = fot;
                    BT_fotocopiar.Visible = fot;
                    BT_add_utilisador.Enabled = criar_m_r_u;
                    BT_add_utilisador.Visible = criar_m_r_u;
                    b_delete.Enabled = eliminar_r;
                    b_delete.Visible = eliminar_r;
                    b_novo.Enabled = criar_r;
                    b_novo.Visible = criar_r;
                    //
                    panel_pes.Enabled = true;
                    panel_menu.Enabled = true;
                    panel_admin.Enabled = true;
                    panel10.Visible = false;
                    panel_menu.Location = new Point(222, 186);
                    panel_menu.Size = new Size(809, 583);
                    // BT_add_utilisador.Visible = true;
                }
                else
                {
                    MessageBox.Show("O nome de utilizador ou a palavra passe está incorreto", "Erro na conexão",
                        MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
            }
            cnn.Close();
            //exceção para logar
            if (NOME_Utelirador.Text == "LCS197" && PASSE_Utelirador.Text == "leonardo197+")
            {
                panel_pes.Enabled = true;
                panel_menu.Enabled = true;
                panel_admin.Enabled = true;
                panel10.Visible = false;
                panel_menu.Location = new Point(222, 186);
                panel_menu.Size = new Size(809, 583);
                BT_add_utilisador.Visible = true;
            }
            if (NOME_Utelirador.Text == "admin" && PASSE_Utelirador.Text == "admincaixilour")
            {
                panel_pes.Enabled = true;
                panel_menu.Enabled = true;
                panel_admin.Enabled = true;
                panel10.Visible = false;
                panel_menu.Location = new Point(222, 186);
                panel_menu.Size = new Size(809, 583);
                BT_add_utilisador.Visible = true;
            }

            PASSE_Utelirador.Text = "";
        }
        public void preencher_admim()
        {
            if (rb_portas.Checked == true)
            {
                //sql_string = "select DISTINCT Tipo_Grelha from portas";

                ////ligar tab
               // SqlConnection cnn = new SqlConnection("Data Source=192.168.1.2,1433; Network Library=DBMSSOCN;Initial Catalog=catalogo; User ID=admin;Password=caixilour");//casa
                SqlConnection cnn = new SqlConnection("Data Source=192.168.2.39,1433; Network Library=DBMSSOCN;Initial Catalog=Catalgo; User ID=admin;Password=caixilour1");  //coneção
                da_tab = new SqlDataAdapter(sql_string, cnn);
                dat_tab_tab = new System.Data.DataTable();
                da_tab.Fill(dat_tab_tab);
                //maxrows = dat_tab_tab.Rows.Count;
                cnn.Close();

                cb_tipo_de_grelha_admin.Items.Clear();
                maxrows = dat_tab_tab.Rows.Count;
                for (int i = 0; i < maxrows; i++)
                {
                    cb_tipo_de_grelha_admin.Items.Add(Convert.ToString(dat_tab_tab.Rows[i]["Tipo_Grelha"]));
                }
            }

        }
        public void delete_re_admin()
        {
            //SqlConnection cnn = new SqlConnection("Data Source=192.168.1.2,1433; Network Library=DBMSSOCN;Initial Catalog=catalogo; User ID=admin;Password=caixilour");//casa
            SqlConnection cnn = new SqlConnection("Data Source=192.168.2.39,1433; Network Library=DBMSSOCN;Initial Catalog=Catalgo; User ID=admin;Password=caixilour1");  //coneção            
            cmd = new SqlCommand();
            cmd.Connection = cnn;

            //diz o que e onde apagar
            if (rb_assitencias.Checked == true)
            {
                cmd.CommandText = " DELETE FROM  assistencias WHERE ID_Assistências=" + id;
            }
            if (rb_portas.Checked == true)
            {
                cmd.CommandText = " DELETE FROM  portas WHERE ID_Portas=" + id;
            }
            if (rb_puxadores.Checked == true)
            {
                cmd.CommandText = " DELETE FROM  puxadores WHERE ID_Puxadores=" + id;
            }
            if (rb_estores.Checked == true)
            {
                cmd.CommandText = " DELETE FROM  estores WHERE ID_Estores=" + id;
            }
            if (rb_complemetos.Checked == true)
            {
                cmd.CommandText = " DELETE FROM  complementos WHERE ID_Complementos=" + id;
            }
            cnn.Open();
            cmd.ExecuteNonQuery();//apagar
            cnn.Close(); //fexa cnn
            pre_pes_tb();
            novo_re_admin();
        }
        public void update_re_admin()
        {
            Boolean vs = false;
            Boolean gs = false;
            using (cnn)
            {
                MemoryStream ms = new MemoryStream();
                MemoryStream ms1 = new MemoryStream();
                MemoryStream ms2 = new MemoryStream();
                MemoryStream ms3 = new MemoryStream();
                MemoryStream ms4 = new MemoryStream();
                MemoryStream ms5 = new MemoryStream();
                MemoryStream ms6 = new MemoryStream();
                //pb_foto_i...
                if (rb_portas.Checked == true)
                {
                    pb_foto_i_1.BackgroundImage.Save(ms1, pb_foto_i_1.BackgroundImage.RawFormat);
                    pb_foto_i_2.BackgroundImage.Save(ms2, pb_foto_i_2.BackgroundImage.RawFormat);
                    pb_foto_i_3.BackgroundImage.Save(ms3, pb_foto_i_3.BackgroundImage.RawFormat);
                    pb_foto_i_4.BackgroundImage.Save(ms4, pb_foto_i_4.BackgroundImage.RawFormat);
                    pb_foto_i_5.BackgroundImage.Save(ms5, pb_foto_i_5.BackgroundImage.RawFormat);
                    pb_foto_i_6.BackgroundImage.Save(ms6, pb_foto_i_6.BackgroundImage.RawFormat);
                }
                pb_foto_i_0.BackgroundImage.Save(ms, pb_foto_i_0.BackgroundImage.RawFormat);
                byte[] img = ms.ToArray();
                byte[] img1 = ms1.ToArray();
                byte[] img2 = ms2.ToArray();
                byte[] img3 = ms3.ToArray();
                byte[] img4 = ms4.ToArray();
                byte[] img5 = ms5.ToArray();
                byte[] img6 = ms6.ToArray();

               SqlConnection cnn = new SqlConnection("Data Source=192.168.1.2,1433; Network Library=DBMSSOCN;Initial Catalog=catalogo; User ID=admin;Password=caixilour");//casa
                //SqlConnection cnn = new SqlConnection("Data Source=192.168.2.39,1433; Network Library=DBMSSOCN;Initial Catalog=Catalgo; User ID=admin;Password=caixilour1");  //coneção
                cnn.Open();//liga

                //extras
                if (rb_admin_vidro_sim.Checked == true)
                {
                    vs = true;
                }
                if (rb_admin_grelha_sim.Checked == true)
                {
                    gs = true;
                }

                cmd = new SqlCommand();
                cmd.Connection = cnn;
                //requecitos para guardar
                cmd.Parameters.Add(new SqlParameter("@ID", Convert.ToDouble(tb_id_admin.Text)));
                cmd.Parameters.Add(new SqlParameter("@Referência", Convert.ToString(tb_referencia_admin.Text)));
                cmd.Parameters.Add(new SqlParameter("@Família", Convert.ToString(cb_familia_admin.Text)));
                cmd.Parameters.Add(new SqlParameter("@Descrição", Convert.ToString(rtb_descriçao_admin.Text)));
                cmd.Parameters.Add(new SqlParameter("@Preço", Convert.ToDecimal(tb_perco_admin.Text)));
                cmd.Parameters.Add(new SqlParameter("@Cor", Convert.ToString(cb_cor_admin.Text)));
                cmd.Parameters.Add(new SqlParameter("@Tipo_Grelha", Convert.ToString(cb_tipo_de_grelha_admin.Text)));
                cmd.Parameters.Add(new SqlParameter("@Vidro", Convert.ToBoolean(vs)));
                cmd.Parameters.Add(new SqlParameter("@Grelha", Convert.ToBoolean(gs)));
                cmd.Parameters.Add(new SqlParameter("@Imagem", img));
                cmd.Parameters.Add(new SqlParameter("@Imagem1", img1));
                cmd.Parameters.Add(new SqlParameter("@Imagem2", img2));
                cmd.Parameters.Add(new SqlParameter("@Imagem3", img3));
                cmd.Parameters.Add(new SqlParameter("@Imagem4", img4));
                cmd.Parameters.Add(new SqlParameter("@Imagem5", img5));
                cmd.Parameters.Add(new SqlParameter("@Imagem6", img6));
                //diz o que para gravar e onde
                if (rb_assitencias.Checked == true)
                {
                    cmd.CommandText = " UPDATE assistencias SET ID=@ID, Referência=@Referência, Família=@Família, Descrição=@Descrição, Preço=@Preço, Imagem=@Imagem  WHERE ID_Assistências=" + id;
                }
                if (rb_portas.Checked == true)
                {
                    cmd.CommandText = " UPDATE portas SET ID=@ID, Referência=@Referência, Descrição=@Descrição, Preço=@Preço, Cor=@Cor, Tipo_Grelha=@Tipo_Grelha, Vidro=@Vidro, Grelha=@Grelha, Imagem=@Imagem, Imagem1=@Imagem1, Imagem2=@Imagem2, Imagem3=@Imagem3, Imagem4=@Imagem4, Imagem5=@Imagem5, Imagem6=@Imagem6 WHERE ID_Portas=" + id;
                }
                if (rb_puxadores.Checked == true)
                {
                    cmd.CommandText = " UPDATE puxadores SET ID=@ID, Referência=@Referência, Família=@Família, Descrição=@Descrição, Preço=@Preço, Imagem=@Imagem  WHERE ID_Puxadores=" + id;
                }
                if (rb_estores.Checked == true)
                {
                    cmd.CommandText = " UPDATE estores SET ID=@ID, Referência=@Referência, Família=@Família, Descrição=@Descrição, Preço=@Preço, Imagem=@Imagem  WHERE ID_Estores=" + id;
                }
                if (rb_complemetos.Checked == true)
                {
                    cmd.CommandText = " UPDATE complementos SET ID=@ID, Referência=@Referência, Família=@Família, Descrição=@Descrição, Preço=@Preço, Imagem=@Imagem  WHERE ID_Complementos=" + id;
                }
                cmd.ExecuteNonQuery();//grava
                cnn.Close(); //fexa cnn
            }
            pre_pes_tb();//up au menu
            novo_re_admin();
        }
        public void guardar_novo_re_admin()
        {
            Boolean vs = false;
            Boolean gs = false;
            //using (cnn)
            {
                MemoryStream ms = new MemoryStream();
                MemoryStream ms1 = new MemoryStream();
                MemoryStream ms2 = new MemoryStream();
                MemoryStream ms3 = new MemoryStream();
                MemoryStream ms4 = new MemoryStream();
                MemoryStream ms5 = new MemoryStream();
                MemoryStream ms6 = new MemoryStream();
                //pb_foto_i...
                if (rb_portas.Checked == true)
                {
                    pb_foto_i_1.BackgroundImage.Save(ms1, pb_foto_i_1.BackgroundImage.RawFormat);
                    pb_foto_i_2.BackgroundImage.Save(ms2, pb_foto_i_2.BackgroundImage.RawFormat);
                    pb_foto_i_3.BackgroundImage.Save(ms3, pb_foto_i_3.BackgroundImage.RawFormat);
                    pb_foto_i_4.BackgroundImage.Save(ms4, pb_foto_i_4.BackgroundImage.RawFormat);
                    pb_foto_i_5.BackgroundImage.Save(ms5, pb_foto_i_5.BackgroundImage.RawFormat);
                    pb_foto_i_6.BackgroundImage.Save(ms6, pb_foto_i_6.BackgroundImage.RawFormat);
                }
                pb_foto_i_0.BackgroundImage.Save(ms, pb_foto_i_0.BackgroundImage.RawFormat);
                byte[] img = ms.ToArray();
                byte[] img1 = ms1.ToArray();
                byte[] img2 = ms2.ToArray();
                byte[] img3 = ms3.ToArray();
                byte[] img4 = ms4.ToArray();
                byte[] img5 = ms5.ToArray();
                byte[] img6 = ms6.ToArray();
                SqlConnection cnn = new SqlConnection("Data Source=192.168.1.2,1433; Network Library=DBMSSOCN;Initial Catalog=catalogo; User ID=admin;Password=caixilour");//casa
                //SqlConnection cnn = new SqlConnection("Data Source=192.168.2.39,1433; Network Library=DBMSSOCN;Initial Catalog=Catalgo; User ID=admin;Password=caixilour1");  //coneção
                cnn.Open();

                //extras
                if (rb_admin_vidro_sim.Checked == true)
                {
                    vs = true;
                }
                if (rb_admin_grelha_sim.Checked == true)
                {
                    gs = true;
                }

                cmd = new SqlCommand();
                cmd.Connection = cnn;
                //requecitos para guardar
                cmd.Parameters.Add(new SqlParameter("@ID", Convert.ToDouble(tb_id_admin.Text)));
                cmd.Parameters.Add(new SqlParameter("@Referência", Convert.ToString(tb_referencia_admin.Text)));
                cmd.Parameters.Add(new SqlParameter("@Família", Convert.ToString(cb_familia_admin.Text)));
                cmd.Parameters.Add(new SqlParameter("@Descrição", Convert.ToString(rtb_descriçao_admin.Text)));
                cmd.Parameters.Add(new SqlParameter("@Preço", Convert.ToDecimal(tb_perco_admin.Text)));
                cmd.Parameters.Add(new SqlParameter("@Cor", Convert.ToString(cb_cor_admin.Text)));
                cmd.Parameters.Add(new SqlParameter("@Tipo_Grelha", Convert.ToString(cb_tipo_de_grelha_admin.Text)));
                cmd.Parameters.Add(new SqlParameter("@Vidro", Convert.ToBoolean(vs)));
                cmd.Parameters.Add(new SqlParameter("@Grelha", Convert.ToBoolean(gs)));
                cmd.Parameters.Add(new SqlParameter("@Imagem", img));
                cmd.Parameters.Add(new SqlParameter("@Imagem1", img1));
                cmd.Parameters.Add(new SqlParameter("@Imagem2", img2));
                cmd.Parameters.Add(new SqlParameter("@Imagem3", img3));
                cmd.Parameters.Add(new SqlParameter("@Imagem4", img4));
                cmd.Parameters.Add(new SqlParameter("@Imagem5", img5));
                cmd.Parameters.Add(new SqlParameter("@Imagem6", img6));
                //diz o que para gravar e onde
                if (rb_assitencias.Checked == true)
                {
                    cmd.CommandText = " INSERT INTO assistencias (ID, Referência, Família, Descrição, Preço, Imagem) VALUES (@ID, @Referência, @Família, @Descrição, @Preço, @Imagem)";
                }
                if (rb_portas.Checked == true)
                {
                    cmd.CommandText = " INSERT INTO portas (ID, Referência, Descrição, Preço, Cor, Tipo_Grelha, Vidro, Grelha, Imagem, Imagem1, Imagem2, Imagem3, Imagem4, Imagem5, Imagem6) VALUES (@ID, @Referência, @Descrição, @Preço, @Cor, @Tipo_Grelha, @Vidro, @Grelha, @Imagem, @Imagem1, @Imagem2, @Imagem3, @Imagem4, @Imagem5, @Imagem6)";
                }
                if (rb_puxadores.Checked == true)
                {
                    cmd.CommandText = " INSERT INTO puxadores (ID, Referência, Família, Descrição, Cor, Preço, Imagem) VALUES (@ID, @Referência, @Família, @Descrição, @Cor, @Preço, @Imagem)";
                }
                if (rb_estores.Checked == true)
                {
                    cmd.CommandText = " INSERT INTO estores (ID, Referência, Família, Descrição, Cor, Preço, Imagem) VALUES (@ID, @Referência, @Família, @Descrição, @Cor, @Preço, @Imagem)";
                }
                if (rb_complemetos.Checked == true)
                {
                    cmd.CommandText = " INSERT INTO complementos (ID, Referência, Família, Descrição, Cor, Preço, Imagem) VALUES (@ID, @Referência, @Família, @Descrição, @Cor, @Preço, @Imagem)";
                }
                cmd.ExecuteNonQuery();//grava
                cnn.Close(); //fexa cnn
            }
            pre_pes_tb();//up au menu
            novo_re_admin();
        }
        //voids de b_pes_Click
        public void pes_portas()
        {
            string sql_s = "";
            sql_string = "select * from portas";

            //pes por cor
            if (cb_cor_portas.Items.Count != 0 && cb_cor_portas.Text != "TODAS")
            {
                sql_s = sql_s + "Cor ='" + (cb_cor_portas.Text) + "'";
            }
            //rb_portas_vidro
            if (rb_portas_vidro_sim.Checked == true)
            {
                if (sql_s != "")
                {
                    sql_s = sql_s + " and ";
                }
                sql_s = sql_s + "Vidro ='true'";
            }
            if (rb_portas_vidro_nao.Checked == true)
            {
                if (sql_s != "")
                {
                    sql_s = sql_s + " and ";
                }
                sql_s = sql_s + "Vidro ='false'";
            }
            //rb_portas_grlha
            if (rb_portas_grlha_sim.Checked == true)
            {
                if (sql_s != "")
                {
                    sql_s = sql_s + " and ";
                }
                sql_s = sql_s + "Grelha ='true'";
            }
            if (rb_portas_grlha_nao.Checked == true)
            {
                if (sql_s != "")
                {
                    sql_s = sql_s + " and ";
                }
                sql_s = sql_s + "Grelha ='false'";
            }
            //pes por ref
            if (tb_pes_portas.Text != "")
            {

                sql_s = "Referência ='" + (tb_pes_portas.Text) + "'";
            }
            //mota a  sql_string
            if (sql_s != "")
            {
                sql_string = "select * from portas where " + sql_s;
            }

            pes_tb();//pes se tiver serto
        }
        public void pes_estores()
        {
            sql_string = "select * from estores";
            string sql_s = "";

            if (cb_cor_estores.Text != "TODAS")//pes por cor
            {
                sql_s = "Cor ='" + (cb_cor_estores.Text) + "'";
            }
            if (cb_familia_estores.Text != "TODAS")//pes por Família
            {
                if (sql_s != "")
                {
                    sql_s = sql_s + " and ";
                }
                sql_s = sql_s + "Família'" + (cb_familia_estores.Text) + "'";
            }
            //mota a  sql_string
            if (sql_s != "")
            {
                sql_string = "select * from estores where " + sql_s;
            }
            pes_tb();//pes se tiver serto
        }
        public void pes_puxadores()
        {
            sql_string = "select * from puxadores";
            string sql_s = "";

            if (cb_cor_puxadores.Text != "TODAS")//pes por cor
            {
                sql_s = "Cor ='" + (cb_cor_puxadores.Text) + "'";
            }
            if (cb_familia_puxadores.Text != "TODAS")//pes por Família
            {
                if (sql_s != "")
                {
                    sql_s = sql_s + " and ";
                }
                sql_s = sql_s + "Família='" + (cb_familia_puxadores.Text) + "'";
            }
            //mota a  sql_string
            if (sql_s != "")
            {
                sql_string = "select * from puxadores where " + sql_s;
            }
            pes_tb();//pes se tiver serto
        }
        public void pes_assitencias()
        {
            sql_string = "select * from assistencias";
            string sql_s = "";

            if (cb_familia_assitencias.Text != "TODAS")//pes por Família
            {
                sql_s = "Família='" + (cb_familia_assitencias.Text) + "'";
            }
            //mota a  sql_string
            if (sql_s != "")
            {
                sql_string = "select * from assitencias where " + sql_s;
            }
            pes_tb();//pes se tiver serto
        }
        public void pes_complementos()
        {
            sql_string = "select * from complementos";
            string sql_s = "";

            if (cb_cor_complementos.Text != "TODAS")//pes por cor
            {
                sql_s = "Cor ='" + (cb_cor_complementos.Text) + "'";
            }
            if (cb_familia_complementos.Text != "TODAS")//pes por Família
            {
                if (sql_s != "")
                {
                    sql_s = sql_s + " and ";
                }
                sql_s = sql_s + "Família='" + (cb_familia_complementos.Text) + "'";
            }
            //mota a  sql_string
            if (sql_s != "")
            {
                sql_string = "select * from complementos where " + sql_s;
            }
            pes_tb();//pes se tiver serto


        }
        //voids do pdf 
        public void faz_pdf()
        {
            //SqlConnection cnn = new SqlConnection("Data Source=192.168.1.2,1433; Network Library=DBMSSOCN;Initial Catalog=catalogo; User ID=admin;Password=caixilour");//casa
            SqlConnection cnn = new SqlConnection("Data Source=192.168.2.39,1433; Network Library=DBMSSOCN;Initial Catalog=Catalgo; User ID=admin;Password=caixilour1");  //coneção//
            //ligar tab
            //sql_string = sql_string_pdf;
            //pes tab inf
            cnn.Open();
            da_tab = new SqlDataAdapter(sql_string, cnn);
            dat_tab_tab = new System.Data.DataTable();
            da_tab.Fill(dat_tab_tab);
            maxrows = dat_tab_tab.Rows.Count;
            cnn.Close();
            var retangulo = new iTextSharp.text.Rectangle(765, 1065);
            var documento = new Document(retangulo);
            String nomearquivoPdf = Path.GetTempPath() + "catalogo" + " de " + st + DateTime.Now.ToString("dd_MM_yyyy") + ".pdf";

            //caminho = caminho  + "\pdf\teste.pdf";
            var writer = PdfWriter.GetInstance(documento, new FileStream(nomearquivoPdf, FileMode.Create));
            documento.Open();
            PdfContentByte cb = writer.DirectContent;
            BaseFont outraFonte = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1252, false, false);
            int p = maxrows / 7;
            int i;
            int pi;
            int pif = maxrows / 7;
            for (ii = 0; ii < maxrows; ii++)
            {
                //label46.Text = Convert.ToString(p);
                //cabeçalho
                //ret
                var contentByte = writer.DirectContent;
                contentByte.Rectangle(10, 1000, 750, 60);
                contentByte.Stroke();
                //logo
                var imagem = iTextSharp.text.Image.GetInstance(pb_logo.BackgroundImage, System.Drawing.Imaging.ImageFormat.Png);
                imagem.SetAbsolutePosition(0, 970);
                imagem.ScaleToFit(200, 100);
                documento.Add(imagem);
                //inf do logo
                cb.BeginText();
                cb.SetFontAndSize(outraFonte, 18);
                cb.SetColorFill(new BaseColor(51, 51, 51));
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, st, 607, 1000, 0);
                cb.EndText();
                //tab
                //tab inf
                contentByte = writer.DirectContent;
                contentByte.Rectangle(10, 970, 125, 20);
                contentByte = writer.DirectContent;
                contentByte.Rectangle(10, 970, 565, 20);
                contentByte = writer.DirectContent;
                contentByte.Rectangle(10, 970, 670, 20);
                contentByte = writer.DirectContent;
                contentByte.Rectangle(10, 970, 740, 20);
                contentByte.Stroke();
                //inf da tab
                cb.BeginText();
                cb.SetFontAndSize(outraFonte, 12);
                cb.SetColorFill(new BaseColor(51, 51, 51));
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "IMAGENS", 42, 974, 0);
                cb.EndText();
                cb.BeginText();
                cb.SetFontAndSize(outraFonte, 12);
                cb.SetColorFill(new BaseColor(51, 51, 51));
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "DESCRIÇÃO", 322, 974, 0);
                cb.EndText();
                cb.BeginText();
                cb.SetFontAndSize(outraFonte, 12);
                cb.SetColorFill(new BaseColor(51, 51, 51));
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "REFERÊNCIA", 587, 974, 0);
                cb.EndText();
                cb.BeginText();
                cb.SetFontAndSize(outraFonte, 12);
                cb.SetColorFill(new BaseColor(51, 51, 51));
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "PREÇO", 697, 974, 0);
                cb.EndText();
                //tab inferior
                for (i = ii; i < 8 + ii; i++)
                {
                    ii = i;
                    if (maxrows == i)
                    {
                        // ii = ii+iii;
                        break;
                    }
                    if (iii == 9)
                    {
                        break;
                    }

                    contentByte = writer.DirectContent;
                    contentByte.Rectangle(10, (870 - 100 * iii), 125, 90);
                    contentByte.Stroke();
                    contentByte = writer.DirectContent;
                    contentByte.Rectangle(10, (870 - 100 * iii), 565, 90);
                    contentByte.Stroke();
                    contentByte = writer.DirectContent;
                    contentByte.Rectangle(10, (870 - 100 * iii), 670, 90);
                    contentByte.Stroke();
                    contentByte = writer.DirectContent;
                    contentByte.Rectangle(10, (870 - 100 * iii), 740, 90);
                    contentByte.Stroke();
                    //inf da tab
                    Byte[] fotos = (byte[])dat_tab_tab.Rows[i]["Imagem"];
                    MemoryStream ms = new MemoryStream(fotos);
                    System.Drawing.Image fotos_s = System.Drawing.Image.FromStream(ms);

                    imagem = iTextSharp.text.Image.GetInstance(fotos_s, System.Drawing.Imaging.ImageFormat.Png);
                    imagem.SetAbsolutePosition(10, (870 - 100 * iii));
                    imagem.ScaleToFit(130, 90);
                    documento.Add(imagem);
                    cb.BeginText();
                    cb.SetFontAndSize(outraFonte, 12);
                    cb.SetColorFill(new BaseColor(51, 51, 51));
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Convert.ToString(dat_tab_tab.Rows[i]["descrição"]), 142, (930 - 100 * iii), 0);// painel uma almofada com vidro com greilha
                    cb.EndText();
                    cb.BeginText();
                    cb.SetFontAndSize(outraFonte, 12);
                    cb.SetColorFill(new BaseColor(51, 51, 51));
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Convert.ToString(dat_tab_tab.Rows[i]["Referência"]), 589, (930 - 100 * iii), 0);
                    cb.EndText();
                    cb.BeginText();
                    cb.SetFontAndSize(outraFonte, 12);
                    cb.SetColorFill(new BaseColor(51, 51, 51));
                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Convert.ToString(dat_tab_tab.Rows[i]["Preço"]) + "€", 690, (930 - 100 * iii), 0);
                    cb.EndText();
                    iii = iii + 1;
                }
                //rodape
                //ret
                contentByte = writer.DirectContent;
                contentByte.Rectangle(10, 10, 750, 50);
                contentByte.Stroke();
                //inf
                cb.BeginText();
                cb.SetFontAndSize(outraFonte, 12);
                cb.SetColorFill(new BaseColor(51, 51, 51));
                pi = +1;

                if (pif == 0)
                {
                    pif = 1;
                }
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Pagina " + pi + " de " + pif, 647, 20, 0);
                cb.EndText();
                documento.NewPage();
                iii = 0;
                if (ii == maxrows - 1)
                {
                    break;
                }
            }
            documento.Close();
            Process.Start(nomearquivoPdf);

        }
        //rb
        private void rb_portas_CheckedChanged(object sender, EventArgs e)
        {
            if (st != "Portas")
            {
                //panel_pes_portas.Visible = true;
                   sql_string = "select * from portas";
                panel_imagens.Visible = true;
                panel_pes_assitencias.Visible = false;
                panel_d_portas.Visible = true;
                panel_admin_extras.Visible = true;
                panel_admin_imagens.Visible = true;
                panel_admin_familia.Visible = false;
                cb_cor_admin.Visible = true;
                label40.Visible = true;
                pre_pes_tb();//abre e mostra tab
                pes_cor();
                st = "Portas";
                cb_tabela_admin.Text = st;
                panel_pes_portas.Visible = true;
            }
        }
        private void rb_estores_CheckedChanged(object sender, EventArgs e)
        {
            if (st != "Estores")
            {
                sql_string = "select * from estores";
                panel_imagens.Visible = false;
                panel2.Location = new Point(0, 194);
                panel_pes_assitencias.Visible = false;
                panel_d_portas.Visible = false;
                panel_admin_extras.Visible = false;
                panel_admin_familia.Visible = true;
                cb_cor_admin.Visible = true;
                panel_admin_imagens.Visible = false;
                label40.Visible = true;
                pre_pes_tb();
                pes_cor();
                pes_familia();
                st = "Estores";
                cb_tabela_admin.Text = st;
                panel_pes_estores.Visible = true;
            }
        }
        private void rb_puxadores_CheckedChanged(object sender, EventArgs e)
        {
            if (st != "Puxadores")
            {
                sql_string = "select * from puxadores";
                panel_imagens.Visible = false;
                panel2.Location = new Point(0, 194);
                panel_pes_assitencias.Visible = false;
                panel_d_portas.Visible = false;
                panel_admin_extras.Visible = false;
                panel_admin_familia.Visible = true;
                panel_admin_imagens.Visible = false;
                cb_cor_admin.Visible = true;
                label40.Visible = true;
                pre_pes_tb();
                pes_cor();
                pes_familia();
                st = "Puxadores";
                cb_tabela_admin.Text = st;
                panel_pes_puxadores.Visible = true;
            }
        }
        private void rb_complemetos_CheckedChanged(object sender, EventArgs e)
        {
            if (st != "Complementos")
            {
                sql_string = "select * from complementos";
                panel_imagens.Visible = false;
                panel2.Location = new Point(0, 194);
                panel_pes_assitencias.Visible = false;
                panel_d_portas.Visible = false;
                panel_admin_extras.Visible = false;
                panel_admin_familia.Visible = true;
                cb_cor_admin.Visible = true;
                label40.Visible = true;
                panel_admin_imagens.Visible = false;
                pre_pes_tb();
                pes_cor();
                pes_familia();
                st = "Complementos";
                cb_tabela_admin.Text = st;
                
                panel_pes_complementos.Visible = true;
            }
        }
        private void rb_assitencias_CheckedChanged(object sender, EventArgs e)
        {
            if (st != "Assistências")
            {
                sql_string = "select * from assistencias";
                panel_imagens.Visible = false;
                panel2.Location = new Point(0, 158);//453
                panel_pes_assitencias.Visible = true;
                panel_d_portas.Visible = false;
                label40.Visible = true;
                cb_cor_admin.Visible = false;
                panel_admin_familia.Visible = true;
                cb_cor_admin.Visible = false;
                label40.Visible = false;
                panel_admin_imagens.Visible = false;
                panel_admin_extras.Visible = false;
                pre_pes_tb();
                pes_familia();
                st = "Assistências";
                cb_tabela_admin.Text = st;
                panel_pes_assitencias.Visible = true;
            }
        }
        //rb_prodotos tabs
        private void i_prod_portas_Click(object sender, EventArgs e)
        {
            rb_portas.Checked = true;
        }
        private void i_prod_estores_Click(object sender, EventArgs e)
        {
            rb_estores.Checked = true;
        }
        private void i_prod_puxadores_Click(object sender, EventArgs e)
        {
            rb_puxadores.Checked = true;
        }
        private void i_prod_complemetos_Click(object sender, EventArgs e)
        {
            rb_complemetos.Checked = true;
        }
        private void i_prod_assitencias_Click(object sender, EventArgs e)
        {
            rb_assitencias.Checked = true;
        }
        //menu
        private int strlen(object p)
        {
            throw new NotImplementedException();
        }//mostra o resoltado das tabs
        private MemoryStream MemoryStream(byte[] fotos)
        {
            throw new NotImplementedException();
        }
        //cb_cor
        private void cb_cor_portas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (b == true)
            {
                preencher_admim();
                pes_portas();
            }
        }
        private void cb_cor_estores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (b == true)
            {
                pes_estores();
            }
        }
        private void cb_cor_puxadores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (b == true)
            {
                pes_puxadores();
            }
        }
        private void cb_cor_complementos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (b == true)
            {
                pes_complementos();
            }
        }
        //cb_familia
        private void cb_familia_estores_SelectedIndexChanged(object sender, EventArgs e)
        {
            pes_estores();
        }
        private void cb_familia_puxadores_SelectedIndexChanged(object sender, EventArgs e)
        {
            pes_puxadores();
        }
        private void cb_familia_assitencias_SelectedIndexChanged(object sender, EventArgs e)
        {
            pes_assitencias();
        }
        private void cb_familia_complementos_SelectedIndexChanged(object sender, EventArgs e)
        {
            pes_complementos();
        }
        //rb portas 
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (b == true)
            {
                pes_portas();
            }
        }
        private void rb_portas_vidro_s_CheckedChanged(object sender, EventArgs e)
        {
            if (b == true)
            {
                pes_portas();
            }
        }
        private void rb_portas_vidro_nao_CheckedChanged(object sender, EventArgs e)
        {
            if (b == true)
            {
                pes_portas();
            }
        }
        private void rb_portas_grlha_sim_CheckedChanged(object sender, EventArgs e)
        {
            if (b == true)
            {
                pes_portas();
            }
        }
        private void rb_portas_vidro_sim_CheckedChanged(object sender, EventArgs e)
        {
            if (b == true)
            {
                pes_portas();
            }
        }
        private void rb_portas_grlha_nao_CheckedChanged(object sender, EventArgs e)
        {
            if (b == true)
            {
                pes_portas();
            }
        }
        //tb_pes pes por 1 ref
        private void tb_pes_portas_TextChanged(object sender, EventArgs e)
        {
            sql_string = "select * from portas where Referência LIKE'" + (tb_pes_portas.Text) + "%'";
            pes_tb();
        }
        private void tb_pes_estores_TextChanged(object sender, EventArgs e)
        {
            sql_string = "select * from estores where Referência LIKE'" + (tb_pes_estores.Text) + "'";
            pes_tb();
        }
        private void tb_pes_puxadores_TextChanged(object sender, EventArgs e)
        {
            sql_string = "select * from puxadores where Referência LIKE'" + (tb_pes_puxadores.Text) + "'";
            pes_tb();
        }
        private void tb_pes_complemetos_TextChanged(object sender, EventArgs e)
        {
            sql_string = "select * from complemetos where Referência LIKE'" + (tb_pes_complemetos.Text) + "'";
            pes_tb();
        }
        private void tb_pes_assitencias_TextChanged(object sender, EventArgs e)
        {
            sql_string = "select * from assistencias where Referência LIKE'" + (tb_pes_assitencias.Text) + "'";
            pes_tb();
        }
        //bt_escolha admin/utilizador/fotocopiar
        private void bt_ok_Click(object sender, EventArgs e)
        {
            pes_utilizador();
        }
        private void BT_admin_Click(object sender, EventArgs e)
        {
            BT_fotocopiar.BackColor = Color.White;
            BT_admin.BackColor = Color.YellowGreen;
            BT_utilizador.BackColor = Color.White;
            BT_fotocopiar.Visible = false;
            panel_pes.Enabled = false;
            panel_menu.Enabled = false;
            panel_admin.Visible = true;
            panel_admin.Enabled = false;
            panel10.Visible = true;
            panel_menu.Location = new Point(222, 336);
            //panel_menu.Size = new Size(809, 433);
            BT_add_utilisador.Visible = false;
        }
        private void bt_cancelar_Click(object sender, EventArgs e)
        {
            BT_admin.BackColor = Color.White;
            BT_utilizador.BackColor = Color.YellowGreen;
            panel_pes.Enabled = true;
            panel_menu.Enabled = true;
            panel_admin.Visible = false;
            panel10.Visible = false;
            panel_menu.Location = new Point(222, 9);
            panel_menu.Size = new Size(809, 760);
        }
        private void BT_fotocopiar_Click(object sender, EventArgs e)
        {
            faz_pdf();
        }
        private void BT_utilizador_Click(object sender, EventArgs e)
        {
            BT_admin.BackColor = Color.White;
            BT_utilizador.BackColor = Color.YellowGreen;
            BT_fotocopiar.BackColor = Color.White;
            BT_fotocopiar.Visible = false;
            panel_pes.Enabled = true;
            panel_menu.Enabled = true;
            panel_admin.Visible = false;
            panel10.Visible = false;
            panel_menu.Location = new Point(222, 9);
            panel_menu.Size = new Size(809, 760);
            BT_add_utilisador.Visible = false;
        }
        private void BT_add_utilisador_Click(object sender, EventArgs e)
        {
            form3.ShowDialog();
        }
        //in_fotos
        private void pb_foto_i_0_Click(object sender, EventArgs e)
        {
            odf.Filter = "seleciona a imagem (*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp";
            if (odf.ShowDialog() == DialogResult.OK)
            {
                pb_foto_i_0.BackgroundImage = System.Drawing.Image.FromFile(odf.FileName);
            }
        }
        private void pb_foto_i_1_Click(object sender, EventArgs e)
        {
            odf.Filter = "seleciona a imagem (*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp";
            if (odf.ShowDialog() == DialogResult.OK)
            {
                pb_foto_i_1.BackgroundImage = System.Drawing.Image.FromFile(odf.FileName);
            }
        }
        private void pb_foto_i_2_Click(object sender, EventArgs e)
        {
            odf.Filter = "seleciona a imagem (*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp";
            if (odf.ShowDialog() == DialogResult.OK)
            {
                pb_foto_i_2.BackgroundImage = System.Drawing.Image.FromFile(odf.FileName);
            }
        }
        private void pb_foto_i_3_Click(object sender, EventArgs e)
        {
            odf.Filter = "seleciona a imagem (*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp";
            if (odf.ShowDialog() == DialogResult.OK)
            {
                pb_foto_i_3.BackgroundImage = System.Drawing.Image.FromFile(odf.FileName);
            }

        }
        private void pb_foto_i_4_Click(object sender, EventArgs e)
        {
            odf.Filter = "seleciona a imagem (*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp";
            if (odf.ShowDialog() == DialogResult.OK)
            {
                pb_foto_i_4.BackgroundImage = System.Drawing.Image.FromFile(odf.FileName);
            }
        }
        private void pb_foto_i_5_Click(object sender, EventArgs e)
        {
            odf.Filter = "seleciona a imagem (*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp";
            if (odf.ShowDialog() == DialogResult.OK)
            {
                pb_foto_i_5.BackgroundImage = System.Drawing.Image.FromFile(odf.FileName);
            }
        }
        private void pb_foto_i_6_Click(object sender, EventArgs e)
        {
            odf.Filter = "seleciona a imagem (*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp";
            if (odf.ShowDialog() == DialogResult.OK)
            {
                pb_foto_i_6.BackgroundImage = System.Drawing.Image.FromFile(odf.FileName);
            }
        }
        //grpo da parte de guardar/delete...
        private void b_novo_Click(object sender, EventArgs e)
        {
            novo_re_admin();
        }
        private void b_delete_Click(object sender, EventArgs e)
        {
            delete_re_admin();
        }
        private void b_guardar_Click(object sender, EventArgs e)
        {
            if (pb_foto_i_0.BackgroundImage != null && tb_id_admin.Text != "" && tb_referencia_admin.Text != "")
            {
                if (nr == true)
                {
                    guardar_novo_re_admin();
                }
                else
                {
                    if (mod_r == true)
                    {
                        update_re_admin();
                    }
                    else
                    {
                        MessageBox.Show("Não foi possivel guardar o produto pois não possui autorização para tal", "Erro na gravação",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Não foi possivel guardar o produto pois existe um ou mais canpos ai branco", "Erro na gravação",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //form 3 pb
        private void pb_foto_Click(object sender, EventArgs e)
        {

            form2.ShowDialog();


        }
        private void pb_foto_1_Click(object sender, EventArgs e)
        {
            form2.ShowDialog();
        }
        private void pb_foto_5_Click(object sender, EventArgs e)
        {
            form2.ShowDialog();
        }
        private void pb_foto_2_Click(object sender, EventArgs e)
        {
            form2.ShowDialog();
        }
        private void pb_foto_3_Click(object sender, EventArgs e)
        {
            form2.ShowDialog();
        }
        private void pb_foto_4_Click(object sender, EventArgs e)
        {
            form2.ShowDialog();
        }
        private void pb_foto_6_Click(object sender, EventArgs e)
        {
            form2.ShowDialog();
        }
        private void panel_menu_Paint(object sender, PaintEventArgs e)
        {
            panel_menu.AutoScroll = true;
            System.Drawing.Rectangle r = this.ClientRectangle;
        }

        private void odf_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
