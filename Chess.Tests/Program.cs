namespace Chess.Tests
{
    internal class Program
    {
        static void Main()
        {
            Game game = new();
            Console.WriteLine($"Moves: {game.LegalMoves.Count}");
            foreach (Move move in game.LegalMoves)
            {
                Console.WriteLine(move);
            }
        }
    }
}
