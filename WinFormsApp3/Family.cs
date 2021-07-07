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
    public partial class Family : Form
    {
        public Family()
        {
            InitializeComponent();
        }
        NpgsqlCommand command;
        NpgsqlDataReader reader;
        string fam_id, m_id, f_id;
        string fam_guardian, m_is, f_is, psoriasis, uveitis, rheumatic, tumors;
        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem.ToString() == "Да") fam_guardian = "1";
            else fam_guardian = "0";
        }
        private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem.ToString() == "Да") m_is = "1";
            else m_is = "0";
        }
        private void comboBox4_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox4.SelectedItem.ToString() == "Да") f_is = "1";
            else f_is = "0";
        }
        private void comboBox5_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox5.SelectedItem.ToString() == "Да") psoriasis = "1";
            else psoriasis = "0";
        }
        private void comboBox6_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox6.SelectedItem.ToString() == "Да") uveitis = "1";
            else uveitis = "0";
        }
        private void comboBox7_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox7.SelectedItem.ToString() == "Да") rheumatic = "1";
            else rheumatic = "0";
        }
        private void comboBox8_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox8.SelectedItem.ToString() == "Да") tumors = "1";
            else tumors = "0";
        }
        private void Family_Load(object sender, EventArgs e)
        {
            fam_id = Disease.Reader("SELECT fam_id FROM family WHERE p_id = " + (Form2.nump + 1));
            comboBox1.Text = Disease.Reader("SELECT fam_struct FROM family WHERE p_id = " + (Form2.nump + 1));
            string yn = Disease.Reader("SELECT fam_guardian FROM family WHERE p_id = " + (Form2.nump + 1));
            if (yn == "1")
            {
                comboBox2.Text = "Да";
                fam_guardian = "1";
            }
            else if (yn == "0")
            {
                comboBox2.Text = "Нет";
                fam_guardian = "0";
            }
            if (fam_id != "")
            {
                m_id = Disease.Reader("SELECT m_id FROM mothers WHERE fam_id = " + fam_id);
                yn = Disease.Reader("SELECT m_is FROM mothers WHERE fam_id = " + fam_id);
                if (yn == "1")
                {
                    comboBox3.Text = "Да";
                    m_is = "1";
                }
                else if (yn == "0")
                {
                    comboBox3.Text = "Нет";
                    m_is = "0";
                }
                textBox1.Text = Disease.Reader("SELECT m_surname FROM mothers WHERE fam_id = " + fam_id);
                textBox2.Text = Disease.Reader("SELECT m_name FROM mothers WHERE fam_id = " + fam_id);
                textBox3.Text = Disease.Reader("SELECT m_patr FROM mothers WHERE fam_id = " + fam_id);
                dateTimePicker1.Text = Disease.Reader("SELECT m_dob FROM mothers WHERE fam_id = " + fam_id);
                textBox4.Text = Disease.Reader("SELECT m_status FROM mothers WHERE fam_id = " + fam_id);
                f_id = Disease.Reader("SELECT f_id FROM fathers WHERE fam_id = " + fam_id);
                yn = Disease.Reader("SELECT f_is FROM fathers WHERE fam_id = " + fam_id);
                if (yn == "1")
                {
                    comboBox4.Text = "Да";
                    f_is = "1";
                }
                else if (yn == "0")
                {
                    comboBox4.Text = "Нет";
                    f_is = "0";
                }
                textBox5.Text = Disease.Reader("SELECT f_surname FROM fathers WHERE fam_id = " + fam_id);
                textBox6.Text = Disease.Reader("SELECT f_name FROM fathers WHERE fam_id = " + fam_id);
                textBox7.Text = Disease.Reader("SELECT f_patr FROM fathers WHERE fam_id = " + fam_id);
                dateTimePicker2.Text = Disease.Reader("SELECT f_dob FROM fathers WHERE fam_id = " + fam_id);
                textBox8.Text = Disease.Reader("SELECT f_status FROM fathers WHERE fam_id = " + fam_id);
            }
            yn = Disease.Reader("SELECT psoriasis FROM family_anamnesis WHERE p_id = " + (Form2.nump + 1));
            if (yn == "1")
            {
                comboBox5.Text = "Да";
                psoriasis = "1";
            }
            else if (yn == "0")
            {
                comboBox5.Text = "Нет";
                psoriasis = "0";
            }
            yn = Disease.Reader("SELECT uveitis FROM family_anamnesis WHERE p_id = " + (Form2.nump + 1));
            if (yn == "1")
            {
                comboBox6.Text = "Да";
                uveitis = "1";
            }
            else if (yn == "0")
            {
                comboBox6.Text = "Нет";
                uveitis = "0";
            }
            yn = Disease.Reader("SELECT rheumatic FROM family_anamnesis WHERE p_id = " + (Form2.nump + 1));
            if (yn == "1")
            {
                comboBox7.Text = "Да";
                rheumatic = "1";
            }
            else if (yn == "0")
            {
                comboBox7.Text = "Нет";
                rheumatic = "0";
            }
            yn = Disease.Reader("SELECT tumors FROM family_anamnesis WHERE p_id = " + (Form2.nump + 1));
            if (yn == "1")
            {
                comboBox8.Text = "Да";
                tumors = "1";
            }
            else if (yn == "0")
            {
                comboBox8.Text = "Нет";
                tumors = "0";
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            command = new NpgsqlCommand("SELECT * FROM family WHERE p_id = " + (Form2.nump + 1), Form1.connection);
            Form1.connection.Open();
            reader = command.ExecuteReader();
            reader.Read();
            if (!reader.HasRows)
            {
                Form1.connection.Close();
                fam_id = Disease.Reader("SELECT MAX(fam_id) + 1 FROM family");
                string insert = "INSERT INTO family VALUES (" + (Form2.nump + 1) + ", " + fam_id + ", '" + comboBox1.Text + "', '" + fam_guardian + "')";
                Form2.Execute(insert);
            }
            else
            {
                Form1.connection.Close();
                string update = "UPDATE family SET fam_struct = '" + comboBox1.Text + "', fam_guardian = '" + fam_guardian + "' WHERE p_id = " + (Form2.nump + 1);
                Form2.Execute(update);
            }
            command = new NpgsqlCommand("SELECT * FROM mothers WHERE fam_id = " + fam_id, Form1.connection);
            Form1.connection.Open();
            reader = command.ExecuteReader();
            reader.Read();
            if (!reader.HasRows)
            {
                Form1.connection.Close();
                m_id = Disease.Reader("SELECT MAX(m_id) + 1 FROM mothers");
                string insert = "INSERT INTO mothers ";
                string order = "(m_id, m_is, m_surname, m_name";
                string values = "(" + m_id + ", '" + m_is + "', '" + textBox1.Text + "', '" + textBox2.Text + "'";
                if (textBox3.Text != "")
                {
                    order += ", m_patr";
                    values += ", '" + textBox3.Text + "'";
                }
                if (dateTimePicker1.Value != DateTime.Now)
                {
                    order += ", m_dob";
                    values += ", '" + dateTimePicker1.Value.ToShortDateString() + "'";
                }
                if (textBox4.Text != "")
                {
                    order += ", m_status";
                    values += ", '" + textBox4.Text + "'";
                }
                order += ", fam_id)"; values += ", " + fam_id + ")";
                insert += order + " VALUES " + values;
                Form2.Execute(insert);
            }
            else
            {
                Form1.connection.Close();
                string update = "UPDATE mothers SET m_is = '" + m_is + "', m_surname = '" + textBox1.Text + "', m_name = '" + textBox2.Text + "'";
                if (textBox3.Text != "")
                {
                    update += ", m_patr = '" + textBox3.Text + "'";
                }
                if (dateTimePicker1.Value != DateTime.Now)
                {
                    update += ", m_dob = '" + dateTimePicker1.Value.ToShortDateString() + "'";
                }
                if (textBox4.Text != "")
                {
                    update += ", m_status = '" + textBox4.Text + "'";
                }
                update += " WHERE m_id = " + m_id;
                Form2.Execute(update);
            }
            command = new NpgsqlCommand("SELECT * FROM fathers WHERE fam_id = " + fam_id, Form1.connection);
            Form1.connection.Open();
            reader = command.ExecuteReader();
            reader.Read();
            if (!reader.HasRows)
            {
                Form1.connection.Close();
                f_id = Disease.Reader("SELECT MAX(f_id) + 1 FROM fathers");
                string insert = "INSERT INTO fathers ";
                string order = "(f_id, f_is, f_surname, f_name";
                string values = "(" + f_id + ", '" + f_is + "', '" + textBox5.Text + "', '" + textBox6.Text + "'";
                if (textBox7.Text != "")
                {
                    order += ", f_patr";
                    values += ", '" + textBox7.Text + "'";
                }
                if (dateTimePicker2.Value != DateTime.Now)
                {
                    order += ", f_dob";
                    values += ", '" + dateTimePicker2.Value.ToShortDateString() + "'";
                }
                if (textBox8.Text != "")
                {
                    order += ", f_status";
                    values += ", '" + textBox8.Text + "'";
                }
                order += ", fam_id)"; values += ", " + fam_id + ")";
                insert += order + " VALUES " + values;
                Form2.Execute(insert);
            }
            else
            {
                Form1.connection.Close();
                string update = "UPDATE fathers SET f_is = '" + f_is + "', f_surname = '" + textBox1.Text + "', f_name = '" + textBox2.Text + "'";
                if (textBox7.Text != "")
                {
                    update += ", f_patr = '" + textBox7.Text + "'";
                }
                if (dateTimePicker2.Value != DateTime.Now)
                {
                    update += ", f_dob = '" + dateTimePicker2.Value.ToShortDateString() + "'";
                }
                if (textBox8.Text != "")
                {
                    update += ", f_status = '" + textBox8.Text + "'";
                }
                update += " WHERE f_id = " + f_id;
                Form2.Execute(update);
            }
            command = new NpgsqlCommand("SELECT * FROM family_anamnesis WHERE p_id = " + (Form2.nump + 1), Form1.connection);
            Form1.connection.Open();
            reader = command.ExecuteReader();
            reader.Read();
            if (!reader.HasRows)
            {
                Form1.connection.Close();
                string insert = "INSERT INTO family_anamnesis VALUES (" + (Form2.nump + 1) + ", '" + psoriasis + "', '" + uveitis + "', '" + rheumatic + "', '" + tumors + "')";
                Form2.Execute(insert);
            }
            else
            {
                Form1.connection.Close();
                string update = "UPDATE family_anamnesis SET psoriasis = '" + psoriasis + "', uveitis = '" + uveitis + "', rheumatic = '" + rheumatic + "', tumors = '" + tumors + "' WHERE p_id = " + (Form2.nump + 1);
                Form2.Execute(update);
            }
        }
    }
}
