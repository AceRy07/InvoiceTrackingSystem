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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace C_Proje2
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        // Veritabanı SQl bağlantısı
        private SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-6J9R1GH\\SQLEXPRESS;Initial Catalog=faturaSistemi;Integrated Security=True");

        // ListView'de verileri gösterir.
        private void VeriGoruntule()
        {
            // Veritabanına bağlanır.
            baglan.Open();

            // SQL sorgusu yazılır.
            SqlCommand komut = new SqlCommand("Select * from Faturalar", baglan);

            // Sorguyu çalıştır ve sonuçları al
            SqlDataReader oku = komut.ExecuteReader();

            // Verileri okuyarak ListView'e ekler
            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();

                ekle.Text = oku["FaturaNo"].ToString();
                ekle.SubItems.Add(oku["MusteriAdi"].ToString());
                ekle.SubItems.Add(oku["FaturaTarihi"].ToString());
                ekle.SubItems.Add(oku["SonOdemeTarihi"].ToString());
                ekle.SubItems.Add(oku["ToplamTutar"].ToString());
                ekle.SubItems.Add(oku["OdemeBilgi"].ToString());
                ekle.SubItems.Add(oku["Aciklama"].ToString());

                listView1.Items.Add(ekle);
            }

            // Bağlantıyı kapatır.
            baglan.Close();
        }
        

        int FaturaNo = 0;

        // Veri Silme
        private void VeriSil()
        {
            baglan.Open();

            // SQL veri silme sorgusu
            SqlCommand komut = new SqlCommand("delete from Faturalar where FaturaNo=(" + FaturaNo + ")", baglan);

            // Sorguyu çalıştır
            komut.ExecuteNonQuery();
            baglan.Close();
        }


        // Yaklaşan Ödemeleri Gösterir
        private void YaklasanOdeme()
        {
            try
            {
                baglan.Open();

                // SQL sorgusunda SonOdemeTarihi'nin bugün ile 7 gün sonrası arasında olmasını istedik.
                SqlCommand komut = new SqlCommand(
                    "SELECT FaturaNo, SonOdemeTarihi, ToplamTutar " +
                    "FROM Faturalar " +
                    "WHERE SonOdemeTarihi BETWEEN GETDATE() AND DATEADD(DAY, 7, GETDATE())",
                    baglan);

                // Sorguyu çalıştır ve sonuçları al
                SqlDataReader reader = komut.ExecuteReader();

                // Mesaj için verileri topla
                string mesaj = "Yaklaşan Ödemeleriniz:\n";
                bool odemeVar = false;

                while (reader.Read())
                {
                    odemeVar = true;
                    mesaj += $"Fatura No: {reader["ToplamTutar"]}, " +
                             $"Son Ödeme Tarihi: {Convert.ToDateTime(reader["SonOdemeTarihi"]).ToShortDateString()}, " +
                             $"Tutar: {reader["ToplamTutar"]}₺\n";
                }

                // Reader'ı kapat
                reader.Close();

                // Mesajı göster
                if (odemeVar)
                {
                    MessageBox.Show(mesaj, "Hatırlatma", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Yaklaşan ödemeniz bulunmamaktadır.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            // Hata Mesajları gösterir.
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Bağlantıyı kapatır.
                if (baglan.State == ConnectionState.Open)
                {
                    baglan.Close();
                }
            }
        }


        // Form yüklenirken Verilerin gelmesi
        private void Form2_Load_1(object sender, EventArgs e)
        {
            VeriGoruntule();
            YaklasanOdeme();
        }

        // Veri Ekleme Formu
        private void button1_Click(object sender, EventArgs e)
        {
            Form3 VeriEkleForm = new Form3();
            VeriEkleForm.Show();
        }

        // Verileri yeniler
        private void button5_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            VeriGoruntule();
        }

        // Verileri Siler.
        private void button2_Click(object sender, EventArgs e)
        {
            VeriSil();
            listView1.Items.Clear();
            VeriGoruntule();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form5 VeriGuncelleForm = new Form5();
            VeriGuncelleForm.Show();
        }

        // ListView'de doubleclik olayı kullnarak fatura seçimi yapılır.
        private void listView1_DoubleClick_1(object sender, EventArgs e)
        {
            FaturaNo = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            textBox1.Text = listView1.SelectedItems[0].SubItems[0].Text;    // SubItems[0] = FaturaNo
            textBox2.Text = listView1.SelectedItems[0].SubItems[1].Text;    // SubItems[1] = MusteriAdi
            textBox3.Text = listView1.SelectedItems[0].SubItems[2].Text;    // SubItems[2] = FaturaTarih
            textBox4.Text = listView1.SelectedItems[0].SubItems[3].Text;
            textBox5.Text = listView1.SelectedItems[0].SubItems[4].Text;
            textBox6.Text = listView1.SelectedItems[0].SubItems[5].Text;
            textBox7.Text = listView1.SelectedItems[0].SubItems[6].Text;
        }

        // Fatura Filtreleme Formu
        private void button3_Click(object sender, EventArgs e)
        {
            Form6 FaturaFiltreleForm = new Form6();

            FaturaFiltreleForm.Show();
        }
    }
}