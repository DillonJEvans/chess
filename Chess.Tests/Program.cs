using Chess.Core;
using System.Diagnostics;
using System.Text;


namespace Chess.Tests
{
    internal class Program
    {
        public static void Main()
        {
            Stopwatch? stopwatch = Stopwatch.StartNew();
            Game game = new();
            stopwatch.Stop();
            string? input;
            do
            {
                Console.Clear();
                Console.WriteLine($"Time Elapsed: {stopwatch.ElapsedTicks / 10000f} ms");
                Console.WriteLine();
                Console.WriteLine(GameToAscii(game));
                Console.WriteLine();
                Console.WriteLine($"{game.Turn}'s Turn");
                Console.WriteLine();
                Console.WriteLine($"{game.LegalMoves.Count} Legal Moves:");
                Console.WriteLine(string.Join(", ", game.LegalMoves));
                Console.WriteLine();
                input = Console.ReadLine();
                foreach (LegalMove move in game.LegalMoves)
                {
                    if (input == move.San || input == move.Uci)
                    {
                        stopwatch = Stopwatch.StartNew();
                        game.Move(move);
                        stopwatch.Stop();
                        break;
                    }
                }
            } while (input != null && input.Trim().ToLower() != "q");
        }


        private static string GameToAscii(Game game)
        {
            StringBuilder asciiGame = new StringBuilder();
            AppendFiles(asciiGame);
            asciiGame.AppendLine();
            AppendBorder(asciiGame);
            for (int y = 7; y >= 0; y--)
            {
                asciiGame.Append(y + 1);
                asciiGame.Append("  | ");
                for (int x = 0; x <= 7; x++)
                {
                    Piece? piece = game.GetPiece(x, y);
                    asciiGame.Append(piece?.ColorSymbol ?? ' ');
                    asciiGame.Append(" | ");
                }
                asciiGame.Append(' ');
                asciiGame.Append(y + 1);
                asciiGame.AppendLine();
                AppendBorder(asciiGame);
            }
            AppendFiles(asciiGame);
            return asciiGame.ToString();
        }

        private static void AppendFiles(StringBuilder asciiGame)
        {
            asciiGame.Append("  ");
            for (char c = 'a'; c <= 'h'; c++)
            {
                asciiGame.Append("   ");
                asciiGame.Append(c);
            }
        }

        private static void AppendBorder(StringBuilder asciiGame)
        {
            asciiGame.Append("   +");
            for (int i = 0; i < 8; i++)
            {
                asciiGame.Append("---+");
            }
            asciiGame.AppendLine();
        }
    }
}
