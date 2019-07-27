using AracKiralama.BAL;
using AracKiralama.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AracKiralama.UI
{
    public partial class frmKiralamaEkrani : Form
    {
        public frmKiralamaEkrani()
        {
            InitializeComponent();
        }
         public KiralamaDetay kiralanacakBilgi = new KiralamaDetay();
        
        int girisYapanId;
        public decimal FiyatHesapla(ArabaSinif sinif)
        {
            decimal fiyat;


            int gun = Convert.ToInt32((dtpTeslimedilecek.Value - dtpAlinacak.Value).TotalDays);

            if (gun > 7)
            {
                if (gun > 30)
                {
                    int bolum = gun / 30;
                    fiyat = 30 * bolum * (sinif.Fiyat * 0.6m) + (gun - 30 * bolum) * sinif.Fiyat;
                }
                else
                {
                    int bolum = gun / 7;
                    fiyat = 7 * bolum * (sinif.Fiyat * 0.7m) + (gun - 7 * bolum) * sinif.Fiyat;
                }
            }
            else
            {
                fiyat = gun * sinif.Fiyat;
            }
            return fiyat;
            //if (gun>=30 )
            //{
            //    fiyat = (30 *( 0.6m * sinif.Fiyat)) + ((gun - 30) * sinif.Fiyat);
            //}
        }

        ArabaController ac;
        List<ArabaSinif> arabaSiniflari = new List<ArabaSinif>();
        MusteriController mc;
        bool girisYapildimi = true;


        List<Resim> resimler = new List<Resim>();
        private void frmAnaSayfa_Load(object sender, EventArgs e)
        {
            ac = new ArabaController();


            resimler = ac.GetResimler();
            pcBoxArabalar.Image = Image.FromFile(Application.StartupPath + @"/../../images/main1.jpg");


            arabaSiniflari = ac.GetArabaSinif();
            foreach (ArabaSinif item in arabaSiniflari)
            {
                cmbSinif.Items.Add(item.Sinif);

            }
            cmbSinif.DataSource = arabaSiniflari;
        }
        List<Araba> arabalar;
        List<Araba> seciliKategoridekiArabalar;
        private void cmbSinif_SelectedIndexChanged(object sender, EventArgs e)
        {

            cmbArac.Items.Clear();
            cmbArac.Text = "Seçiniz";
            ac = new ArabaController();
            arabalar = new List<Araba>();
            arabalar = ac.GetArabalar();
            int sinifId = 0;
            decimal sinifFiyat = 0;

            foreach (ArabaSinif item in arabaSiniflari)
            {
                if (item.Sinif == cmbSinif.Text)
                {
                    sinifId = item.Id;
                    sinifFiyat = item.Fiyat;
                }
            }
            seciliKategoridekiArabalar = new List<Araba>();
            foreach (Araba araba in arabalar)
            {
                if (araba.SinifId == sinifId)
                {

                    cmbArac.Items.Add(araba.Marka);
                    seciliKategoridekiArabalar.Add(araba);

                }
            }
            lblFiyat.Text = "Fiyat : " + sinifFiyat.ToString("###.##") + " TL";
            pcBoxArabalar.Image = Image.FromFile(Application.StartupPath + @"/../../images/main1.jpg");
        }



        private void btnKaydol_Click(object sender, EventArgs e)
        {
            frmKaydol frm = new frmKaydol();
            frm.ShowDialog();

        }
        List<Resim> seciliArabaResimleri;
        int seciliArabaId;
        private void cmbArac_SelectedIndexChanged(object sender, EventArgs e)
        {
            seciliArabaResimleri = new List<Resim>();
            foreach (Araba item in seciliKategoridekiArabalar)
            {
                if (item.Marka == cmbArac.Text)
                {
                    lblModel.Text = "Model :" + item.Model.ToString();
                    lblModelYili.Text = "Yıl : " + item.ModelYili.ToString();
                    lblKm.Text = "Km : " + item.Km.ToString();
                    seciliArabaId = item.Id;
                }




            }
            resimNumarasi = 0;
            seciliArabaResimleri = ac.GetResimlerIdyeGore(seciliArabaId);
            pcBoxArabalar.Image = Image.FromFile(Application.StartupPath + @"/../../" + seciliArabaResimleri[resimNumarasi].ResimYolu.ToString());




        }

        private void dtpAlinacak_ValueChanged(object sender, EventArgs e)
        {
            ArabaSinif snf = new ArabaSinif();
            foreach (ArabaSinif item in arabaSiniflari)
            {
                if (item.Sinif == cmbSinif.SelectedItem.ToString())
                {
                    snf = item;
                }
            }
            lblToplamFiyat.Text = FiyatHesapla(snf).ToString();
        }

        private void dtpTeslimedilecek_ValueChanged(object sender, EventArgs e)
        {
            ArabaSinif snf = new ArabaSinif();
            foreach (ArabaSinif item in arabaSiniflari)
            {
                if (item.Sinif == cmbSinif.SelectedItem.ToString())
                {
                    snf = item;
                }
            }
            lblToplamFiyat.Text = FiyatHesapla(snf).ToString();
        }

        private void btnKirala_Click(object sender, EventArgs e)
        {
            if (girisYapildimi == false)
            {
                MessageBox.Show("Giriş yapmadan araç kiralayamazsınız.");
            }
            else
            {

                List<Musteri> musteriler = new List<Musteri>();
                musteriler = mc.GetMusteriler();
                foreach (Musteri item in musteriler)
                {
                    if (item.Id == girisYapanId)
                    {
                        if (string.IsNullOrWhiteSpace(item.TCKno) || string.IsNullOrWhiteSpace(item.Ad) || string.IsNullOrWhiteSpace(item.Soyad) || string.IsNullOrWhiteSpace(item.Sehir) || item.DogumTarihi == null)
                        //|| string.IsNullOrWhiteSpace(item.KimlikFotokopi)
                        {
                            MessageBox.Show("Güncelleme ekranı açılacak.");
                            frmBilgiGuncelleme frm = new frmBilgiGuncelleme();
                            frm.gelenId = girisYapanId;
                            frm.ShowDialog();

                        }
                        else
                        {


                        }
                    }

                }

            }
        }
        Resim secilenResim = new Resim();
        int resimNumarasi = 0;
        private void btnDegistir_Click(object sender, EventArgs e)
        {
            if (cmbArac.Text != "Seçiniz")
            {
                if (resimNumarasi < 4)
                {
                    resimNumarasi++;
                    secilenResim = seciliArabaResimleri[resimNumarasi];
                    pcBoxArabalar.Image = Image.FromFile(Application.StartupPath + @"/../../" + secilenResim.ResimYolu.ToString());

                }
                else
                {
                    resimNumarasi = 0;
                    secilenResim = seciliArabaResimleri[resimNumarasi];
                    pcBoxArabalar.Image = Image.FromFile(Application.StartupPath + @"/../../" + secilenResim.ResimYolu.ToString());
                    resimNumarasi++;

                }
            }


        }
    }
}
