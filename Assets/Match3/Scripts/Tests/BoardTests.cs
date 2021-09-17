using Match3.Scripts.API.Match3;
using Match3.Scripts.Match3;
using NUnit.Framework;
using UnityEngine;

namespace Match3.Scripts.Tests
{
    public class BoardTests
    {
        const int rows = 3;
        const int columns = 5;
        // A Test behaves as an ordinary method
        [Test]
        public void PlaceSinglePiece()
        {
            var board = new Board(rows,columns);
            board.PlacePiece(PieceType.Blue, 1);
            Assert.AreEqual(PieceType.Blue, board.GetPieceInCoordinate(1,4));
            Assert.AreEqual(PieceType.None, board.GetPieceInCoordinate(1, 3));

        }
        
        [Test]
        public void PlaceTwoPiecesInARow(){
             var board = new Board(rows,columns);
            board.PlacePiece(PieceType.Blue, 1);
            board.PlacePiece(PieceType.Red, 1);
            Assert.AreEqual(PieceType.Blue, board.GetPieceInCoordinate(1,4));
            Assert.AreEqual(PieceType.Red, board.GetPieceInCoordinate(1, 3));
        }

        [Test]
        public void WasPiecePlaced(){
             var board = new Board(rows,columns);
            board.PlacePiece(PieceType.Blue, 1);
            var wasPiecePlaced = board.PlacePiece(PieceType.Red, 1);
            Assert.IsTrue(wasPiecePlaced);
        }

        [Test]
        public void WasPieceNotPlaced(){
             var board = new Board(rows,columns);
            board.PlacePiece(PieceType.Blue, 1);
            board.PlacePiece(PieceType.Blue, 1);
            board.PlacePiece(PieceType.Blue, 1);
            board.PlacePiece(PieceType.Blue, 1);
            board.PlacePiece(PieceType.Blue, 1);

            var wasPiecePlaced = board.PlacePiece(PieceType.Red, 1);
            Assert.False(wasPiecePlaced);
        }
        
        [Test]
        public void IsOutRange(){
            var board = new Board(rows, columns);
            Assert.IsTrue(board.IsOutOfRange(5,5));
            Assert.IsTrue(board.IsOutOfRange(-1,-1));
        }

        [Test]
        public void IsInRange(){
            var board = new Board(rows, columns);
            Assert.IsFalse(board.IsOutOfRange(rows-1, columns-1));
        }

        [Test]
        public void IsRowFull(){
            var board = new Board(rows, columns);
            board.PlacePiece(PieceType.Red, 1);
            board.PlacePiece(PieceType.Green, 1);
            board.PlacePiece(PieceType.Blue, 1);
            board.PlacePiece(PieceType.Orange, 1);
            board.PlacePiece(PieceType.Violet, 1);

            Assert.IsTrue(board.IsRowFull(1));
        }

        [Test]
        public void IsRowNotFull(){
            var board = new Board(rows, columns);
            board.PlacePiece(PieceType.Red, 1);
            board.PlacePiece(PieceType.Green, 1);
            board.PlacePiece(PieceType.Blue, 1);
            board.PlacePiece(PieceType.Orange, 1);

            Assert.IsFalse(board.IsRowFull(1));
        }

        [Test]
        public void CleanPiece(){
            var board = new Board(rows, columns);
            board.PlacePiece(PieceType.Red, 1);
            board.PlacePiece(PieceType.Green, 1);
            board.PlacePiece(PieceType.Blue, 1);
            board.PlacePiece(PieceType.Orange, 1);

            board.ClearPiece(1, 2);
            Assert.AreEqual(PieceType.None, board.GetPieceInCoordinate(1,2), message:"PieceType was not cleared");
            Assert.AreEqual(PieceType.Green, board.GetPieceInCoordinate(1,3), message:"PieceType before clear affected");
             Assert.AreEqual(PieceType.Orange, board.GetPieceInCoordinate(1,1), message:"PieceType before clear affected");
        }

        [Test]
        public void ResetBoard(){
var board = new Board(rows, columns);
            board.PlacePiece(PieceType.Red, 1);     //4
            board.PlacePiece(PieceType.Green, 1);   //3
            board.PlacePiece(PieceType.Blue, 1);    //2 XX
            board.PlacePiece(PieceType.Orange, 1);  //1 XX
            board.PlacePiece(PieceType.Yellow, 1);  //0 

            Debug.Log("Place pieces");
            DebugBoard(board);

            board.ClearPiece(1,2);  //Clear Blue
            board.ClearPiece(1,1);  //Clear Yellow

            Debug.Log("Post Clear");
            DebugBoard(board);

            board.RearrangeBoard();

            Debug.Log("Rearrange");
            DebugBoard(board);

            Assert.AreEqual(PieceType.Red, board.GetPieceInCoordinate(1,4));
            Assert.AreEqual(PieceType.Green, board.GetPieceInCoordinate(1,3));
            Assert.AreEqual(PieceType.Yellow, board.GetPieceInCoordinate(1,2));
            Assert.AreEqual(PieceType.None, board.GetPieceInCoordinate(1,1));
        }

        private void DebugBoard(Board board){
            for(int i = columns -1; i >=0; --i){
                Debug.Log($"{i} - {board.GetPieceInCoordinate(1,i)}");
            }
        }
    }
}
