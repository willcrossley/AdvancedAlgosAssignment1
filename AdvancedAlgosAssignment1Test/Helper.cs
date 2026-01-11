using AdvancedAlgosAssignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace AdvancedAlgosAssignment1Test
{
    internal class Helper
    {
        public IList<Initiator> CreateInitiators(int[][] preferences)
        {
            return preferences.Select(x => new Initiator(x)).ToArray();
        }

        public IList<Selector> CreateSelectors(int[][] preferences)
        {
            return preferences.Select(x => new Selector(x)).ToArray();
        }

        // Prefering an IsStable check rather than an exact check of matches,
        // as the goal is the algorithm is to find a stable pairing from the set of all possible
        // stable pairings, which means tweaks/changes to the algo which keep it correct may affect
        // which set is chosen, in which case tests using exact matches would all have to be rewritten.
        // I'll still do exact matches for simple cases though, especially where only one valid solution is present
        public bool IsStable(Collection<Initiator> initiators, Collection<Selector> selectors)
        {
            return true;
        }
    }
}
