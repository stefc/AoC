namespace advent.of.code.tests.helper;

public static class TestHelper {

    public static IEnumerable<string> ReadPuzzle(this IPuzzle puzzle, [CallerFilePath] string sourceName = "") 
    {
		var directories = sourceName.Split(Path.DirectorySeparatorChar);
		var path = Path.Combine(directories.TakeLast(3).ToArray());  
		return File.ReadLines(Path.ChangeExtension(path, ".Input.txt"))
            .Where(line => !String.IsNullOrEmpty(line))
            .ToArray();
	}
}