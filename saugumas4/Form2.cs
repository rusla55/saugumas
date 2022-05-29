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

namespace saugumas4
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var username = textBox1.Text.Trim();
            var password = textBox2.Text.Trim();
            var encrypt = BCrypt.Net.BCrypt.HashPassword(password);
            MySqlConnection conn = new MySqlConnection(Program.user.connection);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(String.Format("Insert into prisijungimas (vardas, pass) values ('{0}', '{1}')", username, encrypt),conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
            MessageBox.Show("Naujas naudotojas pridetas");
            Close();
        }
    }
}
