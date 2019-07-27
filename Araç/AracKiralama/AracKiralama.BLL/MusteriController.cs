using AracKiralama.DAL;
using AracKiralama.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracKiralama.BAL
{
    public class MusteriController
    {
        MusteriManagement mm = new MusteriManagement();
        public List<Musteri> GetMusteriler()
        {
            List<Musteri> musteriler = new List<Musteri>();
            musteriler = mm.GetirMusteriler();
            return musteriler;

        }
        public Musteri GetMusteriEmaileGore(string email)
        {
            Musteri musteri = new Musteri();
            musteri = mm.GetirEmaileGoreMusteri(email);
            return musteri;
        }
        public List<Musteri> GetMusterilerEmail()
        {
            List<Musteri> musteriler = new List<Musteri>();
            musteriler = mm.GetirMusterilerEmail();
            return musteriler;
        }

        public bool InsertMusteri(Musteri musteri)
        {
            bool sonuc = mm.EkleMusteri(musteri);

            //List<Musteri> mList = new List<Musteri>();
            //mList = GetMusteriler();

            //foreach (Musteri item in mList)
            //{
            //    if (musteri.Email==item.Email)
            //    {
            //        sonuc = false;
            //        return sonuc;
            //    }
            //}
            //if (sonuc==true)
            //{
            //    sonuc = mm.EkleMusteri(musteri);
            //}
           
            
            return sonuc;
        }

        public bool UpdateMusteri(Musteri musteri)
        {
            bool cevap;
            cevap = mm.GuncelleMusteri(musteri);
            return cevap;
        }

       
    }
}
