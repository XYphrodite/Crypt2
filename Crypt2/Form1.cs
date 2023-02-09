using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Crypt2
{
    public partial class Form1 : Form
    {
        private string fileContent = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }

        private void �����������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "txt files (*.txt)|*.txt";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var fileStream = dialog.OpenFile();
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    fileContent = reader.ReadToEnd();
                }
                if (fileContent.Count() == 10000)
                    setPicture();
                else
                {
                    MessageBox.Show($"������������ ����� ������ {fileContent.Count()}", "������",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("������ ������ �����", "������",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void setPicture()
        {
            var bitmap = new Bitmap(100, 100);
            for (int x = 0; x < 100; x++)
            {
                for (int y = 0; y < 100; y++)
                {
                    if (fileContent[x * 100 + y] == '1')
                        bitmap.SetPixel(x, y, Color.Black);
                    else if (fileContent[x * 100 + y] == '0')
                        bitmap.SetPixel(x, y, Color.White);
                    else
                        MessageBox.Show("��������� ������ �������� �� 0 � 1", "������",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            pictureBox1.Image = bitmap;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string compressedFile = compresseFile();
            File.WriteAllText("2.txt", compressedFile);
        }

        private string compresseFile()
        {
            string toReturn = "";
            int numberOfMatches = 0;
            for (int i = 0; i < 10000 / 8;i++)
            {
                toReturn += "11";
                int amount = 0;
                string toCode = fileContent.Substring(i * 8, 8);
                while (amount <= 63 && i+1 < 10000 / 8)
                {
                    string nextCode = fileContent.Substring((i + 1) * 8, 8);
                    if (toCode == nextCode)
                    {
                        i += 1;
                        amount++;
                        
                    }
                    else
                    {
                        numberOfMatches+=amount;
                        break;
                    }
                }
                string binary = SetZeroes(Convert.ToString(amount, 2));
                toReturn += binary;
                toReturn += toCode;
            }
            MessageBox.Show($"���������� ���������� = {numberOfMatches}");
            return toReturn;
        }
        private string SetZeroes(string value)
        {
            string newStr = "";
            for (int i = 0; i < 6 - value.Length; i++)
            {
                newStr += "0";
            }
            newStr += value;
            return newStr;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string compressedFile = string.Empty;
            var dialog = new OpenFileDialog();
            dialog.Filter = "txt files (*.txt)|*.txt";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var fileStream = dialog.OpenFile();
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    compressedFile = reader.ReadToEnd();
                }
                for (int x = 0; x < 100; x++)
                    for (int y = 0; y < 100; y++)
                        if (fileContent[x * 100 + y] != '1' && fileContent[x * 100 + y] != '0')
                        {
                            MessageBox.Show("��������� ������ �������� �� 0 � 1", "������",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                string decompressFile = decompresseFile(compressedFile);
                File.WriteAllText("3.txt", decompressFile);
            }
            else
            {
                MessageBox.Show("������ ������ �����", "������",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private string decompresseFile(string file)
        {
            string toReturn = string.Empty;
            int numberOfMatches = 0;
            //decompress algorithm

            MessageBox.Show($"���������� ���������� = {numberOfMatches}");
            return toReturn;
        }
    }
}