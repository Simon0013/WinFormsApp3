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
    public partial class Disease : Form
    {
        public Disease()
        {
            InitializeComponent();
        }
        static NpgsqlCommand command;
        static NpgsqlDataReader reader;
        string mkb_id, ilar_id, mkb_id2, ilar_id2, status;
        string trauma = "0", infection = "0", hypothermia = "0", insolation = "0", other_factor = "0";
        private void comboBox4_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ilar_id2 = Reader("SELECT ilar_id FROM diagn_ilar WHERE ilar_name = '" + comboBox4.SelectedItem.ToString() + "'");
        }
        private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            mkb_id2 = Reader("SELECT mkb_id FROM diagn_mkb WHERE CONCAT_WS(' ', mkb_kod, mkb_name) = '" + comboBox3.SelectedItem.ToString() + "'");
        }
        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ilar_id = Reader("SELECT ilar_id FROM diagn_ilar WHERE ilar_name = '" + comboBox2.SelectedItem.ToString() + "'");
        }
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            mkb_id = Reader("SELECT mkb_id FROM diagn_mkb WHERE CONCAT_WS(' ', mkb_kod, mkb_name) = '" + comboBox1.SelectedItem.ToString() + "'");
        }
        private void comboBox6_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox6.SelectedItem.ToString() == "Да")
            {
                checkBox1.Visible = true;
                checkBox2.Visible = true;
                checkBox3.Visible = true;
                checkBox4.Visible = true;
                checkBox5.Visible = true;
                checkBox6.Visible = true;
            }
            else
            {
                checkBox1.Visible = false;
                checkBox2.Visible = false;
                checkBox3.Visible = false;
                checkBox4.Visible = false;
                checkBox5.Visible = false;
                checkBox6.Visible = false;
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) trauma = "1";
            else trauma = "0";
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked) infection = "1";
            else infection = "0";
        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked) textBox5.Visible = true;
            else textBox5.Visible = false;
        }
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked) hypothermia = "1";
            else hypothermia = "0";
        }
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked) insolation = "1";
            else insolation = "0";
        }
        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked) other_factor = "1";
            else other_factor = "0";
        }
        public static string Reader (string sql)
        {
            command = new NpgsqlCommand(sql, Form1.connection);
            Form1.connection.Open();
            reader = command.ExecuteReader();
            reader.Read();
            string text;
            try
            {
                text = reader[0].ToString();
            }
            catch
            {
                text = "";
            }
            Form1.connection.Close();
            return text;
        }
        private void comboBox5_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox5.SelectedItem.ToString() == "Да") status = "1";
            else status = "0";
        }
        private void Disease_Load(object sender, EventArgs e)
        {
            command = new NpgsqlCommand("SELECT CONCAT_WS(' ', mkb_kod, mkb_name) FROM diagn_mkb", Form1.connection);
            Form1.connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader[0]);
                comboBox3.Items.Add(reader[0]);
            }
            Form1.connection.Close();
            command = new NpgsqlCommand("SELECT ilar_name FROM diagn_ilar", Form1.connection);
            Form1.connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                comboBox2.Items.Add(reader[0]);
                comboBox4.Items.Add(reader[0]);
            }
            Form1.connection.Close();
            dateTimePicker1.Text = Reader("SELECT start_date FROM info_disease WHERE p_id = " + (Form2.nump + 1));
            dateTimePicker2.Text = Reader("SELECT date_of_appeal FROM info_disease WHERE p_id = " + (Form2.nump + 1));
            string n = "";
            foreach (char c in DateTime.Now.Subtract(dateTimePicker1.Value).ToString())
            {
                if (!char.IsDigit(c)) break;
                else n += c;
            }
            textBox1.Text = (Convert.ToInt32(n)/30).ToString();
            n = "";
            foreach (char c in dateTimePicker2.Value.Subtract(dateTimePicker1.Value).ToString())
            {
                if (!char.IsDigit(c)) break;
                else n += c;
            }
            textBox2.Text = (Convert.ToInt32(n) / 30).ToString();
            mkb_id = Reader("SELECT mkb_id FROM diagnosis_by_debut WHERE p_id = " + (Form2.nump + 1));
            comboBox1.Text = Reader("SELECT CONCAT_WS(' ', mkb_kod, mkb_name) FROM diagn_mkb INNER JOIN diagnosis_by_debut ON diagnosis_by_debut.mkb_id = diagn_mkb.mkb_id AND p_id = " + (Form2.nump + 1));
            dateTimePicker3.Text = Reader("SELECT dbd_date_mkb FROM diagnosis_by_debut WHERE p_id = " + (Form2.nump + 1));
            comboBox2.Text = Reader("SELECT ilar_name FROM diagn_ilar INNER JOIN diagnosis_by_debut ON diagnosis_by_debut.ilar_id = diagn_ilar.ilar_id AND p_id = " + (Form2.nump + 1));
            dateTimePicker4.Text = Reader("SELECT dbd_date_ilar FROM diagnosis_by_debut WHERE p_id = " + (Form2.nump + 1));
            ilar_id = Reader("SELECT ilar_id FROM diagnosis_by_debut WHERE p_id = " + (Form2.nump + 1));
            mkb_id2 = Reader("SELECT mkb_id FROM current_diagnosis WHERE p_id = " + (Form2.nump + 1));
            ilar_id2 = Reader("SELECT ilar_id FROM current_diagnosis WHERE p_id = " + (Form2.nump + 1));
            comboBox3.Text = Reader("SELECT CONCAT_WS(' ', mkb_kod, mkb_name) FROM diagn_mkb INNER JOIN current_diagnosis ON current_diagnosis.mkb_id = diagn_mkb.mkb_id AND p_id = " + (Form2.nump + 1));
            dateTimePicker5.Text = Reader("SELECT cd_date_mkb FROM current_diagnosis WHERE p_id = " + (Form2.nump + 1));
            comboBox4.Text = Reader("SELECT ilar_name FROM diagn_ilar INNER JOIN current_diagnosis ON current_diagnosis.ilar_id = diagn_ilar.ilar_id AND p_id = " + (Form2.nump + 1));
            dateTimePicker6.Text = Reader("SELECT cd_date_ilar FROM current_diagnosis WHERE p_id = " + (Form2.nump + 1));
            if (Reader("SELECT ps_remission FROM pat_state WHERE p_id = " + (Form2.nump + 1)) == "1")
            {
                comboBox5.Text = "Да";
                status = "1";
            }
            else if (Reader("SELECT ps_remission FROM pat_state WHERE p_id = " + (Form2.nump + 1)) == "0")
            {
                comboBox5.Text = "Нет";
                status = "0";
            }
            textBox3.Text = Reader("SELECT ps_activity FROM pat_state WHERE p_id = " + (Form2.nump + 1));
            textBox4.Text = Reader("SELECT ps_index_chaq FROM pat_state WHERE p_id = " + (Form2.nump + 1));
            command = new NpgsqlCommand("SELECT * FROM disease_factors WHERE p_id = " + (Form2.nump + 1), Form1.connection);
            Form1.connection.Open();
            reader = command.ExecuteReader();
            reader.Read();
            if (reader.HasRows)
            {
                comboBox6.Text = "Да";
                checkBox1.Visible = true;
                checkBox2.Visible = true;
                checkBox3.Visible = true;
                checkBox4.Visible = true;
                checkBox5.Visible = true;
                checkBox6.Visible = true;
            }
            Form1.connection.Close();
            if (Reader("SELECT trauma FROM disease_factors WHERE p_id = " + (Form2.nump + 1)) == "1") checkBox1.Checked = true;
            if (Reader("SELECT infection FROM disease_factors WHERE p_id = " + (Form2.nump + 1)) == "1") checkBox2.Checked = true;
            if (Reader("SELECT vaccine FROM disease_factors WHERE p_id = " + (Form2.nump + 1)) != "")
            {
                checkBox3.Checked = true;
                textBox5.Text = Reader("SELECT vaccine FROM disease_factors WHERE p_id = " + (Form2.nump + 1));
            }
            if (Reader("SELECT hypothermia FROM disease_factors WHERE p_id = " + (Form2.nump + 1)) == "1") checkBox4.Checked = true;
            if (Reader("SELECT insolation FROM disease_factors WHERE p_id = " + (Form2.nump + 1)) == "1") checkBox5.Checked = true;
            if (Reader("SELECT other_factor FROM disease_factors WHERE p_id = " + (Form2.nump + 1)) == "1") checkBox6.Checked = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            command = new NpgsqlCommand("SELECT * FROM info_disease WHERE p_id = " + (Form2.nump + 1), Form1.connection);
            Form1.connection.Open();
            reader = command.ExecuteReader();
            reader.Read();
            if (!reader.HasRows)
            {
                Form1.connection.Close();
                string insert = "INSERT INTO info_disease VALUES (" + (Form2.nump + 1) + ", '" + dateTimePicker1.Value.ToShortDateString() + "', '" + dateTimePicker2.Value.ToShortDateString() + "')";
                Form2.Execute(insert);
            }
            else
            {
                Form1.connection.Close();
                string update = "UPDATE info_disease SET start_date = '" + dateTimePicker1.Value.ToShortDateString() + "', date_of_appeal = '" + dateTimePicker2.Value.ToShortDateString() + "' WHERE p_id = " + (Form2.nump + 1);
                Form2.Execute(update);
            }
            command = new NpgsqlCommand("SELECT * FROM diagnosis_by_debut WHERE p_id = " + (Form2.nump + 1), Form1.connection);
            Form1.connection.Open();
            reader = command.ExecuteReader();
            reader.Read();
            if (!reader.HasRows)
            {
                Form1.connection.Close();
                string insert = "INSERT INTO diagnosis_by_debut VALUES (" + (Form2.nump + 1) + ", " + mkb_id + ", '" + dateTimePicker3.Value.ToShortDateString() + "', '" + dateTimePicker4.Value.ToShortDateString() + "', " + ilar_id + ")";
                Form2.Execute(insert);
            }
            else
            {
                Form1.connection.Close();
                string update = "UPDATE diagnosis_by_debut SET mkb_id = " + mkb_id + ", dbd_date_mkb = '" + dateTimePicker3.Value.ToShortDateString() + "', dbd_date_ilar = '" + dateTimePicker4.Value.ToShortDateString() + "', ilar_id = " + ilar_id + " WHERE p_id = " + (Form2.nump + 1);
                Form2.Execute(update);
            }
            command = new NpgsqlCommand("SELECT * FROM current_diagnosis WHERE p_id = " + (Form2.nump + 1), Form1.connection);
            Form1.connection.Open();
            reader = command.ExecuteReader();
            reader.Read();
            if (!reader.HasRows)
            {
                Form1.connection.Close();
                string insert = "INSERT INTO current_diagnosis VALUES (" + (Form2.nump + 1) + ", '" + dateTimePicker5.Value.ToShortDateString() + "', " + mkb_id2 + ", '" + dateTimePicker6.Value.ToShortDateString() + "', " + ilar_id2 + ")";
                Form2.Execute(insert);
            }
            else
            {
                Form1.connection.Close();
                string update = "UPDATE current_diagnosis SET mkb_id = " + mkb_id2 + ", cd_date_mkb = '" + dateTimePicker5.Value.ToShortDateString() + "', cd_date_ilar = '" + dateTimePicker6.Value.ToShortDateString() + "', ilar_id = " + ilar_id2 + " WHERE p_id = " + (Form2.nump + 1);
                Form2.Execute(update);
            }
            command = new NpgsqlCommand("SELECT * FROM pat_state WHERE p_id = " + (Form2.nump + 1), Form1.connection);
            Form1.connection.Open();
            reader = command.ExecuteReader();
            reader.Read();
            if (!reader.HasRows)
            {
                Form1.connection.Close();
                string insert = "INSERT INTO pat_state VALUES (" + (Form2.nump + 1) + ", '" + status + "', '" + textBox3.Text + "', '" + textBox4.Text + "')";
                Form2.Execute(insert);
            }
            else
            {
                Form1.connection.Close();
                string update = "UPDATE pat_state SET ps_remission = '" + status + "', ps_activity = '" + textBox3.Text + "', ps_index_chaq = " + textBox4.Text + " WHERE p_id = " + (Form2.nump + 1);
                Form2.Execute(update);
            }
            command = new NpgsqlCommand("SELECT * FROM disease_factors WHERE p_id = " + (Form2.nump + 1), Form1.connection);
            Form1.connection.Open();
            reader = command.ExecuteReader();
            reader.Read();
            if (!reader.HasRows)
            {
                Form1.connection.Close();
                if (comboBox6.Text == "Да")
                {
                    string insert = "INSERT INTO disease_factors VALUES (" + (Form2.nump + 1) + ", '" + trauma + "', '" + infection + "', ";
                    if (checkBox3.Checked) insert += "'" + textBox5.Text + "'";
                    else insert += "null";
                    insert += ", '" + hypothermia + "', '" + insolation + "', '" + other_factor + "'";
                    Form2.Execute(insert);
                }
            }
            else
            {
                Form1.connection.Close();
                if (comboBox6.Text == "Да")
                {
                    string update = "UPDATE disease_factors SET trauma = '" + trauma + "', infection = '" + infection + "', ";
                    if (checkBox3.Checked) update += "vaccine = '" + textBox5.Text + "', ";
                    update += "hypothermia = '" + hypothermia + "', insolation = '" + insolation + "', other_factor = '" + other_factor + "' WHERE p_id = " + (Form2.nump + 1);
                    Form2.Execute(update);
                }
                else
                {
                    Form2.Execute("DELETE FROM disease_factors WHERE p_id = " + (Form2.nump + 1));
                }
            }
        }
    }
}
