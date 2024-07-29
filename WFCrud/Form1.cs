using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;
namespace WFCrud
{
    public partial class Form1 : Form
    {
        MySqlConnection _connection;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string data_sourse = "datasource=localhost;username=root;password=2Y$7D9eS*R!8fV1@qL5&zB7kW4pH;database=db_agenda";

                _connection = new MySqlConnection(data_sourse);

                string sql = "INSERT INTO contato ( nome, email, telefone) VALUES ('" + txtNome.Text + "','" + txtEmail.Text + "','" + txtTelefone.Text + "' )";

                MySqlCommand comando = new MySqlCommand(sql, _connection);

                _connection.Open();
                comando.ExecuteReader();
                MessageBox.Show("Sucesso!");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: - {ex}, {ex.Message}!");
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}