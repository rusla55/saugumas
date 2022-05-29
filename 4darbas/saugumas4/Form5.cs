using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace saugumas4
{
    public partial class Form5 : Form
    {
        public string id;
        public Form5(string pav, string pass, string comment, string iid)
        {
            InitializeComponent();
            textBox1.Text = pav;
            textBox2.Text = pass;
            textBox3.Text = comment;
            button5.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            id = iid;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = decryptor(Convert.FromBase64String(textBox2.Text.Trim()));
            button1.Enabled = false;
            button5.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
        }
        private string encode(byte[] text)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.Key = Encoding.ASCII.GetBytes(Program.user.key.ToCharArray(), 0, 32);
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.Zeros;
            ICryptoTransform encryptor = aes.CreateEncryptor();
            string result = Convert.ToBase64String(encryptor.TransformFinalBlock(text, 0, text.Length));
            return result;
        }

        private string decryptor(byte[] text)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.Key = Encoding.ASCII.GetBytes(Program.user.key.ToCharArray(), 0, 32);
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.Zeros;
            ICryptoTransform decryptor = aes.CreateDecryptor();
            string result = Encoding.ASCII.GetString(decryptor.TransformFinalBlock(text, 0, text.Length));
            return result;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Text = Membership.GeneratePassword(12, 0);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox2.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(Program.user.connection);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(String.Format("DELETE FROM info WHERE iid='{0}'", id), conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Deleted");
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string epav = encode(Encoding.ASCII.GetBytes(textBox1.Text.Trim()));
            string epass = encode(Encoding.ASCII.GetBytes(textBox2.Text.Trim()));
            string ecomment = encode(Encoding.ASCII.GetBytes(textBox3.Text.Trim()));
            MySqlConnection conn = new MySqlConnection(Program.user.connection);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(String.Format("UPDATE info SET pavadinimas='{0}', password='{1}', komentaras='{2}' WHERE iid='{3}'", epav, epass, ecomment, id), conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Updated");
            Close();
        }
    }
}
