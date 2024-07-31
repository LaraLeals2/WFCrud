using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;
namespace WFCrud
{
    public partial class Form1 : Form
    {
        private MySqlConnection _connection;
        private string data_sourse = "datasource=localhost;username=root;password=2Y$7D9eS*R!8fV1@qL5&zB7kW4pH;database=db_agenda";

        public Form1()
        {
            InitializeComponent();

            lstContato.View = View.Details;
            lstContato.LabelEdit = true;
            lstContato.AllowColumnReorder = true;
            lstContato.FullRowSelect = true;
            lstContato.GridLines = true;

            lstContato.Columns.Add("ID", 30, HorizontalAlignment.Left);
            lstContato.Columns.Add("Nome", 150, HorizontalAlignment.Left);
            lstContato.Columns.Add("Email", 150, HorizontalAlignment.Left);
            lstContato.Columns.Add("Telefone", 150, HorizontalAlignment.Left);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                _connection = new MySqlConnection(data_sourse);

                _connection.Open();

                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = _connection;

                cmd.CommandText = "INSERT INTO contato ( nome, email, telefone)" +
                                  " VALUES " +
                                  "(@nome, @email, @telefone)";
                cmd.Parameters.AddWithValue("@nome",txtNome.Text);
                cmd.Parameters.AddWithValue("@email",txtEmail.Text);
                cmd.Parameters.AddWithValue("@telefone",txtTelefone.Text);
                cmd.Prepare();

                cmd.ExecuteNonQuery();
                
                MessageBox.Show($"Sucesso! {MessageBoxButtons.OK}, {MessageBoxIcon.Information} !");

                /* string sql = "INSERT INTO contato ( nome, email, telefone) VALUES ('" + txtNome.Text + "','" + txtEmail.Text + "','" + txtTelefone.Text + "' )";

                 MySqlCommand comando = new MySqlCommand(sql, _connection);

                 _connection.Open();
                 comando.ExecuteReader();
                 MessageBox.Show("Sucesso!");*/
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Erro: - {ex.Number} ocorreu: {ex.Message}, " +
                    $"{MessageBoxButtons.OK} {MessageBoxIcon.Error} ");
            }catch (Exception ex)
            {
                MessageBox.Show($"Erro: - ocorreu: {ex.Message}, " +
                    $"{MessageBoxButtons.OK}, {MessageBoxIcon.Error} !");
            }
            finally
            {
                _connection.Close();
            }
        }

        private void btnBusca_Click(object sender, EventArgs e)
        {
            try
            {
                string q = "'%" + txtContato.Text + "%'";
                _connection = new MySqlConnection(data_sourse);

                string sql = "SELECT * " +
                             "FROM contato " +
                             "WHERE nome LIKE " + q + " OR email LIKE " + q;

                _connection.Open();
                MySqlCommand comando = new MySqlCommand(sql, _connection);

                MySqlDataReader reader = comando.ExecuteReader();
                lstContato.Items.Clear();

                while (reader.Read())
                {
                    string[] row =
                    {
                        reader.GetInt32(0).ToString(),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3)
                    };
                    var linhaResultado = new ListViewItem(row);
                    lstContato.Items.Add(linhaResultado);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}