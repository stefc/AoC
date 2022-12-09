using MoreLinq;

namespace advent.of.code.y2022;

// http://adventofcode.com/2022/day/8

using Store = ImmutableHashSet<Point>;

class TreeTop : IPuzzle
{


	internal long Count(IEnumerable<string> input) {
		
		var createPointInRow = (int row) => (int index) => new Point(index, row);
		var createPointInColumn = (int col) => (int index) => new Point(col, index);

		var m = input.Select(x => x.Select(ch=>ch));
		var m1 = m.Select( x => new string(x.ToArray()).ToDigits().ToImmutableArray());
		var m2 = m.Transpose().Select( x => new string(x.ToArray()).ToDigits().ToImmutableArray());

		var visibleRows = m1
			.Select( (row,index) => new{ value=row, row=index})
			.Aggregate( Store.Empty, 
			(acc,cur) => Add(acc, createPointInRow(cur.row), cur.value ));

		var visible = m2
			.Select( (col,index) => new{ value=col, col=index})
			.Aggregate( visibleRows, 
			(acc,cur) => Add(acc, createPointInColumn(cur.col), cur.value ));
		
		return visible.Count();
	}

	internal Store Add(Store acc, Func<int,Point> createPoint, ImmutableArray<int> values)
	{
		var visibleFromLeft = values
			.Select( (x,i)=> new {height=x, pt=createPoint(i)})
			.Aggregate( (points: acc, highest: -1),
				(acc,cur) => cur.height > acc.highest ? (acc.points.Add(cur.pt), cur.height) : acc,
				acc => acc.points);

		var visible = values
			.Reverse()
			.Select( (x,i)=> new {height=x, pt=createPoint((values.Length-1)-i)})
			.Aggregate( (points: visibleFromLeft, highest: -1),
				(acc,cur) => cur.height > acc.highest ? (acc.points.Add(cur.pt), cur.height) : acc,
				acc => acc.points);
		return visible;
	}
		
	public long Silver(IEnumerable<string> input) => Count(input);

	public long Gold(IEnumerable<string> input) => 0;
}
