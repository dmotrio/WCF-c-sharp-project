using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

/*
CLASSE UTILIZZATA PER LA GESTIONE DELLE TRANSAZIONI IN ACCOPPIATA AL SERVER SQL DI MICROSOFT AZURE 
 
 
*/

namespace wcfserver
{
    public class Transazione
    {
        

        public int Id { get; set; }
        public int Id_Utente { get; set; }
        public string Data { get; set; }
        public double Valore { get; set; }


        public Transazione(int id_utente, string data, double valore)
        {
            Id_Utente = id_utente;
            Data = "data di oggi che non va";
           
            
            Valore = valore;
        }
        public Transazione()
        {

        }
    }

}

