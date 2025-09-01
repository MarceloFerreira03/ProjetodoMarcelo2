using MySql.Data.MySqlClient;
using Projeto_Socorrista.Classes;
using System;
using System.Data;
using System.Windows.Forms;

namespace Projeto_Socorrista
{
    public partial class frmLoginNovo : Form
    {
        public frmLoginNovo()
        {
            InitializeComponent();
        }


        private void btnEntrar_Click(object sender, EventArgs e)
        {

            string usuario, senha;
            usuario = txtLogin.Text;
            senha = txtSenha.Text;

            if (acessaUsuario(usuario, senha))
            {
                frmMenuPrincipal abrir = new frmMenuPrincipal();
                abrir.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuário ou senha incorretos",
                    "Mensagem do sistema",
                     MessageBoxButtons.OK,
                     MessageBoxIcon.Error,
                     MessageBoxDefaultButton.Button1);
                LimparCampos();
            }               
        } 

        private void lblCadastrese_Click(object sender, EventArgs e)
        {
            frmCadastroVoluntario cadastro = new frmCadastroVoluntario();            
            cadastro.Show();            
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void lblEsqueceu_Click(object sender, EventArgs e)
        {
            frmEnviarSenha abrir = new frmEnviarSenha();
            abrir.Show();
            this.Hide();
    }

        bool resp = false;
        public bool acessaUsuario(string email, string senha)
        {
            MySqlCommand comm = new MySqlCommand();
            comm.CommandText = "select email,senha from tbUsuarios where email=@email and senha=@senha;";
            comm.CommandType = CommandType.Text;

            comm.Parameters.Clear();

            comm.Parameters.Add("@email", MySqlDbType.VarChar, 100).Value = email;
            comm.Parameters.Add("@senha", MySqlDbType.VarChar, 100).Value = senha;

            comm.Connection = ConectaBanco.ObterConexao();

            MySqlDataReader DR;

            try
            {
                DR = comm.ExecuteReader();

                resp = DR.HasRows;

                ConectaBanco.FecharConexao();
            }
            catch (Exception)
            {
                MessageBox.Show("Banco de dados não conectado",
                    "Mensagem do sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
            }
            return resp;
        }
        public void LimparCampos()
        {
            txtLogin.Clear();
            txtSenha.Clear();
            txtLogin.Focus();
        }
    }

}