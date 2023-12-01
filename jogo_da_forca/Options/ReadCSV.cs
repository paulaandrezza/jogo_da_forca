using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace jogo_da_forca.Options
{
    internal class ReadCSV
    {
        private string _filePath;
        private List<(string Hint, string Word)> _words;
        private Random _random;

        public ReadCSV()
        {
            _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Options", "hangman_words.csv");
            _words = ReadCSVFile(_filePath);
            _random = new Random();
        }

        private List<(string Hint, string Word)> ReadCSVFile(string filePath)
        {
            List<(string Hint, string Word)> words = new List<(string Hint, string Word)>();

            try
            {
                var lines = File.ReadAllLines(filePath);

                foreach (var line in lines.Skip(1))
                {
                    var columns = line.Split(',');
                    if (columns.Length == 2)
                    {
                        words.Add((columns[0], columns[1]));
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Erro ao ler arquivo CSV: {ex.Message}");
            }

            return words;
        }

        public (string Hint, string Word) GetRandomWord()
        {
            if (_words.Count == 0)
            {
                throw new InvalidOperationException("Lista de palavras está vazia.");
            }

            int randomIndex = _random.Next(0, _words.Count);

            return _words[randomIndex];
        }


        public void AddWordToCSV(string hint, string word)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(_filePath, true, Encoding.UTF8))
                {
                    sw.WriteLine($"{hint},{word}");
                }

                Console.WriteLine("Palavra adicionada com sucesso!");
                Console.WriteLine("Pressione Enter para continuar...");
                Console.ReadLine();
                Console.Clear();
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Erro ao adicionar palavra ao arquivo CSV: {ex.Message}");
            }
        }
    }
}
