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

namespace C_Proje2
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-6J9R1GH\\SQLEXPRESS;Initial Catalog=faturaSistemi;Integrated Security=True");

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ad = textBox1.Text; // Kullanıcıdan alınan Ad
            string soyad = textBox2.Text; // Kullanıcıdan alınan Soyad
            string kullaniciAdi = textBox3.Text; // Kullanıcıdan alınan Kullanıcı Adı
            string sifre = textBox4.Text; // Kullanıcıdan alınan Şifre

            // Veritabanı bağlantı stringi
            string connectionString = "Data Source=DESKTOP-6J9R1GH\\SQLEXPRESS;Initial Catalog=faturaSistemi;Integrated Security=True";

            // SQL Veri giriş sorgusu
            string query = "Insert into kullanici (Ad, soyad, kullaniciAdi, sifre) VALUES (@Ad, @soyad, @kullaniciAdi, @sifre)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Parametreleri ekleme
                    command.Parameters.AddWithValue("@Ad", ad);
                    command.Parameters.AddWithValue("@soyad", soyad);
                    command.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
                    command.Parameters.AddWithValue("@sifre", sifre);

                    try
                    {
                        connection.Open(); // Bağlantıyı aç
                        int rowsAffected = command.ExecuteNonQuery(); // Sorguyu çalıştır

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Kayıt başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Form1 form1 = new Form1();
                            form1.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Kayıt eklenemedi!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }
    }
}
