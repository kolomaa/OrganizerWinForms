using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Organizer
{
    public partial class MainForm : Form
    {
        int count = 0;
        Random rnd;
        char[] specialSymbols = new char[] {'^','#',')','%','&','*','$'};

        Dictionary<string, double> metrica;

        string[] lengthName = new [] {"mm","cm","dm","m","km","mile"};
        double[] lengthValue = new double[] {1 ,10, 100, 1000, 1000000, 1609344};

        string[] weightName = new[] {"g","kg","t","lb"};
        double[] weightValue = new double[] {1, 1000, 1000000, 453.6};

        public MainForm()
        {
            InitializeComponent();
            rnd = new Random();
            metrica = new Dictionary<string, double>();
            for (int i = 0; i < lengthName.Length; i++)
            {
                metrica.Add(lengthName[i], lengthValue[i]);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This program about my utilities\nAuthor: Shcherbachenko Y.O.", "About");
        }

        private void lblCount_Click(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            count = 0;
            lblCount.Text = count.ToString();
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            count++;
            lblCount.Text = count.ToString();
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            count--;
            lblCount.Text = count.ToString();
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            int n;
            n = rnd.Next(Convert.ToInt32(numericUpDown1.Value), Convert.ToInt32(numericUpDown2.Value)+1);
            lblRandom.Text = Convert.ToString(n);
            if (cbRandom.Checked)
            {
                if (tbRandom.Text.IndexOf(n.ToString()) == -1) tbRandom.AppendText(n + "\r\n");
            }
            else tbRandom.AppendText(n + "\r\n");
        }

        private void btnRandomClear_Click(object sender, EventArgs e)
        {
            tbRandom.Clear();
        }

        private void btnRandomCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(tbRandom.Text);
        }

        private void tsmiInsertData_Click(object sender, EventArgs e)
        {
            rtbNotepad.AppendText(DateTime.Now.ToShortDateString() + "\n");
        }

        private void tsmiInsertTime_Click(object sender, EventArgs e)
        {
            rtbNotepad.AppendText(DateTime.Now.ToShortTimeString() + "\n");
        }

        private void tsmiSave_Click(object sender, EventArgs e)
        {
            try
            {
                rtbNotepad.SaveFile("Notepad.txt");
            }
            catch 
            {
                MessageBox.Show("Save error");
            }
            
        }
        void LoadNotepad()
        {
            try
            {
                rtbNotepad.LoadFile("Notepad.txt");
            }
            catch
            {
                MessageBox.Show("Load error");
            }
        }

        private void tsmiLoad_Click(object sender, EventArgs e)
        {
            LoadNotepad();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadNotepad();
            clbPassword.SetItemChecked(0, true);
            clbPassword.SetItemChecked(1, true);
        }

        private void tsmiClearNotepad_Click(object sender, EventArgs e)
        {
            rtbNotepad.Clear();
        }

        private void btnCreatePassword_Click(object sender, EventArgs e)
        {
            if (clbPassword.CheckedItems.Count == 0) return;
            string password = "";
            for (int i = 0; i < nudPasswordLenght.Value; i++)
            {
                int n = rnd.Next(clbPassword.CheckedItems.Count);
                string s = clbPassword.CheckedItems[n].ToString();
                switch (s)
                {
                    case "Numeral": password += rnd.Next(10).ToString();
                        break;
                    case "Upper case": password += Convert.ToChar(rnd.Next(65, 88));
                        break;
                    case "Lower case": password += Convert.ToChar(rnd.Next(97,122));
                        break;
                    default: password += specialSymbols[rnd.Next(specialSymbols.Length)];
                        break;
                }
                tbPassword.Text = password;
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            double m1 = metrica[cbFrom.Text];
            double m2 = metrica[cbTo.Text];
            double n = Convert.ToDouble(tbFrom.Text);
            tbTo.Text = Convert.ToString(n*m1/m2);
        }

        private void btnSwap_Click(object sender, EventArgs e)
        {
            string t = cbFrom.Text;
            cbFrom.Text = cbTo.Text;
            cbTo.Text = t;
        }

        private void cbMetrics_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbMetrics.Text)
            {
                case "Length":
                    {
                        metrica.Clear();
                        cbFrom.Items.Clear();
                        cbTo.Items.Clear();
                        for (int i = 0; i < lengthName.Length; i++)
                        {
                            metrica.Add(lengthName[i],lengthValue[i]);
                            cbFrom.Items.Add(lengthName[i]);
                            cbTo.Items.Add(lengthName[i]);
                        }
                        cbFrom.Text = lengthName[0];
                        cbTo.Text = lengthName[0];
                        break;
                    }
                case "Weight":
                    {
                        metrica.Clear();
                        cbFrom.Items.Clear();
                        cbTo.Items.Clear();
                        for (int i = 0; i < weightName.Length; i++)
                        {
                            metrica.Add(weightName[i], weightValue[i]);
                            cbFrom.Items.Add(weightName[i]);
                            cbTo.Items.Add(weightName[i]);
                        }
                        cbFrom.Text = weightName[0];
                        cbTo.Text = weightName[0];
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
