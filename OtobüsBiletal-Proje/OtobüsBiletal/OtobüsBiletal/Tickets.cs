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
    public partial class Tickets : Form
    {
        SqlConnection SQLConnection = new SqlConnection("Data Source=.;Initial Catalog=Proje;Integrated Security=True");
        private SqlCommand command;



        public Tickets()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {



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
        private void button7_Click(object sender, EventArgs e)
        {
            ticketinsert();
        }
        private void button8_Click(object sender, EventArgs e)
        {
            ticketdelete();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            ticketupdate();
        }
        private void biletgetir()
        {
            SQLConnection.Open();
            SqlDataAdapter tdGrvAdpter = new SqlDataAdapter("select * from Tickets", SQLConnection);
            DataSet dtGrv = new DataSet();
            tdGrvAdpter.Fill(dtGrv);
            dataGridView1.DataSource = dtGrv.Tables[0];
            SQLConnection.Close();
        }
        private void ticketinsert()
        {
            var routeNo = "";
            string seferno = comboBox3.Text.ToString();
            string[] voyage = seferno.Split('-');
            SQLConnection.Open();
            string query = string.Format("Select * from Route1 where StartPoint='{0}' and StopPoint='{1}'", voyage[0].Trim(), voyage[1].Trim());
            SqlCommand command = new SqlCommand(query, SQLConnection);
            SqlDataReader readroute;
            readroute = command.ExecuteReader();
            while (readroute.Read())
            {
                var firststop = readroute["İntermediatestop1"].ToString();
                var secondstop = readroute["İntermediatestop2"].ToString();
                var thirdstop = readroute["İntermediatestop3"].ToString();
                routeNo = readroute["RouteID"].ToString();
            }
            SQLConnection.Close();

            command = new SqlCommand();
            SQLConnection.Open();
            command.Connection = SQLConnection;
            command.CommandText = "insert into Tickets(RouteID,PassengerName,PassengerSurname,PassengerTckn,PassengerMobil,Passengergender,Seatno) values ('" + routeNo + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox9.Text + "','" + textBox8.Text + "')";
            command.ExecuteNonQuery();
            SQLConnection.Close();
            biletgetir();
        }
        private void ticketdelete()
        {
            command = new SqlCommand();
            SQLConnection.Open();
            command.Connection = SQLConnection;
            command.CommandText = "delete from Tickets where Seatno=" + textBox8.Text + "";
            command.ExecuteNonQuery();
            SQLConnection.Close();
            biletgetir();
        }
        private void ticketupdate()
        {
            SQLConnection.Open();
            command.Connection = SQLConnection;
            command.CommandText = "update Tickets set PassangerName='" + textBox4.Text + "',PassangerSurname='" + textBox5.Text + "',PassangerTckn='" + textBox6.Text + "',PassangerMobil='" + textBox7.Text + "',Passengergender='" + textBox9.Text + "' where Seatno =" + textBox8.Text + "";
            command.ExecuteNonQuery();
            SQLConnection.Close();
            biletgetir();
        }
        private void Fullseats(string routeNo)
        {
            SQLConnection.Open();
            string querytickets = string.Format("select * from Tickets where RouteID='{0}'", routeNo);
            SqlCommand commandticket = new SqlCommand(querytickets, SQLConnection);
            SqlDataReader read = commandticket.ExecuteReader();
            while (read.Read())
            {
                foreach (Control item in panel1.Controls)
                {
                    if (item is Button)
                    {
                        if (read["Seatno"].ToString() == item.Text)
                        {
                            item.BackColor = Color.Red;
                            item.Enabled = false;
                        }
                        else
                        {
                            item.BackColor = Color.White;
                        }
                    }
                }
            }
            SQLConnection.Close();
        }
        private void startstoppoint()
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM Route1";
            command.Connection = SQLConnection;
            command.CommandType = CommandType.Text;

            SqlDataReader dr;
            SQLConnection.Open();
            dr = command.ExecuteReader();
            while (dr.Read())
            {
                var abc = string.Format("{0} - {1}", dr["StartPoint"], dr["StopPoint"]);

                comboBox3.Items.Add(abc);
            }
            SQLConnection.Close();
        }
        private void coloring()
        {
            foreach (Control item in panel1.Controls)
            {
                if (item is Button)
                {
                    item.BackColor = Color.Red;
                }
            }
        }
        private void Btn_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.BackColor == Color.White)
            {
                textBox8.Text = b.Text;

            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //comboBox1.Text = dataGridView1.CurrentRow.Cells[].Value.ToString();
            //comboBox2.Text = dataGridView1.CurrentRow.Cells[].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            textBox9.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
        }
        private void Tickets_Load(object sender, EventArgs e)
        {

            startstoppoint();
            //Mydata.DataTable_Yolcular();
            //Duzenkur();
            //Duzenkur2();
        }
        int sayac;
        void Seatarrangementtwoandone()
        {
            sayac = 1;
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 4; j++)
                {

                    Button btn = new Button();
                    btn.Size = new Size(35, 35);
                    btn.BackColor = Color.White;
                    btn.Font = new Font(btn.Font.FontFamily, 8);
                    btn.Location = new Point(j * 40 + 30, i * 40 + 30);
                    btn.Name = sayac.ToString();
                    btn.Text = sayac.ToString();
                    if (j == 1 || i == 14)
                    {
                        Label lbl2 = new Label();
                        lbl2.BackColor = Color.White;
                        lbl2.ForeColor = Color.Black;
                        lbl2.TextAlign = ContentAlignment.BottomCenter;
                        lbl2.AutoSize = false;
                        lbl2.Size = new Size(26, 559);
                        lbl2.Font = new Font(btn.Font.FontFamily, 14);
                        lbl2.Location = new Point(2 * 39, 30);
                        lbl2.Name = "Gecis";
                        lbl2.Text = "Koridor";
                        this.panel1.Controls.Add(lbl2);


                        continue;
                    }
                    sayac = sayac + 1;
                    this.panel1.Controls.Add(btn);
                    btn.Click += Btn_Click;
                }
            }
        }
        void Seatararengmenttwoandtwo()
        {
            sayac = 1;
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Button btn = new Button();
                    btn.Size = new Size(35, 35);
                    btn.BackColor = Color.White;
                    btn.Font = new Font(btn.Font.FontFamily, 8);
                    btn.Location = new Point(j * 40 + 30, i * 40 + 30);
                    btn.Name = sayac.ToString();
                    btn.Text = sayac.ToString();
                    if (j == 2 || i == 14)
                    {
                        Label lbl2 = new Label();
                        lbl2.BackColor = Color.White;
                        lbl2.ForeColor = Color.Black;
                        lbl2.TextAlign = ContentAlignment.BottomCenter;
                        lbl2.AutoSize = false;
                        lbl2.Size = new Size(26, 559);
                        lbl2.Font = new Font(btn.Font.FontFamily, 14);
                        lbl2.Location = new Point(3 * 39, 30);
                        lbl2.Name = "Gecis";
                        lbl2.Text = "Koridor";
                        this.panel1.Controls.Add(lbl2);
                        continue;
                    }
                    sayac = sayac + 1;
                    this.panel1.Controls.Add(btn);
                    btn.Click += Btn_Click;
                }
            }
        }
        private void textBox9_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                string cinsiyet = textBox9.Text;
                if (cinsiyet == "E")
                {
                    textBox9.BackColor = Color.Cyan;
                }
                else if (cinsiyet == "K")
                {
                    textBox9.BackColor = Color.Pink;
                }
            }
            catch (Exception)
            {
                textBox9.BackColor = Color.White;
            }
        }
        //void fiyatlistele()
        //{
        //    string gelen_deger = comboBox3.Text.ToString();
        //    SQLConnection.Open();
        //    SqlCommand komut = new SqlCommand();
        //    komut.CommandText = "SELECT * FROM Prices WHERE RouteID='{0}' and  " + gelen_deger.ToString();
        //    komut.Connection = SQLConnection;
        //    komut.CommandType = CommandType.Text;
        //    SQLConnection.Close();
        //    SqlDataReader dr;
        //    SQLConnection.Open();
        //    dr = komut.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        textBox2.Text = (string)dr["StartStopPrice"];
        //    }
        //    SQLConnection.Close();

        //}
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            panel1.Controls.Clear();
            biletgetir();
            //renklendirme();
            //DoluKoltuklar();
            //Duzenkur();

            var routeNo = "";
            string seferno = comboBox3.Text.ToString();
            string[] voyage = seferno.Split('-');
            SQLConnection.Open();
            string query = string.Format("Select * from Route1 where StartPoint='{0}' and StopPoint='{1}'", voyage[0].Trim(), voyage[1].Trim());
            SqlCommand command = new SqlCommand(query, SQLConnection);
            SqlDataReader readroute;
            readroute = command.ExecuteReader();
            while (readroute.Read())
            {
                var firststop = readroute["İntermediatestop1"].ToString();
                var secondstop = readroute["İntermediatestop2"].ToString();
                var thirdstop = readroute["İntermediatestop3"].ToString();
                routeNo = readroute["RouteID"].ToString();
                //NeredenCombobox
                comboBox1.Items.Add(voyage[0]);
                comboBox1.Items.Add(firststop);
                comboBox1.Items.Add(secondstop);
                comboBox1.Items.Add(thirdstop);

                //NereyeCombobox
                comboBox2.Items.Add(firststop);
                comboBox2.Items.Add(secondstop);
                comboBox2.Items.Add(thirdstop);
                comboBox2.Items.Add(voyage[1]);
            }
            SQLConnection.Close();

            //2+1 2+2 kontrolü
            SQLConnection.Open();
            string queryCarpersonalcapacity = string.Format("SELECT Carpersonalcapacity FROM Carss LEFT JOIN Voyage2 ON Carss.ID = Voyage2.CarID LEFT JOIN Route1 ON Route1.RouteID = Voyage2.RouteID where Route1.StartPoint = '{0}' and Route1.StopPoint = '{1}'", voyage[0].Trim(), voyage[1].Trim());
            SqlCommand commands = new SqlCommand(queryCarpersonalcapacity, SQLConnection);
            SqlDataReader readcarpersonalcapacity;
            readcarpersonalcapacity = commands.ExecuteReader();
            while (readcarpersonalcapacity.Read())
            {
                if (readcarpersonalcapacity["Carpersonalcapacity"].ToString() == "2+2")
                {
                    Seatararengmenttwoandtwo();
                }
                else if (readcarpersonalcapacity["Carpersonalcapacity"].ToString() == "2+1")
                {
                    Seatarrangementtwoandone();
                }
            }
            SQLConnection.Close();
            //Duzenkur();
            Fullseats(routeNo);
            //Fiyat listesi                   
            //fiyatlistele();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var routeNo = "";
            string seferno = comboBox3.Text.ToString();
            string[] voyage = seferno.Split('-');
            SQLConnection.Open();
            string query = string.Format("Select * from Route1 where StartPoint='{0}' and StopPoint='{1}'", voyage[0].Trim(), voyage[1].Trim());
            SqlCommand command = new SqlCommand(query, SQLConnection);
            SqlDataReader readroute;
            readroute = command.ExecuteReader();
            while (readroute.Read())
            {
                var firststop = readroute["İntermediatestop1"].ToString();
                var secondstop = readroute["İntermediatestop2"].ToString();
                var thirdstop = readroute["İntermediatestop3"].ToString();
                routeNo = readroute["RouteID"].ToString();
            }
            SQLConnection.Close();

            string startStation = comboBox1.Text.ToString();
            string stopStation = comboBox2.Text.ToString();
            string Price = "";

            SQLConnection.Open();
            SqlCommand cmd = new SqlCommand("SP_TicketControl", SQLConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@STARTPOINT", startStation.Trim());
            cmd.Parameters.AddWithValue("@STOPPOINT", stopStation.Trim());
            cmd.Parameters.AddWithValue("@ROUTEID", routeNo);

            SqlParameter RuturnValue = new SqlParameter("@PRICE", SqlDbType.VarChar, 20);
            RuturnValue.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(RuturnValue);
            cmd.ExecuteNonQuery();
            Price = (string)cmd.Parameters["@PRICE"].Value;
            SQLConnection.Close();
            textBox2.Text = Price;
        }
    }           
}

