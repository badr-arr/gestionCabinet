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
    class Antecedent
    {
        string id;
        string idDossier;
        string g;
        string p;
        string typeAccouchement;
        string cause;
        string medicaux;
        string chirurgicaux;
        string obstetricaux;
        string descriptionAntecedent;

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
        public string G
        {
            get
            {
                return g ;
            }
            set
            {
                 g = value;
            }
        }
        public string P
        {
            get
            {
                return p ;
            }
            set
            {
                 p = value;
            }
}
        public string TypeAccouchement
        {
            get
            {
                return typeAccouchement ;
            }
            set
            {
                 typeAccouchement = value;
            }
}
        public string Cause
        {
            get 
            {
                return cause ;
            }
            set
            {
                  cause= value;
            }
}
        public string Chirurgicaux
        {
            get
            {
                return chirurgicaux ;
            }
            set
            {
                  chirurgicaux = value;
            }
}
        public string Medicaux
        {
            get
            {
                return medicaux;
            }
            set
            {
                medicaux = value;
            }
        }
        public string Obstetricaux
        {
            get
            {
                return obstetricaux;
            }
            set
            {
                obstetricaux = value;
            }
        }
        public string DescriptionAntecedent
        {
            get
            {
                return descriptionAntecedent ;
            }
            set
            {
                  descriptionAntecedent = value;
            }
}

        public static void persistAntecedent(Antecedent p)
        {
            DateTime da = DateTime.Today;
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            SqlDataReader dataReader;
            string tab = "antecedent(id_dossier,g,p,type_accouchement,cause,description,medicaux,chirurgicaux,obstetricaux)";
            string v = "values(" + Convert.ToInt32(p.idDossier) + "," + Convert.ToInt32(p.G) + "," + Convert.ToInt32(p.P) + ",'" + p.typeAccouchement + "','" + p.Cause + "','" + p.DescriptionAntecedent + "','"+p.Medicaux+"','"+p.Chirurgicaux+"','"+p.Obstetricaux+"')";
            string q = "insert into " + tab + " " + v;
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                dataReader.Close();
                command.Dispose();
                q = "update dossier_patient set date_modification = '" + da.Date + "' where id = "+ Convert.ToInt32(p.IdDossier);
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                dataReader.Close();
                command.Dispose();
                MessageBox.Show("Ajout d'antécédent avec succès");
                c.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void ModifyAntecedent(Antecedent p)
        {
            DateTime da = DateTime.Today;
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            SqlDataReader dataReader;
            string q = "update antecedent set g=" + Convert.ToInt32(p.G) + ",description='" + p.DescriptionAntecedent + "',p=" + Convert.ToInt32(p.P) + ",type_accouchement='" + p.TypeAccouchement + "',cause='" + p.Cause + "',medicaux='" +p.Medicaux + "',chirurgicaux='"+p.Chirurgicaux+"',obstetricaux='"+p.Obstetricaux+"' where id=" + Convert.ToInt32(p.Id);
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                dataReader.Close();
                command.Dispose();
                q = "update dossier_patient set date_modification='" + da.Date + "' where id=" + Convert.ToInt32(p.IdDossier);
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                dataReader.Close();
                command.Dispose();
                c.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static DataTable AfficherAntecedentDuPatient(string id)
        {
            DataTable dt = new DataTable();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "select * from antecedent where id_dossier =" + Convert.ToInt32(id);
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public static Antecedent getAntecedent(string id)
        {
            Antecedent a = new Antecedent();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            SqlDataReader dataReader;
            string q = "Select * from antecedent where id=" + Convert.ToInt32(id);
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    if (dataReader.HasRows)
                    {
                        a.G = dataReader["g"].ToString();
                        a.P = dataReader["p"].ToString();
                        a.TypeAccouchement = dataReader["type_accouchement"].ToString();
                        a.Cause = dataReader["cause"].ToString();
                        a.Medicaux = dataReader["medicaux"].ToString();
                        a.Chirurgicaux = dataReader["chirurgicaux"].ToString();
                        a.Obstetricaux = dataReader["obstetricaux"].ToString();
                        a.DescriptionAntecedent = dataReader["description"].ToString();
                    }
                }
                dataReader.Close();
                command.Dispose();
                c.Close();
            }
            catch (Exception)
            {

            }
            return a;
        }

        public static void deleteAntecedent(string id)
        {
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            SqlDataReader dataReader;
            string q = "delete from antecedent where id=" + Convert.ToInt32(id);
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                dataReader.Close();
                command.Dispose();
                c.Close();
                MessageBox.Show("Antécédent supprimé");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static DataTable chercherAntecedent(string id, string critere, string mot)
        {
            DataTable dt = new DataTable();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "select * from antecedent where " + critere + " like '%" + mot + "%' and id_dossier =" + Convert.ToInt32(id);
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

    }
}
