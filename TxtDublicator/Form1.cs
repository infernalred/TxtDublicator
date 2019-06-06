using System;
using System.Windows.Forms;

namespace TxtDublicator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
           
        }


        EmailDP em1 = new EmailDP();
        

        

        

        





        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open1 = new OpenFileDialog();
            open1.Filter = "Text files (*.txt) | *.txt";
            open1.ShowDialog();
            open1.OpenFile();
            label1.Text = open1.FileName;
            em1.ClearList();
            em1.Add(open1.FileName);
            FillRichtextbox();


        }

        //Delete double
        private void button2_Click(object sender, EventArgs e)
        {
            em1.DeleteDouble();
            FillRichtextbox();
        }


        //clean richtextbox and then fill up
        public void FillRichtextbox()
        {
            richTextBox1.Clear();
            foreach (var lstemail in em1.EmailTexts)
            {
                richTextBox1.AppendText(lstemail.RetOneString() + "\n");
            }
        }

        //Sort by A-Z
        private void button3_Click(object sender, EventArgs e)
        {
           em1.Sort();
           FillRichtextbox();

        }
        //Sort by Z-A
        private void button4_Click(object sender, EventArgs e)
        {
            em1.Sort(SortCriteria.ByZtoA);
            FillRichtextbox();
        }
        //Sort by DomainName
        private void button5_Click(object sender, EventArgs e)
        {
            em1.Sort(SortCriteria.ByDomain);
            FillRichtextbox();
        }

        //Make all string UpperCase
        private void button7_Click(object sender, EventArgs e)
        {
            foreach (var lstemal in em1.EmailTexts)
            {
                lstemal.StringToUpper();
                FillRichtextbox();
            }

        }

        //Make all string LowerCase
        private void button6_Click(object sender, EventArgs e)
        {
            foreach (var lstemal in em1.EmailTexts)
            {
                lstemal.StringToLower();
                FillRichtextbox();
            }
        }

        //save to txt
        private void button8_Click(object sender, EventArgs e)
        {
            SaveFileDialog sf1 =  new SaveFileDialog();
            sf1.Filter = "Text files (*.txt) | *.txt";
            if (sf1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = sf1.FileName;
            System.IO.File.WriteAllLines(filename, richTextBox1.Lines);
        }
    }



   


}
