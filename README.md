# GuncelDovizKurlariProjesi 💸

Bu proje, .NET Framework üzerinde C# ve Windows Forms kullanılarak geliştirilmiş, **RapidAPI** üzerinden **güncel döviz kurlarını (USD, EUR, GBP)** çekerek basit bir Türk Lirası (TRY) karşılığı hesaplama işlemi yapan bir masaüstü uygulamasıdır.

## 🚀 Genel Bakış

Uygulama, form yüklendiğinde `HttpClient` kullanarak asenkron bir şekilde RapidAPI'deki "Currency Conversion and Exchange Rates" API'sine bağlanır. USD/TRY, EUR/TRY ve GBP/TRY paritelerini alır ve ekranda gösterir. Kullanıcı, bir birim fiyat girdikten sonra Dolar, Euro veya Sterlin seçeneklerinden birini seçerek girilen tutarın anlık kur üzerinden TRY karşılığını hesaplatabilir. JSON verisi işlemek için `Newtonsoft.Json` kütüphanesi kullanılmıştır.

Bu proje, harici bir Web API'sinin nasıl tüketileceğini (`HttpClient`, `HttpRequestMessage`, Headers, `async/await`), JSON yanıtının nasıl parse edileceğini (`JObject.Parse`) ve basit bir WinForms uygulamasında nasıl kullanılacağını göstermek için pratik bir örnektir.

## ✨ Özellikler

*   **API Tüketimi:** RapidAPI'deki döviz kuru API'sine `HttpClient` ile istek gönderme.
*   **Güncel Kur Bilgisi:** Uygulama her açıldığında USD/TRY, EUR/TRY ve GBP/TRY kurlarını API'den çeker.
*   **Asenkron İşlemler:** `async/await` kullanılarak API istekleri asenkron olarak yapılır, böylece UI donmaz.
*   **JSON İşleme:** API'den dönen JSON yanıtı `Newtonsoft.Json` ile parse edilir.
*   **Kur Hesaplama:** Kullanıcının girdiği birim fiyatın, seçilen döviz kuruna göre TRY karşılığı hesaplanır.
*   **Basit WinForms Arayüzü:** Kurları gösterme, kullanıcıdan girdi alma ve sonucu gösterme işlemleri için temel WinForms kontrolleri kullanılır.

## 🛠️ Kullanılan Teknolojiler

*   **Programlama Dili:** C#
*   **Framework:** .NET Framework 4.7.2
*   **Arayüz:** Windows Forms (WinForms)
*   **HTTP İstemcisi:** `System.Net.Http.HttpClient`
*   **JSON Kütüphanesi:** `Newtonsoft.Json`
*   **Harici API:** RapidAPI (Currency Conversion and Exchange Rates endpoint'i)

## ⚠️ Önemli: API Anahtarı Gereksinimi

Bu projenin çalışabilmesi için **RapidAPI** üzerinden alınmış **geçerli bir API anahtarına (API Key)** ve ilgili API'ye (Currency Conversion and Exchange Rates) **aboneliğe** ihtiyacınız vardır.

1.  **RapidAPI Hesabı:** Eğer yoksa [RapidAPI](https://rapidapi.com/) adresinden ücretsiz bir hesap oluşturun.
2.  **API Aboneliği:** [Currency Conversion and Exchange Rates API](https://rapidapi.com/natkapral/api/currency-conversion-and-exchange-rates/) sayfasına gidin ve bir abonelik planı seçin (Genellikle ücretsiz bir başlangıç katmanı bulunur).
3.  **API Anahtarı:** Abonelik sonrası size özel olarak verilen `x-rapidapi-key` değerini kopyalayın.

## 💾 Kurulum ve Çalıştırma

1.  **Gereksinimler:**
    *   Visual Studio 2019 veya üzeri (.NET Framework 4.7.2 desteği ile)
    *   İnternet Bağlantısı (API'ye erişim için)
    *   RapidAPI Anahtarı ve Aboneliği (Yukarıdaki adıma bakın)

2.  **Projeyi Klonlama:**
    ```bash
    git clone https://github.com/kullanici-adiniz/GuncelDovizKurlariProjesi.git
    ```
    *(kullanici-adiniz kısmını kendi GitHub kullanıcı adınızla değiştirin)*

3.  **API Anahtarını Güncelleme:**
    *   `GuncelDovizKurlariProjesi` projesindeki `Form1.cs` dosyasını açın.
    *   `Form1_Load` metodu içindeki `Headers` bölümlerinde bulunan `"x-rapidapi-key"` değerini kendi RapidAPI anahtarınızla değiştirin. **3 yerde de** (Dolar, Euro, Sterlin istekleri için) aynı anahtarı kullanmalısınız.
        ```csharp
        // Örnek (Bu satırı 3 istek için de bulun ve güncelleyin):
        { "x-rapidapi-key", "SIZIN_RAPIDAPI_KEYINIZI_BURAYA_YAPISTIRIN" },
        ```
    *   **DİKKAT:** Kendi API anahtarınızı **asla** herkese açık kod depolarına (GitHub gibi) commit etmeyin! Bu sadece yerel test amaçlıdır. Daha güvenli yöntemler için yapılandırma dosyaları veya kullanıcı sırları (user secrets) kullanılmalıdır.

4.  **Projeyi Derleme ve Çalıştırma:**
    *   Visual Studio'da `GuncelDovizKurlariProjesi.sln` dosyasını açın.
    *   NuGet Paket Yöneticisi'nin `Newtonsoft.Json` paketini geri yüklediğinden emin olun (genellikle otomatik yapılır).
    *   Projeyi derleyin (Build -> Build Solution).
    *   Uygulamayı başlatın (Debug -> Start Debugging veya F5). Form yüklendiğinde kurlar API'den çekilip etiketlerde gösterilecektir.

## 📝 Kullanım

1.  Uygulama açıldığında Dolar, Euro ve Sterlin'in güncel TRY kurları ilgili etiketlerde görünür.
2.  "Birim Tutar" alanına TRY'ye çevirmek istediğiniz miktarı (ilgili döviz cinsinden) girin.
3.  Altındaki RadioButton'lardan hangi döviz kurunu kullanmak istediğinizi (Dolar, Euro, Sterlin) seçin.
4.  "İşlemi Yap" butonuna tıklayın.
5.  Hesaplanan TRY karşılığı "Ödenecek Tutar" alanında görüntülenecektir.

---
Bu README, projenizin harici bir API'yi nasıl kullandığını ve basit bir döviz hesaplaması yaptığını açıklar.
