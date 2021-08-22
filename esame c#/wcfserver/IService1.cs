using System.Collections.Generic;
using System.ServiceModel;

namespace wcfserver
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "IService1" nel codice e nel file di configurazione contemporaneamente.
    [ServiceContract]
    public interface IService1
    {
        /*
         
         [OperationContract]
        void DoWork();
        
         */

        [OperationContract]
        string registertUser(Utente utente);

        [OperationContract]
        Utente GetUtente(int id);

        [OperationContract]
        string DeletUser(int id);

        [OperationContract]
        string ModificaSaldo(int id, float saldo);

        [OperationContract]
        double GetSaldo(int id);

        [OperationContract]         
        int LoginUtente(string nome, string cognome, string password);

        [OperationContract]
        string InsertTransazione(Transazione transazione);

        [OperationContract]
        List<Transazione> TransazioniByIdUtente(int Id);

    }

    
}
