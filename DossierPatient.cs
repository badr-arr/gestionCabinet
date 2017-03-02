using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet
{
    class DossierPatient
    {
        public static DataTable chercherDossier(string critere, string mot)
        {
            DataTable dt = new DataTable();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "Select * from dossier_patient where " + critere + " like '%" + mot + "%'";
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dt);
                }
                return dt;
            }
            catch (Exception)
            {

            }
            return null;
        }

        public static string getIdDossier(string id_patient)
        {
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            SqlDataReader dataReader;
            string q = "Select * from dossier_patient where id_patient=" + Convert.ToInt32(id_patient) + "";
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    if (dataReader.HasRows)
                    {
                        return (dataReader["id"].ToString());
                    }
                }
                dataReader.Close();
                command.Dispose();
                c.Close();
            }
            catch (Exception)
            {

            }
            return null;
        }

    }
}
