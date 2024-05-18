namespace StageEvent2Json
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 1 || args[0].Equals("-h") || args[0].Equals("-help"))
            {
                Console.WriteLine("Usage: <input file> <output file (optional)>");
                return;
            }

            string inputFile = args[0];
            string outputFile = String.Empty;

            try
            {
                string fileExt = Path.GetExtension(inputFile);
                Console.WriteLine($"StageEvent2Json: Attempting to convert {Path.GetFileName(inputFile)}");
                if (Path.GetExtension(inputFile).Equals(".bin", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (args.Length > 1) { outputFile = args[1]; }
                    else outputFile = inputFile.Replace(".bin", ".json");

                    Console.WriteLine($"StageEvent2Json: target file is {Path.GetFileName(outputFile)}");

                    var tblFile = StageEventHandler.ReadBinary(inputFile);
                    var json = StageEventHandler.ToJson(tblFile);
                    File.WriteAllText(outputFile, json);
                }
                else if (Path.GetExtension(inputFile).Equals(".json", StringComparison.CurrentCultureIgnoreCase))
                {
                    if (args.Length > 1) { outputFile = args[1]; }
                    else outputFile = inputFile.Replace(".json", ".bin");

                    Console.WriteLine($"StageEvent2Json: target file is {Path.GetFileName(outputFile)}");

                    var json = File.ReadAllText(inputFile);
                    var tblFile = StageEventHandler.FromJson(json);
                    StageEventHandler.WriteBinary(outputFile, tblFile);
                }
                else
                {
                    Console.WriteLine("Invalid operation.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
