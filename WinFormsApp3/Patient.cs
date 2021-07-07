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
    public partial class Patient : Form
    {
        public Patient()
        {
            InitializeComponent();
        }
        static NpgsqlCommand command;
        NpgsqlDataReader reader;
        private string doc_id, hos_id, fc_id, iac_id;
        private string hczd = "", fgu = "", niir = "", dgkb = "", mgmy = "", iac_status = "0";
        private void Fc_check ()
        {
            bool flag = true;
            command = new NpgsqlCommand("SELECT * FROM federal_centers", Form1.connection);
            Form1.connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
                if ((reader["fc_hczd"].ToString() == hczd) && (reader["fc_fgu"].ToString() == fgu) && (reader["fc_niir"].ToString() == niir) && (reader["fc_dgkb"].ToString() == dgkb) && (reader["fc_mgmy"].ToString() == mgmy))
                {
                    flag = false;
                    fc_id = reader["fc_id"].ToString();
                    break;
                }
            Form1.connection.Close();
            if (flag)
            {
                command = new NpgsqlCommand("INSERT INTO federal_centers (fc_hczd, fc_fgu, fc_niir, fc_dgkb, fc_mgmy) VALUES fc_hczd = '" + hczd + "', fc_fgu = '" + fgu + "', fc_niir = '" + niir + "', fc_dgkb = '" + dgkb + "', fc_mgmy = '" + mgmy + "'", Form1.connection);
                Form1.connection.Open();
                command.ExecuteNonQuery();
                Form1.connection.Close();
                command = new NpgsqlCommand("SELECT MAX(fc_id) FROM federal_centers", Form1.connection);
                Form1.connection.Open();
                reader = command.ExecuteReader();
                reader.Read();
                fc_id = reader[0].ToString();
                Form1.connection.Close();
            }
        }
        private void Iac_check ()
        {
            if (Form2.nump == -1)
            {
                command = new NpgsqlCommand("SELECT MAX(iac_id) + 1 FROM inf_about_consent", Form1.connection);
                Form1.connection.Open();
                reader = command.ExecuteReader();
                reader.Read();
                iac_id = reader[0].ToString();
                Form1.connection.Close();
                string sql = "INSERT INTO inf_about_consent (iac_id, iac_status";
                string values = "(" + iac_id + ", '" + iac_status + "'";
                if (iac_status == "1")
                {
                    sql += ", iac_date";
                    values += ", '" + dateTimePicker1.Value.ToShortDateString() + "'";
                }
                sql += ")";
                values += ")";
                sql += " VALUES " + values;
                command = new NpgsqlCommand(sql, Form1.connection);
                Form1.connection.Open();
                command.ExecuteNonQuery();
                Form1.connection.Close();
            }
            else
            {
                string sql = "UPDATE inf_about_consent SET iac_status = '" + iac_status + "'";
                if (iac_status == "1")
                    sql += ", iac_date = '" + dateTimePicker1.Value.ToShortDateString() + "'";
                else
                    sql += ", iac_date = null";
                sql += " WHERE iac_id = " + iac_id;
                command = new NpgsqlCommand(sql, Form1.connection);
                Form1.connection.Open();
                command.ExecuteNonQuery();
                Form1.connection.Close();
            }
        }
        private string Get_phone ()
        {
            string tel = maskedTextBox1.Text.Replace(" (", "");
            tel = tel.Replace(") ", "");
            tel = tel.Replace("-", "");
            return tel;
        }
        private string Get_id ()
        {
            string id;
            command = new NpgsqlCommand("SELECT MAX(p_id) + 1 FROM patients", Form1.connection);
            Form1.connection.Open();
            reader = command.ExecuteReader();
            reader.Read();
            id = reader[0].ToString();
            Form1.connection.Close();
            return id;
        }
        private bool Snils_check()
        {
            foreach (char c in maskedTextBox2.Text)
            {
                if (char.IsDigit(c)) return true;
            }
            return false;
        }
        private void Reg_check()
        {
            string reg_id, sql;
            command = new NpgsqlCommand("SELECT reg_id FROM reg_addresses WHERE p_id = " + (Form2.nump + 1), Form1.connection);
            Form1.connection.Open();
            reader = command.ExecuteReader();
            reader.Read();
            try
            {
                reg_id = reader["reg_id"].ToString();
            }
            catch
            {
                reg_id = "";
            }
            Form1.connection.Close();
            if (reg_id == "")
            {
                sql = "INSERT INTO reg_addresses (p_id, reg_region, reg_district, reg_adress) VALUES (" + (Form2.nump + 1) + ", '" + textBox6.Text + "', '" + textBox7.Text + "', '" + textBox8.Text + "')";
            }
            else
            {
                sql = "UPDATE reg_addresses SET reg_region = '" + textBox6.Text + "', reg_district = '" + textBox7.Text + "', reg_adress = '" + textBox8.Text + "' WHERE p_id = " + (Form2.nump + 1);
            }
            Form2.Execute(sql);
        }
        private void Res_check()
        {
            string res_id, sql;
            command = new NpgsqlCommand("SELECT res_id FROM res_addresses WHERE p_id = " + (Form2.nump + 1), Form1.connection);
            Form1.connection.Open();
            reader = command.ExecuteReader();
            reader.Read();
            try
            {
                res_id = reader["res_id"].ToString();
            }
            catch
            {
                res_id = "";
            }
            Form1.connection.Close();
            if (res_id == "")
            {
                sql = "INSERT INTO res_addresses (p_id, res_region, res_district, res_adress) VALUES (" + (Form2.nump + 1) + ", '" + textBox9.Text + "', '" + textBox10.Text + "', '" + textBox11.Text + "')";
            }
            else
            {
                sql = "UPDATE res_addresses SET res_region = '" + textBox9.Text + "', res_district = '" + textBox10.Text + "', res_adress = '" + textBox11.Text + "' WHERE p_id = " + (Form2.nump + 1);
            }
            Form2.Execute(sql);
        }
        private void AddInfo_check()
        {
            string ai_id, sql, order, values;
            command = new NpgsqlCommand("SELECT ai_id FROM add_info WHERE p_id = " + (Form2.nump + 1), Form1.connection);
            Form1.connection.Open();
            reader = command.ExecuteReader();
            reader.Read();
            try
            {
                ai_id = reader["ai_id"].ToString();
            }
            catch
            {
                ai_id = "";
            }
            Form1.connection.Close();
            if (ai_id == "")
            {
                order = "(p_id"; values = "(" + (Form2.nump + 1);
                if (Snils_check())
                {
                    order += ", snils"; values += ", '" + maskedTextBox2.Text + "'";
                }
                if (textBox12.Text != "")
                {
                    order += ", kod_lgoty"; values += ", '" + textBox12.Text + "'";
                }
                if (textBox13.Text != "")
                {
                    order += ", pasport"; values += ", '" + textBox13.Text + "'";
                }
                if (textBox14.Text != "")
                {
                    order += ", oms"; values += ", '" + textBox14.Text + "'";
                }
                if (textBox15.Text != "")
                {
                    order += ", strahov"; values += ", '" + textBox15.Text + "'";
                }
                order += ")"; values += ")";
                sql = "INSERT INTO add_info " + order + " VALUES " + values;
            }
            else
            {
                bool f = false;
                sql = "UPDATE add_info SET ";
                if (Snils_check())
                {
                    sql += "snils = '" + maskedTextBox2.Text + "', ";
                }
                if (textBox12.Text != "")
                {
                    sql += "kod_lgoty = '" + textBox12.Text + "', ";
                    f = true;
                }
                if (textBox13.Text != "")
                {
                    sql += "pasport = '" + textBox13.Text + "', ";
                    f = true;
                }
                if (textBox14.Text != "")
                {
                    sql += "oms = '" + textBox14.Text + "', ";
                    f = true;
                }
                if (textBox15.Text != "")
                {
                    sql += "strahov = '" + textBox15.Text + "'";
                }
                if (!f) sql = sql.Remove(sql.Length-2);
                sql += " WHERE p_id = " + (Form2.nump + 1);
            }
            Form2.Execute(sql);
        }
        private void Disability_check()
        {
            string sql, order, values;
            bool flag = true;
            command = new NpgsqlCommand("SELECT p_id FROM disability_status", Form1.connection);
            Form1.connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
                if (reader["p_id"].ToString() == (Form2.nump + 1).ToString())
                {
                    flag = false;
                    break;
                }
            Form1.connection.Close();
            if (flag)
            {
                order = "(p_id"; values = "(" + (Form2.nump + 1);
                if (comboBox6.Text != "")
                {
                    order += ", disabbility_now"; values += ", '" + comboBox6.Text + "'";
                }
                if (comboBox7.Text != "")
                {
                    order += ", certif_of_dis"; values += ", '" + comboBox7.Text + "'";
                }
                if (comboBox8.Text != "")
                {
                    order += ", social_psckage"; values += ", '" + comboBox8.Text + "'";
                }
                order += ", date_last)"; values += ", '" + dateTimePicker3.Value.ToShortDateString() + "')";
                sql = "INSERT INTO disability_status " + order + " VALUES " + values;
            }
            else
            {
                sql = "UPDATE disability_status SET ";
                if (comboBox6.Text != "")
                {
                    sql += "disabbility_now = '" + comboBox6.Text + "', ";
                }
                if (comboBox7.Text != "")
                {
                    sql += "certif_of_dis = '" + comboBox7.Text + "', ";
                }
                if (comboBox8.Text != "")
                {
                    sql += "social_psckage = '" + comboBox8.Text + "', ";
                }
                sql += "date_last = '" + dateTimePicker3.Value.ToShortDateString() + "'";
                sql += " WHERE p_id = " + (Form2.nump + 1);
            }
            Form2.Execute(sql);
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) hczd = "1";
            else hczd = "";
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked) fgu = "1";
            else fgu = "";
        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked) niir = "1";
            else niir = "";
        }
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked) dgkb = "1";
            else dgkb = "";
        }
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked) mgmy = "1";
            else mgmy = "";
        }
        private void Patient_Load(object sender, EventArgs e)
        {
            command = new NpgsqlCommand("SELECT CONCAT_WS(' ', doc_surname, doc_name, doc_patr) FROM doctors", Form1.connection);
            Form1.connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
                comboBox1.Items.Add(reader[0]);
            Form1.connection.Close();
            command = new NpgsqlCommand("SELECT CONCAT_WS(' ', hos_name, hos_city) FROM hospital", Form1.connection);
            Form1.connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
                comboBox2.Items.Add(reader[0]);
            Form1.connection.Close();
            if (Form2.nump > -1)
            {
                label2.Text = Form2.pname + " ";
                command = new NpgsqlCommand("SELECT p_dob FROM patients WHERE p_id = " + (Form2.nump + 1), Form1.connection);
                Form1.connection.Open();
                reader = command.ExecuteReader();
                reader.Read();
                label2.Text += reader[0].ToString();
                Form1.connection.Close();
                label2.Text = label2.Text.Replace(" 0:00:00", "");
                command = new NpgsqlCommand("SELECT patients.doc_id, CONCAT_WS(' ', doc_surname, doc_name, doc_patr) FROM doctors INNER JOIN patients ON patients.doc_id = doctors.doc_id AND p_id = " + (Form2.nump + 1), Form1.connection);
                Form1.connection.Open();
                reader = command.ExecuteReader();
                reader.Read();
                doc_id = reader[0].ToString();
                comboBox1.Text = reader[1].ToString();
                Form1.connection.Close();
                command = new NpgsqlCommand("SELECT patients.hos_id, CONCAT_WS(' ', hos_name, hos_city) FROM hospital INNER JOIN patients ON patients.hos_id = hospital.hos_id AND p_id = " + (Form2.nump + 1), Form1.connection);
                Form1.connection.Open();
                reader = command.ExecuteReader();
                reader.Read();
                hos_id = reader[0].ToString();
                comboBox2.Text = reader[1].ToString();
                Form1.connection.Close();
                command = new NpgsqlCommand("SELECT * FROM federal_centers INNER JOIN patients ON patients.fc_id = federal_centers.fc_id AND p_id = " + (Form2.nump + 1), Form1.connection);
                Form1.connection.Open();
                reader = command.ExecuteReader();
                reader.Read();
                fc_id = reader["fc_id"].ToString();
                if (reader["fc_hczd"].ToString() == "1") checkBox1.Checked = true;
                if (reader["fc_fgu"].ToString() == "1") checkBox2.Checked = true;
                if (reader["fc_niir"].ToString() == "1") checkBox3.Checked = true;
                if (reader["fc_dgkb"].ToString() == "1") checkBox4.Checked = true;
                if (reader["fc_mgmy"].ToString() == "1") checkBox5.Checked = true;
                Form1.connection.Close();
                command = new NpgsqlCommand("SELECT * FROM inf_about_consent INNER JOIN patients ON patients.iac_id = inf_about_consent.iac_id AND p_id = " + (Form2.nump + 1), Form1.connection);
                Form1.connection.Open();
                reader = command.ExecuteReader();
                reader.Read();
                iac_id = reader["iac_id"].ToString();
                if (reader["iac_status"].ToString() == "1")
                {
                    comboBox3.Text = "Да";
                    dateTimePicker1.Text = reader["iac_date"].ToString();
                }
                else
                {
                    comboBox3.Text = "Нет";
                    dateTimePicker1.Enabled = false;
                }
                Form1.connection.Close();
                command = new NpgsqlCommand("SELECT * FROM patients WHERE p_id = " + (Form2.nump + 1), Form1.connection);
                Form1.connection.Open();
                reader = command.ExecuteReader();
                reader.Read();
                textBox1.Text = reader["p_surname"].ToString();
                textBox2.Text = reader["p_name"].ToString();
                textBox3.Text = reader["p_patr"].ToString();
                dateTimePicker2.Text = reader["p_dob"].ToString();
                comboBox4.Text = reader["p_gender"].ToString();
                comboBox5.Text = reader["p_live"].ToString();
                textBox4.Text = reader["p_nationality"].ToString();
                if (reader["p_phone"].ToString() != "")
                    maskedTextBox1.Text = reader["p_phone"].ToString()[0] + " (" + reader["p_phone"].ToString().Substring(1, 3) + ") " + reader["p_phone"].ToString().Substring(4, 3) + "-" + reader["p_phone"].ToString().Substring(7, 4);
                textBox5.Text = reader["p_email"].ToString();
                Form1.connection.Close();
                command = new NpgsqlCommand("SELECT * FROM reg_addresses WHERE p_id = " + (Form2.nump + 1), Form1.connection);
                Form1.connection.Open();
                reader = command.ExecuteReader();
                reader.Read();
                try
                {
                    textBox6.Text = reader["reg_region"].ToString();
                    textBox7.Text = reader["reg_district"].ToString();
                    textBox8.Text = reader["reg_adress"].ToString();
                }
                catch { }
                Form1.connection.Close();
                command = new NpgsqlCommand("SELECT * FROM res_addresses WHERE p_id = " + (Form2.nump + 1), Form1.connection);
                Form1.connection.Open();
                reader = command.ExecuteReader();
                reader.Read();
                try
                {
                    if ((textBox6.Text == reader["res_region"].ToString()) && (textBox7.Text == reader["res_district"].ToString()) && (textBox8.Text == reader["res_adress"].ToString()))
                        checkBox6.Checked = true;
                    else
                    {
                        textBox9.Text = reader["res_region"].ToString();
                        textBox10.Text = reader["res_district"].ToString();
                        textBox11.Text = reader["res_adress"].ToString();
                    }
                }
                catch { }
                Form1.connection.Close();
                command = new NpgsqlCommand("SELECT * FROM add_info WHERE p_id = " + (Form2.nump + 1), Form1.connection);
                Form1.connection.Open();
                reader = command.ExecuteReader();
                reader.Read();
                try
                {
                    maskedTextBox2.Text = reader["snils"].ToString();
                    textBox12.Text = reader["kod_lgoty"].ToString();
                    textBox13.Text = reader["pasport"].ToString();
                    textBox14.Text = reader["oms"].ToString();
                    textBox15.Text = reader["strahov"].ToString();
                }
                catch { }
                Form1.connection.Close();
                command = new NpgsqlCommand("SELECT * FROM disability_status WHERE p_id = " + (Form2.nump + 1), Form1.connection);
                Form1.connection.Open();
                reader = command.ExecuteReader();
                reader.Read();
                try
                {
                    comboBox6.Text = reader["disabbility_now"].ToString();
                    comboBox7.Text = reader["certif_of_dis"].ToString();
                    comboBox8.Text = reader["social_psckage"].ToString();
                    dateTimePicker3.Text = reader["date_last"].ToString();
                }
                catch { }
                Form1.connection.Close();
            }
        }
        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                textBox9.Text = textBox6.Text;
                textBox10.Text = textBox7.Text;
                textBox11.Text = textBox8.Text;
                textBox9.Enabled = false;
                textBox10.Enabled = false;
                textBox11.Enabled = false;
            }
            else
            {
                textBox9.Enabled = true;
                textBox10.Enabled = true;
                textBox11.Enabled = true;
            }
        }
        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            command = new NpgsqlCommand("SELECT doc_id FROM doctors WHERE CONCAT_WS(' ', doc_surname, doc_name, doc_patr) = '" + comboBox1.SelectedItem.ToString() + "'", Form1.connection);
            Form1.connection.Open();
            reader = command.ExecuteReader();
            reader.Read();
            doc_id = reader[0].ToString();
            Form1.connection.Close();
        }
        private void comboBox2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            command = new NpgsqlCommand("SELECT hos_id FROM hospital WHERE CONCAT_WS(' ', hos_name, hos_city) = '" + comboBox2.SelectedItem.ToString() + "'", Form1.connection);
            Form1.connection.Open();
            reader = command.ExecuteReader();
            reader.Read();
            hos_id = reader[0].ToString();
            Form1.connection.Close();
        }
        private void comboBox3_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (comboBox3.SelectedItem.ToString() == "Да")
            {
                iac_status = "1";
                dateTimePicker1.Enabled = true;
            }
            else
            {
                iac_status = "0";
                dateTimePicker1.Enabled = false;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Fc_check();
            Iac_check();
            if (Form2.nump == -1)
            {
                string insert = "INSERT INTO patients ";
                string order = "(p_id, p_surname, p_name, ";
                string values = "(" + Get_id() + ", '" + textBox1.Text + "', '" + textBox2.Text + "', ";
                if (textBox3.Text != "")
                {
                    order += "p_patr, ";
                    values += "'" + textBox3.Text + "', ";
                }
                order += "p_dob, p_gender, p_live, ";
                values += "'" + dateTimePicker2.Value.ToShortDateString() + "', '" + comboBox4.Text + "', '" + comboBox5.Text + "', ";
                if (textBox4.Text != "")
                {
                    order += "p_nationality, ";
                    values += "'" + textBox4.Text + "', ";
                }
                if (maskedTextBox1.Text != "")
                {
                    order += "p_phone, ";
                    values += Get_phone() + ", ";
                }
                if (textBox5.Text != "")
                {
                    order += "p_email, ";
                    values += "'" + textBox5.Text + "', ";
                }
                order += "doc_id, hos_id, fc_id, iac_id)";
                values += doc_id + ", " + hos_id + ", " + fc_id + ", " + iac_id + ")";
                insert += order + " VALUES " + values;
                Form2.Execute(insert);
                label2.Text = textBox1.Text + " " + textBox2.Text + " " + textBox3.Text + " " + dateTimePicker2.Value.ToShortDateString();
            }
            else
            {
                string update = "UPDATE patients SET p_surname = '" + textBox1.Text + "', p_name = '" + textBox2.Text + "'";
                if (textBox3.Text != "") update += ", p_patr = '" + textBox3.Text + "'";
                update += ", p_dob = '" + dateTimePicker2.Value.ToShortDateString() + "', p_gender = '" + comboBox4.Text + "', p_live = '" + comboBox5.Text + "'";
                if (textBox4.Text != "") update += ", p_nationality = '" + textBox4.Text + "'";
                if (maskedTextBox1.Text != "") update += ", p_phone = " + Get_phone();
                if (textBox5.Text != "") update += ", p_email = '" + textBox5.Text + "'";
                update += ", doc_id = " + doc_id + ", hos_id = " + hos_id + ", fc_id = " + fc_id + ", iac_id = " + iac_id + " WHERE p_id = " + (Form2.nump + 1);
                Form2.Execute(update);
                label2.Text = textBox1.Text + " " + textBox2.Text + " " + textBox3.Text + " " + dateTimePicker2.Value.ToShortDateString();
            }
            if (Form2.nump == -1) Form2.nump = Convert.ToInt32(Get_id()) - 1;
            if ((textBox6.Text != "") || (textBox7.Text != "") || (textBox8.Text != "")) Reg_check();
            if ((textBox9.Text != "") || (textBox10.Text != "") || (textBox11.Text != "")) Res_check();
            if (Snils_check() || (textBox12.Text != "") || (textBox13.Text != "") || (textBox14.Text != "") || (textBox15.Text != "")) AddInfo_check();
            if ((comboBox6.Text != "") || (comboBox7.Text != "") || (comboBox8.Text != "")) Disability_check();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (Form2.nump == -1)
            {
                MessageBox.Show("Сначала заполните основные сведения!", "Ошибка");
                return;
            }
            else
            {
                Disease disease = new Disease();
                disease.Show();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (Form2.nump == -1)
            {
                MessageBox.Show("Сначала заполните основные сведения!", "Ошибка");
                return;
            }
            else
            {
                Family family = new Family();
                family.Show();
            }
        }
    }
}
