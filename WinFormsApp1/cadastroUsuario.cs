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

        private int ?id_user_selected = null;
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

            try
            {
            Conexao = new MySqlConnection(data_source);
            Conexao.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = Conexao;

                if(id_user_selected != null)
                {
                    cmd.CommandText = "UPDATE usuario SET nome = @nome, senha = @senha, adm = @adm WHERE id = @id";

                    cmd.Parameters.AddWithValue("@nome", txtName.Text);
                    cmd.Parameters.AddWithValue("@senha", txtPswd.Text);
                    cmd.Parameters.AddWithValue("@adm", checkAdm.Checked);
                    cmd.Parameters.AddWithValue("@id", txtID.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Informações atualizadas!");

                    load_users();

                    txtID.Text = "";
                    txtName.Text = "";
                    txtPswd.Text = "";
                    checkAdm.Checked = false;
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Ocorreu um erro: "+ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexao.Close();
            }
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

                cmd.CommandText = "INSERT INTO usuario (id, nome, senha, adm)" + "VALUES" + "(@id, @nome, @senha, @adm)";

                cmd.Parameters.AddWithValue("id", txtID.Text);
                cmd.Parameters.AddWithValue("@nome", txtName.Text);
                cmd.Parameters.AddWithValue("@senha", txtPswd.Text);
                cmd.Parameters.AddWithValue("@adm", checkAdm.Checked);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Informações inseridas!");

                load_users();

                txtID.Text = "";
                txtName.Text = "";
                txtPswd.Text = "";
                checkAdm.Checked = false;

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

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            telaSelecao ss = new telaSelecao();
            ss.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lstUser_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            ListView.SelectedListViewItemCollection selected_items = lstUser.SelectedItems;

            foreach (ListViewItem item in selected_items)
            {
                id_user_selected = Convert.ToInt32(item.SubItems[0].Text);
                txtID.Text = item.SubItems[0].Text;
                txtName.Text = item.SubItems[1].Text;
                txtPswd.Text = item.SubItems[2].Text;
                checkAdm.Text = item.SubItems[3].Text;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Conexao = new MySqlConnection(data_source);

                Conexao.Open();

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = Conexao;

                if(id_user_selected != null)
                {
                    cmd.CommandText = "DELETE FROM usuario WHERE nome = @nome";

                    cmd.Parameters.AddWithValue("@nome", txtName.Text);
                    cmd.Parameters.AddWithValue("@id", txtID.Text);
                    cmd.Parameters.AddWithValue("@senha", txtPswd.Text);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Informações deletadas!");

                    load_users();

                    txtID.Text = "";
                    txtName.Text = "";
                    txtPswd.Text = "";
                    checkAdm.Checked = false;
                }
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
    }
}
