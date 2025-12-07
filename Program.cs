// Carlos Morales - Lab 11 Mad Libs #2

class Program
{
    static void Main(string[] filename)
    {
        if (filename.Length == 0)
        {
            Console.WriteLine("Error: File name was not provided.");
            return;
        }

        // Dictionary: category → words
        Dictionary<string, List<string>> categoryToWords = new Dictionary<string, List<string>>();

        Console.WriteLine("Creating dictionary from story files.");

        // Pass 1: Building Dictionary and collecting words with category labels
        foreach (string file in filename)
        {
            string[] lines = File.ReadAllLines(file);

            foreach (string line in lines)
            {
                string[] wordsInLine = line.Split(' ');

                foreach (string wordPart in wordsInLine)
                {
                    // Processing words with a category label
                    if (wordPart.Contains("::"))
                    {
                        string[] split = wordPart.Split("::");
                        string word = split[0];
                        string category = split[1];

                        // Adding category to dictionary if it doesn't exist
                        if (!categoryToWords.ContainsKey(category))
                            categoryToWords[category] = new List<string>();

                        // Adding word if it's not already in the list
                        if (!categoryToWords[category].Contains(word))
                            categoryToWords[category].Add(word);
                    }
                }
            }
        }

        // Will select random words from each category when generating the stories.
        Random rand = new Random();

        // Pass 2: Generating new stories
        foreach (string file in filename)
        {
            string outputFile = "generated." + file;

            // Using StreamWriter to write to the output file
            using StreamWriter writer = new StreamWriter(outputFile);

            string[] lines = File.ReadAllLines(file);

            foreach (string line in lines)
            {
                string newLine = "";
                string[] wordsInLine = line.Split(' ');

                foreach (string wordPart in wordsInLine)
                {
                    // If the word has category label, replace it with a random word
                    if (wordPart.Contains("::"))
                    {
                        string[] split = wordPart.Split("::");
                        string category = split[1];

                        if (categoryToWords.ContainsKey(category))
                        {
                            List<string> wordList = categoryToWords[category];
                            string randomWord = wordList[rand.Next(wordList.Count)];
                            newLine += randomWord + " ";
                        }
                        else
                        {
                            newLine += split[0] + " ";
                        }
                    }
                    else
                    {
                        newLine += wordPart + " ";
                    }
                }

                // Writing the completed line to the output file
                writer.WriteLine(newLine);
            }

            // Notifying that the file has been generated
            Console.WriteLine($"Generated: {outputFile}");
        }
    }
}
