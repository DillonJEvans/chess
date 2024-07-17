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

            int forward = (Color == Color.White ? 1 : -1);
            int homeRow = (Color == Color.White ? 1 : 6);
            bool hasMoved = (Position.Y != homeRow);

            if (AddNonCapturingMove(psuedoLegalMoves, 0, forward) && !hasMoved)
            {
                AddNonCapturingMove(psuedoLegalMoves, 0, 2 * forward);
            }

            AddCapturingMove(psuedoLegalMoves, -1, forward);
            AddCapturingMove(psuedoLegalMoves, 1, forward);

            return psuedoLegalMoves;
        }


        protected override bool AddMove(ICollection<Move> psuedoLegalMoves, int deltaX, int deltaY)
        {
            // If the destination is not on the board, do not add the move.
            if (!Position.Add(deltaX, deltaY, out Position destination))
            {
                return false;
            }
            // If the move does not cause the pawn to promote, just add one move.
            if (destination.Y != 0 && destination.Y != 7)
            {
                psuedoLegalMoves.Add(new Move(Position, destination));
            }
            // Otherwise, add a move for each piece that the pawn can promote to.
            else
            {
                Piece[] promotionPieces =
                {
                    new Queen(Color, destination, Game),
                    new Rook(Color, destination, Game),
                    new Bishop(Color, destination, Game),
                    new Knight(Color, destination, Game)
                };
                foreach (Piece promotionPiece in promotionPieces)
                {
                    psuedoLegalMoves.Add(new Move(Position, destination, promotionPiece));
                }
            }
            return true;
        }
    }
}
