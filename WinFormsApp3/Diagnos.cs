using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Npgsql;

namespace WinFormsApp3
{
    public partial class Diagnos : Form
    {
        public Diagnos()
        {
            InitializeComponent();
        }
        public static int k;
        public static void Table_Fill(string name, string sql)
        {
            Form1.connection.Open();
            if (Form1.ds.Tables[name] != null)
                Form1.ds.Tables[name].Clear();
            NpgsqlDataAdapter da;
            da = new NpgsqlDataAdapter(sql, Form1.connection);
            da.Fill(Form1.ds, name);
            Form1.connection.Close();
        }
        private void Diagnos_Load(object sender, EventArgs e)
        {
            string sql;
            if (Form2.kod == 1)
            {
                sql = "SELECT mkb_id AS Номер, mkb_kod AS Код, mkb_name AS Наименование FROM diagn_mkb";
                Table_Fill("Array", sql);
            }
            else if (Form2.kod == 2)
            {
                sql = "SELECT ilar_id AS Номер, ilar_name AS Наименование FROM diagn_ilar";
                Table_Fill("Array", sql);
            }
            else if (Form2.kod == 3)
            {
                sql = "SELECT hos_id AS Номер, hos_name AS Наименование, hos_city AS Город FROM hospital";
                Table_Fill("Array", sql);
            }
            dataGridView1.DataSource = Form1.ds.Tables["Array"];
            dataGridView1.AutoResizeColumns();
            dataGridView1.Columns["Номер"].Visible = false;
            dataGridView1.CurrentCell = null;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            k = -1;
            NewItem item = new NewItem();
            item.Show();
            dataGridView1.CurrentCell = null;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (k == -1)
            {
                MessageBox.Show("Не указан элемент!", "Ошибка");
                return;
            }
            NewItem item = new NewItem();
            item.Show();
            dataGridView1.CurrentCell = null;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (k == -1)
            {
                MessageBox.Show("Не указан элемент!", "Ошибка");
                return;
            }
            if (Form2.kod == 1) Form2.Execute("DELETE FROM diagn_mkb WHERE mkb_id = " + Form1.ds.Tables["Array"].Rows[k]["Номер"].ToString());
            else if (Form2.kod == 2) Form2.Execute("DELETE FROM diagn_ilar WHERE ilar_id = " + Form1.ds.Tables["Array"].Rows[k]["Номер"].ToString());
            else if (Form2.kod == 3) Form2.Execute("DELETE FROM hospital WHERE hos_id = " + Form1.ds.Tables["Array"].Rows[k]["Номер"].ToString());
            Form1.ds.Tables["Array"].Rows.RemoveAt(k);
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            k = dataGridView1.CurrentRow.Index;
        }
    }
}
