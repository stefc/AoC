namespace advent.of.code.tests.helper;

public static class TestHelper {

    public static IEnumerable<string> ReadPuzzle<T>(this IPuzzle<T> puzzle,bool withEmptyLines = false, [CallerFilePath] string sourceName = "")
    {
		var directories = sourceName.Split(Path.DirectorySeparatorChar);
		var path = Path.Combine(directories.TakeLast(3).ToArray());
		return File.ReadLines(Path.ChangeExtension(path, ".Input.txt"))
            .Where(line => withEmptyLines || !String.IsNullOrEmpty(line))
            .ToArray();
	}
    public static IEnumerable<string> ReadPuzzle(this object puzzle, bool withEmptyLines = false, [CallerFilePath] string sourceName = "")
    {
		var directories = sourceName.Split(Path.DirectorySeparatorChar);
		var path = Path.Combine(directories.TakeLast(3).ToArray());
		return File.ReadLines(Path.ChangeExtension(path, ".Input.txt"))
            .Where(line => withEmptyLines || !String.IsNullOrEmpty(line))
            .ToArray();
	}
}
