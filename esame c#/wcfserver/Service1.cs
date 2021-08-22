using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace wcfserver
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di classe "Service1" nel codice e nel file di configurazione contemporaneamente.
    public class Service1 : IService1
    {
        string connessione = "Data Source=127.0.0.1:3306; Initial Catalog=esame; User Id=dmo; Password=password";

       
        public string DeletUser(int id)
        {
            try
            {
                SqlConnection delete = new SqlConnection(connessione);
                delete.Open();

                using (SqlCommand command1 = delete.CreateCommand())
                {
                    command1.CommandText = "delete from Utente where Id = '"+id+"';";
                    var resultSet = command1.ExecuteNonQuery();

                    delete.Close();

                    if (resultSet == 1)
                    {
                        return "true";  //eliminazione eseguita
                    }
                    else
                    {
                        return "false"; //id dell'utente non in tabella percio non presente
                    }
                }


            }
            catch (Exception ex)
            {

                throw new System.ServiceModel.FaultException(ex.Message);
            }
        }

        public string InsertTransazione(Transazione transazione)
        {
            try
            {
                SqlConnection leggiSal = new SqlConnection(connessione);
                leggiSal.Open();

                using (SqlCommand command2 = leggiSal.CreateCommand())
                {
                    command2.CommandText = "select * from Utente where Id ='" + transazione.Id_Utente + "';";
                    var resultSet = command2.ExecuteReader();

                    string v = resultSet.Read().ToString();

                    double saldo_attuale = resultSet.GetDouble(resultSet.GetOrdinal("Saldo"));
                    //double saldo_attuale = (double)resultSet.GetDouble(5);
                    leggiSal.Close();

                    if (saldo_attuale + transazione.Valore < 0)
                    {
                        return "Saldo insufficiente";
                    }
                    else
                    {
                        try
                        {


                            SqlConnection insertTransact = new SqlConnection(connessione);
                            insertTransact.Open();
                            SqlCommand inserttr = new SqlCommand(
                                "INSERT INTO Transazione(Id_Utente, Data, Importo) VALUES(@Id_Utente, @Data, @Importo)", insertTransact);
                            inserttr.Parameters.AddWithValue("@Id_Utente", transazione.Id_Utente);
                            inserttr.Parameters.AddWithValue("@Data", transazione.Data);
                            inserttr.Parameters.AddWithValue("@Importo", transazione.Valore);

                            inserttr.ExecuteNonQuery();
                            insertTransact.Close();

                            try
                            {
                                SqlConnection modificaSal = new SqlConnection(connessione);
                                modificaSal.Open();

                                SqlCommand mod = new SqlCommand(
                                    "UPDATE Utente SET Saldo = @Saldo WHERE Id = @Id", modificaSal);

                                mod.Parameters.AddWithValue("@Saldo", transazione.Valore + saldo_attuale);
                                mod.Parameters.AddWithValue("@Id", transazione.Id_Utente);
                                mod.ExecuteNonQuery();
                                modificaSal.Close();

                                return "saldo aggiornato";
                            }
                            catch (Exception ex)
                            {

                                throw new System.ServiceModel.FaultException(ex.Message);
                            }

                            
                        }
                        catch (Exception ex)
                        {

                            throw new System.ServiceModel.FaultException(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw new System.ServiceModel.FaultException(ex.Message);
            }
            
        }

        public int LoginUtente(string nome, string cognome, string password)
        {
            try
            {
                SqlConnection login = new SqlConnection(connessione);
                login.Open();

                using (SqlCommand command1 = login.CreateCommand())
                {
                    command1.CommandText = "select * from Utente where Nome ='" + nome + "' and Cognome = '" + cognome + "' and Password = '" + password + "';";
                    var resultSet = command1.ExecuteReader();

                    string v = resultSet.Read().ToString();

                    if (v == "True")
                    {
                        int pass = Convert.ToInt32(resultSet["Id"]);
                        return pass;
                    }

                    return 0;
                }
                

            }
            catch (Exception ex)
            {

                throw new System.ServiceModel.FaultException(ex.Message);
            }

        }

        public double GetSaldo(int id)
        {
            try
            {
                SqlConnection leggiSal = new SqlConnection(connessione);
                leggiSal.Open();

                using (SqlCommand command2 = leggiSal.CreateCommand())
                {
                    command2.CommandText = "select * from Utente where Id ='" + id + "';";
                    var resultSet = command2.ExecuteReader();

                    string v = resultSet.Read().ToString();
                    double saldo_attuale = resultSet.GetDouble(resultSet.GetOrdinal("Saldo"));
                    
                    leggiSal.Close();

                    return saldo_attuale;
                }


            }
            catch (Exception ex)
            {

                throw new System.ServiceModel.FaultException(ex.Message);
            }
        }

        public Utente GetUtente(int id)
        {
            try
            {
                SqlConnection GetUserById = new SqlConnection(connessione);
                GetUserById.Open();

                using (SqlCommand command1 = GetUserById.CreateCommand())
                {
                    command1.CommandText = "SELECT * FROM Utente WHERE Id = '" + id + "';";
                    var resultSet = command1.ExecuteReader();

                    Utente utente = new Utente();
                    if (resultSet.Read())
                    {
                        utente.Id = id;
                        utente.Nome = resultSet["Nome"].ToString();
                        utente.Cognome = resultSet["Cognome"].ToString();
                        utente.Password = resultSet["Password"].ToString();
                        utente.Saldo = (float)Convert.ToDouble(resultSet["Saldo"]);
                        utente.IsAdmin = Convert.ToInt32(resultSet["IsAdmin"]);
                        GetUserById.Close();
                        return utente;
                    }
                    GetUserById.Close();
                    utente.Id = 0;
                    return utente;
                    
                    
                    




                }


            }
            catch (Exception ex)
            {

                throw new System.ServiceModel.FaultException(ex.Message);
            }
        }

        public string ModificaSaldo(int id, float saldo)
        {
            try
            {
                SqlConnection modificaSal = new SqlConnection(connessione);
                modificaSal.Open();

                SqlCommand mod = new SqlCommand(
                    "UPDATE Utente SET Saldo = @Saldo WHERE Id = @Id", modificaSal);

                mod.Parameters.AddWithValue("@Saldo", saldo);
                mod.Parameters.AddWithValue("@Id", id);
                mod.ExecuteNonQuery();
                modificaSal.Close();

                return "saldo aggiornato";
            }
            catch (Exception ex)
            {

                throw new System.ServiceModel.FaultException(ex.Message);
            }
        }

        public string registertUser(Utente utente)
        {
            //throw new NotImplementedException();

          
            try
            {
                SqlConnection register = new SqlConnection(connessione);
                register.Open();
                SqlCommand crea_utente = new SqlCommand(
                    "INSERT INTO utente(Nome, Cognome, Password, Saldo, IsAdmin) VALUES(@Nome, @Cognome, @Password, @Saldo, @IsAdmin)", register);
                crea_utente.Parameters.AddWithValue("@Nome", utente.Nome);
                crea_utente.Parameters.AddWithValue("@Cognome", utente.Cognome);
                crea_utente.Parameters.AddWithValue("@Password", utente.Password);
                crea_utente.Parameters.AddWithValue("@Saldo", utente.Saldo);
                crea_utente.Parameters.AddWithValue("@IsAdmin", utente.IsAdmin);
                crea_utente.ExecuteNonQuery();
                register.Close();
                return "work";
            }
            catch (Exception ex)
            {

                throw new System.ServiceModel.FaultException(ex.Message);
            }

        }

        public List<Transazione> TransazioniByIdUtente(int Id)
        {
            try
            {
                SqlConnection GetTransazioniById = new SqlConnection(connessione);
                GetTransazioniById.Open();

                using (SqlCommand command1 = GetTransazioniById.CreateCommand())
                {
                    command1.CommandText = "SELECT * FROM Transazione WHERE Id_Utente = '" + Id + "';";
                    var resultSet = command1.ExecuteReader();

                    List<Transazione> Transazioni = new List<Transazione>();
                    while (resultSet.Read())
                    {
                        Transazioni.Add(new Transazione
                        {
                            Id = Convert.ToInt32(resultSet["Id"]),
                            Id_Utente = Convert.ToInt32(resultSet["Id_Utente"]),
                            Data = resultSet["Data"].ToString(),
                            Valore = (float)Convert.ToDouble(resultSet["Importo"])
                        });
                    }
                    GetTransazioniById.Close();
                    return Transazioni;




                }


            }
            catch (Exception ex)
            {

                throw new System.ServiceModel.FaultException(ex.Message);
            }
        }
    }
}
