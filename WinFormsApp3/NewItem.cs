using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp3
{
    public partial class NewItem : Form
    {
        public NewItem()
        {
            InitializeComponent();
        }
        private void NewItem_Load(object sender, EventArgs e)
        {
            if (Diagnos.k > -1)
            {
                if (Form2.kod == 1)
                {
                    textBox1.Text = Disease.Reader("SELECT mkb_name FROM diagn_mkb WHERE mkb_id = " + Form1.ds.Tables["Array"].Rows[Diagnos.k]["Номер"].ToString());
                    textBox2.Text = Disease.Reader("SELECT mkb_kod FROM diagn_mkb WHERE mkb_id = " + Form1.ds.Tables["Array"].Rows[Diagnos.k]["Номер"].ToString());
                }
                else if (Form2.kod == 2)
                {
                    textBox1.Text = Disease.Reader("SELECT ilar_name FROM diagn_ilar WHERE ilar_id = " + Form1.ds.Tables["Array"].Rows[Diagnos.k]["Номер"].ToString());
                    textBox2.Visible = false;
                    label2.Visible = false;
                }
                else if (Form2.kod == 3)
                {
                    textBox1.Text = Disease.Reader("SELECT hos_name FROM hospital WHERE hos_id = " + Form1.ds.Tables["Array"].Rows[Diagnos.k]["Номер"].ToString());
                    textBox2.Text = Disease.Reader("SELECT hos_city FROM hospital WHERE hos_id = " + Form1.ds.Tables["Array"].Rows[Diagnos.k]["Номер"].ToString());
                    label2.Text = "Город";
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string sql, kod;
            if (Diagnos.k > -1)
            {
                kod = Form1.ds.Tables["Array"].Rows[Diagnos.k]["Номер"].ToString();
                if (Form2.kod == 1)
                    sql = "UPDATE diagn_mkb SET mkb_name = '" + textBox1.Text + "', mkb_kod = '" + textBox2.Text + "' WHERE mkb_id = " + kod;
                else if (Form2.kod == 2)
                    sql = "UPDATE diagn_ilar SET ilar_name = '" + textBox1.Text + "' WHERE ilar_id = " + kod;
                else
                    sql = "UPDATE hospital SET hos_name = '" + textBox1.Text + "', hos_city = '" + textBox2.Text + "' WHERE ilar_id = " + kod;
                Form1.ds.Tables["Array"].Rows.RemoveAt(Diagnos.k);
            }
            else
            {
                if (Form2.kod == 1)
                {
                    kod = Disease.Reader("SELECT MAX(mkb_id) + 1 FROM diagn_mkb");
                    sql = "INSERT INTO diagn_mkb VALUES (" + kod + ", '" + textBox2.Text + "', '" + textBox1.Text + "')";
                }
                else if (Form2.kod == 2)
                {
                    kod = Disease.Reader("SELECT MAX(ilar_id) + 1 FROM diagn_ilar");
                    sql = "INSERT INTO diagn_ilar VALUES (" + kod + ", '" + textBox1.Text + "')";
                }
                else
                {
                    kod = Disease.Reader("SELECT MAX(mkb_id) + 1 FROM diagn_mkb");
                    sql = "INSERT INTO hospital VALUES (" + kod + ", '" + textBox1.Text + "', '" + textBox2.Text + "')";
                }
            }
            Form2.Execute(sql);
            if ((Form2.kod == 1) || (Form2.kod == 3)) Form1.ds.Tables["Array"].Rows.Add(new object[] { kod, textBox1.Text, textBox2.Text });
            else if (Form2.kod == 2) Form1.ds.Tables["Array"].Rows.Add(new object[] { kod, textBox1.Text });
        }
        private void NewItem_FormClosed(object sender, FormClosedEventArgs e)
        {
            Diagnos.k = -1;
        }
    }
}
