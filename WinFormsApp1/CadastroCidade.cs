using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Cms;

namespace WinFormsApp1
{
    public partial class CadastroCidade : Form
    {
        private MySqlConnection Conexao;
        

        string data_source = "datasource=localhost; username=root; password =; database = cadastro_cidade";
        private int ?id_city_selected = null;

        public CadastroCidade()
        {
            InitializeComponent();

            lstCity.View = View.Details;
            lstCity.LabelEdit = true;
            lstCity.AllowColumnReorder = true;
            lstCity.FullRowSelect = true;
            lstCity.GridLines = true;

            lstCity.Columns.Add("ID", 30, HorizontalAlignment.Left);
            lstCity.Columns.Add("NOME", 150, HorizontalAlignment.Left);
            lstCity.Columns.Add("UF", 30, HorizontalAlignment.Left);

            load_cities();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //Criar conexao mysql
                Conexao = new MySqlConnection(data_source);

                Conexao.Open();

                MySqlCommand cmd = new MySqlCommand();
                
                cmd.Connection = Conexao;

                id_city_selected = null;

                if(id_city_selected == null)
                {
                    cmd.CommandText = "INSERT INTO cidade (id, nome, uf)" + "VALUES " + "(@id, @nome, @uf) ";

                    cmd.Parameters.AddWithValue("@id", txtID.Text);
                    cmd.Parameters.AddWithValue("@nome", txtCity.Text);
                    cmd.Parameters.AddWithValue("@uf", txtUF.Text);

                    cmd.ExecuteNonQuery();

                    //Executar comando insert
                    MessageBox.Show("Informações inseridas!");
                }

                txtID.Text = "";
                txtCity.Text = "";
                txtUF.Text = "";

                load_cities();

            }
            catch(Exception ex)
            {
                MessageBox.Show("Ocorreu um erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexao.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                //Criar conexao mysql
                Conexao = new MySqlConnection(data_source);

                Conexao.Open();

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = Conexao;
                if (id_city_selected != null)
                {
                    cmd.CommandText = "DELETE FROM cidade " + "WHERE nome=@nome";

                    cmd.Parameters.AddWithValue("@id", id_city_selected);
                    cmd.Parameters.AddWithValue("@nome", txtCity.Text);
                    cmd.Parameters.AddWithValue("@uf", txtUF.Text);

                    cmd.ExecuteNonQuery();

                    //Executar comando insert
                    MessageBox.Show("Informações deletadas!");

                }

                txtID.Text = "";
                txtCity.Text = "";
                txtUF.Text = "";

                load_cities();

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

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                //Criar conexao mysql
                Conexao = new MySqlConnection(data_source);

                Conexao.Open();

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = Conexao;

                cmd.CommandText = "SELECT * FROM cidade WHERE id LIKE @search OR nome LIKE @search";

                cmd.Parameters.AddWithValue("@search", "%" + txtSearch.Text + "%");

                cmd.ExecuteNonQuery();

                MySqlDataReader reader = cmd.ExecuteReader();

                lstCity.Items.Clear();

                //filtrando resultados
                while (reader.Read())
                {
                    string[] row =
                    {
                        reader.GetString(0),
                        reader.GetString(1),
                        reader.GetString(2),
                    };

                    lstCity.Items.Add(new ListViewItem(row));
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
        private void load_cities()
        {
            try
            {
                //Criar conexao mysql
                Conexao = new MySqlConnection(data_source);

                Conexao.Open();

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = Conexao;

                cmd.CommandText = "SELECT * FROM cidade ORDER BY id ASC ";

                cmd.ExecuteNonQuery();

                MySqlDataReader reader = cmd.ExecuteReader();

                lstCity.Items.Clear();

                //filtrando resultados
                while (reader.Read())
                {
                    string[] row =
                    {
                        reader.GetString(0),
                        reader.GetString(1),
                        reader.GetString(2),
                    };

                    lstCity.Items.Add(new ListViewItem(row));
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //Criar conexao mysql
                Conexao = new MySqlConnection(data_source);

                Conexao.Open();

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = Conexao;
                if(id_city_selected != null)
                {
                    cmd.CommandText = "UPDATE cidade SET id=@id, nome=@nome, uf=@uf " + "WHERE id=@id";

                    cmd.Parameters.AddWithValue("@id", id_city_selected);
                    cmd.Parameters.AddWithValue("@nome", txtCity.Text);
                    cmd.Parameters.AddWithValue("@uf", txtUF.Text);

                    cmd.ExecuteNonQuery();

                    //Executar comando insert
                    MessageBox.Show("Informações atualizadas!");

                }

                txtID.Text = "";
                txtCity.Text = "";
                txtUF.Text = "";

                load_cities();

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

        private void lstCity_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            ListView.SelectedListViewItemCollection selected_items = lstCity.SelectedItems;

            foreach(ListViewItem item in selected_items)
            {
                id_city_selected = Convert.ToInt32(item.SubItems[0].Text);
                txtID.Text = item.SubItems[0].Text;
                txtCity.Text = item.SubItems[1].Text;
                txtUF.Text = item.SubItems[2].Text;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                //Criar conexao mysql
                Conexao = new MySqlConnection(data_source);

                Conexao.Open();

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = Conexao;
                if (id_city_selected != null)
                {
                    cmd.CommandText = "DELETE FROM cidade " + "WHERE nome=@nome";

                    cmd.Parameters.AddWithValue("@id", id_city_selected);
                    cmd.Parameters.AddWithValue("@nome", txtCity.Text);
                    cmd.Parameters.AddWithValue("@uf", txtUF.Text);

                    cmd.ExecuteNonQuery();

                    //Executar comando insert
                    MessageBox.Show("Excluindo...");

                }

                txtID.Text = "";
                txtCity.Text = "";
                txtUF.Text = "";

                load_cities();

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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Application.Run(new telaSelecao());
        }
    }
}
