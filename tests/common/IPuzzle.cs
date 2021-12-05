
namespace advent.of.code.common;

public interface IPuzzle 
{
	public int Silver(IEnumerable<string> values);
	
	public int Gold(IEnumerable<string> values);

	public int Silvered(params string[] values) => Silver(values);
	public int Golded(params string[] values) => Gold(values);
}