using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace WinFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static string conn = "Host = localhost; User Id = postgres; Database = registr; Port = 5432; Password = postgres;";
        public static NpgsqlConnection connection = new NpgsqlConnection(conn);
        public static DataSet ds = new DataSet();
        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool trueLogin = false, truePass = false;
            connection.Open();
            string sql = "SELECT login FROM polzovateli";
            NpgsqlCommand command = new NpgsqlCommand(sql, connection);
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (reader["login"].ToString() == textBox1.Text)
                {
                    trueLogin = true;
                    break;
                }
            }
            connection.Close();
            connection.Open();
            sql = "SELECT pass FROM polzovateli WHERE login = '" + textBox1.Text + "'";
            command = new NpgsqlCommand(sql, connection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (reader["pass"].ToString() == textBox2.Text)
                {
                    truePass = true;
                    break;
                }
            }
            connection.Close();
            if (!trueLogin || !truePass)
            {
                MessageBox.Show("Неправильный логин или пароль", "Ошибка");
                return;
            }
            else
            {
                Hide();
                Form2 sot = new Form2();
                sot.ShowDialog();
                Close();
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                textBox2.UseSystemPasswordChar = false;
            else
                textBox2.UseSystemPasswordChar = true;
        }
    }
}
