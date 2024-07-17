using System;


namespace Chess.Core
{
    /// <summary>
    /// A chess move, moving a piece from one square to another.
    /// </summary>
    public class Move
    {
        internal Move(Position origin, Position destination, Piece? promotion = null)
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
            if (Promotion == null)
            {
                return $"{Origin}{Destination}";
            }
            else
            {
                return $"{Origin}{Destination}{Char.ToUpper(Promotion.Symbol)}";
            }
        }
    }
}
