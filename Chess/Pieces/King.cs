using Chess.Core;
using System.Collections.Generic;


namespace Chess.Pieces
{
    public class King : Piece
    {
        internal King(Color color, Position position, Game game)
            : base(color, position, game) { }


        protected override IEnumerable<Move> GeneratePsuedoLegalMoves()
        {
            ICollection<Move> psuedoLegalMoves = new List<Move>();
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;
                    AddMove(psuedoLegalMoves, i, j);
                }
            }
            return psuedoLegalMoves;
        }
    }
}
