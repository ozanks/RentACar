using AracKiralama.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracKiralama.DAL
{
    public class MusteriManagement
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader rdr;
        public MusteriManagement()
        {
            conn = new SqlConnection("server=.;database=arackiralamadb;integrated security=true;");

        }
        public List<Musteri> GetirMusteriler()
        {
            List<Musteri> musteriler = new List<Musteri>();
            cmd = new SqlCommand("select id,tcnumarasi,ad,soyad,dogumtarihi,sehir,email,sifre,kimlikfotokopi from Musteriler", conn);

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();

                }
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    Musteri musteri;
                    while (rdr.Read())
                    {
                        musteri = new Musteri();
                        musteri.Id = Convert.ToInt32(rdr["id"]);
                        musteri.TCKno = rdr["tcnumarasi"].ToString();
                        musteri.Ad = rdr["ad"].ToString();
                        musteri.Soyad = rdr["soyad"].ToString();
                        musteri.DogumTarihi = Convert.ToDateTime(rdr["dogumtarihi"]);
                        musteri.Sehir = rdr["sehir"].ToString();
                        musteri.Email = rdr["email"].ToString();
                        musteri.Sifre = rdr["sifre"].ToString();
                        musteri.KimlikFotokopi = rdr["kimlikfotokopi"].ToString();

                        musteriler.Add(musteri);
                    }

                }

            }
            catch (Exception)
            {


            }
            finally
            {
                rdr.Close();
                conn.Close();
            }

            return musteriler;
        }
        public Musteri GetirEmaileGoreMusteri(string email)
        {
            Musteri musteri = new Musteri();
            cmd = new SqlCommand("select sifre from Musteriler email=@email", conn);
            cmd.Parameters.AddWithValue("@email",email);
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();

                }
               musteri.Sifre=cmd.ExecuteScalar().ToString();

            }
            catch (Exception)
            {


            }
            finally
            {
                
                conn.Close();
            }

            return musteri;
        }
        public List<Musteri> GetirMusterilerEmail()
        {
            List<Musteri> musteriler = new List<Musteri>();
            cmd = new SqlCommand("select id,tcnumarasi,ad,soyad,dogumtarihi,sehir,email,sifre,kimlikfotokopi from Musteriler", conn);

            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();

                }
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    Musteri musteri;
                    while (rdr.Read())
                    {
                        musteri = new Musteri();
                        musteri.Id = Convert.ToInt32(rdr["id"]);
                        if (rdr["tcnumarasi"] != null)
                        {
                            musteri.TCKno = rdr["tcnumarasi"].ToString();
                        }
                        if (rdr["ad"] != null)
                        {
                            musteri.Ad = rdr["ad"].ToString();
                        }
                        if (rdr["soyad"] != null)
                        {
                            musteri.Soyad = rdr["soyad"].ToString();
                        }
                        if (rdr["dogumtarihi"] != null)
                        {
                            musteri.DogumTarihi = Convert.ToDateTime(rdr["dogumtarihi"]);
                        }
                        if (rdr["dogumtarihi"] != null)
                        {
                            musteri.DogumTarihi = Convert.ToDateTime(rdr["dogumtarihi"]);
                        }
                        if (rdr["sehir"] != null)
                        {

                            musteri.Sehir = rdr["sehir"].ToString();
                        }
                        musteri.Email = rdr["email"].ToString();
                        musteri.Sifre = rdr["sifre"].ToString();
                        if (rdr["kimlikfotokopi"] != null)
                        {

                            musteri.KimlikFotokopi = rdr["kimlikfotokopi"].ToString();
                        }

                        musteriler.Add(musteri);
                    }

                }

            }
            catch (Exception)
            {


            }
            finally
            {
                rdr.Close();
                conn.Close();
            }

            return musteriler;
        }
        public bool EkleMusteri(Musteri eklenecekMusteri)
        {
            bool cevap = false;
            cmd = new SqlCommand("insert into Musteriler (email,Sifre,tcnumarasi,ad,soyad,dogumtarihi,sehir)values(@email,@sifre,@tckn,@ad,@soyad,@dogumtarihi,@sehir)", conn);
            cmd.Parameters.AddWithValue("@email",eklenecekMusteri.Email);
            cmd.Parameters.AddWithValue("@sifre", eklenecekMusteri.Sifre);
            cmd.Parameters.AddWithValue("@tckn",eklenecekMusteri.TCKno);
            cmd.Parameters.AddWithValue("@ad",eklenecekMusteri.Ad);
            cmd.Parameters.AddWithValue("@soyad",eklenecekMusteri.Soyad);
            cmd.Parameters.AddWithValue("@sehir",eklenecekMusteri.Sehir);
            cmd.Parameters.AddWithValue("@dogumtarihi",eklenecekMusteri.DogumTarihi);
            
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();

                }
                int eklendimi;
                eklendimi=cmd.ExecuteNonQuery();
                if (eklendimi > 0)
                {
                    cevap = true;
                }

            }
            catch (Exception)
            {

                
            }
            finally
            {
                
                conn.Close();
            }
            return cevap;
        }

        public bool GuncelleMusteri(Musteri guncellenecekMusteri)
        {
            bool cevap = false;
            cmd = new SqlCommand("update Musteriler set TCNumarasi=@TcNo,Ad=@ad,Soyad=@soyad,DogumTarihi=@dogumTarihi,Sehir=@sehir,Sifre=@sifre where id=@musteriid",conn);
            //,KimlikFotokopi=@kimlikFoto
            cmd.Parameters.AddWithValue("@TcNo",guncellenecekMusteri.TCKno);
            cmd.Parameters.AddWithValue("@ad",guncellenecekMusteri.Ad);
            cmd.Parameters.AddWithValue("@soyad",guncellenecekMusteri.Soyad);
            cmd.Parameters.AddWithValue("@dogumTarihi", guncellenecekMusteri.DogumTarihi);
            cmd.Parameters.AddWithValue("@sehir",guncellenecekMusteri.Sehir);
            cmd.Parameters.AddWithValue("@sifre",guncellenecekMusteri.Sifre);
            cmd.Parameters.AddWithValue("@musteriid",guncellenecekMusteri.Id);
            //cmd.Parameters.AddWithValue("@kimlikFoto",guncellenecekMusteri.KimlikFotokopi);
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();

                }
                int eklendimi;
                eklendimi = cmd.ExecuteNonQuery();
                if (eklendimi > 0)
                {
                    cevap = true;
                }

            }
            catch (Exception)
            {


            }
            finally
            {

                conn.Close();
            }
            return cevap;
        }
    }

    


}
