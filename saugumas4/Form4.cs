using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace saugumas4
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = Membership.GeneratePassword(12, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] pav = Encoding.ASCII.GetBytes(textBox1.Text.Trim());
            byte[] pass = Encoding.ASCII.GetBytes(textBox2.Text.Trim());
            byte[] comment = Encoding.ASCII.GetBytes(textBox3.Text.Trim());
            var epav = encode(pav);
            var epass = encode(pass);
            var ecomment = encode(comment);
            MySqlConnection conn = new MySqlConnection(Program.user.connection);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(String.Format("Insert into info (id, pavadinimas, password, komentaras) values ('{0}', '{1}', '{2}', '{3}')", Program.user.id, epav, epass, ecomment), conn);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
            MessageBox.Show("Pridetas naujas irasas");
            Close();
        }

        private string encode(byte[] text)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.Key = Encoding.ASCII.GetBytes(Program.user.key.ToCharArray(),0,32);
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.Zeros;
            ICryptoTransform encryptor = aes.CreateEncryptor();
            string result = Convert.ToBase64String(encryptor.TransformFinalBlock(text, 0, text.Length));
            return result;
        }
    }
}
