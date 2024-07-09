namespace Chess.Tests
{
    internal class Program
    {
        static void Main()
        {
            Game game = new();
            foreach (Move move in game.LegalMoves)
            {
                Console.WriteLine(move);
            }
        }
    }
}
