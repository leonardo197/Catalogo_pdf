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
    public partial class Form3 : Form
    {
        //bd
        //SqlConnection cnn = new SqlConnection("Data Source=192.168.1.2,1433; Network Library=DBMSSOCN;Initial Catalog=catalogo; User ID=admin;Password=caixilour");
        SqlConnection cnn = new SqlConnection("Data Source=192.168.2.39,1433; Network Library=DBMSSOCN;Initial Catalog=Catalgo; User ID=admin;Password=caixilour1");  //coneção
        string sql_string;  //select ... from
        SqlDataAdapter da_tab;
        SqlCommand cmd;
        DataTable dat_tab_tab;
        //var
        Boolean nr = true;
        int maxrows;
        int i = 0;
        public Form3()
        {
            InitializeComponent();
        }
        public void rn()
        {
            //sql_string = "select DISTINCT Nome from utilizador";
            sql_string = "select * from utilizador";

            ////ligar tab
            //cnn = new SqlConnection("Data Source=192.168.3.13,1433; Network Library=DBMSSOCN;Initial Catalog=caixilour_estoque; User ID=admin;Password=caixilour");
            cnn.Open();
            da_tab = new SqlDataAdapter(sql_string, cnn);
            dat_tab_tab = new System.Data.DataTable();
            da_tab.Fill(dat_tab_tab);
            cnn.Close();

            listBox1.Items.Clear();
            maxrows = dat_tab_tab.Rows.Count;
            for (int i = 0; i < maxrows; i++)
            {
                listBox1.Items.Add(Convert.ToString(dat_tab_tab.Rows[i]["Nome"]));
            }
        }
        private void Form3_Load(object sender, EventArgs e)
        {
            rn();
        }
        private void b_novo_Click(object sender, EventArgs e)
        {
            NOME_Utelirador.Text = "";
            PASSE_Utelirador.Text = "";
            cb_fotocopiar.Checked = false;
            cb_add_registos.Checked = false;
            cb_eliminar_registos.Checked = false;
            cb_Modificar_Registros.Checked = false;
            cb_Criar_Utilizadores.Checked = false;
            //listBox1.Refresh();
            //listBox1.Update();
            nr = true;
            rn();
        }
        private void b_delete_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand();
            cmd.Connection = cnn;
            cnn.Open();
            cmd.CommandText = " DELETE FROM  utilizador WHERE ID_Utilizador=" + Convert.ToString(dat_tab_tab.Rows[i]["ID_Utilizador"]);
            cmd.ExecuteNonQuery();
            cnn.Close();

            NOME_Utelirador.Text = "";
            PASSE_Utelirador.Text = "";
            cb_fotocopiar.Checked = false;
            cb_add_registos.Checked = false;
            cb_eliminar_registos.Checked = false;
            cb_Modificar_Registros.Checked = false;
            cb_Criar_Utilizadores.Checked = false;
            nr = true;
            rn();
        }
        private void b_guardar_Click(object sender, EventArgs e)
        {
            if (NOME_Utelirador.Text == "" | PASSE_Utelirador.Text == "")
            {
                if (MessageBox.Show("Algum campo foi colocado vazio pode ocasionar uma falha na segoransa, se foi proposital click Ok", "Erro na criação de utilizador",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    //variavais
                    Boolean Fotocopiar = false;
                    Boolean Criar_Registos = false;
                    Boolean Eliminar_Registos = false;
                    Boolean Modificar_Registros = false;
                    Boolean Criar_Utilizadores = false;

                    cnn.Open();//liga

                    //prepara cb para guardar
                    if (cb_fotocopiar.Checked == true)
                    {
                        Fotocopiar = true;
                    }
                    if (cb_Criar_Utilizadores.Checked == true)
                    {
                        Criar_Utilizadores = true;
                    }
                    if (cb_Modificar_Registros.Checked == true)
                    {
                        Modificar_Registros = true;
                    }
                    if (cb_eliminar_registos.Checked == true)
                    {
                        Eliminar_Registos = true;
                    }
                    if (cb_add_registos.Checked == true)
                    {
                        Criar_Registos = true;
                    }

                    cmd = new SqlCommand();
                    cmd.Connection = cnn;
                    //requecitos para guardar
                    cmd.Parameters.Add(new SqlParameter("@Nome", Convert.ToString(NOME_Utelirador.Text)));
                    cmd.Parameters.Add(new SqlParameter("@Passe", Convert.ToString(PASSE_Utelirador.Text)));
                    cmd.Parameters.Add(new SqlParameter("@Fotocopiar", Convert.ToBoolean(Fotocopiar)));
                    cmd.Parameters.Add(new SqlParameter("@Criar_Registos", Convert.ToBoolean(Criar_Registos)));
                    cmd.Parameters.Add(new SqlParameter("@Eliminar_Registos", Convert.ToBoolean(Eliminar_Registos)));
                    cmd.Parameters.Add(new SqlParameter("@Modificar_Registros", Convert.ToBoolean(Modificar_Registros)));
                    cmd.Parameters.Add(new SqlParameter("@Criar_Utilizadores", Convert.ToBoolean(Criar_Utilizadores)));


                    if (nr == true)
                    {
                        //diz o que para gravar e onde
                        cmd.CommandText = " INSERT INTO utilizador (Nome, Passe, Fotocopiar, Criar_Registos, Eliminar_Registos, Modificar_Registros, Criar_Utilizadores) VALUES (@Nome, @Passe, @Fotocopiar, @Criar_Registos, @Eliminar_Registos, @Modificar_Registros, @Criar_Utilizadores)";

                    }

                    else
                    {
                        cmd.CommandText = " UPDATE utilizador SET Nome=@Nome, Passe=@Passe, Fotocopiar=@Fotocopiar, Criar_Registos=@Criar_Registos, Eliminar_Registos=@Eliminar_Registos, Criar_Utilizadores=@Criar_Utilizadores WHERE ID_Utilizador=" + Convert.ToString(dat_tab_tab.Rows[i]["ID_Utilizador"]);
                    }
                    cmd.ExecuteNonQuery();//grava
                    cnn.Close(); //fexa cnn
                    rn();

                }
            }
            else
            {
                cmd = new SqlCommand();
                cmd.Connection = cnn;
                //variavais
                Boolean Fotocopiar = false;
                Boolean Criar_Registos = false;
                Boolean Eliminar_Registos = false;
                Boolean Modificar_Registros = false;
                Boolean Criar_Utilizadores = false;

                cnn.Open();//liga

                //prepara cb para guardar
                if (cb_fotocopiar.Checked == true)
                {
                    Fotocopiar = true;
                }
                if (cb_Criar_Utilizadores.Checked == true)
                {
                    Criar_Utilizadores = true;
                }
                if (cb_Modificar_Registros.Checked == true)
                {
                    Modificar_Registros = true;
                }
                if (cb_eliminar_registos.Checked == true)
                {
                    Eliminar_Registos = true;
                }
                if (cb_add_registos.Checked == true)
                {
                    Criar_Registos = true;
                }

                cmd = new SqlCommand();
                cmd.Connection = cnn;
                //requecitos para guardar
                cmd.Parameters.Add(new SqlParameter("@Nome", Convert.ToString(NOME_Utelirador.Text)));
                cmd.Parameters.Add(new SqlParameter("@Passe", Convert.ToString(PASSE_Utelirador.Text)));
                cmd.Parameters.Add(new SqlParameter("@Fotocopiar", Convert.ToBoolean(Fotocopiar)));
                cmd.Parameters.Add(new SqlParameter("@Criar_Registos", Convert.ToBoolean(Criar_Registos)));
                cmd.Parameters.Add(new SqlParameter("@Eliminar_Registos", Convert.ToBoolean(Eliminar_Registos)));
                cmd.Parameters.Add(new SqlParameter("@Modificar_Registros", Convert.ToBoolean(Modificar_Registros)));
                cmd.Parameters.Add(new SqlParameter("@Criar_Utilizadores", Convert.ToBoolean(Criar_Utilizadores)));
                if (nr == true)
                {
                    //diz o que para gravar e onde
                    cmd.CommandText = " INSERT INTO utilizador (Nome, Passe, Fotocopiar, Criar_Registos, Eliminar_Registos, Modificar_Registros, Criar_Utilizadores) VALUES (@Nome, @Passe, @Fotocopiar, @Criar_Registos, @Eliminar_Registos, @Modificar_Registros, @Criar_Utilizadores)";

                }

                else
                {
                    cmd.CommandText = " UPDATE utilizador SET Nome=@Nome, Passe=@Passe, Fotocopiar=@Fotocopiar, Criar_Registos=@Criar_Registos, Eliminar_Registos=@Eliminar_Registos, Criar_Utilizadores=@Criar_Utilizadores WHERE ID_Utilizador=" + Convert.ToString(dat_tab_tab.Rows[i]["ID_Utilizador"]);
                }
                cmd.ExecuteNonQuery();//grava
                cnn.Close(); //fexa cnn
                rn();
            }

        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            i = listBox1.SelectedIndex;
            if (i == -1)
            {
                i = 0;
                listBox1.SelectedIndex = i;
            }
            NOME_Utelirador.Text = Convert.ToString(dat_tab_tab.Rows[i]["Nome"]);
            PASSE_Utelirador.Text = Convert.ToString(dat_tab_tab.Rows[i]["Passe"]);

            cb_fotocopiar.Checked = false;
            cb_add_registos.Checked = false;
            cb_eliminar_registos.Checked = false;
            cb_Modificar_Registros.Checked = false;
            cb_Criar_Utilizadores.Checked = false;
            if (Convert.ToString(dat_tab_tab.Rows[i]["Fotocopiar"]) == "True")
            {
                cb_fotocopiar.Checked = true;
            }
            if (Convert.ToString(dat_tab_tab.Rows[i]["Criar_Registos"]) == "True")
            {
                cb_add_registos.Checked = true;
            }
            if (Convert.ToString(dat_tab_tab.Rows[i]["Eliminar_Registos"]) == "True")
            {
                cb_eliminar_registos.Checked = true;
            }
            if (Convert.ToString(dat_tab_tab.Rows[i]["Modificar_Registros"]) == "True")
            {
                cb_Modificar_Registros.Checked = true;
            }
            if (Convert.ToString(dat_tab_tab.Rows[i]["Criar_Utilizadores"]) == "True")
            {
                cb_Criar_Utilizadores.Checked = true;
            }
            // rn();
            nr = false;
        }

        private void cb_Modificar_Registros_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
