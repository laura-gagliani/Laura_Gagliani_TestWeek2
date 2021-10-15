using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWeek2
{
    public class Task
    {
        public string Descrizione { get; set; }
        public DateTime DataDiScadenza { get; set; }
        public LivelloPriorità LivelloPriorità { get; set; }
        

        public Task()
        {

        }
    }

    public enum LivelloPriorità
    {
        Alto=1,
        Medio=2,
        Basso=3
    }
}
