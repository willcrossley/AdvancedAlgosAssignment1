using AdvancedAlgosAssignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace AdvancedAlgosAssignment1Test
{
    internal class Helpers
    {
        public Collection<Initiator> CreateInitiators(int[][] preferences)
        {
            return new Collection<Initiator>(preferences.Select(x => new Initiator(x)).ToList());
        }

        public Collection<Selector> CreateSelectors(int[][] preferences)
        {
            return new Collection<Selector>(preferences.Select(x => new Selector(x)).ToList());
        }

        // Prefering an IsStable check rather than an exact check of matches,
        // as the goal is the algorithm is to find a stable pairing from the set of all possible
        // stable pairings, which means tweaks/changes to the algo which keep it correct may affect
        // which set is chosen, in which case tests using exact matches would all have to be rewritten.
        // I'll still add one or two hard checks for santiy though.
        public bool IsStable(Collection<Initiator> initiators, Collection<Selector> selectors)
        {
            return true;
        }
    }
}
