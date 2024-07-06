using System.Collections.Generic;


namespace Chess.Pieces
{
    public class Bishop : Piece
    {
        public Bishop(Color color, Position position, Game game)
            : base(color, position, game) { }


        protected override IEnumerable<Move> GeneratePsuedoLegalMoves()
        {
            return new List<Move>();
        }
    }
}
