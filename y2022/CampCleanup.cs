using MoreLinq;

namespace advent.of.code.y2022.day4;

// http://adventofcode.com/2022/day/4

class CampCleanup : IPuzzle
{

	internal int CountingContaining(IEnumerable<string> input) {
		return input.Select( ToRanges ).Count( x => Contains(x.First(), x.Last()));
	}

	internal int CountingOverlaping(IEnumerable<string> input) {
		return input.Select( ToRanges ).Count( x => Overlaps(x.First(), x.Last()));
	}

	internal bool Contains( (int start, int end) a, (int start, int end) b ) 
	=> (a.start >= b.start && a.end <= b.end) || 
		(b.start >= a.start && b.end <= a.end);
	
	internal bool Overlaps( (int start, int end) a, (int start, int end) b ) 
	=> (a.start <= b.end) && (a.end >= b.start);
	
	internal (int start, int end)[] ToRanges(string input) => input.Split(',').Select( x => x.ExtractRange()).ToArray();

	public long Silver(IEnumerable<string> input) => CountingContaining(input);

	public long Gold(IEnumerable<string> input) => CountingOverlaping(input);


}
