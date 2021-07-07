using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp3
{
    public partial class Doctor : Form
    {
        public Doctor()
        {
            InitializeComponent();
        }
        private void Doctor_Load(object sender, EventArgs e)
        {
            textBox1.Text = Disease.Reader("SELECT doc_surname FROM doctors WHERE doc_id = " + (Form2.numd + 1));
            textBox2.Text = Disease.Reader("SELECT doc_name FROM doctors WHERE doc_id = " + (Form2.numd + 1));
            textBox3.Text = Disease.Reader("SELECT doc_patr FROM doctors WHERE doc_id = " + (Form2.numd + 1));
            if (Form2.numd > -1)
                label2.Text = textBox1.Text + " " + textBox2.Text + " " + textBox3.Text;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string patr = (textBox3.Text != "") ? textBox3.Text : "null";
            string id = Disease.Reader("SELECT MAX(doc_id) + 1 FROM doctors");
            if (Form2.numd == -1)
            {
                string insert = "INSERT INTO doctors VALUES (" + id + ", '" + textBox1.Text + "', '" + textBox2.Text + "', ";
                if (patr != "null") insert += "'" + patr + "')";
                else insert += "null)";
                Form2.Execute(insert);
            }
            else
            {
                string update = "UPDATE doctors SET doc_surname = '" + textBox1.Text + "', doc_name = '" + textBox2.Text + "'";
                if (patr != "null") update += ", doc_patr = '" + patr + "'";
                update += " WHERE doc_id = " + (Form2.numd + 1);
                Form2.Execute(update);
            }
            if (Form2.numd == -1) Form2.numd = Convert.ToInt32(id) - 1;
        }
    }
}
