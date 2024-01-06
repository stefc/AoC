namespace advent.of.code.tests.helper;

public static class VisualizeBitArray
{
	public static string Visualize(this BitArray bits, int width, int height)
	{
		var g = new GridData();

		g.TextValue = "test";
		g.ColumnLabels.Add(new GridData.ColumnLabelData("test"));
		for (int y = 0; y < height; y++)
		{
			var row = new GridData.RowData("foo");
			for (int x = 0; x < width; x++)
			{
				row.Columns.Add(new GridData.ColumnData(".", "."));
			}

			g.Rows.Add(row);
		}
		return g.ToString();
	}
}