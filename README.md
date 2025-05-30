# 📝 ASP.NET Core MVC Blog Yönetim Sistemi

Bu proje, kullanıcıların blog yazıları oluşturabildiği, düzenleyebildiği ve silebildiği tam işlevsel bir **ASP.NET Core MVC** uygulamasıdır. Aynı zamanda admin yetkisine sahip kullanıcılar için özel erişim bölümleri de içerir.

## 🚀 Özellikler

- Kullanıcı Kayıt ve Giriş Sistemi (Register & Login)
- Cookie Authentication ile Güvenli Oturum Yönetimi
- Yetki Tabanlı Rol Kontrolü (RBAC) - `User` ve `Admin`
- Blog yazısı oluşturma, listeleme, düzenleme ve silme (CRUD)
- Yalnızca yazarın kendi yazılarını düzenleyip silebilmesi
- Admin Paneli görünürlüğü sadece admin rolüne özel
- Şık ve duyarlı Bootstrap 5 arayüzü
- SQLite veritabanı kullanımı ve Entity Framework ile ORM
- Migration yapısı ve veri tabanı güncellemeleri desteklenir

## 🛠️ Kurulum

```bash
# Projeyi klonlayın
git clone https://github.com/semaakyavuz/-blog-website.git
cd -blog-website

# Gerekli paketleri yükleyin ve veritabanını oluşturun
dotnet restore
dotnet ef database update

# Uygulamayı başlatın
dotnet run
