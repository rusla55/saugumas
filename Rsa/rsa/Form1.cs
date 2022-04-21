namespace rsa
{
    public partial class Form1 : Form
    {
        CrypterDecrypter crypterDecrypter = new CrypterDecrypter();
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = crypterDecrypter.decryptor(textBox2.Text);
            textBox8.Text = Convert.ToString(crypterDecrypter.d);
            textBox9.Text = Convert.ToString(crypterDecrypter.FOut);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Text = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            crypterDecrypter.p = Convert.ToInt32(textBox3.Text);
            crypterDecrypter.q = Convert.ToInt32(textBox4.Text);
            textBox2.Text = crypterDecrypter.encryptor(textBox1.Text);
            textBox5.Text = Convert.ToString(crypterDecrypter.n);
            textBox6.Text = Convert.ToString(crypterDecrypter.FIn);
            textBox7.Text = Convert.ToString(crypterDecrypter.e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string filename = openFileDialog1.FileName;
            string readfile = File.ReadAllText(filename);
            textBox1.Text = readfile;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string filename = openFileDialog1.FileName;
            string readfile = File.ReadAllText(filename);
            textBox2.Text = readfile;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.FileName = "DefaultOutputName.txt";
            save.Filter = "Text File | *.txt";
            if (save.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(save.OpenFile());
                writer.Write(textBox1.Text);
                writer.Dispose();
                writer.Close();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.FileName = "DefaultOutputName.txt";
            save.Filter = "Text File | *.txt";
            if (save.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(save.OpenFile());
                writer.Write(textBox2.Text);
                writer.Dispose();
                writer.Close();
            }
        }
    }
}