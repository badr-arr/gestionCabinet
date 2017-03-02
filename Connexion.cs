using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Projet
{
    class Connexion
    {
        public string user;
        public static SqlConnection connect() //fonction de connexion
        {
            string s = null;
            s = "Server= (local)\\SQLEXPRESS;Database=BDProjet; User ID=sa;Password=Ensa2016";
            return new SqlConnection(s);
        }

        public bool verifierLogin(string user, string password)// verification de login
        {


            SqlConnection c = connect();
            SqlCommand command;
            SqlDataReader dataReader;
            string q = "select * from login where email='" + user + "' and password= '" + password + "'; ";
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    dataReader.Close();
                    command.Dispose();
                    c.Close();
                    this.user = user;
                    return true;
                }
                dataReader.Close();
                command.Dispose();
                c.Close();
                return false;

            }
            catch (Exception)
            {
                MessageBox.Show("Erreur de connexion");
                return false;
            }


        }

        public string RetourneEmailCompte(string email)
        {
            SqlConnection c = connect();
            SqlCommand command;
            SqlDataReader dataReader;
            string q = "select * from login where email='" + email + "' ; ";
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    if (!dataReader["email"].ToString().Equals(null))
                    {
                        string r = dataReader["email"].ToString();
                        dataReader.Close();
                        command.Dispose();
                        c.Close();
                        return r;
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

        public bool modifierComptePassword(string u ,string pass)
        {
            SqlConnection c = connect();
            SqlCommand command;
            SqlDataReader dataReader;   
            string q = "update login set password='"+pass+"',email='"+u+"' where email='"+this.user+"' ;";
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                dataReader.Close();
                command.Dispose();
                c.Close();
                this.user = u;                
                return true;
            }
            catch (Exception ex)
            {
            }
            return false;
        }

        public static bool EmailExists(string email)
        {
            SqlConnection c = connect();
            SqlCommand command;
            SqlDataReader dataReader;
            string q = "select * from login where email='" + email + "' ; ";
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    if (dataReader.HasRows)
                    {
                        dataReader.Close();
                        command.Dispose();
                        c.Close();
                        return true;
                    }
                }
                dataReader.Close();
                command.Dispose();
                c.Close();
            }
            catch (Exception)
            {
            }
            return false;
        }

        public static string RetourneMotDePasse(string email)
        {
            SqlConnection c = connect();
            SqlCommand command;
            SqlDataReader dataReader;
            string q = "select * from login where email='" + email + "' ; ";
            try
            {
                c.Open();
                command = new SqlCommand(q, c);
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    if (!dataReader["email"].ToString().Equals(null))
                    {
                        string s = dataReader["password"].ToString();
                        dataReader.Close();
                        command.Dispose();
                        c.Close();
                        return s;
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

    