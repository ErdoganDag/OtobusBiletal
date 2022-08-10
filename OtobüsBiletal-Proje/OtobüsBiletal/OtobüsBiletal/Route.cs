using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OtobüsBiletal
{
    public partial class Route : Form
    {
        SqlConnection SQLConnection = new SqlConnection("Data Source=.;Initial Catalog=Proje;Integrated Security=True");
        private SqlCommand command;
        public Route()
        {
            InitializeComponent();
            datagrid();
            comboboxins();                             
        }
        private void datagrid()
        {
            SQLConnection.Open();
            SqlDataAdapter tdGrvAdpter = new SqlDataAdapter("select * from Route1", SQLConnection);
            DataSet dtGrv = new DataSet();
            tdGrvAdpter.Fill(dtGrv);
            dataGridView2.DataSource = dtGrv.Tables[0];
            SQLConnection.Close();
        }
        private void comboboxins()
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT *FROM Route1";
            command.Connection = SQLConnection;
            command.CommandType = CommandType.Text;

            SqlDataReader readroute;
            SQLConnection.Open();
            readroute = command.ExecuteReader();
            while (readroute.Read())
            {
                comboBox3.Items.Add(readroute["StartPoint"]);
                comboBox4.Items.Add(readroute["StopPoint"]);
            }
            SQLConnection.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form1 bring = new Form1();
            bring.Show();
            this.Hide();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }                     
        private void button1_Click_1(object sender, EventArgs e)
        {
            object StartPoint = comboBox3.SelectedItem;
            object StopPoint = comboBox4.SelectedItem;
            SQLConnection.Open();
            SqlCommand command = new SqlCommand("INSERT INTO Route1(StartPoint , StopPoint , RouteID , İntermediatestop1 , İntermediatestop2 , İntermediatestop3) VALUES('" + comboBox3.Text + "','" + comboBox4.Text + "','" + textBox1.Text + "','" + textBox4.Text + "','" + textBox3.Text + "','" + textBox5.Text + "');", SQLConnection);
            command.ExecuteNonQuery();
            SQLConnection.Close();    
            MessageBox.Show("Rota Oluşturuldu.");
            datagrid();
            
        }
        private void button5_Click(object sender, EventArgs e)
        {
            command = new SqlCommand();
            SQLConnection.Open();
            command.Connection = SQLConnection;
            command.CommandText = "update Route1 set RouteID='" + textBox1.Text + "',İntermediatestop1='" + textBox4.Text + "',İntermediatestop3='" + textBox5.Text + "',İntermediatestop2='"+textBox3.Text+"'where ID=" + textBox2.Text + "";
            command.ExecuteNonQuery();
            SQLConnection.Close();
            datagrid();
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
            textBox2.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            comboBox3.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            comboBox4.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
            textBox3.Text = dataGridView2.CurrentRow.Cells[5].Value.ToString();
            textBox5.Text = dataGridView2.CurrentRow.Cells[6].Value.ToString();
        }
    }
}
