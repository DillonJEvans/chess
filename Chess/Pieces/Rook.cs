using Chess.Core;
using System.Collections.Generic;


namespace Chess.Pieces
{
    public class Rook : Piece
    {
        internal Rook(Color color, Position position, Game game)
            : base(color, position, game) { }


        protected override IEnumerable<Move> GeneratePsuedoLegalMoves()
        {
            ICollection<Move> psuedoLegalMoves = new List<Move>();
            AddMovesAlongRay(psuedoLegalMoves,  0,  1); // Up
            AddMovesAlongRay(psuedoLegalMoves,  1,  0); // Right
            AddMovesAlongRay(psuedoLegalMoves,  0, -1); // Down
            AddMovesAlongRay(psuedoLegalMoves, -1,  0); // Left
            return psuedoLegalMoves;
        }
    }
}
