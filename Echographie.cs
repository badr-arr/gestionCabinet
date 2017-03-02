using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Projet
{
    class Echographie
    {
        string id;
        string idDossier;
        string descriptionEchographie;
        DateTime dateEchographie;

        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public string IdDossier
        {
            get
            {
                return idDossier;
            }
            set
            {
                idDossier = value;
            }
        }
        public DateTime DateEchographie
        {
            get
            {
                return dateEchographie;
            }
            set
            {
                dateEchographie = value;
            }
        }
        public string DescriptionEchographie
        {
            get
            {
                return descriptionEchographie;
            }
            set
            {
                descriptionEchographie = value;
            }
        }
        
        public static void persistEchographie(Echographie r)
        {

            SqlConnection c = Connexion.connect();
            SqlCommand command;
            DateTime d = DateTime.Today;
            string tab = "echographie(id_dossier,date_echo,Description)";
            string v = "values(" + Convert.ToInt32(r.IdDossier) + ",'" + r.DateEchographie.Date + "','"+r.DescriptionEchographie+"')";
            string q = "insert into " + tab + " " + v;
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                command.ExecuteNonQuery();
                command.Dispose();
                MessageBox.Show("Ajout d'échographie avec succès");
                c.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void ModifyEchographie(Echographie p)
        {
            DateTime da = DateTime.Today;
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "update echographie set date_echo='" + p.DateEchographie.Date + "',Description='" + p.DescriptionEchographie + "' where id=" + Convert.ToInt32(p.Id);
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                command.ExecuteNonQuery();
                command.Dispose();
                q = "update dossier_patient set date_modification='" + da.Date + "' where id=" + Convert.ToInt32(p.IdDossier);
                command = new SqlCommand(q, c);
                command.ExecuteNonQuery();
                command.Dispose();
                c.Close();
                MessageBox.Show("Modification avec succès");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void deleteEchographie(string id)
        {
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            SqlDataReader dataReader;
            string q = "delete from echographie where id=" + Convert.ToInt32(id);
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                dataReader.Close();
                command.Dispose();
                c.Close();
                MessageBox.Show("Echographie supprimée");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static Echographie getEcho(string id)
        {
            Echographie p = new Echographie();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            SqlDataReader dataReader;
            string q = "Select * from echographie where id=" + Convert.ToInt32(id) + "";
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    if (dataReader.HasRows)
                    {
                        p.DateEchographie = (DateTime)dataReader["date_echo"];
                        p.DescriptionEchographie = dataReader["Description"].ToString();
                    }
                }
                dataReader.Close();
                command.Dispose();
                c.Close();
            }
            catch (Exception)
            {

            }
            return p;
        }

        public static DataTable AfficherEchographieDuPatient(string id)
        {
            DataTable dt = new DataTable();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "Select * from echographie where id_dossier =" + Convert.ToInt32(id);
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

        public static DataTable chercherEchographie(string id, string critere, string mot)
        {
            DataTable dt = new DataTable();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "Select * from echographie where " + critere + " like '%" + mot + "%' and id_dossier =" + Convert.ToInt32(id);
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

        public static DataTable chercherEchographieAvecDate(string id, DateTime t)
        {
            DataTable dt = new DataTable();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "Select * from echographie where date_echo='" + t.Date + "'  and id_dossier =" + Convert.ToInt32(id);
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

        
    }
}
