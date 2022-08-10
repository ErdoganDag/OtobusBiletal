using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace OtobüsBiletal
{
    public partial class Cars : Form
    {
        SqlConnection SQLConnection = new SqlConnection("Data Source=.;Initial Catalog=Proje;Integrated Security=True");
        private SqlCommand command;

        public Cars()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bringcar();
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
        private void button5_Click(object sender, EventArgs e)
        {
            carinsert();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            cardelete();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            carupdate();
        }
        private void bringcar()
        {
            SQLConnection.Open();
            SqlDataAdapter tdGrvAdpter = new SqlDataAdapter("select * from Carss", SQLConnection);
            DataSet dtGrv = new DataSet();
            tdGrvAdpter.Fill(dtGrv);
            dataGridView1.DataSource = dtGrv.Tables[0];
            SQLConnection.Close();
        }
        private void carinsert()
        {

            command = new SqlCommand();
            SQLConnection.Open();
            command.Connection = SQLConnection;
            command.CommandText = "insert into Carss(CarModel,Carage,Carpersonalcapacity,Personalcapacity) values ('" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text+"')";
            command.ExecuteNonQuery();
            SQLConnection.Close();
            bringcar();
        }
        private void carupdate()
        {
            SQLConnection.Open();
            command.Connection = SQLConnection;
            command.CommandText = "update Carss set CarModel='" + textBox2.Text + "',CarAge='" + textBox3.Text + "',Carpersonalcapacity='" + textBox4.Text + "',Personalcapacity='"+ textBox5.Text+"' where ID=" + textBox1.Text + "";
            command.ExecuteNonQuery();
            SQLConnection.Close();
            bringcar();
        }
        private void cardelete()
        {
            command = new SqlCommand();
            SQLConnection.Open();
            command.Connection = SQLConnection;
            command.CommandText = "delete from Carss where ID=" + textBox1.Text + "";
            command.ExecuteNonQuery();
            SQLConnection.Close();
            bringcar();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }
    }
}
