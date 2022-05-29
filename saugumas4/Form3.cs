using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace saugumas4
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            MySqlConnection conn = new MySqlConnection(Program.user.connection);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(String.Format("SELECT * FROM info where id = '{0}'", Program.user.id), conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i][2] = decryptor(Convert.FromBase64String(dt.Rows[i][2].ToString()));
                dt.Rows[i][4] = decryptor(Convert.FromBase64String(dt.Rows[i][4].ToString()));
            }
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            conn.Close();
            this.dataGridView1.CellClick += DataGridView1_CellClick;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var name = textBox1.Text.Trim();
            MySqlConnection conn = new MySqlConnection(Program.user.connection);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(String.Format("SELECT * FROM info where id = '{0}'", Program.user.id), conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i][2] = decryptor(Convert.FromBase64String(dt.Rows[i][2].ToString()));
                dt.Rows[i][4] = decryptor(Convert.FromBase64String(dt.Rows[i][4].ToString()));
            }
            dataGridView1.DataSource = dt;
            if(name != "")
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string temp = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    char c = '\0';
                    temp = temp.Trim(c);
                    if (temp.ToString() == name)
                        dataGridView1.Rows[i].Visible = true;
                    else
                    {
                        CurrencyManager currencyManager1 = (CurrencyManager)BindingContext[dataGridView1.DataSource];
                        currencyManager1.SuspendBinding();
                        dataGridView1.Rows[i].Visible = false;
                        currencyManager1.ResumeBinding();
                    }
                }
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 form = new Form4();
            form.ShowDialog();
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

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
                string pav = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                string pass = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                string comment = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                string id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                Form5 form = new Form5(pav,pass,comment,id);
                form.ShowDialog();
        }

    }
}
