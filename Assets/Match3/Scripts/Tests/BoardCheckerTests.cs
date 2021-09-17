using Match3.Scripts.API.Match3;
using Match3.Scripts.API.Match3.Control;
using Match3.Scripts.Match3.Control;
using NSubstitute;
using NUnit.Framework;

namespace Match3.Scripts.Tests
{
    public class BoardCheckerTests
    {
        private const int rows = 3;
        private const int cols = 5;
        // A Test behaves as an ordinary method
        [Test]
        public void AreMatchingLeft_True()
        {
            IBoard board = Substitute.For<IBoard>();
            board.Rows.Returns(rows);
            board.Columns.Returns(cols);
            board.IsCoordinateMatchingPiece(PieceType.Blue,0, 4).Returns(true);
            board.IsOutOfRange(0, 4).Returns(false);
            board.IsCoordinateMatchingPiece(PieceType.Blue, 1, 4).Returns(true);
            board.IsOutOfRange(1, 4).Returns(false);
            // Use the Assert class to test conditions

            BoardChecker boardChecker = new BoardChecker();
            Assert.IsTrue(boardChecker.AreNeightboorsMatched(board, 2, 4, UnidirectionalDirection.Left, 1, PieceType.Blue),
                "One depth check failed");
            Assert.IsTrue(boardChecker.AreNeightboorsMatched(board, 2, 4, UnidirectionalDirection.Left, 2, PieceType.Blue),
                "Two Depth Check failed");
        }
        
        [Test]
        public void AreMatchingDiagonalUp_True()
        {
            IBoard board = Substitute.For<IBoard>();
            board.Rows.Returns(rows);
            board.Columns.Returns(cols);
            board.IsCoordinateMatchingPiece(PieceType.Red,3, 1).Returns(true);
            board.IsOutOfRange(3, 1).Returns(false);
            board.IsCoordinateMatchingPiece(PieceType.Red, 4, 2).Returns(true);
            board.IsOutOfRange(4, 2).Returns(false);
            // Use the Assert class to test conditions

            BoardChecker boardChecker = new BoardChecker();
            Assert.IsTrue(boardChecker.AreNeightboorsMatched(board, 2, 0, UnidirectionalDirection.RightUp, 1, PieceType.Red),
                "One depth check failed");
            Assert.IsTrue(boardChecker.AreNeightboorsMatched(board, 2, 0, UnidirectionalDirection.RightUp, 2, PieceType.Red),
                "Two Depth Check failed");
        }
        
        [Test]
        public void AreMatchingLeft_False_Range()
        {
            IBoard board = Substitute.For<IBoard>();
            board.Rows.Returns(rows);
            board.Columns.Returns(cols);
            //board.IsCoordinateMatchingPiece(PieceType.Blue,-1, 4).Returns(false); //ThisWouldn't Be Evaluated
            board.IsOutOfRange(-1, 4).Returns(true);
            board.IsCoordinateMatchingPiece(PieceType.Blue, 0, 4).Returns(true);
            board.IsOutOfRange(0, 4).Returns(false);
            // Use the Assert class to test conditions

            BoardChecker boardChecker = new BoardChecker();
            Assert.IsTrue(boardChecker.AreNeightboorsMatched(board, 1, 4, UnidirectionalDirection.Left, 1, PieceType.Blue),
                "One depth check failed");
            Assert.IsFalse(boardChecker.AreNeightboorsMatched(board, 1, 4, UnidirectionalDirection.Left, 2, PieceType.Blue),
                "Two Depth Check failed");
        }
        
        [Test]
        public void AreMatchingLeft_False_Color()
        {
            IBoard board = Substitute.For<IBoard>();
            board.Rows.Returns(rows);
            board.Columns.Returns(cols);
            board.IsCoordinateMatchingPiece(PieceType.Blue,0, 4).Returns(false);
            board.IsOutOfRange(0, 4).Returns(false);
            board.IsCoordinateMatchingPiece(PieceType.Blue, 1, 4).Returns(true);
            board.IsOutOfRange(1, 4).Returns(false);
            // Use the Assert class to test conditions

            BoardChecker boardChecker = new BoardChecker();
            Assert.IsTrue(boardChecker.AreNeightboorsMatched(board, 2, 4, UnidirectionalDirection.Left, 1, PieceType.Blue),
                "One depth check failed");
            Assert.IsFalse(boardChecker.AreNeightboorsMatched(board, 2, 4, UnidirectionalDirection.Left, 2, PieceType.Blue),
                "Two Depth Check failed");
        }
        
        [Test]
        public void AreMatchingSides_Success()
        {
            IBoard board = Substitute.For<IBoard>();
            board.Rows.Returns(rows);
            board.Columns.Returns(cols);
            board.IsCoordinateMatchingPiece(PieceType.Blue,0, 4).Returns(true);
            board.IsOutOfRange(0, 4).Returns(false);
            board.IsCoordinateMatchingPiece(PieceType.Blue, 2, 4).Returns(true);
            board.IsOutOfRange(2, 4).Returns(false);
            // Use the Assert class to test conditions

            BoardChecker boardChecker = new BoardChecker();
            Assert.IsTrue(boardChecker.AreNeightboorsMatched(board, 1, 4, BidirectionalDirection.Horizontal, 1, PieceType.Blue),
                "One depth check failed");
        }
        
        [Test]
        public void AreMatchingSides_FailRange()
        {
            IBoard board = Substitute.For<IBoard>();
            board.Rows.Returns(rows);
            board.Columns.Returns(cols);
            board.IsCoordinateMatchingPiece(PieceType.Blue,-1, 4).Returns(true);
            board.IsOutOfRange(-1, 4).Returns(true);
            board.IsCoordinateMatchingPiece(PieceType.Blue, 1, 4).Returns(true);
            board.IsOutOfRange(1, 4).Returns(false);
            // Use the Assert class to test conditions

            BoardChecker boardChecker = new BoardChecker();
            Assert.IsFalse(boardChecker.AreNeightboorsMatched(board, 0, 4, BidirectionalDirection.Horizontal, 1, PieceType.Blue),
                "One depth check failed");
        }
        
        [Test]
        public void AreMatchingSides_Fail_Type()
        {
            IBoard board = Substitute.For<IBoard>();
            board.Rows.Returns(rows);
            board.Columns.Returns(cols);
            board.IsCoordinateMatchingPiece(PieceType.Blue,0, 4).Returns(true);
            board.IsOutOfRange(0, 4).Returns(false);
            board.IsCoordinateMatchingPiece(PieceType.Blue, 2, 4).Returns(false);
            board.IsOutOfRange(2, 4).Returns(false);
            // Use the Assert class to test conditions

            BoardChecker boardChecker = new BoardChecker();
            Assert.IsFalse(boardChecker.AreNeightboorsMatched(board, 1, 4, BidirectionalDirection.Horizontal, 1, PieceType.Blue),
                "One depth check failed");
        }
    }
}
