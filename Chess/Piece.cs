using System.Collections.Generic;


namespace Chess
{
    public abstract class Piece
    {
        public Piece(Color color, Position position, Game game)
        {
            Color = color;
            Position = position;
            Game = game;
            LegalMoves = new List<Move>().AsReadOnly();
        }


        public readonly Color Color;
        public Position Position { get; internal set; }
        public IReadOnlyCollection<Move> LegalMoves { get; private set; }

        protected Game Game { get; }


        /// <summary>
        /// Updates <c>LegalMoves</c> to be a collection of all the legal moves
        /// that this piece can make from the current board position.
        /// </summary>
        /// <remarks>
        /// Intended only to be called by <c>Game</c> after a move is made.
        /// </remarks>
        internal void UpdateLegalMoves()
        {

        }

        /// <summary>
        /// Determines if this piece is attacking a position on the board.
        /// </summary>
        /// <param name="position">
        /// The position to check if this piece is attacking.
        /// </param>
        /// <returns>
        /// True if this piece is attacking the position; otherwise, false.
        /// </returns>
        internal bool IsAttacking(Position position)
        {
            return false;
        }


        /// <summary>
        /// Generates a collection of
        /// <a href="https://www.chessprogramming.org/Pseudo-Legal_Move">
        /// psuedo-legal moves
        /// </a>,
        /// or moves that may be legal, but do not consider
        /// whether the king is in check after the move is made.
        /// </summary>
        /// <returns>
        /// A collection of
        /// <a href="https://www.chessprogramming.org/Pseudo-Legal_Move">
        /// psuedo-legal moves
        /// </a>.
        /// </returns>
        protected abstract ICollection<Move> GeneratePsuedoLegalMoves();
    }
}
