
namespace advent.of.code.common;

public interface IPuzzle<T> 
{
	public T Silver(IEnumerable<string> values);
	
	public T Gold(IEnumerable<string> values);

	public T Silvered(params string[] values) => Silver(values);
	public T Golded(params string[] values) => Gold(values);
}

public interface IPuzzle : IPuzzle<long> {
}

public interface IPuzzleTest {
	public void PuzzleSilver();
	public void PuzzleGold();
}