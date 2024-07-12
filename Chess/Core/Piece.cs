using System.Collections.Generic;
using System.Linq;


namespace Chess.Core
{
    /// <summary>
    /// A chess piece.
    /// </summary>
    public abstract class Piece
    {
        internal Piece(Color color, Position position, Game game)
        {
            Color = color;
            Position = position;
            Game = game;
            legalMoves = new List<Move>();
        }


        public readonly Color Color;
        public Position Position { get; internal set; }
        public IReadOnlyCollection<Move> LegalMoves => legalMoves.AsReadOnly();

        protected Game Game { get; }

        private List<Move> legalMoves;


        /// <summary>
        /// Updates <c>LegalMoves</c> to be a collection of all the legal moves
        /// that this piece can make from the current board position.
        /// </summary>
        /// <remarks>
        /// Intended only to be called by <c>Game</c> after a move is made.
        /// </remarks>
        internal void UpdateLegalMoves()
        {
            legalMoves.Clear();
            // If it is not this piece's turn, it cannot move.
            if (Color != Game.Turn)
            {
                return;
            }
            // Otherwise, add every legal move to the legalMoves list.
            IEnumerable<Move> psuedoLegalMoves = GeneratePsuedoLegalMoves();
            foreach (Move psuedoLegalMove in psuedoLegalMoves)
            {
                if (Game.IsLegalMove(psuedoLegalMove))
                {
                    legalMoves.Add(psuedoLegalMove);
                }
            }
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
            IEnumerable<Move> psuedoLegalMoves = GeneratePsuedoLegalMoves();
            return psuedoLegalMoves.Any(move => move.Destination == position);
        }


        /// <summary>
        /// Generates an enumerable of
        /// <a href="https://www.chessprogramming.org/Pseudo-Legal_Move">
        /// psuedo-legal moves
        /// </a>,
        /// or moves that may be legal, but do not consider
        /// whether the king is in check after the move is made.
        /// </summary>
        /// <returns>
        /// An enumerable of
        /// <a href="https://www.chessprogramming.org/Pseudo-Legal_Move">
        /// psuedo-legal moves
        /// </a>.
        /// </returns>
        protected abstract IEnumerable<Move> GeneratePsuedoLegalMoves();


        /// <summary>
        /// Potentially adds a move to <paramref name="psuedoLegalMoves"/>.
        /// The origin of the move would be <c>Position</c>,
        /// and the destination would be <c>Position</c> plus the given delta.
        /// The move is not added if the destination
        /// is off the board or occupied by a friendly piece.
        /// </summary>
        /// <param name="psuedoLegalMoves">The collection of psuedo-legal moves to add to.</param>
        /// <param name="deltaX">
        /// The difference in X from <c>Position</c> to the destination.
        /// </param>
        /// <param name="deltaY">
        /// The difference in Y from <c>Position</c> to the destination.
        /// </param>
        /// <param name="addNonCaptures">Whether or not non-capturing moves will be added.</param>
        /// <param name="addCaptures">Whether or not capturing moves will be added.</param>
        /// <returns>True if the move was added; otherwise, false.</returns>
        protected bool AddMove(ICollection<Move> psuedoLegalMoves,
                               int deltaX,
                               int deltaY,
                               bool addNonCaptures = true,
                               bool addCaptures = true)
        {
            // Don't add the move if the destination is not on the board.
            if (!Position.Add(deltaX, deltaY, out Position destination))
            {
                return false;
            }
            Piece? capturedPiece = Game.GetPiece(destination);
            bool isCapture = capturedPiece != null;
            // If the destination is occupied by a friendly piece, do not add the move.
            if (capturedPiece?.Color == Color)
            {
                return false;
            }
            // If the move is not a capture and non-captures aren't added,
            // or if the move is a capture and captures aren't added,
            // don't add the move.
            if ((!isCapture && !addNonCaptures) || (isCapture && !addCaptures))
            {
                return false;
            }
            // Add the move.
            psuedoLegalMoves.Add(new Move(Position, destination));
            return true;
        }

        /// <summary>
        /// Starts at <c>Position</c>, going in the given direction one square
        /// at a time, adding moves to <paramref name="psuedoLegalMoves"/>
        /// that go from <c>Position</c> to each square.
        /// Stops once the end of the board or another piece is found.
        /// </summary>
        /// <param name="psuedoLegalMoves">
        /// The collection of psuedo-legal moves to add to.
        /// </param>
        /// <param name="directionX">The X of the ray's direction.</param>
        /// <param name="directionY">The Y of the ray's direction.</param>
        protected void AddMovesAlongRay(ICollection<Move> psuedoLegalMoves,
                                        int directionX,
                                        int directionY)
        {
            Position destination = Position;
            Piece? capturedPiece = null;
            // Keep adding moves until a piece is found,
            // or until the end of the board is reached.
            while (capturedPiece == null &&
                   destination.Add(directionX, directionY, out destination))
            {
                capturedPiece = Game.GetPiece(destination);
                // Only add the move if the square is unoccupied,
                // or occupied by an opposing piece.
                if (capturedPiece?.Color != Color)
                {
                    psuedoLegalMoves.Add(new Move(Position, destination));
                }
            }
        }
    }
}
