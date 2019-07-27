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
    public partial class frmKaydol : Form
    {
        public frmKaydol()
        {
            InitializeComponent();
        }
        MusteriController mc = new MusteriController();
        private void btnKaydol_Click(object sender, EventArgs e)
        {
            Musteri eklenecekMusteri = new Musteri();
            eklenecekMusteri.Email = txtEmail.Text;
            eklenecekMusteri.Sifre = txtSifre.Text;
            eklenecekMusteri.Ad = txtAd.Text;
            eklenecekMusteri.Soyad = txtSoyad.Text;
            eklenecekMusteri.Sehir = txtSehir.Text;
            eklenecekMusteri.TCKno = txtTc.Text;
            eklenecekMusteri.DogumTarihi = dtpDogum.Value;

            bool cevap;
            cevap = mc.InsertMusteri(eklenecekMusteri);
            if (cevap)
            {
                MessageBox.Show("Kayıt Başarılı");
                this.Close();
            }
            else
            {
                MessageBox.Show("Girdiginiz E-maille kayıtlı kullannıcı bulunmaktadır.");
            }
        }
    }
}
