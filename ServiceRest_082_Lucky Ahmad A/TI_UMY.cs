using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.SqlClient;

namespace ServiceRest_082_Lucky_Ahmad_A
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class TI_UMY : ITI_UMY
    {
        SqlConnection sqlConnection;
        string connectionString = "Data Source=DESKTOP-OLFSSOV;Initial catalog=TI UMY;Persist Security Info=True;User ID=sa;Password=123";

        public string CreateMahasiswa(Mahasiswa mhs)
        {
            string msg = "Gagal";

            sqlConnection = new SqlConnection(connectionString);
            string query = string.Format("Insert into dbo.Mahasiswa values ('{0}', '{1}', '{2}', '{3}')", mhs.nim, mhs.nama, mhs.prodi, mhs.angkatan);
            SqlCommand cmd = new SqlCommand(query, sqlConnection);

            try
            {
                sqlConnection.Open();
                Console.WriteLine(query);
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
                msg = "Sukses";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(query);
            }
            return msg;
        }

        public List<Mahasiswa> GetAllMahasiswa()
        {
            List<Mahasiswa> mahas = new List<Mahasiswa>();
            sqlConnection = new SqlConnection(connectionString);
            string query = "Select NIM, Nama, Prodi, Angkatan from dbo.Mahasiswa";
            SqlCommand cmd = new SqlCommand(query, sqlConnection);

            try
            {
                sqlConnection.Open();
                //mendapatkan data yang telah diexecute
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Mahasiswa mhs = new Mahasiswa();
                    mhs.nim = reader.GetString(0);
                    mhs.nama = reader.GetString(1);
                    mhs.prodi = reader.GetString(2);
                    mhs.angkatan = reader.GetString(3);

                    mahas.Add(mhs);
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(query);
            }
            return mahas;
        }

        public Mahasiswa GetMahasiswaByNIM(string nim)
        {
            Mahasiswa mhs = new Mahasiswa();
            sqlConnection = new SqlConnection(connectionString);
            string query = String.Format("Select NIM, Nama, Prodi, Angkatan from dbo.Mahasiswa where NIM = '{0}'", nim);
            SqlCommand com = new SqlCommand(query, sqlConnection);

            try
            {
                sqlConnection.Open();
                SqlDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    mhs.nim = reader.GetString(0);
                    mhs.nama = reader.GetString(1);
                    mhs.prodi = reader.GetString(2);
                    mhs.angkatan = reader.GetString(3);
                }
                com.Clone();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(query);
            }
            return mhs;
        }

    }
}
