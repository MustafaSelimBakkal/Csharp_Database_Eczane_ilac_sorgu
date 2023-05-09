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

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Listele();
        }

        SqlConnection baglanti = new SqlConnection(@"server=(localdb)\Projects;database=EczaneDb;Trusted_Connection=true");
        DataTable dt;

        private void Listele()
        {
            baglanti.Open();
            SqlCommand sorgu = new SqlCommand("select * from ilaclar ", baglanti);
            SqlDataReader sonuc = sorgu.ExecuteReader();

            dt = new DataTable();
            dt.Load(sonuc);

            dataGridView1.DataSource = dt;
            baglanti.Close();
        }       
     
       
      
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = string.Format("ad LIKE '" + textBox4.Text + "%'");
            dataGridView1.DataSource = dv;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("insert into ilaclar(ad,fiyat,aciklama)values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "')", baglanti);


            cmd.ExecuteNonQuery();

            baglanti.Close();
            Listele();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd2 = new SqlCommand("delete from ilaclar where ilacNo=" + textBox5.Text, baglanti);

            cmd2.ExecuteNonQuery();

            baglanti.Close();
            Listele();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string sorgu = "Update ilaclar Set ad=@ad, fiyat=@fiyat, aciklama=@aciklama Where ilacNo=@ilacNo";
            SqlCommand cmd = new SqlCommand(sorgu, baglanti);
            cmd.Parameters.AddWithValue("@ilacNo", Convert.ToInt32(textBox5.Text));
            cmd.Parameters.AddWithValue("@ad", textBox1.Text);
            cmd.Parameters.AddWithValue("@fiyat", textBox2.Text);
            cmd.Parameters.AddWithValue("@aciklama", textBox3.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            Listele();
        }   

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox5.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox5.Clear();
        }

    }
}
