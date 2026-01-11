using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedAlgosAssignment1.Models
{
    public abstract class Participant
    //Had to make non generic parent for the MatchType assersion otherwise its
    //circular (would have to be Participant<Participant<MatchType>> which isn't allowed)
    { }

    public abstract class Participant<MatchType> : Participant 
        where MatchType : Participant
    {
        /// <summary>
        /// Preferences array stores the index of the opposing set.
        /// So index 0 of this array is the index of the #1 pick of the other set
        /// </summary>
        public int[] Preferences;
        public MatchType Match;

        public Participant(int[] preferences)
        {
            Preferences = preferences;
        }

        public bool HasMatch() => Match != null;
    }
}
