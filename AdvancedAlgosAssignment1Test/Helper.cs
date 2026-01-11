using AdvancedAlgosAssignment1.Models;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;

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

        //Requires a list of expected matches for initiators and selectors in the case the sets are different sizes
        //and the matches are not 1-1. Also not using IsStable for most tests as although the goal is only to achieve
        //a stable matching, Gale-Shapely must do this AND pick the initiator optimal solution from the set of stable
        //solutions, so an exact assertion should be made, as it shouldn't change in the future with tweaks.
        //In a real system where we only care about the goal and future tweaks could happen, I would just use IsStable.
        /// <summary>
        /// Use -1 to signify no match for a certain participant
        /// </summary>
        public void AssertMatches(IList<Initiator> initiators, IList<Selector> selectors, int[] initiatorMatches, int[] selectorMatches)
        {
            for (int i = 0; i < initiatorMatches.Length; i++)
            {
                var initiator = initiators[i];
                if (initiatorMatches[i] == -1)
                {
                    Assert.True(!initiator.HasMatch(), $"expected inditiator {i} to have no match");
                }
                else
                {
                    Assert.That(initiator.Match, Is.SameAs(selectors[initiatorMatches[i]]), $"expected initiator {i} to match with {initiatorMatches[i]}, instead was {FindIndex(initiator.Match, selectors)}");
                }
            }

            for (int i = 0; i < selectorMatches.Length; i++)
            {
                var selector = selectors[i];
                if (selectorMatches[i] == -1)
                {
                    Assert.True(!selector.HasMatch(), $"expected selector {i} to have no match");
                }
                else
                {
                    Assert.That(selector.Match, Is.SameAs(initiators[selectorMatches[i]]), $"expected selector {i} to match with { selectorMatches[i] }, instead was {FindIndex(selector.Match, initiators)}");
                }
            }
        }

        //Using IsStable for the random test where a constant equality isn't possible
        public bool IsStable(Collection<Initiator> initiators, Collection<Selector> selectors)
        {
            return true;
        }

        int FindIndex(Initiator initiator, IList<Initiator> initiators)
        {
            return initiators.IndexOf(initiator);
        }

        int FindIndex(Selector selector, IList<Selector> selectors)
        {
            return selectors.IndexOf(selector);
        }
    }
}
