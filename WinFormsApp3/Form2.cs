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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public static int nump = -1, numd = -1, kod = 0;
        public static string pname, dname;
        static NpgsqlCommand command;
        static NpgsqlDataReader reader;
        public static void Execute(string sql)
        {
            command = new NpgsqlCommand(sql, Form1.connection);
            Form1.connection.Open();
            try
            {
                command.ExecuteNonQuery();
            }
            catch (NpgsqlException ne)
            {
                MessageBox.Show(sql, "Ошибка");
                Form1.connection.Close();
                return;
            }
            Form1.connection.Close();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            command = new NpgsqlCommand("SELECT p_id, CONCAT_WS(' ', p_surname, p_name, p_patr) FROM patients ORDER BY p_id", Form1.connection);
            Form1.connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader[1]);
                comboBox3.Items.Add(reader[1]);
            }
            Form1.connection.Close();
            command = new NpgsqlCommand("SELECT doc_id, CONCAT_WS(' ', doc_surname, doc_name, doc_patr) FROM doctors ORDER BY doc_id", Form1.connection);
            Form1.connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                comboBox2.Items.Add(reader[1]);
                comboBox4.Items.Add(reader[1]);
            }
            Form1.connection.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            nump = -1;
            Patient patient = new Patient();
            patient.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Не указан элемент!", "Ошибка");
                return;
            }
            nump = comboBox1.SelectedIndex;
            Patient patient = new Patient();
            patient.Show();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            numd = -1;
            Doctor doctor = new Doctor();
            doctor.Show();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Не указан элемент!", "Ошибка");
                return;
            }
            numd = comboBox2.SelectedIndex;
            Doctor doctor = new Doctor();
            doctor.Show();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (kod == 0)
            {
                MessageBox.Show("Не указан элемент!", "Ошибка");
                return;
            }
            Diagnos diagnos = new Diagnos();
            diagnos.Show();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            nump = comboBox3.SelectedIndex;
            if (nump == -1)
            {
                MessageBox.Show("Не указан элемент!", "Ошибка");
                return;
            }
            string message = "Вы точно хотите удалить из картотеки пациента " + comboBox3.Text + "?";
            string caption = "Удаление";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.No) { return; }
            Execute("DELETE FROM patients WHERE p_id = " + (nump + 1));
            nump = comboBox1.SelectedIndex;
        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked) kod = 1;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked) kod = 2;
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked) kod = 3;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            numd = comboBox4.SelectedIndex;
            if (numd == -1)
            {
                MessageBox.Show("Не указан элемент!", "Ошибка");
                return;
            }
            string message = "Вы точно хотите удалить из картотеки врача " + comboBox4.Text + "?";
            string caption = "Удаление";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.No) { return; }
            Execute("DELETE FROM doctors WHERE doc_id = " + (numd + 1));
            numd = comboBox2.SelectedIndex;
        }
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            nump = comboBox1.SelectedIndex;
            pname = comboBox1.SelectedItem.ToString();
        }
        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            numd = comboBox2.SelectedIndex;
            dname = comboBox2.SelectedItem.ToString();
        }
    }
}
