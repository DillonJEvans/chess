namespace Chess
{
    public class Game
    {
        public Color Turn { get; private set; }


        /// <summary>
        /// Determines if the
        /// <a href="https://www.chessprogramming.org/Pseudo-Legal_Move">
        /// psuedo-legal move
        /// </a>
        /// is legal or not.
        /// </summary>
        /// <param name="psuedoLegalMove">
        /// A
        /// <a href="https://www.chessprogramming.org/Pseudo-Legal_Move">
        /// psuedo-legal move
        /// </a>.
        /// </param>
        /// <returns>
        /// True if the
        /// <a href="https://www.chessprogramming.org/Pseudo-Legal_Move">
        /// psuedo-legal move
        /// </a>
        /// is legal; otherwise, false.
        /// </returns>
        internal bool IsLegalMove(Move psuedoLegalMove)
        {
            return false;
        }
    }
}
