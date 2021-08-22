using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
CLASSE UTILIZZATA PER LA GESTIONE DEGLI UTENTI IN ACCOPPIATA AL SERVER SQL DI MICROSOFT AZURE 
 
 
*/
namespace wcfserver
{
    public class Utente
    {
        
        public int Id{ get; set; }
        public string Nome{ get; set; }
        public string Cognome{ get; set; }
        public string Password{ get; set; }
        public float Saldo{ get; set; }
        public int IsAdmin { get; set; }


        public Utente(string nome, string cognome, string password, float saldo, int isadmin)
        {
            
            Nome = nome;
            Cognome = cognome;
            Password = password;
            Saldo = saldo;
            IsAdmin = isadmin;
        }

        public Utente()
        {

        }
    }
}
