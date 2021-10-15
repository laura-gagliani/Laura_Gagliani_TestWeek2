using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWeek2
{
    public static class AgendaManager
    {
        public static List<Task> listaDiTask = new List<Task>();


        /// <summary>
        /// metodo che prende in ingresso due int corrispondenti agli estremi della rosa di scelte dello specifico menu,
        ///che controlla che la scelta dell'utente sia corretta, e che rende la scelta (come int) dell'utente 
        /// </summary>
        /// <param name="limiteInferiore"></param>
        /// <param name="limiteSuperiore"></param>
        /// <returns></returns>
        public static int GetCorrectIntInputFromMenu(int limiteInferiore, int limiteSuperiore)
        {
            bool sceltaCorretta = int.TryParse(Console.ReadLine(), out int sceltaMenu);

            while ((!sceltaCorretta) || sceltaMenu < limiteInferiore || sceltaMenu > limiteSuperiore)
            {
                Console.WriteLine("Errore. Scelta incorretta! Premi di nuovo:");
                sceltaCorretta = int.TryParse(Console.ReadLine(), out sceltaMenu);
            }
            return sceltaMenu;
        }

        public static void VisualizzaTaskInAgenda()
        {
            StampaTaskDaLista(listaDiTask);
        }

        private static void StampaTaskDaLista(List<Task> givenList)
        {
            Console.WriteLine("Data di scadenza\tLivello di priorità\tDescrizione");
            foreach (var item in givenList)
            {
                Console.Write($"{item.DataDiScadenza}\t{item.LivelloPriorità}\t{item.Descrizione}");
            }
        }

        public static void AggiungiTask()
        {
            
            Task task = new Task();

            Console.WriteLine("Inserisci una descrizione");
            task.Descrizione = Console.ReadLine();
            Console.WriteLine("Inserisci una data di scadenza");
            task.DataDiScadenza = InserisciDataScadenza();
            Console.WriteLine("Inserisci un livello di priorità");
            task.LivelloPriorità = InserisciLivelloPriorità();

            listaDiTask.Add(task);
        }

        private static LivelloPriorità InserisciLivelloPriorità()
        {
            Console.WriteLine("\nPremi 1 per assegnare livello di priorità ALTO");
            Console.WriteLine("Premi 2 per assegnare livello di priorità MEDIO");
            Console.WriteLine("Premi 3 per assegnare livello di priorità BASSO");
            int livelloPriorità = GetCorrectIntInputFromMenu(1, 3);
            return (LivelloPriorità)livelloPriorità;
        }

        private static DateTime InserisciDataScadenza()
        {
            bool dataCorretta = DateTime.TryParse(Console.ReadLine(), out DateTime dataScadenza);
            while ((!dataCorretta)||dataScadenza <= DateTime.Today)
            {
                Console.WriteLine("Errore! Inserisci una data valida:");
                dataCorretta = DateTime.TryParse(Console.ReadLine(), out dataScadenza);
            }
            return dataScadenza;
        }


        public static void EliminaTask()
        {
            throw new NotImplementedException();
        }

        public static void FiltraTaskPerPriorità()
        {
            throw new NotImplementedException();
        }

        public static void TaskDiProva()
        {

        }
    }
}
