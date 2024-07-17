using Chess.Core;
using System.Collections.Generic;


namespace Chess.Pieces
{
    public class King : Piece
    {
        internal King(Color color, Position position, Game game)
            : base(color, position, game) { }


        public override char Symbol => 'K';

        // Constants that represent the direction along the x-axis for the two different sides.
        private const int Kingside = 1;
        private const int Queenside = -1;


        protected override IEnumerable<Move> GeneratePsuedoLegalMoves()
        {
            ICollection<Move> psuedoLegalMoves = new List<Move>();
            // Non-castling moves.
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;
                    AddMove(psuedoLegalMoves, i, j);
                }
            }
            // Castling moves.
            if (Game.CanCastleKingside(Color))
            {
                AddCastlingMove(psuedoLegalMoves, Kingside);
            }
            if (Game.CanCastleQueenside(Color))
            {
                AddCastlingMove(psuedoLegalMoves, Queenside);
            }
            return psuedoLegalMoves;
        }


        /// <summary>
        /// Adds the castling move to <paramref name="psuedoLegalMoves"/>
        /// if all the
        /// <a href="https://en.wikipedia.org/wiki/Castling">requirements</a>
        /// for castling are met.
        /// </summary>
        /// <param name="psuedoLegalMoves">The collection of psuedo-legal moves to add to.</param>
        /// <param name="xDirection">The direction along the x-axis to castle.</param>
        /// <returns>True if the move is legal and was added; otherwise, false.</returns>
        private bool AddCastlingMove(ICollection<Move> psuedoLegalMoves, int xDirection)
        {
            // If there is a piece between the king and the rook,
            // or the king would move through an attacked square,
            // then castling is not legal and the move is not added.
            if (!IsCastlingPathEmpty(xDirection) || IsCastlingPathAttacked(xDirection))
            {
                return false;
            }
            // Otherwise, castling is legal, and the move is added.
            // The destination of the king when castling is 2 squares to the
            // left (when castling queenside) or right (when castling kingside).
            int destionationX = Position.X + 2 * xDirection;
            Position destination = new Position(destionationX, Position.Y);
            psuedoLegalMoves.Add(new Move(Position, destination));
            return true;
        }

        /// <summary>
        /// Determines if the path between the king and the rook is empty.
        /// </summary>
        /// <remarks>
        /// Castling is only legal if there are no pieces between the king and the rook.
        /// </remarks>
        /// <param name="xDirection">The direction along the x-axis to castle.</param>
        /// <returns>True if the path is empty; otherwise, false.</returns>
        private bool IsCastlingPathEmpty(int xDirection)
        {
            int x = Position.X + xDirection;
            while (x > 0 && x < 7)
            {
                if (Game.GetPiece(x, Position.Y) != null)
                {
                    return false;
                }
                x += xDirection;
            }
            return true;
        }

        /// <summary>
        /// Determines if the path from the king to it's castling destination is attacked.
        /// </summary>
        /// <remarks>
        /// Castling is only legal if the king does not leave,
        /// cross over, or finish on a square attacked by an enemy piece.
        /// </remarks>
        /// <param name="xDirection">The direction along the x-axis to castle.</param>
        /// <returns>True if the path is attacked; otherwise, false.</returns>
        private bool IsCastlingPathAttacked(int xDirection)
        {
            Position position = Position;
            for (int i = 0; i < 3; i++)
            {
                if (Game.IsAttacked(position, Color.Opposite()))
                {
                    return true;
                }
                position.Add(xDirection, 0, out position);
            }
            return false;
        }
    }
}
