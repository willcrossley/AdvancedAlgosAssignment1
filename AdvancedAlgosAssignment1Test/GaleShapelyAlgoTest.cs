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

            helper.AssertMatches(initiators, selectors, expectedInitiatorMatches, expectedSelectorMatches);
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

            helper.AssertMatches(initiators, selectors, expectedInitiatorMatches, expectedSelectorMatches);
        }

        [Test]
        public void DuplicateInitiatorPrefernces()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void DuplicateSelectorPreferences()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void AllDuplicatePreferences()
        {
            throw new NotImplementedException();
        }

        [Repeat(10)]
        [Test]
        public void RandomisedStressTest()
        {
            throw new NotImplementedException();
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