using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedAlgosAssignment1.Models
{
    internal class Initiator : Participant
    {
        Selector CurrentSelection;
        
        public Initiator(int[] preferences)
            : base(preferences)
        {

        }
    }
}
