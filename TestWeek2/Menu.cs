using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laura_Gagliani_TestWeek2
{
    class Menu
    {
        public static void Start()
        {
            AgendaManager.TaskDiProva();
            Console.WriteLine("Benvenuto nel Gestore Agenda!\n");
            bool continua = true;
            do
            {
                Console.WriteLine("\n\nMENU");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("Premi 1 per visualizzare i task in agenda");
                Console.WriteLine("Premi 2 per aggiungere un nuovo task");
                Console.WriteLine("Premi 3 per eliminare un task");
                Console.WriteLine("Premi 4 per filtrare i task per livello di priorità");
                Console.WriteLine("Premi 0 per uscire");

                int scelta = AgendaManager.GetCorrectIntInputFromMenu(0, 4);

                switch (scelta)
                {
                    case 0:
                        Console.WriteLine("\nChiusura Gestore...");
                        Console.WriteLine("\nRegistrazione task su file...");
                        AgendaManager.RegistraTaskSuFile();
                        continua = false;
                        break;
                    case 1:
                        AgendaManager.VisualizzaTaskInAgenda();
                        break;
                    case 2:
                        AgendaManager.AggiungiTask();
                        break;
                    case 3:
                        AgendaManager.EliminaTask();
                        break;
                    case 4:
                        AgendaManager.FiltraTaskPerPriorità();
                        break;
                }

            } while (continua);


        }
    }
}
