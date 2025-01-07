using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C_Proje2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

      

        private void Form1_Load(object sender, EventArgs e)
        {   
        }

        private SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-6J9R1GH\\SQLEXPRESS;Initial Catalog=faturaSistemi;Integrated Security=True");
        
        // Giriş Butonu
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 kGiris = new Form1();
          
            string connectionString = @"Data Source=DESKTOP-6J9R1GH\SQLEXPRESS;Initial Catalog=faturaSistemi;Integrated Security=True";
            string query = "SELECT COUNT(*) FROM kullanici WHERE kullaniciAdi = @kullaniciAdi AND sifre = @sifre";

            string kullaniciAdi = textBox1.Text.Trim();
            string sifre = textBox2.Text.Trim();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Parametreleri ekleyin
                        command.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
                        command.Parameters.AddWithValue("@sifre", sifre);

                        // Sorguyu çalıştırın
                        int count = (int)command.ExecuteScalar();

                        if (count > 0)
                        {
                            // Giriş başarılı
                            MessageBox.Show("Giriş başarılı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // form2'yi göster ve bu formu gizle
                            Form2 form2 = new Form2(); 
                            form2.Show();
                            this.Hide();
                        }
                        else
                        {
                            // Kullanıcı adı veya şifre yanlış
                            MessageBox.Show("Kullanıcı adı veya şifre hatalı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Hata mesajını göster
                    MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
           
        }

        // Çıkış Butonu
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Form4 nesnesi oluşturuz.
            Form4 form4 = new Form4();
            // Form4 Açılır.
            form4.Show();

            // Bu form kapanır.
            this.Hide();
        }
    }
}
