using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedAlgosAssignment1.Models
{
    internal class Selector : Participant
    {
        Dictionary<Initiator, int> preferenceDict;

        public Selector(int[] preferences)
            :base(preferences)
        {
            preferenceDict = new Dictionary<Initiator, int>();
        }

        public void InitialisePreferenceDict(Collection<Initiator> initiators)
        {
            if (Preferences.Length != initiators.Count)
            {
                throw new ArgumentException("Pref size not same as init size");
            }

            for (int i = 0; i < initiators.Count; i++)
            {
                var preference = Preferences[i];
                var initiator = initiators[preference];

                preferenceDict.Add(initiator, i);
            }
        }

        public bool Prefers(Initiator newProposal)
        {
            //O(1) lookup here saves this algo from being O(n^3)
            //object.GetHashCode does a good enough job to ensure O(1) essentially always
            return preferenceDict[newProposal] < preferenceDict[(Initiator)Match];
        }
    }
}
