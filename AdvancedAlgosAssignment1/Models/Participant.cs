using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedAlgosAssignment1.Models
{
    internal class Participant
    {
        /// <summary>
        /// Preferences array stores the index of the opposing set.
        /// So index 0 of this array is the index of the #1 pick of the other set
        /// </summary>
        public int[] Preferences;
        public Participant Match;

        public Participant(int[] preferences)
        {
            Preferences = preferences;
        }

        public bool HasMatch() => Match != null;
    }
}
