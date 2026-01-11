using AdvancedAlgosAssignment1;

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
            throw new NotImplementedException();
        }

        [Test]
        public void SizeOneMatches() 
        {
            throw new NotImplementedException();
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

        Helper helper;
        GaleShapelyAlgo algo;
    }
}