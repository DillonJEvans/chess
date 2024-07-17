using Chess.Core;
using System.Collections.Generic;


namespace Chess.Pieces
{
    public class Bishop : Piece
    {
        internal Bishop(Color color, Position position, Game game)
            : base(color, position, game) { }


        public override char Symbol => 'B';


        protected override IEnumerable<Move> GeneratePsuedoLegalMoves()
        {
            ICollection<Move> psuedoLegalMoves = new List<Move>();
            AddMovesAlongRay(psuedoLegalMoves,  1,  1); // Up   Right
            AddMovesAlongRay(psuedoLegalMoves,  1, -1); // Down Right
            AddMovesAlongRay(psuedoLegalMoves, -1, -1); // Down Left
            AddMovesAlongRay(psuedoLegalMoves, -1,  1); // Up   Left
            return psuedoLegalMoves;
        }
    }
}
