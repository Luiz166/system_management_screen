using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class telaSelecao : Form
    {
        public telaSelecao()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            CadastroCidade ss = new CadastroCidade();
            ss.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            cadastroUsuario ss = new cadastroUsuario();
            ss.Show();
        }
    }
}
