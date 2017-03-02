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
    class RDVS
    {

        string id;
        string idPatient;
        string nomPatient;
        string prenomPatient;
        DateTime date;
        string heure;
        string duree;
        string heureFin;
        string typeRDV;
        string description;
        string statut;

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
        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
        }
        public string IdPatient
        {
            get
            {
                return idPatient;
            }
            set
            {
                idPatient = value;
            }
        }
        public string NomPatient
        {
            get
            {
                return nomPatient;
            }
            set
            {
                nomPatient = value;
            }
        }
        public string PrenomPatient
        {
            get
            {
                return prenomPatient;
            }
            set
            {
                prenomPatient = value;
            }
        }
        public string Heure
        {
            get
            {
                return heure;
            }
            set
            {
                heure = value;
            }
        }
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }
        public string Statut
        {
            get
            {
                return statut;
            }
            set
            {
                statut = value;
            }
        }
        public string Duree
        {
            get
            {
                return duree;
            }
            set
            {
                duree = value;
            }
        }
        public string TypeRDV
        {
            get
            {
                return typeRDV;
            }
            set
            {
                typeRDV = value;
            }
        }
        public string HeureFin
        {
            get
            {
                return heureFin;
            }
            set
            {
               heureFin = value;
            }
        }

        public static void persistRDV(RDVS p)
        {
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            SqlDataReader dataReader;
            DateTime t = DateTime.Today.Date;
            if (p.Date.Date < t)
            {
                MessageBox.Show("Impossible de réserver dans le :" + p.Date.ToString());
                throw new Exception();
            }
            string tab = "rdv(id_patient,date,heure,duree,heure_fin,type_rdv,description,nom_patient,prenom_patient,statut)";
            string v = "values(" + Convert.ToInt32(p.IdPatient) + ",'" + p.Date.Date + "','" + p.Heure + "','" + p.Duree + "','" + p.HeureFin + "','" + p.TypeRDV + "','" + p.Description + "','" + p.NomPatient + "','" + p.PrenomPatient + "','"+p.Statut+"')";
            string q = "insert into " + tab + " " + v;
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                dataReader.Close();
                command.Dispose();
                MessageBox.Show("RDV ajouté avec succès");
                c.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static DataTable chercherRDVToday()
        {
            DateTime d = DateTime.Today;
            DataTable dt = new DataTable();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "Select * from rdv where date= '" + d.Date + "'";
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

        public static RDVS getRDV(string id)
        {
            RDVS p = new RDVS();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            SqlDataReader dataReader;
            string q = "Select * from rdv where id=" + Convert.ToInt32(id) + "";
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    if (dataReader.HasRows)
                    {
                        p.Id = dataReader["id"].ToString();
                        p.IdPatient = dataReader["id_patient"].ToString();
                        p.NomPatient = dataReader["nom_patient"].ToString();
                        p.PrenomPatient = dataReader["prenom_patient"].ToString();
                        p.Date = Convert.ToDateTime(dataReader["date"].ToString());
                        p.Heure = dataReader["heure"].ToString();
                        p.Description = dataReader["description"].ToString();
                        p.Statut = dataReader["statut"].ToString();
                        p.Duree = dataReader["duree"].ToString();
                        p.HeureFin = dataReader["heure_fin"].ToString();
                        p.TypeRDV = dataReader["type_rdv"].ToString();
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

        public static void ModifyRDV(RDVS p)
        {
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            SqlDataReader dataReader;
            DateTime d = p.Date;
            string q;
            q = "update rdv set id_patient="+Convert.ToInt32(p.IdPatient)+", nom_patient='" + p.NomPatient + "',prenom_patient='" + p.PrenomPatient + "',date='" + d.Date + "',heure='" + p.Heure + "',description='" + p.Description + "',statut='" + p.Statut + "',duree='" + p.Duree + "',heure_fin='" + p.HeureFin + "',type_rdv='"+p.TypeRDV+"' where id=" + Convert.ToInt32(p.Id);
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                dataReader.Close();
                command.Dispose();
                c.Close();
                MessageBox.Show("Modification avec succès");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void deleteRDV(string p)
        {
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            SqlDataReader dataReader;
            string q = "delete from rdv where id=" + Convert.ToInt32(p);
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                dataReader.Close();
                command.Dispose();
                c.Close();
                MessageBox.Show("Rendez-vous supprimé");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static bool RDVIsFree(RDVS d)
        {
            RDVS p = new RDVS();
            int t1, t2, t1comp, t2comp;
            string[] dd = d.Heure.Split(':');
            t1 = (Convert.ToInt32(dd[0]) * 60) + Convert.ToInt32(dd[1]);
            string[] hh = d.HeureFin.Split(':');
            t2 = (Convert.ToInt32(hh[0]) * 60) + Convert.ToInt32(hh[1]);
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            SqlDataReader dataReader;
            string q = "Select * from rdv where date='" + d.Date.Date + "' and statut='En attente'";
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    if (dataReader.HasRows)
                    {
                        p.Heure = dataReader["heure"].ToString();
                        p.HeureFin = dataReader["heure_fin"].ToString();
                        string[] dd1 = p.Heure.Split(':');
                        t1comp = (Convert.ToInt32(dd1[0]) * 60) + Convert.ToInt32(dd1[1]);
                        string[] hh1 = p.HeureFin.Split(':');
                        t2comp = (Convert.ToInt32(hh1[0]) * 60) + Convert.ToInt32(hh1[1]);
                        if ((t1 >= t1comp && t1 <= t2comp) || (t2 >= t1comp && t2 <= t2comp) || (t1 <= t1comp && t2 >= t2comp))
                        {
                            MessageBox.Show("Rendez-vous déjà pris !! Veuillez choisir une autre heure");
                            return false;
                        }
                        else if (t2 >= 1080)
                        {
                            MessageBox.Show("Rendez-vous dépasse le temps réglementaire !! Veuillez choisir une autre heure");
                            return false;
                        }
                    }
                }
                dataReader.Close();
                command.Dispose();
                c.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return true;
        }

        public static bool RDVIsFree(RDVS d, string idd)
        {
            RDVS p = new RDVS();
            int t1, t2, t1comp, t2comp;
            string[] dd = d.Heure.Split(':');
            t1 = (Convert.ToInt32(dd[0]) * 60) + Convert.ToInt32(dd[1]);
            string[] hh = d.HeureFin.Split(':');
            t2 = (Convert.ToInt32(hh[0]) * 60) + Convert.ToInt32(hh[1]);
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            SqlDataReader dataReader;
            string q = "Select * from rdv where date='" + d.Date.Date + "' and statut='En attente' and id != " + Convert.ToInt32(idd);
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    if (dataReader.HasRows)
                    {
                        p.Heure = dataReader["heure"].ToString();
                        p.HeureFin = dataReader["heure_fin"].ToString();
                        string[] dd1 = p.Heure.Split(':');
                        t1comp = (Convert.ToInt32(dd1[0]) * 60) + Convert.ToInt32(dd1[1]);
                        string[] hh1 = p.HeureFin.Split(':');
                        t2comp = (Convert.ToInt32(hh1[0]) * 60) + Convert.ToInt32(hh1[1]);
                        if ((t1 >= t1comp && t1 <= t2comp) || (t2 >= t1comp && t2 <= t2comp) || (t1 <= t1comp && t2 >= t2comp))
                        {
                            MessageBox.Show("Rendez-vous déjà pris !! Veuillez choisir une autre heure");
                            return false;
                        }
                        else if (t2 >= 930)
                        {
                            MessageBox.Show("Rendez-vous dépasse le temps réglementaire !! Veuillez choisir une autre heure");
                            return false;
                        }
                    }
                }
                dataReader.Close();
                command.Dispose();
                c.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return true;
        }

        public static Int32 NombreTotalPatient()
        {
            Int32 nbre = 0;
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "select count(*) from rdv where date='" + DateTime.Today.Date + "'";
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                nbre = (Int32)command.ExecuteScalar();
                command.Dispose();
                c.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return nbre;
        }

        public static Int32 NombreTotalPatientStatut(string statut)
        {
            Int32 nbre = 0;
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "select count(*) from rdv where date='" + DateTime.Today.Date + "' and statut='"+statut+"'";
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                nbre = (Int32)command.ExecuteScalar();
                command.Dispose();
                c.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return nbre;
        }

        public static Int32 NombreTotalPatientType(string type)
        {
            Int32 nbre = 0;
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "select count(*) from rdv where date='" + DateTime.Today.Date + "' and type_rdv like '%" + type + "%'";
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                nbre = (Int32)command.ExecuteScalar();
                command.Dispose();
                c.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return nbre;
        }

        public static DataTable chercherRDV(string critere, string mot)
        {
            DataTable dt = new DataTable();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "Select * from RDV where " + critere + " like '%" + mot + "%'";
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

        public static DataTable chercherRDVAvecDate(string critere, string mot, DateTime t)
        {
            DataTable dt = new DataTable();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "Select * from RDV where " + critere + " like '%" + mot + "%' and date='" + t.Date + "'";
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

        public static DataTable chercherRDV(string id, string critere, string mot)
        {
            DataTable dt = new DataTable();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "Select * from RDV where " + critere + " like '%" + mot + "%' and id_patient =" + Convert.ToInt32(id);
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

        public static DataTable chercherRDVAvecDate(string id, string critere, string mot, DateTime t)
        {
            DataTable dt = new DataTable();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "Select * from RDV where " + critere + " like '%" + mot + "%' and date='" + t.Date + "'  and id_patient =" + Convert.ToInt32(id);
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

        public static DataTable AfficherRDVDuPatient(string idPatient)
        {
            DataTable dt = new DataTable();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "Select * from RDV where id_patient =" + Convert.ToInt32(idPatient);
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

        public static DataTable AfficherRDVAujourdhui()
        {
            DataTable dt = new DataTable();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "Select * from RDV where date ='"+DateTime.Today.Date+"'";
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

        public static DataTable AfficherRDVAujourdhuiAvecStatut(string statut)
        {
            DataTable dt = new DataTable();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "Select * from rdv where date='" + DateTime.Today.Date + "' and statut='" + statut + "'";
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

        public static DataTable AfficherRDVAujourdhuiAvecType(string type)
        {
            DataTable dt = new DataTable();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "select * from rdv where date='" + DateTime.Today.Date + "' and type_rdv like '%" + type + "%'";
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
