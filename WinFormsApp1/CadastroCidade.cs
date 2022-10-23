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
    public partial class CadastroCidade : Form
    {
        private MySqlConnection Conexao;
        string data_source = "datasource=localhost; username=root; password =; database = cadastro_cidade";

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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //Criar conexao mysql

                Conexao = new MySqlConnection(data_source);

                string insert = "INSERT INTO cidade (id, nome, uf)" + "VALUES " + "(" + txtID.Text + ",'" + txtCity.Text + "', '" + txtUF.Text + "')";
                
                //Executar comando insert
                MySqlCommand comando = new MySqlCommand(insert, Conexao);
                Conexao.Open();

                comando.ExecuteReader();

                MessageBox.Show("Informações inseridas!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Conexao.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string data_source = "datasource=localhost; username=root; password =; database = cadastro_cidade";
                Conexao = new MySqlConnection(data_source);

                //excluir dados
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                string busca = "'%" + txtSearch.Text + "%'";
    
                Conexao = new MySqlConnection(data_source);

                string search = "SELECT * " +
                                "FROM cidade " +
                                "WHERE id LIKE " + busca + "OR nome LIKE " + busca;

                //Executar comando de busca
                MySqlCommand comando = new MySqlCommand(search, Conexao);
                Conexao.Open();

                MySqlDataReader reader = comando.ExecuteReader();

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
                var linha_listview = new ListViewItem(row);

                    lstCity.Items.Add(linha_listview);

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

            }
        }
    }
}
