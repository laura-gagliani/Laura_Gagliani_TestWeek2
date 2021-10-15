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

        public static void CaricaTaskDaFile()
        {
            string rigaAgenda;
            string path = @"F:\Laura\Documenti\Avanade\Academy\Week2Test\TestWeek2\TestWeek2\RegistroTask.txt";

            foreach (string line in System.IO.File.ReadLines(path))
            {
                rigaAgenda = line;
                Task task = new();
                var arrayDiProprietà = rigaAgenda.Split(",");

                task.DataDiScadenza = DateTime.Parse(arrayDiProprietà[0]);
                task.LivelloPriorità = (LivelloPriorità)Enum.Parse(typeof(LivelloPriorità), arrayDiProprietà[1]);
                task.Descrizione = arrayDiProprietà[2];
                listaDiTask.Add(task);

            }
        }


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
                Console.WriteLine("\nErrore. Scelta incorretta! Premi di nuovo:");
                sceltaCorretta = int.TryParse(Console.ReadLine(), out sceltaMenu);
            }
            return sceltaMenu;
        }

        public static void RegistraTaskSuFile()
        {
            string path = @"F:\Laura\Documenti\Avanade\Academy\Week2Test\TestWeek2\TestWeek2\RegistroTask.txt";
            using (StreamWriter sw1 = new StreamWriter(path))
            {
                foreach (var item in listaDiTask)
                {
                    //sw1.Write($"Data di scadenza: {item.DataDiScadenza.ToShortDateString()}; Livello di priorità: {item.LivelloPriorità}; Descrizione: {item.Descrizione}\n");
                    sw1.Write($"{item.DataDiScadenza},{item.LivelloPriorità},{item.Descrizione}\n");
                }

            }
        }

        public static void VisualizzaTaskInAgenda()
        {
            Console.WriteLine("\nI task in agenda sono:");
            StampaTaskDaLista(listaDiTask);
        }

        public static void StampaTaskDaLista(List<Task> givenList)
        {

            if (givenList.Count == 0)
            {
                Console.WriteLine("\nNessun task trovato!");
            }
            else
            {
                Console.WriteLine("Data di scadenza\tLivello di priorità\tDescrizione");
                Console.WriteLine("------------------------------------------------------------------------------");
                foreach (var item in givenList)
                {
                    Console.Write($"{item.DataDiScadenza}\t\t{item.LivelloPriorità}\t\t{item.Descrizione}\n");
                }
                Console.WriteLine("------------------------------------------------------------------------------");

            }

        }

        public static void AggiungiTask()
        {

            Task task = new Task();
            bool ripetizione;
            do
            {
                Console.WriteLine("\nInserisci una descrizione:");
                task.Descrizione = Console.ReadLine();
                Console.WriteLine("\nInserisci una data di scadenza:");
                task.DataDiScadenza = InserisciDataScadenza();
                Console.WriteLine("\nInserisci un livello di priorità:");
                task.LivelloPriorità = InserisciLivelloPriorità();

                ripetizione = ControllaTaskRipetuto(task);
            } while (ripetizione);

            listaDiTask.Add(task);
            Console.WriteLine("\nTask inserito con successo in agenda");
        }

        public static bool ControllaTaskRipetuto(Task task)
        {
            bool ripetizione = false;
            foreach (var item in listaDiTask)
            {
                if (item.Descrizione.ToLower() == task.Descrizione.ToLower())
                {
                    ripetizione = true;
                }
            }
            if (ripetizione)
            {
                Console.WriteLine("\nAttenzione! Un task con questa descrizione è già presente in agenda! Inserire un task diverso:");
            }
            return ripetizione;
        }

        public static LivelloPriorità InserisciLivelloPriorità()
        {
            Console.WriteLine("\nPremi 1 per livello di priorità Alto");
            Console.WriteLine("Premi 2 per livello di priorità Medio");
            Console.WriteLine("Premi 3 per livello di priorità Basso");
            int livelloPriorità = GetCorrectIntInputFromMenu(1, 3);
            return (LivelloPriorità)livelloPriorità;
        }

        public static DateTime InserisciDataScadenza()
        {
            bool dataCorretta = DateTime.TryParse(Console.ReadLine(), out DateTime dataScadenza);
            while ((!dataCorretta) || dataScadenza <= DateTime.Now)
            {
                Console.WriteLine("Errore! Inserisci una data valida:");
                dataCorretta = DateTime.TryParse(Console.ReadLine(), out dataScadenza);
            }
            return dataScadenza;
        }


        public static void EliminaTask()
        {
            Console.WriteLine("\nI task attualmente in agenda sono:");
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
            Task task1 = new Task() { Descrizione = "Portare gatto dal veterinario", LivelloPriorità = (LivelloPriorità)1, DataDiScadenza = new DateTime(2021, 11, 03, 15, 30, 00) };
            listaDiTask.Add(task1);
            Task task2 = new Task() { Descrizione = "Innaffiare le piante", LivelloPriorità = (LivelloPriorità)1, DataDiScadenza = new DateTime(2021, 10, 17) };
            listaDiTask.Add(task2);
            Task task3 = new Task() { Descrizione = "Comprare concime per le piante", LivelloPriorità = (LivelloPriorità)3, DataDiScadenza = new DateTime(2021, 10, 21) };
            listaDiTask.Add(task3);
        }


    }
}
