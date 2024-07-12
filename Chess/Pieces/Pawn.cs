using Chess.Core;
using System.Collections.Generic;


namespace Chess.Pieces
{
    public class Pawn : Piece
    {
        internal Pawn(Color color, Position position, Game game)
            : base(color, position, game) { }


        protected override IEnumerable<Move> GeneratePsuedoLegalMoves()
        {
            ICollection<Move> psuedoLegalMoves = new List<Move>();
            // Determine which way is forward and if this pawn has moved yet.
            int forward;
            bool hasMoved;
            if (Color == Color.White)
            {
                forward = 1;
                hasMoved = Position.Y != 1;
            }
            else
            {
                forward = -1;
                hasMoved = Position.Y != 6;
            }
            // Non-capturing moves.
            bool isForwardClear = AddMove(psuedoLegalMoves, 0, forward, addCaptures: false);
            if (isForwardClear && !hasMoved)
            {
                AddMove(psuedoLegalMoves, 0, 2 * forward, addCaptures: false);
            }
            // Capturing moves.
            AddMove(psuedoLegalMoves, -1, forward, addNonCaptures: false);
            AddMove(psuedoLegalMoves,  1, forward, addNonCaptures: false);
            return psuedoLegalMoves;
        }
    }
}
