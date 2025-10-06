using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using ChatApp.Properties;
using Guna.UI2.WinForms;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using WinFormAnimation;
using Xamarin.Essentials;
using System.Text.RegularExpressions; // Regex için gerekli

namespace ChatApp
{
    public partial class Form2 : Form
    {
        public string emailname { set; get; }

        public Form2()
        {
            InitializeComponent();
        }

        string constring = "Data Source=;";

        private void Form2_Load(object sender, EventArgs e)
        {


            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 100; // 0.5 saniye
            timer.Tick += new EventHandler(timer3_Tick);
            timer.Start();

            MessageChat();

            label2.Text = emailname; // Giriş yapan e-posta
            byte[] getimage = new byte[0];
            // Profil resmi çekimi
            SqlConnection con = new SqlConnection(constring);
            con.Open();

            string q = "select * from Login WHERE email = '" + label2.Text + "'";
            SqlCommand cmd = new SqlCommand(q, con);

            SqlDataReader dataReader = cmd.ExecuteReader();
            dataReader.Read();
            if (dataReader.HasRows)
            {
                label2.Text = dataReader["email"].ToString();
                guna2TextBox1.Text = dataReader["Ad"].ToString();
                guna2TextBox5.Text = dataReader["firstname"].ToString();


                guna2TextBox2.Text = dataReader["Soyad"].ToString();
                guna2TextBox6.Text = dataReader["lastname"].ToString();


                guna2TextBox3.Text = dataReader["Email"].ToString();
                guna2TextBox7.Text = dataReader["Email"].ToString();


                guna2TextBox4.Text = dataReader["Şifre"].ToString();



                byte[] images = (byte[])dataReader["image"];
                if (images != null)
                {
                    guna2CirclePictureBox1.Image = null;
                    guna2CirclePictureBox2.Image = null;
                    guna2CirclePictureBox3.Image = null;

                }
                else
                {
                    MemoryStream me = new MemoryStream(images);
                    guna2CirclePictureBox1.Image = Image.FromStream(me);
                    guna2CirclePictureBox2.Image = Image.FromStream(me);
                    guna2CirclePictureBox3.Image = Image.FromStream(me);

                }
            }
            con.Close();
        }


        private bool check;
        // Panelin açılıp kapanma durumunu kontrol eden değişken
        bool isPanelOpen = false;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isPanelOpen)
            {
                // Açılma animasyonu
                panel1.Width += 10;
                if (panel1.Width >= panel1.MaximumSize.Width)
                {
                    panel1.Width = panel1.MaximumSize.Width; // Maksimum boyuta ulaşıldığında durdur
                    timer1.Stop();
                }
            }
            else
            {
                // Kapanma animasyonu
                panel1.Width -= 10;
                if (panel1.Width <= panel1.MinimumSize.Width)
                {
                    panel1.Width = panel1.MinimumSize.Width; // Minimum boyuta ulaşıldığında durdur
                    timer1.Stop();
                    check = true;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Timer'ı başlat ve panelin durumunu tersine çevir
            isPanelOpen = !isPanelOpen;
            timer1.Start();

            // Resmi her tıklamada değiştir
            if (isPanelOpen)
            {
                pictureBox1.Image = Resources.left3;  // Panel açılınca gösterilecek resim
            }
            else
            {
                pictureBox1.Image = Resources.nokta3;  // Panel kapanınca gösterilecek resim
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (panel4.Visible == false)
            {
                panel4.Visible = true;
                panel4.BringToFront(); // Paneli diğer tüm kontrollerin üstüne taşır
            }
            else
            {
                panel4.Visible = false;
            }
        }


        private void guna2Button5_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            this.Hide();
            f1.ShowDialog();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            if (panel5.Visible == false)
            {
                panel5.Visible = true;
            }
            else
            {
                panel5.Visible = false;
            }
            panel5.BringToFront(); // Paneli diğer tüm kontrollerin üstüne taşır


            if (panel6.Visible)
                panel6.Visible = false;

            if (panel8.Visible)
                panel8.Visible = false;

            if (panel9.Visible)
                panel9.Visible = false;

            if (panel10.Visible)
                panel10.Visible = false;

            if (panel11.Visible)
                panel11.Visible = false;
            if (flowLayoutPanel2.Visible)
                flowLayoutPanel2.Visible = false;
        }

        private void guna2CirclePictureBox3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "select image (*Jpg; *.png; *Gif| *.Jpg; *.png; *Gif;)";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                guna2CirclePictureBox3.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        public void showprofile()
        {
            {
                byte[] getimage = new byte[0];
                // Profil resmi çekimi
                SqlConnection con = new SqlConnection(constring);
                con.Open();

                string q = "select * from Login WHERE email = '" + guna2TextBox7.Text + "'";
                SqlCommand cmd = new SqlCommand(q, con);

                SqlDataReader dataReader = cmd.ExecuteReader();
                dataReader.Read();
                if (dataReader.HasRows)
                {
                    label2.Text = dataReader["email"].ToString();
                    guna2TextBox1.Text = dataReader["Ad"].ToString();
                    guna2TextBox5.Text = dataReader["firstname"].ToString();


                    guna2TextBox2.Text = dataReader["Soyad"].ToString();
                    guna2TextBox6.Text = dataReader["lastname"].ToString();


                    guna2TextBox3.Text = dataReader["Email"].ToString();
                    guna2TextBox7.Text = dataReader["Email"].ToString();


                    guna2TextBox4.Text = dataReader["Şifre"].ToString();



                    byte[] images = (byte[])dataReader["image"];
                    if (images != null)
                    {
                        guna2CirclePictureBox1.Image = null;
                        guna2CirclePictureBox2.Image = null;
                        guna2CirclePictureBox3.Image = null;

                    }
                    else
                    {
                        MemoryStream me = new MemoryStream(images);
                        guna2CirclePictureBox1.Image = Image.FromStream(me);
                        guna2CirclePictureBox2.Image = Image.FromStream(me);
                        guna2CirclePictureBox3.Image = Image.FromStream(me);

                    }
                }
                con.Close();
            }
        }

        // Profil güncelleme butonunun click event'i
        private void guna2Button7_Click(object sender, EventArgs e)
        {
            // Gerekli alanların boş olup olmadığını kontrol et
            if (string.IsNullOrEmpty(guna2TextBox5.Text.Trim())) // Ad alanı
            {
                errorProvider1.SetError(guna2TextBox5, "Firstname is required");
                return;
            }
            else
            {
                errorProvider1.SetError(guna2TextBox5, string.Empty);
            }

            if (string.IsNullOrEmpty(guna2TextBox6.Text.Trim())) // Soyad alanı
            {
                errorProvider1.SetError(guna2TextBox6, "Lastname is required");
                return;
            }
            else
            {
                errorProvider1.SetError(guna2TextBox6, string.Empty);
            }

            if (string.IsNullOrEmpty(guna2TextBox7.Text.Trim())) // E-posta alanı
            {
                errorProvider1.SetError(guna2TextBox7, "Email is required");
                return;
            }
            else if (!IsValidEmail(guna2TextBox7.Text)) // Geçerli e-posta kontrolü
            {
                errorProvider1.SetError(guna2TextBox7, "Invalid email format");
                return;
            }
            else
            {
                errorProvider1.SetError(guna2TextBox7, string.Empty);
            }

            // Veritabanı bağlantısı
            SqlConnection con = new SqlConnection(constring);

            try
            {
                con.Open(); // Bağlantıyı aç
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veritabanı bağlantı hatası: " + ex.Message);
                return;
            }

            // E-posta adresinin daha önce kullanılıp kullanılmadığını kontrol et
            string checkEmailQuery = "SELECT COUNT(*) FROM Login WHERE email = @newEmail AND email != @currentEmail";
            SqlCommand checkEmailCmd = new SqlCommand(checkEmailQuery, con);
            checkEmailCmd.Parameters.AddWithValue("@newEmail", guna2TextBox7.Text); // Yeni e-posta
            checkEmailCmd.Parameters.AddWithValue("@currentEmail", guna2TextBox3.Text); // Mevcut e-posta (kullanıcının mevcut e-posta adresi)

            int emailExists = (int)checkEmailCmd.ExecuteScalar(); // Eğer 1 dönerse, e-posta var demektir

            if (emailExists > 0)
            {
                MessageBox.Show("Bu e-posta adresi zaten kullanılıyor. Lütfen başka bir e-posta adresi girin.");
                return;
            }

            // SQL sorgusunu oluştur
            string q = "UPDATE Login SET ";

            // Parametreleri dinamik olarak ekle
            List<SqlParameter> parameters = new List<SqlParameter>();
            bool first = true;

            // Firstname parametresi
            if (!string.IsNullOrEmpty(guna2TextBox5.Text.Trim()))
            {
                if (!first) q += ", ";
                q += "firstname = @fname";
                parameters.Add(new SqlParameter("@fname", guna2TextBox5.Text));
                first = false;
            }

            // Lastname parametresi
            if (!string.IsNullOrEmpty(guna2TextBox6.Text.Trim()))
            {
                if (!first) q += ", ";
                q += "lastname = @lname";
                parameters.Add(new SqlParameter("@lname", guna2TextBox6.Text));
                first = false;
            }

            // Email parametresi
            if (!string.IsNullOrEmpty(guna2TextBox7.Text.Trim()))
            {
                if (!first) q += ", ";
                q += "email = @email";
                parameters.Add(new SqlParameter("@email", guna2TextBox7.Text));
                first = false;
            }

            // Profil resmi parametresi
            MemoryStream me = new MemoryStream();
            if (guna2CirclePictureBox3.Image != null)
            {
                guna2CirclePictureBox3.Image.Save(me, guna2CirclePictureBox3.Image.RawFormat);
            }

            byte[] imageBytes = me.ToArray().Length > 0 ? me.ToArray() : null;
            if (imageBytes != null)
            {
                if (!first) q += ", ";
                q += "image = @image";
                parameters.Add(new SqlParameter("@image", imageBytes));
                first = false;
            }

            // Şifre parametresi
            if (!string.IsNullOrEmpty(guna2TextBox4.Text.Trim()))
            {
                if (!first) q += ", ";
                q += "password = @password";
                parameters.Add(new SqlParameter("@password", guna2TextBox4.Text));
                first = false;
            }

            // WHERE koşulunu ekle
            q += " WHERE email = @currentEmail";
            parameters.Add(new SqlParameter("@currentEmail", guna2TextBox3.Text));  // Mevcut e-posta (güncel e-posta ile karşılaştırma)

            // SQL komutunu oluştur
            SqlCommand cmd = new SqlCommand(q, con);

            // Parametreleri ekle
            cmd.Parameters.AddRange(parameters.ToArray());

            try
            {
                // SQL sorgusunu çalıştır ve güncellemeyi yap
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    // Eğer satır güncellenmişse
                    MessageBox.Show("Profil Güncellendi");
                }
                else
                {
                    // Eğer satır güncellenmemişse
                    MessageBox.Show("Güncelleme başarısız. E-posta adresi ile eşleşen bir kullanıcı bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                con.Close(); // Bağlantıyı kapat
            }

            // Kullanıcı bilgilerini güncelledikten sonra formda göster
            SetUserData(
                string.IsNullOrEmpty(guna2TextBox5.Text.Trim()) ? guna2TextBox1.Text : guna2TextBox5.Text,
                string.IsNullOrEmpty(guna2TextBox6.Text.Trim()) ? guna2TextBox2.Text : guna2TextBox6.Text,
                string.IsNullOrEmpty(guna2TextBox7.Text.Trim()) ? guna2TextBox3.Text : guna2TextBox7.Text,
                string.IsNullOrEmpty(guna2TextBox4.Text.Trim()) ? guna2TextBox4.Text : guna2TextBox4.Text,
                imageBytes
            );
        }

        // E-posta doğrulama fonksiyonu
        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
        //PROFİL GÜNCELLEME AD SOYAD RESİM EMAİL

        // Kullanıcı bilgilerini almak için bir metot
        public void SetUserData(string firstname, string lastname, string email, string password, byte[] imageBytes)
        {
            // Kullanıcı bilgilerini ilgili alanlara yerleştir
            guna2TextBox1.Text = firstname;  // Ad
            guna2TextBox2.Text = lastname;   // Soyad
            guna2TextBox3.Text = email;      // Email
            guna2TextBox4.Text = password;   // Şifre
            label2.Text = email;
            // Eğer resim varsa, resmi PictureBox'lara yerleştir
            if (imageBytes != null && imageBytes.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    Image img = Image.FromStream(ms);

                    // Resmi her iki PictureBox'a atayalım
                    guna2CirclePictureBox2.Image = img;
                    guna2CirclePictureBox1.Image = img;
                }
            }
            else
            {
                // Eğer resim yoksa, PictureBox'ları temizleyebilirsiniz veya varsayılan bir resim atanabilir
                guna2CirclePictureBox2.Image = null;
                guna2CirclePictureBox1.Image = null;
            }
        }
        //PROFİL GÖRÜNTÜLEME

        //AÇILMA KAPANMA ANİMASYONU DÜZELTİLECEK
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (isPanelOpen)
            {
                // Açılma animasyonu
                panel7.Height += 10;
                guna2Button10.Image = Resources.yukari2; // Buton görselini değiştir

                if (panel7.Height >= panel7.MaximumSize.Height) // Panel maksimum yüksekliğe ulaşınca dur
                {
                    timer2.Stop();
                    check = false;
                }
            }
            else
            {
                // Kapanma animasyonu
                panel7.Height -= 10;
                guna2Button10.Image = Resources.asagi2; // Buton görselini değiştir

                if (panel7.Height <= panel7.MinimumSize.Height) // Panel minimum yüksekliğe ulaşınca dur
                {
                    timer2.Stop();
                    check = true;
                }
            }
        }


        //AÇILMA KAPANMA ANİMASYONU DÜZELTİLECEK


        //ayarları açıp kapatma
        private void guna2Button10_Click(object sender, EventArgs e)
        {
            // Butona her tıklandığında görsel değişimini yap
            if (isPanelOpen)
            {
                guna2Button10.Image = Resources.yukari2; // Kapanma görseli
                isPanelOpen = false;  // Panelin kapalı olduğunu işaretle
            }
            else
            {
                guna2Button10.Image = Resources.asagi2;  // Açılma görseli
                isPanelOpen = true;  // Panelin açık olduğunu işaretle
            }

            // Panel animasyonunu başlat
            timer2.Start();
        }

        //ayarları açıp kapatma



        //PROFİL YERİ buton
        private void guna2Button9_Click(object sender, EventArgs e)
        {
            // Panel6'nın görünürlüğünü değiştir
            panel6.Visible = !panel6.Visible;
            panel6.BringToFront(); // Paneli diğer tüm kontrollerin üstüne taşır

        }


        private void pictureBox3_Click(object sender, EventArgs e)
        {
            // Panel6'nın görünürlüğünü değiştir
            panel6.Visible = !panel6.Visible;
        }


        //PROFİL YERİ buton


        //SOHBET BUTON AÇILIŞ


        private void guna2Button2_Click(object sender, EventArgs e)
        {
            timer4.Start();
            timer3.Start();

            UserItem();

            // Panel9'un görünürlüğünü tersine çevir
            panel9.Visible = !panel9.Visible;

            // Eğer panel9 açılıyorsa diğer panelleri kapat
            if (panel9.Visible)
            {
                panel6.Visible = false;
                panel8.Visible = false;
                panel9.BringToFront();
            }
        }


        //SOHBET BUTON KAPANIŞ


        //PASSWORD YERİ
        //PASSWORD YERİ BUTON

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            // Panel8'in görünürlüğünü değiştir
            panel8.Visible = !panel8.Visible;
            panel8.BringToFront();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            // Panel8'in görünürlüğünü değiştir
            panel8.Visible = !panel8.Visible;
        }
        //PASSWORD YERİ BUTON


        //password değiştirme butonu
        private void guna2Button11_Click(object sender, EventArgs e)
        {
            // Mevcut şifreyi kontrol et
            if (string.IsNullOrEmpty(guna2TextBox8.Text.Trim()))
            {
                errorProvider1.SetError(guna2TextBox8, "Mevcut şifrenizi giriniz.");
                return;
            }
            else
            {
                errorProvider1.SetError(guna2TextBox8, string.Empty);
            }

            // Yeni şifreyi kontrol et
            if (string.IsNullOrEmpty(guna2TextBox9.Text.Trim()))
            {
                errorProvider1.SetError(guna2TextBox9, "Yeni şifrenizi giriniz.");
                return;
            }
            else
            {
                errorProvider1.SetError(guna2TextBox9, string.Empty);
            }

            // Yeni şifreyi doğrulama kısmını kontrol et
            if (string.IsNullOrEmpty(guna2TextBox10.Text.Trim()))
            {
                errorProvider1.SetError(guna2TextBox10, "Yeni şifrenizi onaylayınız.");
                return;
            }
            else
            {
                errorProvider1.SetError(guna2TextBox10, string.Empty);
            }

            // Yeni şifre ile doğrulama şifresinin eşleştiğini kontrol et
            if (guna2TextBox9.Text != guna2TextBox10.Text)
            {
                MessageBox.Show("Yeni şifreler eşleşmiyor.");
                return;
            }

            // Mevcut şifreyi kontrol et, SetUserData'dan alınan şifre ile karşılaştır
            string currentPassword = guna2TextBox4.Text; // SetUserData ile gelen mevcut şifre

            // Mevcut şifrenin doğruluğunu kontrol et
            if (guna2TextBox8.Text != currentPassword)
            {
                MessageBox.Show("Mevcut şifreniz hatalı.");
                return;
            }

            // SQL işlemi
            SqlConnection con = new SqlConnection(constring);
            try
            {
                con.Open(); // Bağlantıyı aç

                // SQL sorgusu (sadece password alanını güncelle)
                string query = "UPDATE Login SET password = @NewPassword WHERE password = @CurrentPassword";
                SqlCommand cmdUpdate = new SqlCommand(query, con);

                // Parametreleri ekle
                cmdUpdate.Parameters.AddWithValue("@NewPassword", guna2TextBox9.Text); // Yeni şifre
                cmdUpdate.Parameters.AddWithValue("@CurrentPassword", guna2TextBox8.Text); // Mevcut şifre

                // SQL komutunu çalıştır ve kaç satır etkilendiğini öğren
                int rowsAffected = cmdUpdate.ExecuteNonQuery();

                // Eğer etkilenen satır yoksa
                if (rowsAffected == 0)
                {
                    MessageBox.Show("Şifre değiştirilemedi. Lütfen mevcut şifrenizi kontrol edin.");
                }
                else
                {
                    MessageBox.Show("Şifreniz başarıyla değiştirilmiştir.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message); // Hata mesajını al
            }
            finally
            {
                con.Close(); // Bağlantıyı kapat
            }
        }

        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2TextBox8.PasswordChar == '*' && guna2TextBox9.PasswordChar == '*' && guna2TextBox10.PasswordChar == '*')
            {
                guna2TextBox8.PasswordChar = '\0';
                guna2TextBox9.PasswordChar = '\0';
                guna2TextBox10.PasswordChar = '\0';
            }
            else
            {
                guna2TextBox8.PasswordChar = '*';
                guna2TextBox9.PasswordChar = '*';
                guna2TextBox10.PasswordChar = '*';
            }
        }

		//password değiştirme butonu

		//PASSWORD YERİ

	private void UserItem()
{
    flowLayoutPanel1.Controls.Clear();
    flowLayoutPanel1.WrapContents = false;
    flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
    flowLayoutPanel1.Height = 400;
    flowLayoutPanel1.HorizontalScroll.Enabled = false;
    flowLayoutPanel1.HorizontalScroll.Visible = false;
    flowLayoutPanel1.VerticalScroll.Enabled = true;
    flowLayoutPanel1.VerticalScroll.Visible = false;
    flowLayoutPanel1.AutoScroll = true;
    flowLayoutPanel1.BackColor = Color.Black;
    flowLayoutPanel1.ForeColor = Color.DarkBlue;

    // 🔹 Test kullanıcıları (veritabanı yerine sabit liste)
    var users = new List<(string Name, string Role)>
    {
        ("Admin", "Yönetici"),
        ("Emre", "Kullanıcı"),
        ("Ahmet", "Kullanıcı"),
        ("Ayşe", "Kullanıcı"),
        ("Zeynep", "Kullanıcı")
    };

    foreach (var user in users)
    {
        // UserControl oluştur
        UserControl1 userControl = new UserControl1();
        userControl.Title = user.Name;

        // Örnek olarak sahte okunmamış mesaj sayısı
        userControl.unreadCountLabel.Text = $"{new Random().Next(0, 5)} okunmamış mesaj";

        // 🔸 Eğer giriş yapan admin ise herkesi görebilsin
        if (ActiveUser.FirstName?.ToLower() == "admin")
        {
            flowLayoutPanel1.Controls.Add(userControl);
        }
        // 🔸 Normal kullanıcı kendi adını listede görmesin
        else
        {
            if (userControl.Title.ToLower() != ActiveUser.FirstName?.ToLower())
            {
                flowLayoutPanel1.Controls.Add(userControl);
            }
        }

        // Kullanıcıya tıklanınca sohbet ekranı açsın (örnek)
        userControl.Click += (s, e) =>
        {
            MessageBox.Show($"{userControl.Title} ile sohbet başlatılıyor...", "Sohbet", MessageBoxButtons.OK, MessageBoxIcon.Information);
        };
    }
}







		private int GetUnreadMessageCount(int activeUserId, string otherUserId)
        {
            int unreadCount = 0;
            string senderName = string.Empty; // Gönderen kullanıcı adı
            string connectionString = "Data Source=";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Bireysel mesajlar için sorgu
                string query = @"
            SELECT COUNT(*), userone, usertwo 
            FROM [Chat] 
            WHERE 
                ((userone = @ActiveUserId AND usertwo = @OtherUserId) OR 
                 (usertwo = @ActiveUserId AND userone = @OtherUserId)) 
                AND status = 0
            GROUP BY userone, usertwo";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Parametreleri string olarak gönderiyoruz
                    command.Parameters.AddWithValue("@ActiveUserId", activeUserId.ToString());  // Parametreyi string'e dönüştürdük
                    command.Parameters.AddWithValue("@OtherUserId", otherUserId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            unreadCount = reader.GetInt32(0); // unreadCount'ı al

                            // Mesajı atan kullanıcıyı belirle
                            if (reader["userone"].ToString() != activeUserId.ToString())
                            {
                                senderName = reader["userone"].ToString(); // Eğer userone eşleşmiyorsa, demek ki userone mesajı atmış
                            }
                            else
                            {
                                senderName = reader["usertwo"].ToString(); // usertwo mesajı atmış
                            }
                        }
                    }
                }

                // Grup mesajları için sorgu
                string groupQuery = @"
            SELECT COUNT(*)
            FROM [Chat] c
            JOIN [Groups] g ON c.GroupID = g.GroupID
            WHERE 
                ((g.UserOneID = @ActiveUserId OR g.UserTwoID = @ActiveUserId) 
                 AND c.status = 0)
            AND (g.UserOneID = @OtherUserId OR g.UserTwoID = @OtherUserId)"; // Burada, hem aktif kullanıcı hem de diğer kullanıcı grubun içinde olmalı

                using (SqlCommand groupCommand = new SqlCommand(groupQuery, connection))
                {
                    groupCommand.Parameters.AddWithValue("@ActiveUserId", activeUserId.ToString());  // Parametreyi string'e dönüştürdük
                    groupCommand.Parameters.AddWithValue("@OtherUserId", otherUserId);
                    unreadCount += (int)groupCommand.ExecuteScalar();
                }
            }

            // Mesajı atan kullanıcıyı ve okunmamış mesaj sayısını göstermek için
            if (unreadCount > 0)
            {
                return unreadCount;
            }
            else
            {
                return 0;
            }
        }



        // Seçilen resim yolunu tutacak değişken
        private string selectedImagePath = null;

        private void guna2Button12_Click(object sender, EventArgs e)
        {
            timer4.Start();


            SqlConnection con = new SqlConnection(constring);

            // Kullanıcı ID'lerini al
            int userOneID = GetUserID(guna2TextBox1.Text);
            int userTwoID = GetUserID(label12.Text);

            // Kullanıcılar arasında grup var mı kontrol et
            int groupId = GetGroupId(userOneID, userTwoID);

            // Eğer grup yoksa, yeni bir grup oluştur
            if (groupId == 0)
            {
                groupId = CreateGroup(userOneID, userTwoID);
            }

            // Mesajı veritabanına ekle
            string q = "insert into Chat (GroupID, userone, usertwo, message, image) values (@groupId, @userone, @usertwo, @message, @image)";
            SqlCommand cmd = new SqlCommand(q, con);

            cmd.Parameters.Add("@groupId", SqlDbType.Int).Value = groupId;
            cmd.Parameters.Add("@userone", SqlDbType.NVarChar).Value = guna2TextBox1.Text;
            cmd.Parameters.Add("@usertwo", SqlDbType.NVarChar).Value = label12.Text;
            cmd.Parameters.Add("@message", SqlDbType.NVarChar).Value = guna2TextBox11.Text;

            // Resim varsa ekle, yoksa null gönder
            byte[] imageBytes = null;
            if (!string.IsNullOrEmpty(selectedImagePath))
            {
                // Resim verisini ekle
                imageBytes = System.IO.File.ReadAllBytes(selectedImagePath);
                cmd.Parameters.Add("@image", SqlDbType.VarBinary).Value = imageBytes;
            }
            else
            {
                // Resim yoksa null gönder
                cmd.Parameters.Add("@image", SqlDbType.VarBinary).Value = DBNull.Value;
            }

            try
            {
                con.Open();
                cmd.ExecuteNonQuery(); // Mesajı veritabanına ekle

                // Mesajı ve resmi sohbet ekranına ekle
                AddMessageToChat(guna2TextBox1.Text, label12.Text, guna2TextBox11.Text, imageBytes);

                guna2TextBox11.Clear(); // Mesaj kutusunu temizle
                selectedImagePath = null; // Seçilen resim yolunu sıfırla

                // Timer'ı başlat: Veriyi güncellemek için sadece UserItem çağrılır
                if (timer4.Enabled)
                {
                    timer4.Stop();  // Timer'ı durdur
                }

                timer4.Start();  // Timer'ı yeniden başlat
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mesaj gönderilemedi: " + ex.Message);
            }
            finally
            {
                timer4.Start();
                con.Close();
            }
        }





        private void MessageChat()
        {
            timer4.Start();
            SqlConnection con = new SqlConnection(constring);

            // Kullanıcı ID'lerini al
            int userOneID = GetUserID(guna2TextBox1.Text);
            int userTwoID = GetUserID(label12.Text);

            // Kullanıcılar arasında grup var mı kontrol et
            int groupId = GetGroupId(userOneID, userTwoID);

            SqlDataAdapter adapter = new SqlDataAdapter(
                "SELECT * FROM Chat WHERE GroupID = @groupId",
                con
            );

            adapter.SelectCommand.Parameters.AddWithValue("@groupId", groupId);

            DataTable table = new DataTable();
            adapter.Fill(table);

            flowLayoutPanel2.Controls.Clear();

            if (table != null && table.Rows.Count > 0)
            {
                foreach (DataRow row in table.Rows)
                {
                    byte[] imageBytes = row["image"] as byte[];

                    // Mesajı doğru kullanıcıya göre göster
                    if (guna2TextBox1.Text == row["userone"].ToString() && label12.Text == row["usertwo"].ToString())
                    {
                        AddMessageToChat(guna2TextBox1.Text, label12.Text, row["message"].ToString(), imageBytes);
                    }
                    else if (label12.Text == row["userone"].ToString() && guna2TextBox1.Text == row["usertwo"].ToString())
                    {
                        AddMessageToChat(label12.Text, guna2TextBox1.Text, row["message"].ToString(), imageBytes);
                    }
                }

                // Mesajları okundu olarak işaretle
                MarkMessagesAsRead();
            }
            else
            {
                Label noMessagesLabel = new Label();
                noMessagesLabel.Text = "Daha Önce Hiç mesajlaşmadınız.";
                noMessagesLabel.Dock = DockStyle.Top;
                noMessagesLabel.TextAlign = ContentAlignment.MiddleCenter;
                noMessagesLabel.AutoSize = true;
                flowLayoutPanel2.Controls.Add(noMessagesLabel);
            }
        }


        private int GetUserID(string username)
        {
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("SELECT ID FROM [dbo].[Login] WHERE firstname = @username", con); // Login tablosunda firstname ile eşleşme yapıyoruz
            cmd.Parameters.AddWithValue("@username", username);

            try
            {
                con.Open();
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    return Convert.ToInt32(result); // Kullanıcı ID'sini döndürüyoruz
                }
                else
                {
                    return 0; // Kullanıcı bulunamadı
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kullanıcı ID'si alınırken hata oluştu: " + ex.Message);
                return 0;
            }
            finally
            {
                con.Close();
            }
        }

        // Kullanıcılar arasında bir grup var mı kontrolü
        private int GetGroupId(int userOneID, int userTwoID)
        {
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("SELECT GroupID FROM [dbo].[Groups] WHERE (UserOneID = @userOneID AND UserTwoID = @userTwoID) OR (UserOneID = @userTwoID AND UserTwoID = @userOneID)", con);
            cmd.Parameters.AddWithValue("@userOneID", userOneID);
            cmd.Parameters.AddWithValue("@userTwoID", userTwoID);

            try
            {
                con.Open();
                var result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0; // Grup varsa GroupID, yoksa 0 döndür
            }
            catch (Exception ex)
            {
                MessageBox.Show("Grup kontrolü sırasında hata oluştu: " + ex.Message);
                return 0;
            }
            finally
            {
                con.Close();
            }
        }

        // Yeni grup oluşturma
        private int CreateGroup(int userOneID, int userTwoID)
        {
            SqlConnection con = new SqlConnection(constring);
            string q = "INSERT INTO [dbo].[Groups] (UserOneID, UserTwoID, GroupName) VALUES (@userOneID, @userTwoID, @groupName)";
            SqlCommand cmd = new SqlCommand(q, con);

            cmd.Parameters.AddWithValue("@userOneID", userOneID);
            cmd.Parameters.AddWithValue("@userTwoID", userTwoID);
            cmd.Parameters.AddWithValue("@groupName", userOneID + "_" + userTwoID);  // Grup adı, kullanıcı ID'lerinin birleşiminden oluşturuluyor

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                return GetGroupId(userOneID, userTwoID);  // Grup oluşturulduktan sonra ID'sini döndürüyoruz
            }
            catch (Exception ex)
            {
                MessageBox.Show("Grup oluşturulurken hata oluştu: " + ex.Message);
                return 0;
            }
            finally
            {
                con.Close();
            }
        }




        private void MarkMessagesAsRead()
        {
            timer4.Start();
            using (SqlConnection con = new SqlConnection(constring))
            {
                string query = "UPDATE Chat SET status = 1 WHERE usertwo = @currentUser AND userone = @chatUser AND status = 0";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@currentUser", guna2TextBox1.Text); // Mevcut kullanıcı
                cmd.Parameters.AddWithValue("@chatUser", label12.Text); // Sohbet edilen kullanıcı

                try
                {
                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        Console.WriteLine($"{rowsAffected} mesaj okundu olarak işaretlendi.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Mesajları güncellerken bir hata oluştu: " + ex.Message);
                }
            }
        }


        private bool CheckForUnreadMessages()
        {
            timer4.Start();
            using (SqlConnection con = new SqlConnection(constring))
            {
                string query = "SELECT COUNT(*) FROM Chat WHERE usertwo = @currentUser AND status = 0";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@currentUser", guna2TextBox1.Text);

                try
                {
                    con.Open();
                    int unreadCount = (int)cmd.ExecuteScalar();
                    return unreadCount > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bildirim kontrolü sırasında bir hata oluştu: " + ex.Message);
                    return false;
                }
            }
        }


        private void AddMessageToChat(string userOne, string userTwo, string message, byte[] imageBytes)
        {
            // Kullanıcı adlarına göre uygun UserControl oluştur
            UserControl userControl;

            // Kullanıcı 1 ve Kullanıcı 2'yi karşılaştır ve uygun UserControl'ü seç
            if (userOne == guna2TextBox1.Text && userTwo == label12.Text)
            {
                userControl = new UserControl2(); // İlk kullanıcı için UserControl2
            }
            else if (userOne == label12.Text && userTwo == guna2TextBox1.Text)
            {
                userControl = new UserControl3(); // İkinci kullanıcı için UserControl3
            }
            else
            {
                return; // Uygun olmayan kullanıcılar için işlem yapma
            }

            // UserControl'ün Dock özelliğini ayarla
            userControl.Dock = DockStyle.Top;

            // Mesajı UserControl'e ekle
            if (userControl is UserControl2)
            {
                UserControl2 userControl2 = (UserControl2)userControl;
                userControl2.Title = message; // Başlık mesajını ayarla

                // Resim varsa, ekle
                if (imageBytes != null)
                {
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        // Resmi Image özelliğine atayın (Resmi gönderen kişinin profil resmi için)
                        userControl2.Image = Image.FromStream(ms);
                    }
                }

                // Resmi ve başlığı doğru şekilde göster
                userControl2.DisplayImage();  // PictureBox'a resmi atamak için
            }
            else if (userControl is UserControl3)
            {
                UserControl3 userControl3 = (UserControl3)userControl;
                userControl3.Title = message; // Başlık mesajını ayarla

                // Resim varsa, ekle
                if (imageBytes != null)
                {
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        // Resmi MessageImage özelliğine atayın (Alıcıdaki gelen mesaj resmi)
                        userControl3.MessageImage = Image.FromStream(ms);
                    }
                }

                // Profil resmi ekleme (Alıcının profil resmini ekle)
                userControl3.Icon = guna2CirclePictureBox4.Image; // Profil resmi alıcıdan alınıyor
            }

            // FlowLayoutPanel'e ekle
            flowLayoutPanel2.Controls.Add(userControl);
            flowLayoutPanel2.ScrollControlIntoView(userControl);
        }


        private void pictureBox6_Click(object sender, EventArgs e)
        {
            // OpenFileDialog'u başlat
            openFileDialog2.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            openFileDialog2.Title = "Bir Resim Seçin";

            // Resim seçildiğinde yolu al
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                selectedImagePath = openFileDialog2.FileName;
            }
        }

        private void AddImageToChat(string userOne, string userTwo, string imagePath)
        {
            if (userOne == guna2TextBox1.Text && userTwo == label12.Text)
            {
                // Kullanıcı 1 için resim gösterimi
                UserControl2 userControl2 = new UserControl2();
                userControl2.Dock = DockStyle.Top;

                PictureBox pictureBox = new PictureBox();
                pictureBox.Image = Image.FromFile(imagePath);
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.Width = 200; // Resim genişliği
                pictureBox.Height = 200; // Resim yüksekliği
                userControl2.Controls.Add(pictureBox);

                flowLayoutPanel2.Controls.Add(userControl2);
                flowLayoutPanel2.ScrollControlIntoView(userControl2);
            }
            else if (userOne == label12.Text && userTwo == guna2TextBox1.Text)
            {
                // Kullanıcı 2 için resim gösterimi
                UserControl3 userControl3 = new UserControl3();
                userControl3.Dock = DockStyle.Top;

                PictureBox pictureBox = new PictureBox();
                pictureBox.Image = Image.FromFile(imagePath);
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.Width = 200;
                pictureBox.Height = 200;
                userControl3.Controls.Add(pictureBox);

                flowLayoutPanel2.Controls.Add(userControl3);
                flowLayoutPanel2.ScrollControlIntoView(userControl3);
            }
        }

        private void userControl11_Load(object sender, EventArgs e)
        {
            if (panel10.Visible == false && panel11.Visible == false && flowLayoutPanel2.Visible == false)
            {
                panel10.Visible = true;
                panel11.Visible = true;
                flowLayoutPanel2.Visible = true;
            }
            UserControl1 control = (UserControl1)sender;
            label12.Text = control.Title;
            guna2CirclePictureBox4.Image = control.Icon;
            MessageChat();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (panel10.Visible == true && panel11.Visible == true && flowLayoutPanel2.Visible == true)
            {
                panel10.Visible = false;
                panel11.Visible = false;
                flowLayoutPanel2.Visible = false;
            }
        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        int lastMessageId = 0; // Global bir değişken olarak tanımlayın



		// Global değişken
		bool dbWarningShown = false;

		private void timer3_Tick(object sender, EventArgs e)
		{
			// Eğer uyarı zaten gösterildiyse timer artık işlemesin
			if (dbWarningShown) return;

			try
			{
				using (SqlConnection con = new SqlConnection(constring))
				{
					con.Open();

					SqlCommand cmd = new SqlCommand(
						"SELECT MAX(id) FROM Chat WHERE (userone = @userone AND usertwo = @usertwo) OR (userone = @usertwo AND usertwo = @userone)",
						con
					);

					cmd.Parameters.AddWithValue("@userone", guna2TextBox1.Text);
					cmd.Parameters.AddWithValue("@usertwo", label12.Text);

					var result = cmd.ExecuteScalar();
					int latestId = result != DBNull.Value ? Convert.ToInt32(result) : 0;

					if (latestId > lastMessageId)
					{
						lastMessageId = latestId;
						MessageChat();
					}
				}
			}
			catch
			{
				MessageBox.Show("Database bağlı değil. Durdurmak için ESC basın. Lütfen bağlantıyı kontrol edin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				dbWarningShown = true; // Artık uyarı gösterilmesin
			}
		}




		//HOME BUTONU AÇIK PANELLERİ KAPATIYOZ
		private void guna2Button1_Click(object sender, EventArgs e)
        {
            // Örnek: Panel isimlerine göre açık olanları kapatma
            if (panel4.Visible)
                panel4.Visible = false;

            if (panel5.Visible)
                panel5.Visible = false;

            if (panel6.Visible)
                panel6.Visible = false;

            if (panel8.Visible)
                panel8.Visible = false;

            if (panel9.Visible)
                panel9.Visible = false;

            if (panel10.Visible)
                panel10.Visible = false;

            if (panel11.Visible)
                panel11.Visible = false;
            // Daha fazla panel varsa ekleyebilirsiniz
        }

        private void guna2TextBox11_TextChanged(object sender, EventArgs e)
        {
            MessageChat();

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            // Butona her tıklandığında aynı işlevi gerçekleştirecek şekilde
            if (isPanelOpen)
            {
                guna2Button10.Image = Resources.down; // Kapanma görseli
                isPanelOpen = false;  // Panelin kapalı olduğunu işaretle
            }
            else
            {
                guna2Button10.Image = Resources.up;  // Açılma görseli
                isPanelOpen = true;  // Panelin açık olduğunu işaretle
            }

            // Panel animasyonunu başlat
            timer2.Start();
        }

        private void guna2TextBox11_TextChanged_1(object sender, EventArgs e)
        {
            timer3.Start();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            // Aktif kullanıcının UserId'sini MessageBox ile göster
            MessageBox.Show("Aktif kullanıcının ID'si: " + ActiveUser.UserId,
                            "Kullanıcı ID", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //USERCONTROL1 YENİLERİZ BU TİMER İLE
        private void timer4_Tick(object sender, EventArgs e)
        {
            // Mevcut kaydırma pozisyonunu sakla
            int currentScrollPosition = flowLayoutPanel1.VerticalScroll.Value;
            int maxScrollPosition = flowLayoutPanel1.VerticalScroll.Maximum;
            int minScrollPosition = flowLayoutPanel1.VerticalScroll.Minimum;

            // İçeriği güncellemek için Layout işlemlerini durduruyoruz
            flowLayoutPanel1.SuspendLayout();

            // Eski içerikleri geçici olarak saklıyoruz, ancak kaybolmadan önce yeni veriyi ekliyoruz
            var oldControls = new List<Control>(flowLayoutPanel1.Controls.Cast<Control>());

            // Yeni öğeleri ekleyin, bu işlemi eski öğeleri silmeden önce yapıyoruz
            UserItem();  // Yeni öğeleri ekliyoruz

            // Kaydırma pozisyonunu sabitlemek için, içerik yenilendikten sonra kaydırma çubuğu aynı pozisyonda kalmalı
            if (currentScrollPosition == maxScrollPosition)
            {
                flowLayoutPanel1.VerticalScroll.Value = maxScrollPosition;
            }
            else if (currentScrollPosition == minScrollPosition)
            {
                flowLayoutPanel1.VerticalScroll.Value = minScrollPosition;
            }
            else
            {
                flowLayoutPanel1.VerticalScroll.Value = currentScrollPosition;
            }

            // Eski öğeleri kaldırıyoruz, bu işlemi yeni öğeler eklendikten sonra yapıyoruz
            foreach (var control in oldControls)
            {
                flowLayoutPanel1.Controls.Remove(control);  // Eski öğeleri kaldırıyoruz
            }

            // İçeriğin yüklenmesi bittiğinde, Layout'u yeniden başlatıyoruz
            flowLayoutPanel1.ResumeLayout();

            // Yenileme işlemi tamamlandıktan sonra, timer'ı durdurabiliriz
            timer4.Stop();
        }
        //USERCONTROL1 YENİLERİZ BU TİMER İLE


        //Profil fotoğrafına basıldığında profil ekranı açılır.
        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            if (panel5.Visible == false)
            {
                panel5.Visible = true;
            }
            else
            {
                panel5.Visible = false;
            }
            panel5.BringToFront(); // Paneli diğer tüm kontrollerin üstüne taşır


            if (panel6.Visible)
                panel6.Visible = false;

            if (panel8.Visible)
                panel8.Visible = false;

            if (panel9.Visible)
                panel9.Visible = false;

            if (panel10.Visible)
                panel10.Visible = false;

            if (panel11.Visible)
                panel11.Visible = false;
            if (flowLayoutPanel2.Visible)
                flowLayoutPanel2.Visible = false;
        }
        //Profil fotoğrafına basıldığında profil ekranı açılır.

    }
}