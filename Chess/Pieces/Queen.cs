using System.Collections.Generic;


namespace Chess.Pieces
{
    public class Queen : Piece
    {
        public Queen(Color color, Position position, Game game)
            : base(color, position, game) { }


        protected override IEnumerable<Move> GeneratePsuedoLegalMoves()
        {
            return new List<Move>();
        }
    }
}
