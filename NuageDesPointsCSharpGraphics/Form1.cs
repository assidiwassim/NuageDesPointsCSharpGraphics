using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DALL;
namespace NuageDesPointsCSharpGraphics
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {

        Graphics l;
        int[] x;
        int[] y;
        NuagePoints N;
        public Form1()
        {
            InitializeComponent();
            Remplir();

        }
        public void Remplir()
        {
            x = new int[DataNuage.Select().Rows.Count];
            y = new int[DataNuage.Select().Rows.Count];

            gridControl1.DataSource = DataNuage.Select();
            int i = 0;
            foreach (DataRow row in DataNuage.Select().Rows)
            {


                x[i] = Int32.Parse(row[0].ToString());
                y[i] = Int32.Parse(row[1].ToString());
                i++;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            NuagePoints N = new NuagePoints(e.Graphics, x, y);
            N.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "")
            {
                int i = DataNuage.Insert(Int32.Parse(textBox1.Text.ToString()), Int32.Parse(textBox2.Text.ToString()));
                gridControl1.DataSource = DataNuage.Select();
                Remplir();

                textBox1.Text = "";
                textBox2.Text = "";

                Refresh();
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
          

                try
                {
                    if (cardView1.RowCount > 2)
                    {
                        if (cardView1.RowCount != 1)
                        {

                            foreach (int i in cardView1.GetSelectedRows())
                            {
                                DataRow row = cardView1.GetDataRow(i);
                                DataNuage.Delete(Int32.Parse(Convert.ToString(row[0])), Int32.Parse(Convert.ToString(row[1])));
                                gridControl1.DataSource = DataNuage.Select();
                                Remplir();
                                Refresh();

                            }
                        }
                        else
                        {
                            XtraMessageBox.Show("Il faut existe  au moin un element !", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                        XtraMessageBox.Show("Selectioner une ligne", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Erreur:" + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
        }
        //Function to get random number
        private static readonly Random getrandom = new Random();
        private static readonly object syncLock = new object();
        public static int GetRandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return getrandom.Next(min, max);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int x=GetRandomNumber(1, 10000);
            int y= GetRandomNumber(1, 10000);

            int i = DataNuage.Insert(x, y);
            gridControl1.DataSource = DataNuage.Select();
            Remplir();

            textBox1.Text = x.ToString();
            textBox2.Text = y.ToString();

            Refresh();
        }
    }
}
