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
            for (var i = 0; i < initiatorMatches.Length; i++)
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

            for (var i = 0; i < selectorMatches.Length; i++)
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
        public bool IsStable(IList<Initiator> initiators, IList<Selector> selectors)
        {
            //look through each initiators higher preference
            //if any selector has the current initiator at a higher preference than their current match
                //fail
            //else pass
            foreach (var initiator in initiators)
            {
                var match = initiator.Match;
                var matchIndex = FindIndex(match, selectors);

                for (var i = 0; i < initiator.Preferences.Length; i++)
                {
                    var currentSelectorIndex = initiator.Preferences[i];

                    if (currentSelectorIndex  == matchIndex)
                    {
                        break; //No better match for this initiator
                    }

                    var currentSelector = selectors[currentSelectorIndex];

                    if (currentSelector.Prefers(initiator)) //No Match case covered in Prefers
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public int[][] CreateRandomPreferenceArray(int size)
        {
            var result = new int[size][];
            var rand = new Random();

            for (int i  = 0; i < size; i++)
            {
                result[i] = new int[size];
                for (var j = 0; j < size; j++)
                {
                    result[i][j] = i;
                }

                for (var k = size - 1; k > 0; k--) //Fisher-Yates shuffle (for a bonus algorithm I found when looking this up ;) )
                {
                    var randomIndex = rand.Next(i + 1);

                    var temp = result[i][randomIndex];
                    result[i][randomIndex] = result[i][k];
                    result[i][k] = temp;

                }
            }

            return result;
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
