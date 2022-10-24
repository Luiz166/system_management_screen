using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WinFormsApp1
{
    public partial class cadastroUsuario : Form
    {
        private MySqlConnection Conexao;

        string data_source = "datasource=localhost; username=root; password =; database = cadastro_cidade";


        public cadastroUsuario()
        {
            InitializeComponent();

            lstUser.View = View.Details;
            lstUser.LabelEdit = true;
            lstUser.AllowColumnReorder = true;
            lstUser.FullRowSelect = true;
            lstUser.GridLines = true;

            lstUser.Columns.Add("ID", 30, HorizontalAlignment.Left);
            lstUser.Columns.Add("NOME", 150, HorizontalAlignment.Left);
            lstUser.Columns.Add("SENHA", 150, HorizontalAlignment.Left);
            lstUser.Columns.Add("ADM", 50, HorizontalAlignment.Left);

            load_users();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            load_users();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Conexao = new MySqlConnection(data_source);

                Conexao.Open();

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = Conexao;

                cmd.CommandText = "INSERT INTO usuario (nome, senha, adm)" + "VALUES" + "(@nome, @senha, @adm)";

                cmd.Parameters.AddWithValue("@nome", txtName.Text);
                cmd.Parameters.AddWithValue("@senha", txtPswd.Text);
                cmd.Parameters.AddWithValue("@adm", checkAdm.Checked);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Informações inseridas!");

                load_users();

                txtName.Text = "";
                txtPswd.Text = "";

            }
            catch(Exception ex)
            {
                MessageBox.Show("Um erro ocorreu" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexao.Close();    
            }
        }

        private void checkAdm_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Conexao = new MySqlConnection(data_source);

                Conexao.Open();

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = Conexao;

                cmd.CommandText = "SELECT * FROM usuario WHERE id LIKE @search OR nome LIKE @search";

                cmd.Parameters.AddWithValue("@search", "%"+txtSearch.Text+"%");

                cmd.ExecuteNonQuery();

                MySqlDataReader reader = cmd.ExecuteReader();

                lstUser.Items.Clear();

                while (reader.Read())
                {
                    string[] row =
                    {
                        reader.GetString(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                    };
                    lstUser.Items.Add(new ListViewItem(row));
                }    

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro: "+ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexao.Close();
            }
        }
        private void load_users()
        {
            try
            {
                //Criar conexao mysql
                Conexao = new MySqlConnection(data_source);

                Conexao.Open();

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = Conexao;

                cmd.CommandText = "SELECT * FROM usuario ORDER BY id ASC ";

                cmd.ExecuteNonQuery();

                MySqlDataReader reader = cmd.ExecuteReader();

                lstUser.Items.Clear();

                //filtrando resultados
                while (reader.Read())
                {
                    string[] row =
                    {
                        reader.GetString(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                    };

                    lstUser.Items.Add(new ListViewItem(row));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexao.Close();
            }

        }

    }
}
