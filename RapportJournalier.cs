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
    class RapportJournalier
    {
        string jour;
        public string Jour
        {
            get
            {
                return jour;
            }
            set
            {
                jour = value;
            }
        }
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
        string t1NC;
        public string T1NC
        {
            get
            {
                return t1NC;
            }
            set
            {
                t1NC = value;
            }
        }
        string t1AC;
        public string T1AC
        {
            get
            {
                return t1AC;
            }
            set
            {
                t1AC = value;
            }
        }
        string t2NC;
        public string T2NC
        {
            get
            {
                return t2NC;
            }
            set
            {
                t2NC = value;
            }
        }
        string t2AC;
        public string T2AC
        {
            get
            {
                return t2AC;
            }
            set
            {
                t2AC = value;
            }
        }
        string t3NC;
        public string T3NC
        {
            get
            {
                return t3NC;
            }
            set
            {
                t3NC = value;
            }
        }
        string t3AC;
        public string T3AC
        {
            get
            {
                return t3AC;
            }
            set
            {
                t3AC = value;
            }
        }
        string pecNC;
        public string PECNC
        {
            get
            {
                return pecNC;
            }
            set
            {
                pecNC = value;
            }
        }
        string pecAC;
        public string PECAC
        {
            get
            {
                return pecAC;
            }
            set
            {
                pecAC = value;
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

        public static Int32 CPNInscription(string cpn, DateTime avant, string inscription)
        {
            Int32 nbre = 0;
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "select count(*) from cpn where type='" + cpn + "' and date_cpn='" + avant.Date + "' and inscription='" + inscription + "'";
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

        public static Int32 CpnGar(string gar, DateTime avant)
        {
            Int32 nbre = 0;
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "select count(*) from cpn where gestionGAR='" + gar + "' and date_cpn='" + avant.Date + "' ";
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

        public static Int32 CpnGarInscription(string gar, DateTime avant,string inscription)
        {
            Int32 nbre = 0;
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "select count(*) from cpn where gestionGAR='" + gar + "' and date_cpn='" + avant.Date + "' and inscription='"+inscription+"' ";
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
            string q = "select * from RapportJournalier where mois='" + mois + "' and date_creation = '" + d.Date + "'";
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

        public static void persistRapport(RapportJournalier r)
        {

            SqlConnection c = Connexion.connect();
            SqlCommand command;
            DateTime d = DateTime.Today;
            string tab = "rapportJournalier(t1_nc,t1_ac,t2_nc,t2_ac,t3_nc,t3_ac,pec_nc,pec_ac,referee,mois,date_creation,jour)";
            string v = "values(" + Convert.ToInt32(r.T1NC) + "," + Convert.ToInt32(r.T1AC) + "," + Convert.ToInt32(r.T2NC) + "," + Convert.ToInt32(r.T2AC) + "," + Convert.ToInt32(r.T3NC) + "," + Convert.ToInt32(r.T3AC) + "," + Convert.ToInt32(r.PECNC) + "," + Convert.ToInt32(r.PECAC) + "," + Convert.ToInt32(r.Referee) + ",'" + r.Mois + "','"+r.DateCreation.Date  + "','"+r.Jour+"')";
            string q = "insert into " + tab + " " + v;
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                command.ExecuteNonQuery();
                command.Dispose();
                MessageBox.Show("Rapport Journalier crée");
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
            string q = "delete from rapportJournalier where id=" + Convert.ToInt32(id);
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

        public static DataTable chercherRapportJournalier(string critere, string mot)
        {
            DataTable dt = new DataTable();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "Select * from RapportJournalier where " + critere + " like '%" + mot + "%'";
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

        public static DataTable chercherRapportJournalierAvecDate(string critere, string mot, DateTime t)
        {
            DataTable dt = new DataTable();
            SqlConnection c = Connexion.connect();
            SqlCommand command;
            string q = "Select * from rapportJournalier where " + critere + " like '%" + mot + "%' and date_creation='" + t.Date + "'";
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
                RapportJournalier r = new RapportJournalier();
                System.IO.Directory.CreateDirectory("C:/Users/HP/Desktop/RapportJournalier");
                DateTime firstDay = DateTime.Today;                

                r.DateCreation = DateTime.Today;
                r.Mois = DateTime.Now.AddMonths(0).ToString("MMMM", CultureInfo.CreateSpecificCulture("fr"));
                var culture = new System.Globalization.CultureInfo("fr-FR");
                var day = culture.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek);
                r.Jour = day.ToString().ToUpper();

                r.T1NC = RapportJournalier.CPNInscription("CPN1", firstDay, "NI").ToString();
                r.T1AC = RapportJournalier.CPNInscription("CPN1", firstDay, "AI").ToString();
                r.T2NC = RapportJournalier.CPNInscription("CPN2", firstDay, "NI").ToString();
                r.T2AC = RapportJournalier.CPNInscription("CPN2", firstDay, "AI").ToString();
                r.T3NC = RapportJournalier.CPNInscription("CPN3", firstDay, "NI").ToString();
                r.T3AC = RapportJournalier.CPNInscription("CPN3", firstDay, "AI").ToString();

                r.PECNC = RapportJournalier.CpnGarInscription("PEC", firstDay,"NI").ToString();
                r.PECAC = RapportJournalier.CpnGarInscription("PEC", firstDay,"AI").ToString();
                r.Referee = RapportJournalier.CpnGar("Référée", firstDay).ToString();

                if (!RapportJournalier.Exist(r.Mois, r.DateCreation))
                {
                    Document doc = new Document(PageSize.A4);
                    PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("C:/Users/HP/Desktop/RapportJournalier/" + r.DateCreation.Day.ToString() + "-" + r.DateCreation.Month.ToString() + "-" + r.DateCreation.Year.ToString() + ".pdf", FileMode.Create));
                    doc.Open();
                    Paragraph titre = new Paragraph("\nFICHE JOURNALIERE\n\n\n");
                    titre.Alignment = Element.ALIGN_CENTER;
                    Paragraph creation = new Paragraph("Rapport du mois de : " + r.Mois.ToUpper() + "\nDate de création :" + r.DateCreation.Date.ToString());
                    creation.Alignment = Element.ALIGN_RIGHT;
                    doc.Add(creation);
                    doc.Add(titre);

                    PdfPTable table = new PdfPTable(5);
                    PdfPCell cell = new PdfPCell(new Phrase("ACTIVITES"));
                    cell.Colspan = 2;
                    cell.Rowspan = 2;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase("DATE"));
                    cell.Rowspan = 2;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase(DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString()));
                    cell.Colspan = 2;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);
                    table.AddCell("NC");
                    table.AddCell("AC");

                    cell = new PdfPCell(new Phrase("PRENATALES"));
                    cell.Rowspan = 3;
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase("TRIM I"));
                    cell.Colspan = 2;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);
                    table.AddCell(r.T1NC);
                    table.AddCell(r.T1AC);
                    cell = new PdfPCell(new Phrase("TRIM II"));
                    cell.Colspan = 2;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);
                    table.AddCell(r.T2NC);
                    table.AddCell(r.T2AC);
                    cell = new PdfPCell(new Phrase("TRIM III"));
                    cell.Colspan = 2;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_CENTER;
                    table.AddCell(cell);
                    table.AddCell(r.T3NC);
                    table.AddCell(r.T3AC);

                    cell = new PdfPCell(new Phrase("Grossesse à risques dépistés"));
                    cell.Colspan = 3;
                    table.AddCell(cell);
                    table.AddCell(r.PECNC);
                    table.AddCell(r.PECAC);

                    cell = new PdfPCell(new Phrase("Grossesse à risques référés"));
                    cell.Colspan = 3;
                    table.AddCell(cell);
                    cell = new PdfPCell(new Phrase(r.Referee));
                    cell.Colspan = 2;
                    table.AddCell(cell);

                    doc.Add(table);
                    doc.Close();
                    RapportJournalier.persistRapport(r);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
