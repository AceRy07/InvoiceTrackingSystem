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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();

                
        }

        private SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-6J9R1GH\\SQLEXPRESS;Initial Catalog=faturaSistemi;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglan.Open();

                // Parametreli sorgu kullanıyoruz
                string sorgu = $"UPDATE Faturalar SET {comboBox1.Text} = @Deger WHERE FaturaNo = @FaturaNo";
                using (SqlCommand komut = new SqlCommand(sorgu, baglan))
                {
                    // `textBox2`'den gelen değeri kontrol edip uygun türde ekliyoruz
                    if (comboBox1.Text == "SonOdemeTarihi" || comboBox1.Text == "FaturaTarihi") // Tarih alanları
                    {
                        DateTime tarihDegeri;
                        if (DateTime.TryParse(textBox2.Text, out tarihDegeri))
                        {
                            komut.Parameters.AddWithValue("@Deger", tarihDegeri);
                        }
                        else
                        {
                            MessageBox.Show("Geçerli bir tarih giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        // Diğer alanlar (string, int, vb.)
                        komut.Parameters.AddWithValue("@Deger", textBox2.Text);
                    }

                    // Fatura numarasını parametre olarak ekliyoruz
                    int faturaNo;
                    if (int.TryParse(textBox3.Text, out faturaNo))
                    {
                        komut.Parameters.AddWithValue("@FaturaNo", faturaNo);
                    }
                    else
                    {
                        MessageBox.Show("Geçerli bir Fatura Numarası giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Sorguyu çalıştırıyoruz
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Kayıt başarıyla güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (baglan.State == ConnectionState.Open)
                {
                    baglan.Close();
                }
                this.Close();
            }
        }
    }
}