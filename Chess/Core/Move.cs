namespace Chess.Core
{
    /// <summary>
    /// A chess move, moving a piece from one square to another.
    /// </summary>
    public class Move
    {
        public Move(Position origin, Position destination, Piece? promotion = null)
        {
            Origin = origin;
            Destination = destination;
            Promotion = promotion;
        }


        public readonly Position Origin;
        public readonly Position Destination;
        public readonly Piece? Promotion;


        public override string ToString()
        {
            return $"{Origin}{Destination}";
        }
    }
}
