using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Projet
{
    class RapportCPoN
    {
        string mois;
        public string Mois
        {
            get
            {
                return mois;
            }
            set
            {
                mois = value;
            }
        }
        DateTime dateCreation;
        public DateTime DateCreation
        {
            get
            {
                return dateCreation;
            }
            set
            {
                dateCreation = value;
            }
        }
        string precoce;
        public string Precoce
        {
            get
            {
                return precoce;
            }
            set
            {
                precoce = value;
            }
        }
        string tardive;
        public string Tardive
        {
            get
            {
                return tardive;
            }
            set
            {
                tardive = value;
            }
        }
        string autre;
        public string Autre
        {
            get
            {
                return autre;
            }
            set
            {
                autre = value;
            }
        }
        string complication;
        public string Complication
        {
            get
            {
                return complication;
            }
            set
            {
                complication = value;
            }
        }
        string hemorragie;
        public string Hemorragie
        {
            get
            {
                return hemorragie;
            }
            set
            {
                hemorragie = value;
            }
        }
        string infection;
        public string Infection
        {
            get
            {
                return infection;
            }
            set
            {
                infection = value;
            }
        }
        string eclampsie;
        public string Eclampsie
        {
            get
            {
                return eclampsie;
            }
            set
            {
                eclampsie = value;
            }
        }
        string phlebite;
        public string Phlebite
        {
            get
            {
                return phlebite;
            }
            set
            {
                phlebite = value;
            }
        }
        string mammaire;
        public string Mammaire
        {
            get
            {
                return mammaire;
            }
            set
            {
                mammaire = value;
            }
        }
        string anemie;
        public string Anemie
        {
            get
            {
                return anemie;
            }
            set
            {
                anemie = value;
            }
        }
        string autreComplication;
        public string AutreComplication
        {
            get
            {
                return autreComplication;
            }
            set
            {
                autreComplication = value;
            }
        }
        string pec;
        public string PEC
        {
            get
            {
                return pec;
            }
            set
            {
                pec= value;
            }
        }
        string referee;
        public string Referee
        {
            get
            {
                return referee;
            }
            set
            {
                referee = value;
            }
        }
        string fer;
        public string Fer
        {
            get
            {
                return fer;
            }
            set
            {
                fer = value;
            }
        }


        public static Int32 NombreCPoN(string cpn, DateTime avant, DateTime apres)
        {
            Int32 nbre = 0;
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "select count(*) from cpon where type_consultation='" + cpn + "' and date_consultation between '" + avant.Date + "' and '" + apres.Date + "'";
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

        public static Int32 NombreCPoNComplication(string risque, DateTime avant, DateTime apres)
        {
            Int32 nbre = 0;
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "select count(*) from cpon where " + risque + "='Oui' and date_consultation between '" + avant.Date + "' and '" + apres.Date + "'";
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

        public static Int32 NombreCponGar(string gar, DateTime avant, DateTime apres)
        {
            Int32 nbre = 0;
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "select count(*) from cpon where gestion_complication='" + gar + "' and date_consultation between '" + avant.Date + "' and '" + apres.Date + "'";
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

        public static Int32 NombreCPoNFer(DateTime avant, DateTime apres)
        {
            Int32 nbre = 0;
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "select count(*) from cpon where fer='Oui' and date_consultation between '" + avant.Date + "' and '" + apres.Date + "'";
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

        public static bool Exist(string mois, DateTime d)
        {
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            SqlDataReader dataReader;
            Int32 compteur = 0;
            string q = "select * from rapportCPoN where mois='" + mois + "' and date_creation = '" + d.Date + "'";
            try
            {

                c.Open();
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    if (dataReader.HasRows)
                    {
                        return true;
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
            return false;
        }

        public static void persistRapport(RapportCPoN r)
        {

            SqlConnection c = Connexion.connect();
            SqlCommand command;
            DateTime d = DateTime.Today;
            string tab = "rapportCPoN(Mois,date_creation,precoce,tardive,autre_type,hemorragie,infection,eclampsie,phlebite,mammaire,anemie,autre_complication,pec,referee,fer)";
            string v = "values('" + r.Mois + "','" + r.DateCreation.Date + "'," + Convert.ToInt32(r.Precoce) + "," + Convert.ToInt32(r.Tardive) + "," + Convert.ToInt32(r.Autre) + "," + Convert.ToInt32(r.Hemorragie) + "," + Convert.ToInt32(r.Infection) + "," + Convert.ToInt32(r.Eclampsie) + "," + Convert.ToInt32(r.Phlebite) + "," + Convert.ToInt32(r.Mammaire) + "," + Convert.ToInt32(r.Anemie) + "," + Convert.ToInt32(r.AutreComplication) + "," + Convert.ToInt32(r.PEC) + "," + Convert.ToInt32(r.Referee) + "," + Convert.ToInt32(r.Fer) + ")";
            string q = "insert into " + tab + " " + v;
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                command.ExecuteNonQuery();
                command.Dispose();
                c.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void deleteRapport(string id)
        {

            SqlConnection c = Connexion.connect();
            SqlCommand command;
            DateTime d = DateTime.Today;
            string q = "delete from rapportCPoN where id=" + Convert.ToInt32(id);
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                command.ExecuteNonQuery();
                command.Dispose();
                MessageBox.Show("Rapport supprimé");
                c.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static DataTable chercherRapportCPoN(string critere, string mot)
        {
            DataTable dt = new DataTable();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "Select * from rapportCPoN where " + critere + " like '%" + mot + "%'";
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

        public static DataTable chercherRapportCPoNAvecDate(string critere, string mot, DateTime t)
        {
            DataTable dt = new DataTable();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "Select * from rapportCPoN where " + critere + " like '%" + mot + "%' and date_creation='" + t.Date + "'";
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

        public static void CreerRapport()
        {
            try
            {
                RapportCPoN r = new RapportCPoN();
                System.IO.Directory.CreateDirectory("C:/Users/HP/Desktop/RapportCPoN");
                var yr = DateTime.Today.Year;
                var mth = DateTime.Today.Month;
                DateTime firstDay = new DateTime(yr, mth, 1).AddMonths(-1);
                DateTime lastDay = new DateTime(yr, mth, 1).AddDays(-1);
                r.DateCreation = DateTime.Today;
                r.Mois = DateTime.Now.AddMonths(-1).ToString("MMMM", CultureInfo.CreateSpecificCulture("fr"));

                r.Precoce = RapportCPoN.NombreCPoN("Précoce",firstDay,lastDay).ToString();
                r.Tardive = RapportCPoN.NombreCPoN("Tardive", firstDay, lastDay).ToString();
                r.Autre = RapportCPoN.NombreCPoN("Autre", firstDay, lastDay).ToString();

                r.Hemorragie = RapportCPoN.NombreCPoNComplication("hemorragie",firstDay,lastDay).ToString();
                r.Infection = RapportCPoN.NombreCPoNComplication("infection", firstDay, lastDay).ToString();
                r.Eclampsie = RapportCPoN.NombreCPoNComplication("eclampsie", firstDay, lastDay).ToString();
                r.Phlebite = RapportCPoN.NombreCPoNComplication("phlebite", firstDay, lastDay).ToString();
                r.Mammaire = RapportCPoN.NombreCPoNComplication("mammaire", firstDay, lastDay).ToString();
                r.Anemie = RapportCPoN.NombreCPoNComplication("anemie", firstDay, lastDay).ToString();
                r.AutreComplication = RapportCPoN.NombreCPoNComplication("autre", firstDay, lastDay).ToString();

                r.PEC = RapportCPoN.NombreCponGar("PEC",firstDay,lastDay).ToString();
                r.Referee = RapportCPoN.NombreCponGar("Référée", firstDay, lastDay).ToString();

                r.Fer = RapportCPoN.NombreCPoNFer(firstDay, lastDay).ToString();

                if (!RapportCPoN.Exist(r.Mois, r.DateCreation))
                {
                    Document doc = new Document(PageSize.A4);
                    PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("C:/Users/HP/Desktop/RapportCPoN/" + r.DateCreation.Day.ToString() + "-" + r.DateCreation.Month.ToString() + "-" + r.DateCreation.Year.ToString() + ".pdf", FileMode.Create));
                    doc.Open();
                    Paragraph titre = new Paragraph("\nCONSULTATION POSTNATALE\n\n");
                    titre.Alignment = Element.ALIGN_CENTER;
                    Paragraph creation = new Paragraph("Rapport du mois de : " + r.Mois.ToUpper() + "\nDate de création" + r.DateCreation.Date.ToString());
                    creation.Alignment = Element.ALIGN_RIGHT;
                    Paragraph mois = new Paragraph("Date début :" + firstDay.Date + "\nDate fin :" + lastDay.Date);
                    mois.Alignment = Element.ALIGN_LEFT;
                    doc.Add(creation);
                    doc.Add(mois);
                    doc.Add(titre);
                    PdfPTable table = new PdfPTable(4);
                    PdfPCell cell = new PdfPCell(new Phrase("Performances\n"));
                    cell.Colspan = 3;
                    table.AddCell(cell);
                    table.AddCell("Urbain");

                    cell = new PdfPCell(new Phrase("Nombre de femmes\nexaminées en consultation\ndu post partum"));
                    cell.Rowspan = 3;
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase("Précoce"));
                    cell.Colspan = 2;
                    table.AddCell(cell);
                    table.AddCell(r.Precoce);
                    cell = new PdfPCell(new Phrase("Tardif"));
                    cell.Colspan = 2;
                    table.AddCell(cell);
                    table.AddCell(r.Tardive);
                    cell = new PdfPCell(new Phrase("Autres"));
                    cell.Colspan = 2;
                    table.AddCell(cell);
                    table.AddCell(r.Autre);

                    cell = new PdfPCell(new Phrase("Nombre de cas compliqués\n"));
                    cell.Rowspan = 9;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase("Complications\n"));
                    cell.Rowspan = 7;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_CENTER;
                    cell.Rotation = 90;
                    table.AddCell(cell);
                    table.AddCell("Hémorragie");
                    table.AddCell(r.Hemorragie);
                    table.AddCell("Infection puerpérale");
                    table.AddCell(r.Infection);
                    table.AddCell("Eclampsie");
                    table.AddCell(r.Eclampsie);
                    table.AddCell("Phlébite");
                    table.AddCell(r.Phlebite);
                    table.AddCell("Complications mammaires");
                    table.AddCell(r.Mammaire);
                    table.AddCell("Anémie");
                    table.AddCell(r.Anemie);
                    table.AddCell("Autres\n");
                    table.AddCell(r.AutreComplication);
                    cell = new PdfPCell(new Phrase("Nombre de cas compliqués PEC"));
                    cell.Colspan = 2;
                    table.AddCell(cell);
                    table.AddCell(r.PEC);
                    cell = new PdfPCell(new Phrase("Nombre de cas compliqués Référés\n"));
                    cell.Colspan = 2;
                    table.AddCell(cell);
                    table.AddCell(r.Referee);

                    cell = new PdfPCell(new Phrase("Nombre de femmes ayant reçu le Fer"));
                    cell.Colspan = 3;
                    table.AddCell(cell);
                    table.AddCell(r.Fer);
                    
                    doc.Add(table);
                    doc.Close();
                    RapportCPoN.persistRapport(r);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
