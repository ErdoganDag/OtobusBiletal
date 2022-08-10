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
    public partial class Driver : Form
    {
        SqlConnection SQLConnection = new SqlConnection("Data Source=.;Initial Catalog=Proje;Integrated Security=True");
        private SqlCommand command;
        public Driver()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bringdriver();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form1 getir = new Form1();
            getir.Show();
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
            driverinsert();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            driverdelete();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            soforupdate();
        }
        private void bringdriver()
        {
            SQLConnection.Open();
            SqlDataAdapter tdGrvAdpter = new SqlDataAdapter("select * from Drivers", SQLConnection);
            DataSet dtGrv = new DataSet();
            tdGrvAdpter.Fill(dtGrv);
            dataGridView1.DataSource = dtGrv.Tables[0];
            SQLConnection.Close();
        }
        private void driverinsert()
        {
            command = new SqlCommand();
            SQLConnection.Open();
            command.Connection = SQLConnection;
            command.CommandText = "insert into Drivers(Drivername,Driversurname,Driverage,Driverexperinece,Driverlicence) values ('" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "')";
            command.ExecuteNonQuery();
            SQLConnection.Close();
            bringdriver();
        }
        private void driverdelete()
        {
            command = new SqlCommand();
            SQLConnection.Open();
            command.Connection = SQLConnection;
            command.CommandText = "delete from Drivers where ID=" + textBox1.Text + "";
            command.ExecuteNonQuery();
            SQLConnection.Close();
            bringdriver();
        }
        private void soforupdate()
        {
            SQLConnection.Open();
            command.Connection = SQLConnection;
            command.CommandText = "update Drivers set Drivername='" + textBox2.Text + "',Driversurname='" + textBox3.Text + "',Driverage='" + textBox4.Text + "',Driverexperience='" + textBox5.Text + "'Driverlicence='" + textBox6.Text + "' where ID=" + textBox1.Text + "";
            command.ExecuteNonQuery();
            SQLConnection.Close();
            bringdriver();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }        
    }
}
