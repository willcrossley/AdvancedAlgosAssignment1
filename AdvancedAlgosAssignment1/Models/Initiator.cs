using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedAlgosAssignment1.Models
{
    internal class Initiator : Participant<Selector>
    {
        //By storing a next proposal index on the Initiator, we can avoid
        //having to start looking through their preferences from the pref[currentRound]
        //to find the next open match at the start of each round.
        //Eg we are on round 3 but in round 2 we found index 1-5 of this initiators preferences
        //were already matched, so we dont need to look through them again.
        public int NextProposalIndex { get; set; }
        
        public Initiator(int[] preferences)
            : base(preferences)
        {

        }

        public bool HasRemainingProposals => NextProposalIndex < Preferences.Length;
    }
}
