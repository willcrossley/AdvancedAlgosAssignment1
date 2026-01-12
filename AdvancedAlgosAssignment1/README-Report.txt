See https://docs.google.com/document/d/1-VmTA8whdhvPpgUbzLLpbu6D7r8s3tkLWUa6RQW445Q/edit?usp=sharing for the formatted report in google docs

Otherwise:
-----------Overview-----------
The Gale-Shapley algorithm is a popular match making algorithm to pair two sets of participants based on their preferences for the other set.
Its goal is to find a stable matching where no two participants would prefer each other over their current matching. 
It does this in rounds where:
Each initiator without a match proposes to their highest priority selector to whom they have not yet proposed.
Upon receiving a proposal, a selector checks their own priorities, and if the new initiator is better than 
their current (no match yet is considered worst), they will be matched. If the selector was previously matched, their unmatched suitor would rejoin the queue.
It's worth noting that for most problems, there is a set of stable solutions, and this approach guarantees the best (average match is higher on the preference list) solution for the initiators.
This approach also guarantees everyone gets a match in equal set sizes, and in unequal sets, the only participants without a match will be the ones left from the larger set when there are no matches left in the smaller set.
This algorithm is prone to some game theory where selectors or groups of initiators could alter their preferences to gain a better match, so it is best to keep the preferences of parties private where possible.

-----------Application domain-----------
This algorithm is used in many places, from dating, education & job matches, donor organ matches (heavily modified), or many other cases where matching based on the following trade offs is desired.
There are other options for the stable matching problem, so developers solving this problem need to know and choose the trade offs.
For the positives: deterministic/stability guarantee, easy modification by changing preference criteria (in my implementation, modifying the Prefers method), large research base for possible modifications, and good performance.
For the negatives: Favours the initiator (doesn’t produce globally optimal result), not strategy-proof, requires complete preference lists (in original algorithm, can be modified for incomplete preference lists), no ability to represent equal preferences, no ability to modify preferences without a recalculation of the whole thing, and performance issues around high correlation sets (where many preferences are shared, creates big bottlenecks).
Anyone planning to implement a stable matching solution needs to be aware of and consider each of these points to see whether Gale-Shapley is the right fit for them.

-----------Complexity-----------
The theoretical worst-case time complexity of the Gale–Shapley algorithm is O(n^2), as at most, every initiator has to make a proposal to every selector (assuming O(1) for the selector to decide if a proposal is better than their current match). However, a naive implementation that performs linear preference scans on every proposal can degrade to O(n^3). In practice, the actual running of the algorithm is almost always less than that, as almost always, most initiators will not have to loop through their entire preference list to find a match if you use a queue to store unmatched initiators (important), and the O(n^2) comes from a prerun step having to populate a dictionary, but if you can argue this dictionary should already be populated the best case scenario drops to O(n^2), and omega(n)).
For space complexity (I will be doing auxiliary space complexity), it depends on which time complexity approach is used.
For space complexity (I will be talking about auxiliary complexity, as total is mainly dictated by the input and output data types, doesn’t change much, and isn’t meaningful in the same way the auxiliary complexity is):
For the O(n^3) approach, no new data needs to be generated (the difference in approach involves looping through the selectors' preference list every time a proposal is made to them, to determine whether their current match is better than the proposal).
For the O(n^2) approach, a dictionary is required to store the selectors' preference list, so it is O(n) per selector, and the overall auxiliary space complexity 

My implementation uses the dictionary approach, so has a constant (best & worst case) setup step of O(n^2). Each actual round of proposals is O(1) as it is a reasonable assumption C# Dictionary<K,V> with the object reference as a key is O(1), and for every initiator, the worst case is they have to propose to each selector, so O(n^2) proposals. I chose this as practically, even though an absolute worst case of O(n^2 + n^2) iterations, this still beats the O(n^3) solution in cases where n > 2, and it is almost never actually O(n^2 + n^2). Also, worth considering the O(n^2) step is just a dictionary population, so it is not very costly per iteration.

-----------Compromises & design choices-----------
The main compromise I chose was the one above, where I chose O(n^2) time complexity and O(n) space complexity over no extra space complexity and O(n^3) time complexity. Another compromise was choosing to represent the preference lists as an int[] rather than object references. Object references would be a more accurate to a real world approach, but requires both sets to be created before the preference lists are populated, so for simplicity in the tests I went with a design that mimicked using the Participant/Initiator/Selector as a wrapper class around whatever business object you are doing the selections for, so it makes sense that their preferences can be stored as an array as it would be constructed at the time the algorithm is run and only the match is returned to the actual business object. 

I also chose not to allow for incomplete preference lists or equal preferences, as this is not really an inclusion in the standard Gale-Shapley, and doing so wouldn’t make this a regular Gale-Shapley implementation.

I chose the OO approach since it significantly improves readability, robustness (less chance of one array being misconfigured and causing the whole thing to break), as a form of anti plagarism proof as most examples etc found online would be generic using a group of arrays etc, and also to more closely match what is seen in the real world, you would more likely develop an algo like this to match existing business objects not a group of arrays.

-----------Tests-----------
The tests validate correctness by covering worst-case rejection chains, unequal set sizes, and deterministic preference configurations. Tests explicitly assert stability by verifying that no initiator/selector pair exists that would mutually prefer each other over their assigned matches. This provides confidence that the implementation adheres to the formal definition of a stable matching and the Gale-Shapley chosen set.
The tests purposefully do not check for common but non-standard features of the Gale-Shapley, like equal preference or incomplete preference array, as these were out of scope for this implementation, as the core algorithm doesn't include this. 

-----------Insights-----------
For insights, the main one would be choosing between the O(n^2) and O(n^3) approach based on hardware and situation limitations, but would almost always recommend the O(n^2) approach.
The second insight is to use a dictionary for selector preferences to achieve the O(n^2) complexity, as this is not immediately obvious, but is a massive improvement in most cases.
Also, ensure you store the current index the initiator is at in their preference array. Without it, the implementation is more complicated and causes unnecessary proposals.

References:
https://en.wikipedia.org/wiki/Gale%E2%80%93Shapley_algorithm
https://datingmansecrets.com/how-does-hinge-algorithm-work/#:~:text=Hinge's%20system%20is%20based%20on,of%20getting%20a%20like%20back. 
https://medium.com/@mohith.j/cracking-the-code-of-compatibility-exploring-the-gale-shapley-algorithm-for-stable-matching-63687ec3c646 

