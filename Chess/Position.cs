using System.Diagnostics.Contracts;


namespace Chess
{
    public readonly struct Position
    {
        public Position(int x, int y)
        {
            Contract.Requires(x < 0 || x > 7, "X must be between 0 and 7.");
            Contract.Requires(y < 0 || y > 7, "Y must be between 0 and 7.");
            X = x;
            Y = y;
        }

        public Position(string algebraicNotation)
        {
            Contract.Requires(algebraicNotation.Length != 2, "Algebraic notation must have a length of exactly 2.");
            int x = algebraicNotation[0] - 'a';
            int y = algebraicNotation[1] - '1';
            Contract.Requires(x < 0 || x > 7, "File must be between a and h.");
            Contract.Requires(y < 0 || y > 7, "Rank must be between 1 and 8.");
            X = x;
            Y = y;
        }


        public readonly int X;
        public readonly int Y;

        public readonly int File => X;
        public readonly int Rank => Y;

        public readonly int Column => X;
        public readonly int Row => Y;


        public override string ToString()
        {
            char file = (char) ('a' + X);
            int rank = Y + 1;
            return $"{file}{rank}";
        }
    }
}
