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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        // Veritabanı bağlantısı
        private SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-6J9R1GH\\SQLEXPRESS;Initial Catalog=faturaSistemi;Integrated Security=True");

        // Filtreleme Kodu
        void filtreleme(string filterAdi)
        {
            string filtre = textBox1.Text; // Kullanıcının girdiği filtre değeri
            string mesaj = "İstenilen Bilgiler:\n";
            bool odemeVar = false;
            List<object> list = new List<object>();


            try
            {
                string query = "SELECT FaturaNo, MusteriAdi, FaturaTarihi, SonOdemeTarihi, ToplamTutar, OdemeBilgi, Aciklama " +
                               "FROM Faturalar " +
                               $"WHERE {filterAdi} LIKE @Filtre";

                using (SqlCommand cmd = new SqlCommand(query, baglan))
                {
                    cmd.Parameters.AddWithValue("@Filtre", "%" + filtre + "%");
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        odemeVar = true;
                        mesaj += $"Fatura No: {reader["FaturaNo"]}, \n" +
                                 $"Musteri Adı: {reader["MusteriAdi"]}, \n" +
                                 $"Fatura Tarihi: {Convert.ToDateTime(reader["FaturaTarihi"]).ToShortDateString()}, \n" +
                                 $"Son Ödeme Tarihi: {Convert.ToDateTime(reader["SonOdemeTarihi"]).ToShortDateString()}, \n" +
                                 $"Tutar: {reader["ToplamTutar"]}, \n" +
                                 $"Ödeme Bilgisi: {reader["OdemeBilgi"]}, \n" +
                                 $"Açıklama: {reader["Aciklama"]}\n\n";
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            // Sonuçları kullanıcıya göster
            if (odemeVar)
            {
                MessageBox.Show(mesaj, "Filtreleme Sonuçları", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Girilen değerde fatura bilgisi bulunmamaktadır.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglan.Open();


            // Fatura No ile Filtreleme
            if(radioButton1.Checked)
            {
                textBox1.Text = "";
                filtreleme("FaturaNo");
            }

            else if(radioButton2.Checked)
            {
                textBox1.Text = "";
                filtreleme("MusteriAdi");
            }

            else if(radioButton3.Checked)
            {
                textBox1.Text = "";
                filtreleme("ToplamTutar");
            }

            else if (radioButton4.Checked)
            {
                textBox1.Text = "";
                filtreleme("OdemeBilgi");
            }

            else if (radioButton5.Checked)
            {
                textBox1.Text = "";
                filtreleme("Aciklama");
            }

            baglan.Close();
        }
    }
}
