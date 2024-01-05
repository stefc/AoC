using MoreLinq;

namespace advent.of.code.y2023;

partial

// http://adventofcode.com/2023/day/4

class Scratchcards : IPuzzle
{

	public static Card ParseCard(string card)
	{
		var cardRegex = CardRegex();

		// how to parse a string with the following format into a Card object?
		// Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53

		var cardMatch = cardRegex.Match(card);
		if (!cardMatch.Success)
			throw new ArgumentException("Invalid game record format.");

		var cardNumber = int.Parse(cardMatch.Groups[1].Value);
		var numbersString = cardMatch.Groups[2].Value.Split('|');
		var numbers = numbersString.Select(numbers => numbers.Split(' ')
			.Select(number => int.TryParse(number, out var num) ? num : -1)
			.Where(number => number > 0)
			.ToImmutableHashSet());

		return new Card(cardNumber, numbers.First(), numbers.Last());
	}

	public long Silver(IEnumerable<string> input) => input.Select(ParseCard).Sum(card => card.Score);

	public long Gold(IEnumerable<string> input)
	{
		var cards = input.Select(ParseCard).ToImmutableList();

		var copies = cards.ToDictionary(card => card.CardNumber, _ => 1);

		cards.Select((card, index) => new { Card = card, Index = index })
			.ForEach(item =>
			{
				for (int i = 0; i < item.Card.Matches && item.Index + i + 1 < cards.Count; i++)
					copies[cards[item.Index + i + 1].CardNumber] += copies[item.Card.CardNumber];
			});

		return copies.Values.Sum();
	}

	[GeneratedRegex(@"Card\s+(\d+): (.+)")]
	private static partial Regex CardRegex();

	internal record Card(int CardNumber, ImmutableHashSet<int> Winning, ImmutableHashSet<int> Numbers)
	{
		public int Score => (int)Math.Pow(2, this.Winning.Intersect(this.Numbers).Count - 1);

		public int Matches => this.Winning.Intersect(this.Numbers).Count;
	};

}
