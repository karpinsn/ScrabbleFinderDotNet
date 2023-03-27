// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

if (args.Length == 0)
    throw new ArgumentException(nameof(args));

for (int i = 0; i < 20; ++i)
{
    var letters = SortString(args[0]);

    using var fs = File.Open("words_alpha.txt", new FileStreamOptions() { BufferSize = 0 });
    using var sr = new StreamReader(fs);

    string? word;
    while ((word = await sr.ReadLineAsync()) != null)
    {
        string sorted = SortString(word);
        int lettersIndex = 0;
        int wordIndex = 0;

        while (lettersIndex < letters.Length && wordIndex < sorted.Length)
        {
            if (letters[lettersIndex] == sorted[wordIndex])
            {
                // Letters are the same, increment both
                lettersIndex++;
                wordIndex++;
            }
            else if (sorted[wordIndex] < letters[lettersIndex])
            {
                // We don't have the needed letter
                break;
            }
            else
            {
                // We don't need this letter in the word
                lettersIndex++;
            }

            if (wordIndex == sorted.Length)
            {
                Console.WriteLine(word);
            }
        }
    }
}

string SortString(string toSort)
{
    return String.Concat(toSort.OrderBy(c => c));
}