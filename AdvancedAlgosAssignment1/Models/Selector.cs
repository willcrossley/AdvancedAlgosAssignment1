using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedAlgosAssignment1.Models
{
    internal class Selector : Participant
    {
        Participant[] Proposals;

        public Selector(int[] preferences)
            :base(preferences)
        {
            
        }
    }
}
