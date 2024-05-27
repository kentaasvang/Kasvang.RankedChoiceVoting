namespace Kasvang.RankedChoiceVoting;

public class Election
{
    public List<string> Candidates { get; }
    public List<Vote> Votes { get; }
    public string? Winner { get; set; }

    public Election(List<string> candidates, List<Vote> votes)
    {
        Candidates = candidates;
        Votes = votes;
    }

    public void RunElection()
    {
        var candidateVotes = new Dictionary<string, int>();

        while (true)
        {
            foreach (var vote in Votes)
            {
                var topCandidate = vote.Rankings.FirstOrDefault();

                if (topCandidate == null) 
                    continue;

                if (candidateVotes.ContainsKey(topCandidate))
                {
                    candidateVotes[topCandidate]++;
                }
                else
                {
                    candidateVotes[topCandidate] = 1;
                }

            }

            Console.WriteLine("Current votes:");
            foreach (var candidate in candidateVotes)
            {
                Console.WriteLine($"{candidate.Key}: {candidate.Value}");
            }

            // Find the candidate with the most votes
            // make sure to handle ties
            // if there is a winner, set the Winner property and return
            // if there is no winner, remove the candidate with the least votes and replace with the next ranking
            var maxVotes = candidateVotes.Values.Max();
            var winners = candidateVotes.Where(x => x.Value == maxVotes).Select(x => x.Key).ToList();
            if (winners.Count == 1)
            {
                Console.WriteLine($"Winner is {winners.First()} with {maxVotes} votes");
                Winner = winners.First();
                return;
            }

            var minVotes = candidateVotes.Values.Min();
            var losers = candidateVotes
                .Where(x => x.Value == minVotes)
                .Select(x => x.Key);

            Console.WriteLine($"No winner yet. Removing losers: {string.Join(", ", losers)}");

            // remove the loser from all votes if they are ranked first
            foreach (var vote in Votes)
            {
                var first = vote.Rankings.FirstOrDefault();
                if (losers.Contains(first))
                {
                    vote.Rankings.RemoveAt(0);
                }
            }

            candidateVotes = new Dictionary<string, int>();
        }
    }
}

public class Vote
{
    public List<string> Rankings { get; }

    public Vote(List<string> rankings)
    {
        Rankings = rankings;
    }
}
