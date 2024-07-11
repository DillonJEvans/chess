namespace Chess.Core
{
    /// <summary>
    /// A chess move, moving a piece from one square to another.
    /// </summary>
    public class Move
    {
        public Move(Position origin, Position destination)
        {
            Origin = origin;
            Destination = destination;
        }


        public readonly Position Origin;
        public readonly Position Destination;


        public override string ToString()
        {
            return $"{Origin}{Destination}";
        }
    }
}
