using Chess.Core;
using System.Collections.Generic;


namespace Chess.Pieces
{
    public class Queen : Piece
    {
        internal Queen(Color color, Position position, Game game)
            : base(color, position, game) { }


        public override char Symbol => 'Q';


        protected override IEnumerable<Move> GeneratePsuedoLegalMoves()
        {
            ICollection<Move> psuedoLegalMoves = new List<Move>();
            AddMovesAlongRay(psuedoLegalMoves,  0,  1); // Up
            AddMovesAlongRay(psuedoLegalMoves,  1,  1); // Up   Right
            AddMovesAlongRay(psuedoLegalMoves,  1,  0); //      Right
            AddMovesAlongRay(psuedoLegalMoves,  1, -1); // Down Right
            AddMovesAlongRay(psuedoLegalMoves,  0, -1); // Down
            AddMovesAlongRay(psuedoLegalMoves, -1, -1); // Down Left
            AddMovesAlongRay(psuedoLegalMoves, -1,  0); //      Left
            AddMovesAlongRay(psuedoLegalMoves, -1,  1); // Up   Left
            return psuedoLegalMoves;
        }
    }
}
