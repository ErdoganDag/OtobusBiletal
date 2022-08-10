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
    public partial class Voyage : Form
    {   SqlConnection SQLConnection = new SqlConnection("Data Source=.;Initial Catalog=Proje;Integrated Security=True");
        private SqlCommand command;
        public Voyage()
        {
            InitializeComponent();
            textBox8.Enabled = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string RouteId = "";
            string DriverId = "";
            string CarId = "";
            //Split metotdu kullanımı
            string routeno = comboBox1.Text.ToString();
            string[] voyage = routeno.Split('-');
            string driverno = comboBox2.Text.ToString();
            string[] voyagedriver = driverno.Split('-');
            string carno = comboBox3.Text.ToString();
            string[] voyagecar = carno.Split('-');
            //Rota-String değişkenin içerisine string format yöntemi kullanarak veri ekleme
            string routeQuery = string.Format("(select RouteID from Route1 where StartPoint='{0}' and StopPoint='{1}')", voyage[0].Trim(), voyage[1].Trim());
            SQLConnection.Open();
            SqlCommand command = new SqlCommand(routeQuery, SQLConnection);
            SqlDataReader readRouteId;
            readRouteId = command.ExecuteReader();
            while (readRouteId.Read())
            {
                 RouteId = readRouteId["RouteID"].ToString();
            }
            SQLConnection.Close();
            //Sürücü-String değişkenin içerisine string format yöntemi kullanarak veri ekleme           
            string driverQuery = string.Format("(select ID from Drivers where Drivername='{0}' and Driversurname='{1}' and Driverage= '{2}')", voyagedriver[0].Trim(), voyagedriver[1].Trim(), voyagedriver[2].Trim());
            SQLConnection.Open();
            SqlCommand commandDriver = new SqlCommand(driverQuery, SQLConnection);
            SqlDataReader readDriverId;
            readDriverId = commandDriver.ExecuteReader();
            while (readDriverId.Read())
            {
                 DriverId = readDriverId["ID"].ToString();
            }
            SQLConnection.Close();
            //Araba-String değişkenin içerisine string format yöntemi kullanarak veri ekleme
            string carQuery = string.Format("(select ID from Carss where Carmodel='{0}' and Carpersonalcapacity='{1}')", voyagecar[0].Trim(), voyagecar[1].Trim());
            SQLConnection.Open();
            SqlCommand commandCar = new SqlCommand(carQuery, SQLConnection);
            SqlDataReader readCarId;
            readCarId = commandCar.ExecuteReader();
            while (readCarId.Read())
            {
                 CarId = readCarId["ID"].ToString();
            }
            SQLConnection.Close();
            //Tanımlanan değişken içerisinde çağırılması.
           VoyageInsert(RouteId,CarId,DriverId);
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
            voyagedelete();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            voyageupdate();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            bringvoyage();
        }
        private void bringvoyage()
        {
            //SQL SELECT DEĞİŞKENİ
            SQLConnection.Open();
            SqlDataAdapter tdGrvAdpter = new SqlDataAdapter("select * from Voyage2", SQLConnection);
            DataSet dtGrv = new DataSet();
            tdGrvAdpter.Fill(dtGrv);
            dataGridView1.DataSource = dtGrv.Tables[0];
            SQLConnection.Close();
        }
        private void VoyageInsert(string RouteId,string CarId,string DriverId)
        {
            //SQL'E VERİ EKLEME METODU
            command = new SqlCommand();
            SQLConnection.Open();
            command.Connection = SQLConnection;
            command.CommandText = "insert into Voyage2(RouteID,DriverID,CarID,Departuretime,Departuretimestation1,Departuretimestation2,Departuretimestation3) values ( " + RouteId + "," + DriverId + "," + CarId + ",'" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text+  "','" + textBox7.Text + "')";
            command.ExecuteNonQuery();
            SQLConnection.Close();
            bringvoyage();
        }
        private void voyageupdate()
        {
            //SQL'DEKİ VERİYİ GÜNCELLEME
            SQLConnection.Open();
            command.Connection = SQLConnection;
            command.CommandText = "update Voyage2 set RouteID='" + comboBox1.Text + "',DriverID='" + comboBox2.Text + "',CarID='" + comboBox3.Text + "',Departuretime='" + textBox4.Text + "',Departuretimestation1'" + textBox5.Text + "',Departuretimestation2'" + textBox6.Text + "',Departuretimestation3'" + textBox7.Text + "' where ID=" + textBox8.Text + "";
            command.ExecuteNonQuery();
            SQLConnection.Close();
            bringvoyage();
        }
        private void voyagedelete()
        {
            //SQL'DEKİ VERİYİ SİLME METODU
            command = new SqlCommand();
            SQLConnection.Open();
            command.Connection = SQLConnection;
            command.CommandText = "delete from Voyage2 where ID=" + textBox8.Text + "";
            command.ExecuteNonQuery();
            SQLConnection.Close();
            bringvoyage();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox8.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void voyageroute()
        {
            //Rotaları combobox'a ekleme
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM Route1";
            command.Connection = SQLConnection;
            command.CommandType = CommandType.Text;

            SqlDataReader readRoute;
            SQLConnection.Open();
            readRoute = command.ExecuteReader();
            while (readRoute.Read())
            {
                var abc = string.Format("{0} - {1} - {2} - {3} - {4}", readRoute["StartPoint"], readRoute["StopPoint"], readRoute["İntermediatestop1"], readRoute["İntermediatestop2"], readRoute["İntermediatestop3"]);

                comboBox1.Items.Add(abc);
            }
            SQLConnection.Close();
        }
        private void voyagedriver()
        {
            //Sürücüleri combobox'a ekleme
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM Drivers";
            command.Connection = SQLConnection;
            command.CommandType = CommandType.Text;

            SqlDataReader readDrivers;
            SQLConnection.Open();
            readDrivers = command.ExecuteReader();
            while (readDrivers.Read())
            {
                var abc = string.Format("{0} - {1} - {2}", readDrivers["Drivername"], readDrivers["Driversurname"], readDrivers["Driverage"]);

                comboBox2.Items.Add(abc);
            }
            SQLConnection.Close();
        }
        private void voyagecar()
        {
            //arabaları combobox'a ekleme
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM Carss";
            command.Connection = SQLConnection;
            command.CommandType = CommandType.Text;

            SqlDataReader readCars;
            SQLConnection.Open();
            readCars = command.ExecuteReader();
            while (readCars.Read())
            {
                var abc = string.Format("{0} - {1} ", readCars["CarModel"], readCars["Carpersonalcapacity"]);

                comboBox3.Items.Add(abc);
            }
            SQLConnection.Close();
        }

        private void Voyage_Load(object sender, EventArgs e)
        {
            bringvoyage();
            voyageroute();
            voyagedriver();
            voyagecar();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    using (var command = new SqlCommand("insert into Voyage2(RouteID,DriverID,CarID,Departuretime,Departuretimestation1,Departuretimestation2,Departuretimestation3) values (@RouteID,@DriverID,@CarID,@Departuretime,@Departuretimestation1,@Departuretimestation2,@Departuretimestation3)")) ;

            //    command.Connection = SQLConnection;
            //    command.Parameters.Add("@RouteID", comboBox1.TabIndex);
            //    command.Parameters.Add("@DriverID", comboBox2.TabIndex);
            //    command.Parameters.Add("@CarID", comboBox3.TabIndex);
            //    command.Parameters.Add("@Departuretime", textBox4.Text);
            //    command.Parameters.Add("@Departuretimestation1", textBox5.Text);
            //    command.Parameters.Add("@Departuretimestation2", textBox6.Text);
            //    command.Parameters.Add("@Departuretimestation3", textBox7.Text);
            //    SQLConnection.Open();
            //    if (command.ExecuteNonQuery() > 0)
            //    {
            //        MessageBox.Show("Voyage İnserted");
            //    }
            //    else
            //    {
            //        MessageBox.Show("Voyage Failed");
            //    }
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show("Error during İnsert");
            //}
            //SQLConnection.Close();
        }
    }
}
            
            
        
    

