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
    public partial class Ticket : Form
    {
        SqlConnection SQLBaglantisi = new SqlConnection("Data Source=.;Initial Catalog=Proje;Integrated Security=True");
        private SqlCommand cmd;
        int sayac = 0;
        public Ticket()
        {
            InitializeComponent();
        }

        private void Ticket_Load(object sender, EventArgs e)
        {

        }
        private void DoluKoltuklar()
        {
            SQLBaglantisi.Open();
            SqlCommand komut = new SqlCommand("select * from Ticket where PassangerName='" + textBox1.Text + "' and PassangerSurname='" + textBox2.Text + "' and PassangerTckn='" + textBox3.Text + "' and PassangerMobil='" + textBox4.Text + "'", SQLBaglantisi);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {

                foreach (Control item in panel1.Controls)
                {
                    if (item is Button)
                    {
                        if (read["koltukNo"].ToString() == item.Text)
                        {
                            item.BackColor = Color.Red;

                        }
                    }
                }
            }                   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            biletgetir();
        }
        private void biletgetir()
        {
            SQLBaglantisi.Open();
            SqlDataAdapter tdGrvAdpter = new SqlDataAdapter("select * from Ticket", SQLBaglantisi);
            DataSet dtGrv = new DataSet();
            tdGrvAdpter.Fill(dtGrv);
            dataGridView1.DataSource = dtGrv.Tables[0];
            SQLBaglantisi.Close();
        }
    }
    public static void BosKoltuklar()
    {
        sayac = 1;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 9; j++)
            {

                Button btn = new Button();
                btn.Size = new Size(35, 35);
                btn.BackColor = Color.White;
                btn.Font = new Font(btn.Font.FontFamily, 8);
                btn.Location = new Point(j * 40 + 30, i * 40 + 30);
                btn.Name = sayac.ToString();
                btn.Text = sayac.ToString();
                if (j == 5 || i == 4)
                {
                    Label lbl3 = new Label();
                    lbl3.BackColor = Color.Green;
                    lbl3.ForeColor = Color.White;
                    lbl3.TextAlign = ContentAlignment.TopCenter;
                    lbl3.Size = new Size(26, 25);
                    lbl3.Font = new Font(btn.Font.FontFamily, 11);
                    lbl3.Location = new Point(6 * 39, 22);
                    lbl3.Name = "↓";
                    lbl3.Text = "↓";
                    this.panel1.Controls.Add(lbl3);
                    Label lbl2 = new Label();
                    lbl2.BackColor = Color.White;
                    lbl2.ForeColor = Color.Black;
                    lbl2.TextAlign = ContentAlignment.BottomCenter;
                    lbl2.AutoSize = false;
                    lbl2.Size = new Size(26, 319);
                    lbl2.Font = new Font(btn.Font.FontFamily, 14);
                    lbl2.Location = new Point(6 * 39, 30);
                    lbl2.Name = "Gecis";
                    lbl2.Text = "Geçişş";
                    this.panel1.Controls.Add(lbl2);
                    Label lbl = new Label();
                    lbl.BackColor = Color.White;
                    lbl.ForeColor = Color.Black;
                    lbl.TextAlign = ContentAlignment.MiddleLeft;
                    lbl.Size = new Size(360, 27);
                    lbl.Font = new Font(btn.Font.FontFamily, 11);
                    lbl.Location = new Point(30, 5 * 39);
                    lbl.Name = "Gecis";
                    lbl.Text = "Geçiş";
                    this.panel1.Controls.Add(lbl);

                    continue;
                }
                sayac = sayac + 1;
                this.panel1.Controls.Add(btn);
                btn.Click += Btn_Click;
            }
        }
    }
    private void cmdolukoltuklar()
    {
        foreach (Control item in panel1.Controls)
        {
            if (item is Button)
            {
                if (item.BackColor == Color.Red)
                {
                    textbox1.text(item.Text);
                }
            }
        }
    }
    
    private void renklendirme()
    {
        foreach (Control item in panel1.Controls)
        {
            if (item is Button)
            {
                item.BackColor = Color.White;
            }
        }
    }
    private void Btn_Click(object sender, EventArgs e)
    {
        Button b = (Button)sender;
        if (b.BackColor == Color.White)
        {
            txtKoltuk.Text = b.Text;

        }
    }
}
