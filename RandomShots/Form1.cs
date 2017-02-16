using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Configuration;

namespace RandomShots
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
                Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
                config.AppSettings.Settings.Remove("FileSearchPath");
                config.AppSettings.Settings.Add("FileSearchPath", textBox1.Text);
                config.Save(ConfigurationSaveMode.Full);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog2.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = folderBrowserDialog2.SelectedPath;
                Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
                config.AppSettings.Settings.Remove("FileSavePath");
                config.AppSettings.Settings.Add("FileSavePath", textBox2.Text);
                config.Save(ConfigurationSaveMode.Full);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 1;
            progressBar1.Minimum = 1;
            progressBar1.Maximum = Convert.ToInt32(textBox4.Text);
            progressBar1.Step = 1;

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("filepath", typeof(String)));

            ProcessDirectory(textBox1.Text.ToString(), ref dt, ref progressBar1);
            progressBar1.Value = progressBar1.Maximum;
            int RowCount = dt.Rows.Count;
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings.Remove("NumberEstimate");
            config.AppSettings.Settings.Add("NumberEstimate", RowCount.ToString());
            config.Save(ConfigurationSaveMode.Full);
            textBox4.Text = RowCount.ToString();


            textBox3.Text = RowCount.ToString();

            Random rnd = new Random();
            linkLabel1.Text = dt.Rows[(rnd.Next(RowCount) - 1)][0].ToString();
            rnd.Next(RowCount);
            linkLabel2.Text = dt.Rows[(rnd.Next(RowCount) - 1)][0].ToString();
            rnd.Next(RowCount);
            linkLabel3.Text = dt.Rows[(rnd.Next(RowCount) - 1)][0].ToString();
            rnd.Next(RowCount);
            linkLabel4.Text = dt.Rows[(rnd.Next(RowCount) - 1)][0].ToString();
            rnd.Next(RowCount);
            linkLabel5.Text = dt.Rows[(rnd.Next(RowCount) - 1)][0].ToString();
            rnd.Next(RowCount);
            linkLabel6.Text = dt.Rows[(rnd.Next(RowCount) - 1)][0].ToString();
            rnd.Next(RowCount);
            linkLabel7.Text = dt.Rows[(rnd.Next(RowCount) - 1)][0].ToString();
            rnd.Next(RowCount);
            linkLabel8.Text = dt.Rows[(rnd.Next(RowCount) - 1)][0].ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(linkLabel1.Text != "")
            {
                string FileFrom = ConfigurationManager.AppSettings["FileSearchPath"].ToString();
                string FileTo = ConfigurationManager.AppSettings["FileSavePath"].ToString();

                File.Copy(linkLabel1.Text, (FileTo + "\\" + Path.GetFileName(linkLabel1.Text)));
                File.Copy(linkLabel2.Text, (FileTo + "\\" + Path.GetFileName(linkLabel2.Text)));
                File.Copy(linkLabel3.Text, (FileTo + "\\" + Path.GetFileName(linkLabel3.Text)));
                File.Copy(linkLabel4.Text, (FileTo + "\\" + Path.GetFileName(linkLabel4.Text)));
                File.Copy(linkLabel5.Text, (FileTo + "\\" + Path.GetFileName(linkLabel5.Text)));
                File.Copy(linkLabel6.Text, (FileTo + "\\" + Path.GetFileName(linkLabel6.Text)));
                File.Copy(linkLabel7.Text, (FileTo + "\\" + Path.GetFileName(linkLabel7.Text)));
                File.Copy(linkLabel8.Text, (FileTo + "\\" + Path.GetFileName(linkLabel8.Text)));
            }
        }

        public static void ProcessDirectory(string targetDirectory, ref DataTable dt, ref ProgressBar bar)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {
                //sb.AppendLine(fileName);
                if(fileName.EndsWith(".jpg") | fileName.EndsWith(".png"))
                {
                    DataRow r = dt.NewRow();
                    r["filepath"] = fileName;
                    dt.Rows.Add(r);
                    bar.PerformStep();
                }

            }

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
            {
                ProcessDirectory(subdirectory, ref dt, ref bar);
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel1.Text);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel2.Text);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel3.Text);
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel4.Text);
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel5.Text);
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel8.Text);
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel7.Text);
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel6.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = ConfigurationManager.AppSettings["FileSearchPath"].ToString();
            textBox2.Text = ConfigurationManager.AppSettings["FileSavePath"].ToString();
            textBox4.Text = ConfigurationManager.AppSettings["NumberEstimate"].ToString();
        }
    }
}
