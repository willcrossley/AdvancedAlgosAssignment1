using AdvancedAlgosAssignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedAlgosAssignment1
{
    //--------------------------------------------- README ---------------------------------------------\\ 

    //For this algo, I had the choice to use an OO based approach or a more traditional/generic/mathmatical
    //approach using a collection of arrays/sets to represent the different actors, their preferences and matches

    //I chose the OO approach since it SIGNIFICANTLY improves readability, robustness (less chance of one array
    //being misconfigured and causing the whole thing to break), as a form of anti plagarism proof
    //as most examples etc found online would be generic using a group of arrays etc, and also to more closely
    //match what is seen in the real world, you would more likely develop an algo like this to match existing
    //business objects not a group of arrays unless you are working on some super low level machines or want
    //to wrap your low level implementaion in a high level wrapper to fit a broader set of cases with one implementation.

    //This implementation also generates a match for each participant AND returns a matrix for all matches to
    //allow for match lookup from a single participant OR develop analytics on match effectiveness/bulk actions.
    //I know its not the most space efficient but it allows for a wider range of applications, if the situation
    //around why I was writing this algo was more descriptive I would pick just 1 but this gives flexibility.

    //--------------------------------------------- README ---------------------------------------------\\
    internal class GaleShapelyAlgo
    {
        public int[][] Match(Initiator[] initiators, Selector[] selectors)
        {
            var remainingInitiators = new Queue<Initiator>(initiators);

            if (initiators.Length != selectors.Length)
            {
                throw new ArgumentException("Set sizes not equal");
            }

            while (remainingInitiators.Count > 0)
            {
                var initiator = remainingInitiators.Peek();

                if (initiator.Match != null)
                {
                    remainingInitiators.Dequeue();
                }

            }

            return new int[0][];
        }
    }
}
