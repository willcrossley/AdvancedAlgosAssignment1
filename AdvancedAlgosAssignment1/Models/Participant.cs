using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedAlgosAssignment1.Models
{
    internal class Participant
    {
        public int[] Preferences;
        public Participant Match;

        public Participant(int[] preferences)
        {
            Preferences = preferences;
        }
    }
}
