using Laura_Gagliani_TestWeek2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laura_Gagliani_TestWeek2
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

        internal static void RegistraTaskSuFile()
        {
            string path = @"F:\Laura\Documenti\Avanade\Academy\Week2Test\TestWeek2\TestWeek2\RegistroTask.txt";
            using (StreamWriter sw1 = new StreamWriter(path))      
            {
                foreach (var item in listaDiTask)
                {
                    sw1.Write($"Data di scadenza: {item.DataDiScadenza.ToShortDateString()}; Livello di priorità: {item.LivelloPriorità}; Descrizione: {item.Descrizione}\n");
                }
                
            }
        }

        public static void VisualizzaTaskInAgenda()
        {
            StampaTaskDaLista(listaDiTask);
        }

        public static void StampaTaskDaLista(List<Task> givenList)
        {
            
            if (givenList.Count == 0)
            {
                Console.WriteLine("Nessun task trovato");
            }
            else
            {
                Console.WriteLine("\nData di scadenza\tLivello di priorità\tDescrizione");
                Console.WriteLine("------------------------------------------------------------------------------");
                foreach (var item in givenList)
                {
                    Console.Write($"{item.DataDiScadenza.ToShortDateString()}\t\t\t{item.LivelloPriorità}\t\t{item.Descrizione}\n");
                }
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

        public static LivelloPriorità InserisciLivelloPriorità()
        {
            Console.WriteLine("\nPremi 1 per livello di priorità ALTO");
            Console.WriteLine("Premi 2 per livello di priorità MEDIO");
            Console.WriteLine("Premi 3 per livello di priorità BASSO");
            int livelloPriorità = GetCorrectIntInputFromMenu(1, 3);
            return (LivelloPriorità)livelloPriorità;
        }

        public static DateTime InserisciDataScadenza()
        {
            bool dataCorretta = DateTime.TryParse(Console.ReadLine(), out DateTime dataScadenza);
            while ((!dataCorretta) || dataScadenza <= DateTime.Today)
            {
                Console.WriteLine("Errore! Inserisci una data valida:");
                dataCorretta = DateTime.TryParse(Console.ReadLine(), out dataScadenza);
            }
            return dataScadenza;
        }


        public static void EliminaTask()
        {
            Console.WriteLine("I task attualmente in agenda sono:");
            StampaTaskDaLista(listaDiTask);

            Console.WriteLine("\nInserisci una parola chiave o parte della descrizione del task da eliminare:");
            Task taskDaEliminare = CercaTaskPerParolaChiaveInDescrizione();
            if (taskDaEliminare != null)
            {
                listaDiTask.Remove(taskDaEliminare);
                Console.WriteLine("\nIl task è stato rimosso");
            }


        }

        public static Task CercaTaskPerParolaChiaveInDescrizione()
        {
            bool taskUnivoco;
            do
            {
                taskUnivoco = true;
                string parolaChiave = Console.ReadLine();
                Task itemToReturn = null;
                int itemTrovati = 0;
                foreach (var item in listaDiTask)
                {
                    bool parolaTrovata = item.Descrizione.Contains(parolaChiave);
                    if (parolaTrovata)
                    {
                        itemToReturn = item;
                        itemTrovati++;
                    }
                }

                if (itemTrovati == 0)
                {
                    Console.WriteLine("\nAttenzione! Nessun task trovato con questa parola chiave. Riprova:");
                    taskUnivoco = false;
                }
                else if (itemTrovati == 1)
                {
                    Console.WriteLine("\nTask trovato!");
                    return itemToReturn;
                }
                else if (itemTrovati > 1)
                {
                    Console.WriteLine("\nAttenzione! Trovati multipli task contententi la stessa parola chiave.\nRiprova con una frase più specifica:");
                    taskUnivoco = false;
                }
            } while (!taskUnivoco);
            return null;
        }

        public static void FiltraTaskPerPriorità()
        {
            Console.WriteLine("Scegli il livello in base al quale filtrare:");
            LivelloPriorità livelloDaCercare = InserisciLivelloPriorità();
            List<Task> listaFiltrata = new List<Task>();
            foreach (var item in listaDiTask)
            {
                if (livelloDaCercare == item.LivelloPriorità)
                {
                    listaFiltrata.Add(item);
                }
            }
            StampaTaskDaLista(listaFiltrata);
        }

        

        public static void TaskDiProva()
        {
            Task task1 = new Task() { Descrizione = "portare gatto dal veterinario", LivelloPriorità = (LivelloPriorità)1, DataDiScadenza = new DateTime(2021, 10, 20, 15,30) };
            listaDiTask.Add(task1);
            Task task2 = new Task() { Descrizione = "innaffiare le piante", LivelloPriorità = (LivelloPriorità)1 };
            listaDiTask.Add(task2);
            Task task3 = new Task() { Descrizione = "comprare concime per le piante", LivelloPriorità = (LivelloPriorità)3 };
            listaDiTask.Add(task3);
        }


    }
}
