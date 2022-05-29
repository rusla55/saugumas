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
    public partial class Form1 : Form
    {
        public Form1()
        {   
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var username = textBox1.Text.Trim();
            var password = textBox2.Text.Trim();
            MySqlConnection conn = new MySqlConnection(Program.user.connection);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(String.Format("SELECT id FROM prisijungimas where vardas = '{0}'", username),conn);
            MySqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            Program.user.id = Convert.ToInt32(dr.GetValue(0));
            dr.Close();
            MySqlCommand cmd1 = new MySqlCommand(String.Format("SELECT pass FROM prisijungimas where vardas = '{0}'", username), conn);
            dr = cmd1.ExecuteReader();
            dr.Read();
            string temp = Convert.ToString(dr.GetValue(0));
            Program.user.key = temp;
            dr.Close();
            if (BCrypt.Net.BCrypt.Verify(password,temp))
            {
                Form3 form3 = new Form3();
                form3.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }
    }
}
