using Chess.Core;
using Chess.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Chess
{
    /// <summary>
    /// A game of chess.
    /// </summary>
    public class Game
    {
        // Temporary default constructor
        public Game()
        {
            board = new Piece[8, 8];
            // Kings
            WhiteKing = new King     (Color.White, new Position(4, 0), this);
            BlackKing = new King     (Color.Black, new Position(4, 7), this);
            // White pieces (1st rank)
            board[0, 0] = new Rook   (Color.White, new Position(0, 0), this);
            board[1, 0] = new Knight (Color.White, new Position(1, 0), this);
            board[2, 0] = new Bishop (Color.White, new Position(2, 0), this);
            board[3, 0] = new Queen  (Color.White, new Position(3, 0), this);
            board[4, 0] = WhiteKing;
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
            board[4, 7] = BlackKing;
            board[5, 7] = new Bishop (Color.Black, new Position(5, 7), this);
            board[6, 7] = new Knight (Color.Black, new Position(6, 7), this);
            board[7, 7] = new Rook   (Color.Black, new Position(7, 7), this);
            // White and Black Pieces
            pieces = new List<Piece>();
            foreach (Piece? piece in board)
            {
                if (piece == null) continue;
                pieces.Add(piece);
            }
            whitePieces = pieces.Where(piece => piece.Color == Color.White).ToList();
            blackPieces = pieces.Where(piece => piece.Color == Color.Black).ToList();
            // Legal moves
            Turn = Color.White;
            CanWhiteCastleKingside = true;
            CanWhiteCastleQueenside = true;
            CanBlackCastleKingside = true;
            CanBlackCastleQueenside = true;
            legalMoves = new List<Move>();
            UpdateLegalMoves();
        }


        public Color Turn { get; private set; }

        public readonly King WhiteKing;
        public readonly King BlackKing;

        public IReadOnlyCollection<Piece> Pieces => pieces.AsReadOnly();
        public IReadOnlyCollection<Piece> WhitePieces => whitePieces.AsReadOnly();
        public IReadOnlyCollection<Piece> BlackPieces => blackPieces.AsReadOnly();

        public IReadOnlyCollection<Move> LegalMoves => legalMoves.AsReadOnly();

        public bool CanWhiteCastleKingside { get; private set; }
        public bool CanWhiteCastleQueenside { get; private set; }
        public bool CanBlackCastleKingside { get; private set; }
        public bool CanBlackCastleQueenside { get; private set; }

        private Piece?[,] board;
        private List<Piece> pieces;
        private List<Piece> whitePieces;
        private List<Piece> blackPieces;

        private List<Move> legalMoves;


        /// <summary>
        /// Gets the piece at the position on the board.
        /// </summary>
        /// <param name="x">The file (column) of the position.</param>
        /// <param name="y">The rank (row) of the position.</param>
        /// <returns>The piece at the position on the board.</returns>
        public Piece? GetPiece(int x, int y)
        {
            return GetPiece(new Position(x, y));
        }

        /// <summary>
        /// Gets the piece at the position on the board.
        /// </summary>
        /// <param name="position">The position on the board.</param>
        /// <returns>The piece at the position on the board.</returns>
        public Piece? GetPiece(Position position)
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
        public Piece? GetPiece(string algebraicNotation)
        {
            return GetPiece(new Position(algebraicNotation));
        }


        /// <summary>
        /// Retrieves the king of the specified color.
        /// </summary>
        /// <param name="color">The color of the king to retrieve.</param>
        /// <returns>The king of the specified color.</returns>
        public King GetKing(Color color)
        {
            return color == Color.White ? WhiteKing : BlackKing;
        }

        /// <summary>
        /// Retrieves the pieces of the specified color.
        /// </summary>
        /// <param name="color">The color of the pieces to retrieve.</param>
        /// <returns>The pieces of the specified color.</returns>
        public IReadOnlyCollection<Piece> GetPieces(Color color)
        {
            return color == Color.White ? WhitePieces : BlackPieces;
        }

        /// <summary>
        /// Determines if the specified color has the right to castle kingside.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>
        /// True if the color has the right to castle kingside; otherwise, false.
        /// </returns>
        public bool CanCastleKingside(Color color)
        {
            return color == Color.White ? CanWhiteCastleKingside : CanBlackCastleKingside;
        }

        /// <summary>
        /// Determines if the specified color has the right to castle queenside.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns>
        /// True if the color has the right to castle queenside; otherwise, false.
        /// </returns>
        public bool CanCastleQueenside(Color color)
        {
            return color == Color.White ? CanWhiteCastleQueenside : CanBlackCastleQueenside;
        }


        // Temporary move method
        public void Move(Move move)
        {
            int ox = move.Origin.X;
            int oy = move.Origin.Y;
            int dx = move.Destination.X;
            int dy = move.Destination.Y;
            Piece? capturedPiece = board[dx, dy];
            if (capturedPiece != null)
            {
                List<Piece> capturedPieces;
                if (capturedPiece.Color == Color.White)
                {
                    capturedPieces = whitePieces;
                }
                else
                {
                    capturedPieces = blackPieces;
                }
                capturedPieces.Remove(capturedPiece);
                pieces.Remove(capturedPiece);
            }
            board[dx, dy] = board[ox, oy];
            board[ox, oy] = null;
            Piece? piece = board[dx, dy];
            if (piece != null)
            {
                piece.Position = move.Destination;
            }
            Turn = Turn.Opposite();
            UpdateLegalMoves();
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
        /// <remarks>
        /// Intended only to be called by <c>Piece</c> for each of the
        /// <a href="https://www.chessprogramming.org/Pseudo-Legal_Move">
        /// psuedo-legal moves
        /// </a>
        /// it generates.
        /// </remarks>
        internal bool IsLegalMove(Move psuedoLegalMove)
        {
            // Variables to make indexing more convenient.
            int ox = psuedoLegalMove.Origin.X;
            int oy = psuedoLegalMove.Origin.Y;
            int dx = psuedoLegalMove.Destination.X;
            int dy = psuedoLegalMove.Destination.Y;
            // Temporarily make the move.
            Piece? capturedPiece = board[dx, dy];
            board[dx, dy] = board[ox, oy];
            board[ox, oy] = null;
            // Get the king and the pieces that might be checking the king.
            Position king = GetKing(Turn).Position;
            IReadOnlyCollection<Piece> opposingPieces = GetPieces(Turn.Opposite());
            // If the king is the piece being moved, use it's position
            // after the move rather than it's position before the move.
            if (king == psuedoLegalMove.Origin)
            {
                king = psuedoLegalMove.Destination;
            }
            // Determine if the king is being attacked or not.
            bool isKingAttacked = opposingPieces.Any(piece =>
                piece != capturedPiece && piece.IsAttacking(king));
            // Undo the move.
            board[ox, oy] = board[dx, dy];
            board[dx, dy] = capturedPiece;
            // If the king would be in check after the move is made,
            // the move is not legal; otherwise, the move is legal.
            return !isKingAttacked;
        }

        /// <summary>
        /// Determines if <paramref name="position"/> is being attacked
        /// by <paramref name="color"/>.
        /// </summary>
        /// <param name="position">The position that might be being attacked.</param>
        /// <param name="color">The color that might be attacking position.</param>
        /// <returns>True if the position is attacked; otherwise, false.</returns>
        internal bool IsAttacked(Position position, Color color)
        {
            IReadOnlyCollection<Piece> attackingPieces = GetPieces(color);
            return attackingPieces.Any(piece => piece.IsAttacking(position));
        }


        /// <summary>
        /// Updates <c>LegalMoves</c> for all pieces.
        /// Updates <c>LegalMoves</c> for this <c>Game</c>
        /// to be all legal moves.
        /// </summary>
        private void UpdateLegalMoves()
        {
            legalMoves.Clear();
            foreach (Piece piece in Pieces)
            {
                piece.UpdateLegalMoves();
                legalMoves.AddRange(piece.LegalMoves);
            }
        }
    }
}
