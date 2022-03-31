namespace Aescypher
{
    public partial class Form1 : Form
    {
        AEScypfer scypfer = new AEScypfer();
        public Form1()
        {
            InitializeComponent();
            textBox3.Text = Convert.ToBase64String(scypfer.key);
            textBox4.Text = Convert.ToBase64String(scypfer.IV);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = scypfer.cypferECB(textBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            scypfer.key = Convert.FromBase64String(textBox3.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = scypfer.decypferECB(textBox2.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            scypfer.IV = Convert.FromBase64String(textBox4.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox2.Text = scypfer.cypferCBC(textBox1.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text = scypfer.decypferCBC(textBox2.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string filename = openFileDialog1.FileName;
            string readfile = File.ReadAllText(filename);
            textBox1.Text=readfile;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text = null;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox2.Text = null;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            string filename = openFileDialog1.FileName;
            string readfile = File.ReadAllText(filename);
            textBox2.Text = readfile;
        }

        private void button8_Click(object sender, EventArgs e)
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

        private void button12_Click(object sender, EventArgs e)
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

        private void button13_Click(object sender, EventArgs e)
        {
            scypfer.randomKey();
            textBox3.Text = Convert.ToBase64String(scypfer.key);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            scypfer.randomIV();
            textBox4.Text = Convert.ToBase64String(scypfer.IV);
        }
    }
}