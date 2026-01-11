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
            var initiators = helper.CreateInitiators([new[] { 0 }]);
            var selectors = helper.CreateSelectors([new[] { 0 }]);

            RunMatch(initiators, selectors);

            Assert.That(selectors[0], Is.SameAs(initiators[0].Match));
            Assert.That(initiators[0], Is.SameAs(selectors[0].Match));
        }

        [Test]
        public void NoConflictingProposals() 
        {
            throw new NotImplementedException();
        }

        [Test]
        public void SimpleConflictingProposal() 
        {
            throw new NotImplementedException();
        }

        [Test]
        public void MoreInitiatorsThanSelectors() 
        {
            throw new NotImplementedException();
        }

        [Test]
        public void MoreSelectorsThanInitiators() 
        {
            throw new NotImplementedException();
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

        void RunMatch(IList<Initiator> initiators, IList<Selector> selectors) => algo.Match(initiators, selectors);

        Helper helper;
        GaleShapelyAlgo algo;
    }
}