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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static string constring = ("Data Source=localhost\\SQLEXPRESS;Initial Catalog=bakım;Integrated Security=True;Encrypt=False");
        SqlConnection baglan = new SqlConnection(constring);

        public void kayitlari_getir()
        {
            baglan.Open();
            string getir = "select * from bakım";
            SqlCommand komut = new SqlCommand(getir, baglan);

            SqlDataAdapter ad = new SqlDataAdapter(komut);

            DataTable dt = new DataTable();
            ad.Fill(dt);
            dataGridView1.DataSource = dt;
            baglan.Close();





        }


        public void verisil(int mno)
        {
            string sil = "Delete from bakım Where mno=@mno";

            SqlCommand komut = new SqlCommand(sil, baglan);
            baglan.Open();
            komut.Parameters.AddWithValue("@mno", mno);

            komut.ExecuteNonQuery();
            baglan.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'bakımDataSet2.bakım' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.

            // TODO: Bu kod satırı 'bakımDataSet1.bakım' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.bakımTableAdapter1.Fill(this.bakımDataSet1.bakım);
            // TODO: Bu kod satırı 'bakımDataSet.bakım' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.bakımTableAdapter.Fill(this.bakımDataSet.bakım);

        }





        private void button1_Click(object sender, EventArgs e)
        {


            object kayıt = "Select * from bakım Where plaka=@plaka";
            SqlCommand komut = new SqlCommand((string)kayıt,baglan);

            komut.Parameters.AddWithValue("@plaka", textBox1.Text);

            SqlDataAdapter da = new SqlDataAdapter(komut);

            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglan.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow drow in dataGridView1.SelectedRows)
            {
                int mno = Convert.ToInt32(drow.Cells[0].Value);
                verisil(mno);
                kayitlari_getir();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (baglan.State == ConnectionState.Closed)
                {
                    baglan.Open();
                    string kayıt = "insert into bakım (plaka,marka,model,ureti_yili,renk,kilometre,bakim_km,son_bakim_bilgi,son_bakim_tarih) values (@plaka," +
                        "@marka,@model,@ureti_yili,@renk,@kilometre,@bakim_km,@son_bakim_bilgi,@son_bakim_tarih)";
                    SqlCommand komut = new SqlCommand(kayıt, baglan);
                    komut.Parameters.AddWithValue("@plaka", textBox2.Text);
                    komut.Parameters.AddWithValue("@marka", textBox3.Text);
                    komut.Parameters.AddWithValue("@model", textBox4.Text);
                    komut.Parameters.AddWithValue("@ureti_yili", textBox7.Text);
                    komut.Parameters.AddWithValue("@renk", textBox9.Text);
                    komut.Parameters.AddWithValue("@kilometre", textBox6.Text);
                    komut.Parameters.AddWithValue("@bakim_km", textBox8.Text);
                    komut.Parameters.AddWithValue("@son_bakim_bilgi", textBox5.Text);
                    komut.Parameters.AddWithValue("@son_bakim_tarih", textBox10.Text);

                    komut.ExecuteNonQuery();

                    MessageBox.Show("KAYIT EKLEME İŞLEMİ TAMAMLANDI");
                }

            }
            catch (Exception hata)
            {
                MessageBox.Show("BİR HATA VAR" + hata.Message);
            }
        }
    }
}
