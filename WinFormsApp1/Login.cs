namespace WinFormsApp1
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtUser.Text.Equals("root") && txtPswd.Text.Equals("root"))
                {
                    this.Hide();

                    telaSelecao ss = new telaSelecao();
                    ss.Show();
                }
                else
                {
                    MessageBox.Show("Usuário ou senha incorretos.",
                        "Erro", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Error);
                    
                    txtUser.Text = "";
                    txtPswd.Text = "";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Erro", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}