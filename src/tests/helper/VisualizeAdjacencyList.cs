namespace advent.of.code.tests.helper;

public static class VisualizeAdjacencyList
{
	public static string Visualize(this AdjacencyList ll)
	{
		var m = ll.Matrix.Keys.OrderBy(x => x).ToArray();

		var d = new GraphData();

		d.Nodes = m.Select( n => new GraphData.NodeData(ll.Resolve(n)) { Label = ll.Resolve(n) }).ToArray();
		d.Edges = m.SelectMany( n => ll.Matrix[n], (xs, e) => new GraphData.EdgeData( ll.Resolve(xs), ll.Resolve(e.end)) { Label = e.weight.ToString()}).ToArray();

		return d.ToString();
	}
}