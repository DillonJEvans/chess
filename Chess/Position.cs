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


        public override int GetHashCode()
        {
            // X and Y can only be between 0 and 7 (inclusive).
            // The following produces unique hash codes for each position,
            // based on how squares of the chess board are commonly numbered.
            // For example: http://hgm.nubati.net/book_format.html
            // A description of the Polyglot opening book format uses:
            //   a1 == 0, h1 == 7, a2 == 8, ..., h8 == 63
            // which is reproduced by this hashing function.
            return Y * 8 + X;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Position position)
            {
                return Equals(position);
            }
            return false;
        }

        public bool Equals(Position position)
        {
            return X == position.X && Y == position.Y;
        }

        public static bool operator ==(Position lhs, Position rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Position lhs, Position rhs)
        {
            return !(lhs == rhs);
        }


        public override string ToString()
        {
            char file = (char) ('a' + X);
            int rank = Y + 1;
            return $"{file}{rank}";
        }
    }
}
