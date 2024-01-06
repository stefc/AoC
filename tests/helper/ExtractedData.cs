#nullable enable

using Newtonsoft.Json;

namespace advent.of.code.tests.helper
{
	[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
	public abstract class ExtractedData
	{
		[JsonProperty("kind")]
		public Dictionary<string, bool> Kind
		{
			get
			{
				var d = new Dictionary<string, bool>();
				foreach (var tag in Tags)
				{
					d.Add(tag, true);
				}
				return d;
			}
		}

		[JsonIgnore]
		public abstract string[] Tags { get; }

		public override string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}
	}

	public class TextData : ExtractedData
	{
		public override string[] Tags => new string[] { "text" };

		[JsonProperty("text")]
		public string TextValue { get; set; }

		public TextData(string text)
		{
			this.TextValue = text;
		}
	}

	public class GridData : ExtractedData
	{
		public override string[] Tags => new string[] { "grid" };

		[JsonProperty("text")]
		public string TextValue { get; set; }


		[JsonProperty("columnLabels")]
		public IList<ColumnLabelData> ColumnLabels { get; set; } = new List<ColumnLabelData>();


		[JsonProperty("rows")]
		public IList<RowData> Rows { get; set; } = new List<RowData>();

		[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
		public class ColumnLabelData
		{
			public ColumnLabelData(string label) => Label = label;

			[JsonProperty("label")]
			public string? Label { get; set; }
		}

		[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
		public class ColumnData
		{
			public ColumnData(string content, string tag) { this.Content = content; this.Tag = tag; }

			[JsonProperty("content")]
			public string Content { get; set; }

			[JsonProperty("tag")]
			public string Tag { get; set; }
		}

		[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
		public class RowData
		{
			public RowData(string label) { this.Label = label; }

			[JsonProperty("label")]
			public string? Label { get; set; }

			[JsonProperty("columns")]
			public IList<ColumnData> Columns { get; set; } = new List<ColumnData>();
		}
	}
	/*
    {
    "kind": { "grid": true },
    "text": "test",
    "columnLabels": [
        {
            "label": "test"
        }
    ],
    "rows": [
        {
            "label": "foo",
            "columns": [
                {
                    "content": "a",
                    "tag": "a"
                },
                {
                    "content": "b",
                    "tag": "b"
                }
            ]
        }
    ]
}
    */

	public class GraphData : ExtractedData
	{
		public override string[] Tags => new string[] { "graph" };

		[JsonProperty("nodes")]
		public IList<NodeData> Nodes { get; set; } = new List<NodeData>();

		[JsonProperty("edges")]
		public IList<EdgeData> Edges { get; set; } = new List<EdgeData>();

		[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
		public class NodeData
		{
			public NodeData(string id)
			{
				Id = id;
			}

			[JsonProperty("id")]
			public string Id { get; set; }

			[JsonProperty("label")]
			public string? Label { get; set; }

			[JsonProperty("color")]
			public string? Color { get; set; }

			[JsonProperty("shape")]
			public string? Shape { get; set; }
		}

		[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]

		public class EdgeData
		{
			public EdgeData(string from, string to)
			{
				From = from;
				To = to;
			}

			[JsonProperty("from")]
			public string From { get; set; }

			[JsonProperty("to")]
			public string To { get; set; }

			[JsonProperty("label")]
			public string? Label { get; set; }

			[JsonProperty("id")]
			public string? Id { get; set; }

			[JsonProperty("color")]
			public string? Color { get; set; }

			[JsonProperty("dashes")]
			public bool? Dashes { get; set; }
		}

		public static GraphData From<T>(IEnumerable<T> items, Func<T, NodeInfo<T>, NodeInfo<T>> f) where T : notnull
		{
			var d = new GraphData();
			var q = new Queue<T>(items);
			var i = 0;
			var infos = new Dictionary<T, NodeInfo<T>>();

			string GetId(NodeInfo<T> item)
			{
				if (item.Id == null)
					item.Id = "hediet.de/" + (i++);
				return item.Id;
			}

			NodeInfo<T> GetNodeInfo(T item)
			{
				if (infos.ContainsKey(item))
					return infos[item];

				var info = f(item, new NodeInfo<T>());
				infos.Add(item, info);
				return info;
			}

			while (q.Count > 0)
			{
				var nodeInfo = GetNodeInfo(q.Dequeue());
				var nd = new NodeData(GetId(nodeInfo));
				d.Nodes.Add(nd);

				nd.Label = nodeInfo.Label;
				nd.Color = nodeInfo.Color;
				// nd.Shape = nodeInfo.Shape;

				foreach (var e in nodeInfo.Edges)
				{
					var ed = new EdgeData(nd.Id, GetId(GetNodeInfo(e.To)));
					d.Edges.Add(ed);

					ed.Label = e.Label;
					ed.Id = e.Id;

					q.Enqueue(e.To);
				}
			}

			return d;
		}

		public class NodeInfo<T>
		{
			public IList<EdgeInfo<T>> Edges { get; set; } = new List<EdgeInfo<T>>();
			public string? Label { get; set; }
			public string? Id { get; set; }
			public string? Color { get; set; }
			public string? Shape { get; set; }

			public NodeInfo<T> AddEdge(T to, string? id = null, string? label = null)
			{
				var e = new EdgeInfo<T>(to);
				e.Id = id;
				e.Label = label;
				Edges.Add(e);
				return this;
			}
		}

		public class EdgeInfo<T>
		{
			public T To { get; set; }
			public string? Label { get; set; }
			public string? Id { get; set; }

			public EdgeInfo(T to)
			{
				To = to;
			}
		}
	}
}

