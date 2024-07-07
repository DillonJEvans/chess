using Chess.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Chess
{
    public class Game
    {
        // Temporary default constructor
        public Game()
        {
            board = new Piece[8, 8];
            // White pieces (1st rank)
            board[0, 0] = new Rook   (Color.White, new Position(0, 0), this);
            board[1, 0] = new Knight (Color.White, new Position(1, 0), this);
            board[2, 0] = new Bishop (Color.White, new Position(2, 0), this);
            board[3, 0] = new Queen  (Color.White, new Position(3, 0), this);
            board[4, 0] = new King   (Color.White, new Position(4, 0), this);
            board[5, 0] = new Bishop (Color.White, new Position(5, 0), this);
            board[6, 0] = new Knight (Color.White, new Position(6, 0), this);
            board[7, 0] = new Rook   (Color.White, new Position(7, 0), this);
            // White pawns (2nd rank)
            board[0, 1] = new Pawn   (Color.White, new Position(0, 1), this);
            board[1, 1] = new Pawn   (Color.White, new Position(1, 1), this);
            board[2, 1] = new Pawn   (Color.White, new Position(2, 1), this);
            board[3, 1] = new Pawn   (Color.White, new Position(3, 1), this);
            board[4, 1] = new Pawn   (Color.White, new Position(4, 1), this);
            board[5, 1] = new Pawn   (Color.White, new Position(5, 1), this);
            board[6, 1] = new Pawn   (Color.White, new Position(6, 1), this);
            board[7, 1] = new Pawn   (Color.White, new Position(7, 1), this);
            // Black pawns (7th rank)
            board[0, 6] = new Pawn   (Color.Black, new Position(0, 6), this);
            board[1, 6] = new Pawn   (Color.Black, new Position(1, 6), this);
            board[2, 6] = new Pawn   (Color.Black, new Position(2, 6), this);
            board[3, 6] = new Pawn   (Color.Black, new Position(3, 6), this);
            board[4, 6] = new Pawn   (Color.Black, new Position(4, 6), this);
            board[5, 6] = new Pawn   (Color.Black, new Position(5, 6), this);
            board[6, 6] = new Pawn   (Color.Black, new Position(6, 6), this);
            board[7, 6] = new Pawn   (Color.Black, new Position(7, 6), this);
            // Black pieces (8th rank)
            board[0, 7] = new Rook   (Color.Black, new Position(0, 7), this);
            board[1, 7] = new Knight (Color.Black, new Position(1, 7), this);
            board[2, 7] = new Bishop (Color.Black, new Position(2, 7), this);
            board[3, 7] = new Queen  (Color.Black, new Position(3, 7), this);
            board[4, 7] = new King   (Color.Black, new Position(4, 7), this);
            board[5, 7] = new Bishop (Color.Black, new Position(5, 7), this);
            board[6, 7] = new Knight (Color.Black, new Position(6, 7), this);
            board[7, 7] = new Rook   (Color.Black, new Position(7, 7), this);
            // Kings
            WhiteKing = (King) board[4, 0];
            BlackKing = (King) board[4, 7];
            // White and Black Pieces
            IList<Piece> whitePieces = new List<Piece>();
            IList<Piece> blackPieces = new List<Piece>();
            foreach (Piece piece in board)
            {
                if (piece == null) continue;
                if (piece.Color == Color.White) whitePieces.Add(piece);
                if (piece.Color == Color.Black) blackPieces.Add(piece);
            }
            WhitePieces = whitePieces.ToList().AsReadOnly();
            BlackPieces = blackPieces.ToList().AsReadOnly();
        }


        public Color Turn { get; private set; }

        public readonly King WhiteKing;
        public readonly King BlackKing;

        public IReadOnlyCollection<Piece> WhitePieces { get; private set; }
        public IReadOnlyCollection<Piece> BlackPieces { get; private set; }

        private Piece[,] board;


        /// <summary>
        /// Gets the piece at the position on the board.
        /// </summary>
        /// <param name="x">The file (column) of the position.</param>
        /// <param name="y">The rank (row) of the position.</param>
        /// <returns>The piece at the position on the board.</returns>
        public Piece GetPiece(int x, int y)
        {
            return GetPiece(new Position(x, y));
        }

        /// <summary>
        /// Gets the piece at the position on the board.
        /// </summary>
        /// <param name="position">The position on the board.</param>
        /// <returns>The piece at the position on the board.</returns>
        public Piece GetPiece(Position position)
        {
            return board[position.X, position.Y];
        }

        /// <summary>
        /// Gets the piece at the position on the board.
        /// </summary>
        /// <param name="algebraicNotation">
        /// The position on the board, represented by
        /// <a href="https://en.wikipedia.org/wiki/Algebraic_notation_(chess)">
        /// algebraic notation
        /// </a>.
        /// </param>
        /// <returns>The piece at the position on the board.</returns>
        public Piece GetPiece(string algebraicNotation)
        {
            return GetPiece(new Position(algebraicNotation));
        }


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
