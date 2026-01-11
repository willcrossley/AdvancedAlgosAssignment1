using AdvancedAlgosAssignment1;
using AdvancedAlgosAssignment1.Models;

namespace AdvancedAlgosAssignment1Test
{
    public class GaleShapelyAlgoTest
    {
        [SetUp]
        public void Setup()
        {
            helper = new Helper();
            algo = new GaleShapelyAlgo();
        }

        [Test]
        public void EmptyList()
        {
            Assert.DoesNotThrow(() => { algo.Match(Array.Empty<Initiator>(), Array.Empty<Selector>()); });
        }

        [Test]
        public void SizeOneMatches()
        {
            var initiators = helper.CreateInitiators([[0]]);
            var selectors = helper.CreateSelectors([[0]]);

            RunMatch(initiators, selectors);

            Assert.That(selectors[0], Is.SameAs(initiators[0].Match));
            Assert.That(initiators[0], Is.SameAs(selectors[0].Match));
        }

        [Test]
        public void NoConflictingProposals()
        {
            var initiators = helper.CreateInitiators([
                [0, 1],
                [1, 0],
            ]);

            var selectors = helper.CreateSelectors([
                [0, 1],
                [1, 0],
            ]);

            RunMatch(initiators, selectors);

            int[] expectedInitiatorMatches = [0, 1];
            int[] expectedSelectorMatches = [0, 1];

            helper.AssertMatches(initiators, selectors, expectedInitiatorMatches, expectedSelectorMatches);
        }

        [Test]
        public void SimpleConflictingProposal()
        {
            var initiators = helper.CreateInitiators([
                [0, 2, 1],
                [0, 1, 2],
                [1, 2, 0],
            ]);

            var selectors = helper.CreateSelectors([
                [0, 1, 2],
                [2, 0, 1],
                [1, 0, 2],
            ]);

            RunMatch(initiators, selectors);

            int[] expectedInitiatorMatches = [0, 2, 1];
            int[] expectedSelectorMatches = [0, 2, 1];

            Assert.That(helper.IsStable(initiators, selectors)); //Doing this for all tests not easily verified as a sanity check
            helper.AssertMatches(initiators, selectors, expectedInitiatorMatches, expectedSelectorMatches); //and this for a more rigorous check
        }

        [Test]
        public void MoreInitiatorsThanSelectors()
        {
            var initiators = helper.CreateInitiators([
                [0, 1],
                [0, 1],
                [1, 0],
            ]);

            var selectors = helper.CreateSelectors([
                [0, 1, 2],
                [2, 0, 1],
            ]);

            RunMatch(initiators, selectors);

            int[] expectedInitiatorMatches = [0, -1, 1];
            int[] expectedSelectorMatches = [0, 2];

            Assert.That(helper.IsStable(initiators, selectors));
            helper.AssertMatches(initiators, selectors, expectedInitiatorMatches, expectedSelectorMatches);
        }

        [Test]
        public void MoreSelectorsThanInitiators()
        {
            var initiators = helper.CreateInitiators([
                [0, 1, 2],
                [2, 0, 1],
            ]);

            var selectors = helper.CreateSelectors([
                [0, 1],
                [0, 1],
                [1, 0],
            ]);

            RunMatch(initiators, selectors);

            int[] expectedInitiatorMatches = [0, 2];
            int[] expectedSelectorMatches = [0, -1, 1];

            Assert.That(helper.IsStable(initiators, selectors));
            helper.AssertMatches(initiators, selectors, expectedInitiatorMatches, expectedSelectorMatches);
        }

        [Test]
        public void DuplicateInitiatorPrefernces()
        {
            var initiators = helper.CreateInitiators([
                [2, 0, 1, 3],
                [2, 0, 1, 3],
                [3, 2, 0, 1],
                [1, 3, 0, 2],
            ]);

            var selectors = helper.CreateSelectors([
                [3, 1, 2, 0],
                [0, 2, 1, 3],
                [1, 2, 3, 0],
                [1, 3, 0, 2],
            ]);

            RunMatch(initiators, selectors);

            int[] expectedInitiatorMatches = [0, 2, 3, 1];
            int[] expectedSelectorMatches = [0, 3, 1, 2];

            Assert.That(helper.IsStable(initiators, selectors));
            helper.AssertMatches(initiators, selectors, expectedInitiatorMatches, expectedSelectorMatches);
        }

        [Test]
        public void DuplicateSelectorPreferences()
        {
            var initiators = helper.CreateInitiators([
                [3, 1, 2, 0],
                [0, 2, 1, 3],
                [1, 2, 3, 0],
                [1, 3, 0, 2],
            ]);

            var selectors = helper.CreateSelectors([
                [2, 0, 1, 3],
                [2, 0, 1, 3],
                [3, 2, 0, 1],
                [1, 3, 0, 2],
            ]);

            RunMatch(initiators, selectors);

            int[] expectedInitiatorMatches = [2, 0, 1, 3];
            int[] expectedSelectorMatches = [1, 2, 0, 3];

            Assert.That(helper.IsStable(initiators, selectors));
            helper.AssertMatches(initiators, selectors, expectedInitiatorMatches, expectedSelectorMatches);
        }

        [Test]
        public void AllDuplicatePreferences()
        {
            var initiators = helper.CreateInitiators([
                [3, 1, 2, 0],
                [3, 1, 2, 0],
                [3, 1, 2, 0],
                [3, 1, 2, 0],
            ]);

            var selectors = helper.CreateSelectors([
                [1, 0, 2, 3],
                [1, 0, 2, 3],
                [1, 0, 2, 3],
                [1, 0, 2, 3],
            ]);

            RunMatch(initiators, selectors);

            int[] expectedInitiatorMatches = [1, 3, 2, 0];
            int[] expectedSelectorMatches = [3, 0, 2, 1];

            Assert.That(helper.IsStable(initiators, selectors));
            helper.AssertMatches(initiators, selectors, expectedInitiatorMatches, expectedSelectorMatches);
        }

        [Test]
        public void MostRejectionsPossible()
        {
            var initiators = helper.CreateInitiators([
                [0, 1, 2, 3],
                [0, 1, 2, 3],
                [0, 1, 2, 3],
                [0, 1, 2, 3],
            ]);

            var selectors = helper.CreateSelectors([
                [1, 2, 3, 0],
                [2, 3, 0, 1],
                [3, 0, 1, 2],
                [0, 1, 2, 3],
            ]);

            RunMatch(initiators, selectors);

            int[] expectedInitiatorMatches = [3, 0, 1, 2];
            int[] expectedSelectorMatches = [1, 2, 3, 0];

            Assert.That(helper.IsStable(initiators, selectors));
            helper.AssertMatches(initiators, selectors, expectedInitiatorMatches, expectedSelectorMatches);
        }

        [Repeat(10)]
        [Test]
        public void RandomisedStressTest()
        {
            const int matrixSize = 10;

            var initiators = helper.CreateInitiators(helper.CreateRandomPreferenceArray(matrixSize));
            var selectors = helper.CreateSelectors(helper.CreateRandomPreferenceArray(matrixSize));


            
        }

        [Test]
        public void TestOtherDataTypes()
        {
            throw new NotImplementedException();
        }

        void RunMatch(IList<Initiator> initiators, IList<Selector> selectors) => algo.Match(initiators, selectors);

        Helper helper;
        GaleShapelyAlgo algo;
    }
}