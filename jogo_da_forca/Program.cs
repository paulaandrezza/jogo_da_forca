using System.Globalization;
using System.Text;

namespace jogo_da_forca
{
    using Options;
    internal class Program
    {
        static void Main(string[] args)
        {
            int numberOption = -1;

            try
            {
                while(true) {
                    bool validChoice = false;

                    while (!validChoice)
                    {
                        Console.WriteLine("Jogo da Forca | DiverseDev");
                        Console.WriteLine("1 - Jogar com uma palavra aleatória");
                        Console.WriteLine("2 - Cadastrar nova palavra");
                        Console.WriteLine("0 - Sair");
                        Console.Write("\nEscolha uma opção: ");
                        if (
                            int.TryParse(Console.ReadLine(), out numberOption)
                            && numberOption >= 0
                            && numberOption <= 2
                        )
                        {
                            validChoice = true;
                        }
                        else
                        {
                            Console.Error.WriteLine(
                                "Opção inválida. Insira um número inteiro válido correspondente a uma opção.\n"
                            );
                            Console.WriteLine("Pressione Enter para continuar...");
                            Console.ReadLine();
                            Console.Clear();
                        }
                    }
                    Console.Clear();
                    if (numberOption == 0) break;

                    if (numberOption == 1)
                    {
                        ReadCSV csvReader = new ReadCSV();
                        var (hint, word) = csvReader.GetRandomWord();

                        Play play = new Play(word, hint);
                        play.StartGame();
                    }
                    else
                    {
                        string word, hint;

                        Console.Write("\nInforme a palavra que deseja adicionar: ");
                        word = RemoveAccents(Console.ReadLine());

                        Console.Write("Informe a dica para a palavra: ");
                        hint = Console.ReadLine();

                        ReadCSV csvReader = new ReadCSV();
                        csvReader.AddWordToCSV(hint, RemoveAccents(word));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error: " + ex.Message);
            }

            
        }

        static string RemoveAccents(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder(capacity: normalizedString.Length);

            for (int i = 0; i < normalizedString.Length; i++)
            {
                char c = normalizedString[i];
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder
                .ToString()
                .Normalize(NormalizationForm.FormC);
        }

    }
}