
namespace advent.of.code.common;

public interface IPuzzle 
{
	public long Silver(IEnumerable<string> values);
	
	public long Gold(IEnumerable<string> values);

	public long Silvered(params string[] values) => Silver(values);
	public long Golded(params string[] values) => Gold(values);
}