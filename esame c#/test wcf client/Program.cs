using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test_wcf_client.WCF;

namespace test_wcf_client
{
    class Program
    {
        static void Main(string[] args)
        {
            var wcf = new WCF.Service1Client();//ISTANZO IL SERVIZIO wcf PER POTERLO UTILIZZARE
            
            try
            {
                int? scelta = null;
                int appoggio = 0;
                
                while (scelta != 0)
                {
                    Console.WriteLine("Selezionare con l'apposito numero l'operazione da effettuare:");
                    Console.WriteLine("1 - Login Utente");
                    Console.WriteLine("0 - Uscire dal programma");

                    Console.WriteLine("Per registrarti chiedi ad un amministratore!");

                    string controllo_scelta = Console.ReadLine();

                    if (int.TryParse(controllo_scelta, out appoggio))
                    {
                        if (appoggio >= 0 && appoggio <=1)
                        {
                            //scelta valida
                            scelta = appoggio;

                            switch (scelta)
                            {
                                case 1:
                                    string nome, cognome, password;
                                    Console.WriteLine("Inserisci il nome, cognome e password dell'account");

                                    Console.Write("Nome: ");
                                    nome = Console.ReadLine();
                                    Console.Write("Cognome: ");
                                    cognome = Console.ReadLine();
                                    Console.Write("Password: ");
                                    password = Console.ReadLine();

                                    

                                    int Risultato_Login = wcf.LoginUtente(nome, cognome, password);
                                    
                                    if (Risultato_Login != 0)
                                    {
                                        // l'utente è presente nel db e le credenziali sono giuste


                                        Utente Persona_Login = new Utente();

                                        Persona_Login = wcf.GetUtente(Risultato_Login);

                                        //Console.WriteLine(Persona_Login.IsAdmin);
                                        if (Persona_Login.IsAdmin == 1) //Utente admin
                                        {
                                            int? scelta_admin = null;
                                            int appoggio_admin = 0;

                                            while (scelta_admin != 0)
                                            {
                                                Console.WriteLine("Selezionare con l'apposito numero l'operazione da effettuare:");
                                                Console.WriteLine("1 - Crea Utente");
                                                Console.WriteLine("2 - Elimina Utente");
                                                Console.WriteLine("3 - Modifica Saldo");
                                                Console.WriteLine("0 - Log Out");

                                                

                                                string controllo_scelta_admin = Console.ReadLine();

                                                if (int.TryParse(controllo_scelta_admin, out appoggio_admin))
                                                {
                                                    if (appoggio_admin >= 0 && appoggio_admin <= 3)
                                                    {
                                                        //scelta valida
                                                        scelta_admin = appoggio_admin;

                                                        switch (scelta_admin)
                                                        {
                                                            case 1: //CREA UTENTE
                                                                string create_nome, create_cognome, create_password;
                                                                float create_saldo;
                                                                int create_is_admin;

                                                                Console.WriteLine("Inserisci il nome, cognome, password, saldo e se l'utente è admin o no dell'account da creare");

                                                                Console.Write("Nome: ");
                                                                create_nome = Console.ReadLine();
                                                                Console.Write("Cognome: ");
                                                                create_cognome = Console.ReadLine();
                                                                Console.Write("Password: ");
                                                                create_password = Console.ReadLine();
                                                                Console.Write("Saldo: ");
                                                                create_saldo = (float)Convert.ToDouble(Console.ReadLine());
                                                                Console.Write("1 per admin 0 per utente normale: ");
                                                                create_is_admin = Convert.ToInt32(Console.ReadLine());

                                                                Utente registra_utente = new Utente();
                                                                registra_utente.Nome = create_nome;
                                                                registra_utente.Cognome = create_cognome;
                                                                registra_utente.Password = create_password;
                                                                registra_utente.Saldo = create_saldo;
                                                                registra_utente.IsAdmin = create_is_admin;

                                                                wcf.registertUser(registra_utente);
                                                                break;


                                                            case 2: //ELIMINA UTENTE
                                                                int delete_id;
                                                                Console.WriteLine("Inserisci l'id dell'utente da eliminare");

                                                                Console.Write("Id: ");
                                                                delete_id = Convert.ToInt32(Console.ReadLine());
                                                                wcf.DeletUser(delete_id);
                                                                break;


                                                            case 3: //MODIFICA SALDO
                                                                int mod_id;
                                                                float mod_saldo;
                                                                Console.WriteLine("Inserisci l'id dell'utente al quale modificare il saldo e il nuovo saldo");

                                                                Console.Write("Id: ");
                                                                mod_id = Convert.ToInt32(Console.ReadLine());
                                                                Console.Write("Id: ");
                                                                mod_saldo= (float)Convert.ToDouble(Console.ReadLine());
                                                                wcf.ModificaSaldo(mod_id, mod_saldo);
                                                                break;


                                                            default:
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("hai selezionato un'opzione non valida!");
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Non hai inserito un numero!");
                                                }

                                            }
                                        }
                                        else//utente normale
                                        {
                                            int? scelta_utente = null;
                                            int appoggio_utente = 0;

                                            while (scelta_utente != 0)
                                            {
                                                Console.WriteLine("Selezionare con l'apposito numero l'operazione da effettuare:");
                                                Console.WriteLine("1 - Preleva denaro");
                                                Console.WriteLine("2 - Deposita denaro");
                                                Console.WriteLine("3 - Tutte le tranazioni");
                                                Console.WriteLine("4 - Controlla saldo");
                                                Console.WriteLine("0 - Log Out");



                                                string controllo_scelta_admin = Console.ReadLine();

                                                if (int.TryParse(controllo_scelta_admin, out appoggio_utente))
                                                {
                                                    if (appoggio_utente >= 0 && appoggio_utente <= 4)
                                                    {
                                                        //scelta valida
                                                        scelta_utente = appoggio_utente;

                                                        switch (scelta_utente)
                                                        {
                                                            case 1: //PRELEVA DENARO
                                                                Transazione preleva = new Transazione();
                                                                Console.WriteLine("Inserisci la somma da prelevare");

                                                                Console.Write("Somma: ");

                                                                double preleva_saldo = Convert.ToDouble(Console.ReadLine());

                                                                if (preleva_saldo < 0)
                                                                {
                                                                    //prelievo negativo non valido
                                                                    Console.WriteLine("hai inserito un numero negativo!");
                                                                    break;
                                                                }
                                                                else
                                                                {
                                                                    preleva.Valore = preleva_saldo * -1;
                                                                }

                                                                preleva.Id_Utente = Persona_Login.Id;
                                                                
                                                                DateTime datap = DateTime.Now;

                                                                
                                                                preleva.Data = datap.ToString();

                                                                string prelievo_res = wcf.InsertTransazione(preleva);
                                                                Console.WriteLine(prelievo_res);

                                                                break;


                                                            case 2: //DEPOSITA DENARO

                                                                Transazione deposita = new Transazione();
                                                                Console.WriteLine("Inserisci la somma da depositare");

                                                                Console.Write("Somma: ");

                                                                double deposita_saldo = Convert.ToDouble(Console.ReadLine());

                                                                if (deposita_saldo < 0)
                                                                {
                                                                    //prelievo negativo non valido
                                                                    Console.WriteLine("hai inserito un numero negativo!");
                                                                    break;
                                                                }


                                                                deposita.Id_Utente = Persona_Login.Id;

                                                                DateTime datad = DateTime.Now;


                                                                deposita.Data = datad.ToString();
                                                                deposita.Valore = deposita_saldo;

                                                                string deposita_res = wcf.InsertTransazione(deposita);
                                                                Console.WriteLine(deposita_res);

                                                                break;


                                                            case 3: //TUTTE LE TRANSAZIONI
                                                                List<Transazione> transactions = wcf.TransazioniByIdUtente(Persona_Login.Id).ToList();


                                                                int count = transactions.Count();
                                                                for (int i = 0; i < count; i++)
                                                                {
                                                                    Console.WriteLine("La transazione del " + transactions[i].Data + " è di importo: " + transactions[i].Valore);
                                                                    
                                                                }
                                                                break;

                                                            case 4: //CONTROLLA SALDO
                                                                Console.Write("Il tuo saldo è: ");
                                                                Console.WriteLine(wcf.GetSaldo(Persona_Login.Id));
                                                                break;

                                                            default:
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("hai selezionato un'opzione non valida!");
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Non hai inserito un numero!");
                                                }

                                            }
                                        }
                                    }
                                    else
                                    {
                                        //accesso errato
                                        Console.WriteLine("Credenziali sbagliate o utente non presente nel sistema");
                                    }

                                    break;


                                
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("hai selezionato un'opzione non valida!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Non hai inserito un numero!");
                    }

                }

            }
            catch (Exception ex)
            {

                throw new System.ServiceModel.FaultException(ex.Message);
            }
        }
    }
}
