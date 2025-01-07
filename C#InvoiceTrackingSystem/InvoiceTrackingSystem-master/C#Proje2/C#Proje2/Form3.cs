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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-6J9R1GH\\SQLEXPRESS;Initial Catalog=faturaSistemi;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglan.Open();

                string query = "INSERT INTO Faturalar (MusteriAdi, FaturaTarihi, SonOdemeTarihi, ToplamTutar, OdemeBilgi, Aciklama) " +
                               "VALUES (@MusteriAdi, @FaturaTarihi, @SonOdemeTarihi, @ToplamTutar, @OdemeBilgi, @Aciklama)";

                using (SqlCommand komut = new SqlCommand(query, baglan))
                {
                    // Parametreleri ekliyoruz
                    komut.Parameters.AddWithValue("@MusteriAdi", textBox1.Text);
                    komut.Parameters.AddWithValue("@FaturaTarihi", dateTimePicker1.Value); // DateTimePicker'dan DateTime değeri
                    komut.Parameters.AddWithValue("@SonOdemeTarihi", dateTimePicker2.Value);
                    komut.Parameters.AddWithValue("@ToplamTutar", decimal.Parse(textBox2.Text)); // Eğer sayıysa decimal kullanın
                    komut.Parameters.AddWithValue("@OdemeBilgi", "Ödenmedi");
                    komut.Parameters.AddWithValue("@Aciklama", textBox4.Text);

                    // Komutu çalıştırıyoruz
                    komut.ExecuteNonQuery();
                }

                // Kod çalıştırdıktan sonra form kapatılır.
                this.Close();
            }

            // Diğer hatalar
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Bağlantıyı kapatırız
                if (baglan.State == ConnectionState.Open)
                {
                    baglan.Close();
                }
            }
        }
    }
}
