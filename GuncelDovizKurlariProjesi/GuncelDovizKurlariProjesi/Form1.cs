using System; // Temel .NET sınıflarını kullanmak için.
using System.Collections.Generic; // Generic koleksiyonları kullanmak için.
using System.ComponentModel; // WinForms tasarımcısı için gerekli.
using System.Data; // Veri yapılarını kullanmak için (kullanılmıyor).
using System.Drawing; // Grafik işlemleri için (WinForms).
using System.Linq; // LINQ sorguları için (kullanılmıyor).
using System.Text; // Metin işlemleri için.
using System.Threading.Tasks; // Asenkron işlemler (async/await) için.
using System.Windows.Forms; // Windows Forms kontrolleri için.
using System.Net.Http; // HTTP istekleri yapmak için (API).
using Newtonsoft.Json.Linq; // JSON verilerini işlemek için (Newtonsoft.Json).

namespace GuncelDovizKurlariProjesi // Projenin ad alanı.
{
    public partial class Form1 : Form // Ana form sınıfı, Form'dan türetilmiş.
    {
        public Form1() // Formun yapıcı metodu (constructor).
        {
            InitializeComponent(); // Form tasarımındaki kontrolleri yükler.
        }

        // Döviz kurlarını saklamak için sınıf seviyesinde değişkenler.
        decimal dollar = 0; // Dolar kuru (TRY).
        decimal euro = 0;   // Euro kuru (TRY).
        decimal pound = 0;  // Sterlin kuru (TRY).

        // Form yüklendiğinde çalışan olay işleyici (asenkron).
        private async void Form1_Load(object sender, EventArgs e)
        {
            #region Dolar Kuru Alma
            var client = new HttpClient(); // HTTP istekleri için istemci oluştur.
            var request = new HttpRequestMessage // Yeni bir HTTP isteği tanımla.
            {
                Method = HttpMethod.Get, // İstek yöntemi: GET.
                RequestUri = new Uri("https://currency-conversion-and-exchange-rates.p.rapidapi.com/convert?from=USD&to=TRY&amount=1"), // API adresi (USD->TRY).
                Headers = // İstek başlıkları (headers).
                {
                    { "x-rapidapi-key", "630ce9cc86msh271c60cffe62d5ep1b514djsn0fe292593744" }, // API anahtarı.
                    { "x-rapidapi-host", "currency-conversion-and-exchange-rates.p.rapidapi.com" }, // API sunucusu.
                },
            };
            // İsteği gönder ve yanıtı bekle ('using' ile kaynakları yönet).
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode(); // Yanıtın başarılı olduğunu kontrol et (hata varsa exception fırlatır).
                var body = await response.Content.ReadAsStringAsync(); // Yanıt içeriğini string olarak oku.
                var json = JObject.Parse(body); // JSON metnini parse et.
                var value = json["result"].ToString(); // JSON'dan kur değerini ('result' alanı) al.
                lblDollar.Text = "Dolar: " + value; // Dolar etiketini güncelle.
                dollar = decimal.Parse(value); // Kur değerini decimal'a çevirip değişkene ata.
            }
            #endregion

            #region Euro Kuru Alma
            var client2 = new HttpClient(); // Yeni HTTP istemcisi (Euro için).
            var request2 = new HttpRequestMessage // Yeni HTTP isteği (Euro için).
            {
                Method = HttpMethod.Get, // İstek yöntemi: GET.
                RequestUri = new Uri("https://currency-conversion-and-exchange-rates.p.rapidapi.com/convert?from=EUR&to=TRY&amount=1"), // API adresi (EUR->TRY).
                Headers = // İstek başlıkları.
                {
                    { "x-rapidapi-key", "630ce9cc86msh271c60cffe62d5ep1b514djsn0fe292593744" }, // API anahtarı.
                    { "x-rapidapi-host", "currency-conversion-and-exchange-rates.p.rapidapi.com" }, // API sunucusu.
                },
            };
            // İsteği gönder ve yanıtı bekle.
            using (var response2 = await client2.SendAsync(request2))
            {
                response2.EnsureSuccessStatusCode(); // Yanıt başarısını kontrol et.
                var body2 = await response2.Content.ReadAsStringAsync(); // Yanıt içeriğini oku.
                var json2 = JObject.Parse(body2); // JSON'u parse et.
                var value2 = json2["result"].ToString(); // Kur değerini al.
                lblEuro.Text = "Euro: " + value2; // Euro etiketini güncelle.
                euro = decimal.Parse(value2); // Kuru decimal'a çevirip değişkene ata.
            }
            #endregion

            #region Sterlin Kuru Alma
            var client3 = new HttpClient(); // Yeni HTTP istemcisi (Sterlin için).
            var request3 = new HttpRequestMessage // Yeni HTTP isteği (Sterlin için).
            {
                Method = HttpMethod.Get, // İstek yöntemi: GET.
                RequestUri = new Uri("https://currency-conversion-and-exchange-rates.p.rapidapi.com/convert?from=GBP&to=TRY&amount=1"), // API adresi (GBP->TRY).
                Headers = // İstek başlıkları.
                {
                    { "x-rapidapi-key", "630ce9cc86msh271c60cffe62d5ep1b514djsn0fe292593744" }, // API anahtarı.
                    { "x-rapidapi-host", "currency-conversion-and-exchange-rates.p.rapidapi.com" }, // API sunucusu.
                },
            };
            // İsteği gönder ve yanıtı bekle.
            using (var response3 = await client3.SendAsync(request3))
            {
                response3.EnsureSuccessStatusCode(); // Yanıt başarısını kontrol et.
                var body3 = await response3.Content.ReadAsStringAsync(); // Yanıt içeriğini oku.
                var json3 = JObject.Parse(body3); // JSON'u parse et.
                var value3 = json3["result"].ToString(); // Kur değerini al.
                lblPound.Text = "Pound: " + value3; // Sterlin etiketini güncelle.
                pound = decimal.Parse(value3); // Kuru decimal'a çevirip değişkene ata.
            }
            #endregion

            txtTotalPrice.Enabled = false; // Toplam tutar metin kutusunu devre dışı bırak (sadece gösterim).
        }

        // Buton 1'e tıklandığında çalışan olay işleyici.
        private void button1_Click(object sender, EventArgs e)
        {
            // Birim fiyat metin kutusundaki değeri decimal'a çevir.
            decimal unitPrice = decimal.Parse(txtUnitPrice.Text);

            // Toplam tutar değişkeni.
            decimal totalPrice = 0;

            // Seçili olan radio butona göre hesaplama yap.
            if (rdbDollar.Checked) // Dolar seçiliyse.
            {
                totalPrice = unitPrice * dollar; // Toplam = Birim Fiyat * Dolar Kuru.
            }
            if (rdbEuro.Checked) // Euro seçiliyse.
            {
                totalPrice = unitPrice * euro; // Toplam = Birim Fiyat * Euro Kuru.
            }
            if (rdbPound.Checked) // Sterlin seçiliyse.
            {
                totalPrice = unitPrice * pound; // Toplam = Birim Fiyat * Sterlin Kuru.
            }

            // Hesaplanan toplam tutarı metin kutusunda göster.
            txtTotalPrice.Text = totalPrice.ToString();
        }
    }
}