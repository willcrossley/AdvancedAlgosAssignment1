using AdvancedAlgosAssignment1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

    //I also chose the output of this algorithm to be relations on each object rather than a 2D matrix of matches
    //as its more applicable to the OO approach, and allows the use of queues to speed things up slightly, without
    //having to do a lookup to get the index of the participant to make a match (if the matrix is storing the index of
    //someones match at their position) or store the index on the participant which would be bad OO.

    //I could also have chosen an object reference array for the preferences & hashmaps on an id for lookup rather than 
    //index as it makes more sense from an OO perspective except it just means each Participant array must be 
    //constructed then their preferences set, which is more pain than its worth for an algorithm focused project,
    //but is what I would do if I were to write this for a real application, as usually participants exist before they
    //make selections. This way I can create each object in one op for the tests.

    //--------------------------------------------- README ---------------------------------------------\\

    internal class GaleShapelyAlgo
    {
        public void Match(Collection<Initiator> initiators, Collection<Selector> selectors)
        {
            if (initiators.Count != selectors.Count)
            {
                throw new ArgumentException("Set sizes not equal");
            }

            var sizeOfSet = initiators.Count;
            var remainingInitiators = new Queue<Initiator>(initiators);
            PreRunSetup(initiators, selectors);

            while (remainingInitiators.Count > 0)
            {
                var initiator = remainingInitiators.Dequeue();

                if (!initiator.HasRemainingProposals) continue;

                var nextSelection = selectors[initiator.Preferences[initiator.NextProposalIndex++]];

                if (!nextSelection.HasMatch())
                {
                    Match(initiator, nextSelection);
                }
                else
                {
                    if (nextSelection.Prefers(initiator))
                    {
                        var selectorsMatch = nextSelection.Match;
                        remainingInitiators.Enqueue(selectorsMatch);
                        UnMatch(nextSelection);
                        Match(initiator, nextSelection);
                    }
                    else
                    {
                        remainingInitiators.Enqueue(initiator);
                    }
                }
            }
        }

        void Match(Initiator initiator, Selector selector)
        {
            initiator.Match = selector;
            selector.Match = initiator;
        }

        void UnMatch(Selector selector)
        {
            var initiator = selector.Match;
            selector.Match = null;
            initiator.Match = null;
        }

        void PreRunSetup(Collection<Initiator> initiators, Collection<Selector> selectors)
        {
            //This is an O(n^2) setup operation, but prevents having to do an O(n) lookup down the initiators
            //to compare a current match with a new prosal, which would push the overall complexity to O(n^3)
            //Worse for very small sets (n < 2, laughable) as its now O(n^2) + O(n^2), but better for large sets.
            foreach (var selector in selectors)
            {
                selector.InitialisePreferenceDict(initiators);
            }
        }
    }
}
