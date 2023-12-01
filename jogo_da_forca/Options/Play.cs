using System;
using System.Collections.Generic;

namespace jogo_da_forca.Options
{
    internal class Play
    {
        private string _word;
        private string _hint;
        private List<char> _guessedLetters;
        private int _remainingChances;

        public Play(string word, string hint)
        {
            _word = word.ToLower();
            _hint = hint;
            _guessedLetters = new List<char>();
            _remainingChances = 6;
        }

        public void StartGame()
        {
            while (!IsGameOver())
            {
                Console.WriteLine($"A dica é: {_hint}");
                displayGuessedLetters();
                DisplayHangman();
                DisplayWordStatus();
                Console.WriteLine($"\n\nChances restantes: {_remainingChances}");
                Console.Write("Digite uma letra: ");
                char guess = Console.ReadLine().ToLower()[0];

                if (IsValidGuess(guess))
                {
                    _guessedLetters.Add(guess);
                    Console.Clear();
                }
            }

            Console.WriteLine($"A palavra era: {_word}");
            Console.WriteLine("Fim do jogo!");
            Console.WriteLine("Pressione Enter para continuar...");
            Console.ReadLine();
            Console.Clear();
        }

        private void displayGuessedLetters()
        {
            Console.Write("Letras já inseridas: ");
            Console.WriteLine(string.Join(", ", _guessedLetters));
        }

        private void DisplayWordStatus()
        {
            Console.Write("_|_  ");
            foreach (char letter in _word)
            {
                if (_guessedLetters.Contains(letter))
                {
                    Console.Write($"{letter} ");
                }
                else
                {
                    Console.Write("_ ");
                }
            }

            Console.WriteLine();
        }

        private void DisplayHangman()
        {
            Console.WriteLine();
            switch (_remainingChances)
            {
                case 6:
                    Console.WriteLine("  ____ ");
                    Console.WriteLine(" |    |");
                    Console.WriteLine(" |      ");
                    Console.WriteLine(" |      ");
                    Console.WriteLine(" |      ");
                    Console.WriteLine(" |      ");
                    break;
                case 5:
                    Console.WriteLine("  ____ ");
                    Console.WriteLine(" |    |");
                    Console.WriteLine(" |    O");
                    Console.WriteLine(" |      ");
                    Console.WriteLine(" |      ");
                    Console.WriteLine(" |      ");
                    break;
                case 4:
                    Console.WriteLine("  ____ ");
                    Console.WriteLine(" |    |");
                    Console.WriteLine(" |    O");
                    Console.WriteLine(" |    | ");
                    Console.WriteLine(" |      ");
                    Console.WriteLine(" |      ");
                    break;
                case 3:
                    Console.WriteLine("  ____ ");
                    Console.WriteLine(" |    |");
                    Console.WriteLine(" |    O");
                    Console.WriteLine(" |   /| ");
                    Console.WriteLine(" |      ");
                    Console.WriteLine(" |      ");
                    break;
                case 2:
                    Console.WriteLine("  ____ ");
                    Console.WriteLine(" |    |");
                    Console.WriteLine(" |    O");
                    Console.WriteLine(" |   /|\\");
                    Console.WriteLine(" |      ");
                    Console.WriteLine(" |      ");
                    break;
                case 1:
                    Console.WriteLine("  ____ ");
                    Console.WriteLine(" |    |");
                    Console.WriteLine(" |    O");
                    Console.WriteLine(" |   /|\\");
                    Console.WriteLine(" |   /  ");
                    Console.WriteLine(" |      ");
                    break;
                case 0:
                    Console.WriteLine("  ____ ");
                    Console.WriteLine(" |    |");
                    Console.WriteLine(" |    O");
                    Console.WriteLine(" |   /|\\");
                    Console.WriteLine(" |   / \\");
                    Console.WriteLine(" |      ");
                    break;
            }
        }

        private bool IsGameOver()
        {
            if (_remainingChances <= 0)
            {
                Console.WriteLine("Game over! Você perdeu!");
                return true;
            }

            foreach (char letter in _word)
            {
                if (!_guessedLetters.Contains(letter))
                {
                    return false;
                }
            }

            Console.WriteLine("Parabéns! Você acertou a palavra!");
            return true;
        }

        private bool IsValidGuess(char guess)
        {

            if (Char.IsLetter(guess))
            {
                if (!_word.Contains(guess) && !_guessedLetters.Contains(guess))
                {
                    _remainingChances--;
                }
                else if (_guessedLetters.Contains(guess))
                {
                    Console.WriteLine("\nVocê já inseriu essa letra. Tente novamente!");
                    Console.WriteLine("Pressione Enter para continuar...");
                    Console.ReadLine();
                    Console.Clear();
                    return false;
                }
                return true;
            }

            Console.WriteLine("\nLetra inválida. Tente novamente.");
            Console.WriteLine("Pressione Enter para continuar...");
            Console.ReadLine();
            Console.Clear();
            return false;
        }

    }
}
