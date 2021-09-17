using System;
using System.Collections.Generic;
using Match3.Scripts.API.Match3;
using Match3.Scripts.API.Match3.Control;
using Match3.Scripts.API.Match3.Player;
using Match3.Scripts.Match3.Data;
using Match3.Scripts.Match3.Player;
using UnityEngine;

namespace Match3.Scripts.Match3.Control
{
    public class Match3Controller : MonoBehaviour
    {
        #region Editor Fields
        #pragma warning disable 649
        [SerializeField] private int rows;
        [SerializeField] private int columns;
        [SerializeField] private AbstractPlayerMockup[] _players;
        [SerializeField] private PieceTypeRandomizerScriptableObject pieceRandomizer;
        [SerializeField] private ScoreMockup scoreMockup;
        [SerializeField] private bool shouldFillNegativeScore = false;
        [SerializeField] private bool shouldFillResetFullBoard = false;
        #pragma warning disable 649
        #endregion

        #region fields
        private GameState gameState = GameState.Preselecting;
        private PieceType pieceType;
        private IBoardChecker boardChecker;
        #endregion
        
        #region Properties
        private IPlayer[] players => _players;
        public PieceType NextPiece => pieceType;
        #endregion
        

        private void Awake()
        {
            foreach (var player in players)
            {
                player.SetupPlayer(rows, columns);
            }
            boardChecker = new BoardChecker();
        }

        private void Update()
        {
            switch (gameState)
            {
                case GameState.Preselecting:
                    preselectingUpdate();
                    break;
                case GameState.Selecting:
                    selectingUpdate();
                    break;
                case GameState.Processing:
                    processingUpdate();
                    break;
            }
        }

        private void preselectingUpdate()
        {
            pieceType = getRandomPieceType();

            foreach (var player in players)
            {
                player.ResetPieceSelection();
            }
            gameState = GameState.Selecting;
        }

        private PieceType getRandomPieceType()
        {
            PieceType randomPiece = PieceType.None;
            do
            {
                randomPiece = pieceRandomizer.GetRandomPieceType();
            } while (randomPiece == PieceType.None);
            return randomPiece;
        }

        private void selectingUpdate()
        {
            bool arePlayersPendingSelection = false;
            foreach (var player in players)
            {
                if (player.PlayerState != PlayerState.SelectingPiece) continue;
                player.ProcessPieceSelection(pieceType);
                arePlayersPendingSelection = true;
            }

            if (arePlayersPendingSelection) return;

            foreach (var player in players)
            {
                if(!player.DesiredMovement.HasValue) continue;
                if (player.Board.IsRowFull(player.DesiredMovement.Value))
                {
                    player.SetFilledRow(player.DesiredMovement.Value);
                    if (shouldFillResetFullBoard)
                    {
                        for (int i = 0; i < player.Board.Rows; ++i)
                        {
                            player.Board.ClearRow(i);
                        }
                    }
                    else
                    {
                        player.Board.ClearRow(player.DesiredMovement.Value);
                    }
                    
                }
                else
                {
                    player.Board.PlacePiece(pieceType, player.DesiredMovement.Value);
                }
                player.BoardVisualizer.UpdateView();
            }
            gameState = GameState.Processing;
        }

        private void processingUpdate()
        {
            for(int i = 0; i < players.Length; i++)
            {
                if (players[i].FilledLine.HasValue)
                {
                    players[i].Board.RearrangeBoard();
                    players[i].ResetPieceSelection();
                    players[i].ResetFilledRow();
                    if(shouldFillNegativeScore) scoreMockup.AddScore(-1, i);
                    continue;
                }
                players[i].ResetPieceSelection();
                var triadsToClear = 0;
                do
                {
                    triadsToClear = 0;
                    triadsToClear += ClearTriads(players[i].Board);
                    players[i].Board.RearrangeBoard();
                    if(triadsToClear > 0) scoreMockup.AddScore(triadsToClear, i);
                } while (triadsToClear != 0);
                
                players[i].BoardVisualizer.UpdateView();
            }
            gameState = GameState.Preselecting;
        }



        private int ClearTriads(IBoard board)
        {
            int triadsCleared = 0;
            for (int i = 0; i < board.Rows; ++i)
            {
                for (int j = 0; j < board.Columns; ++j)
                {
                    if (!board.IsCoordinateMatchingPiece(PieceType.None, i, j))
                    {
                        triadsCleared += AttemptClear(board, i, j);
                    }
                }
            }
            return triadsCleared;
        }
        
        private int AttemptClear(IBoard board, int row, int column)
        {
            int triadsCleared = 0;
            var piece = board.GetPieceInCoordinate(row, column);
            var unidirections = Enum.GetValues(typeof(UnidirectionalDirection));
            foreach (var unidirection in unidirections)
            {
                var dir = (UnidirectionalDirection)unidirection;
                if (boardChecker.AreNeightboorsMatched(board, row, column, dir, 2, piece))
                {
                    var coordinatesToClear =
                        boardChecker.GetCoordinateSet(board, row, column, dir, 2);
                    coordinatesToClear.Add(new Vector2Int(row, column));
                    ClearCoordinates(board, coordinatesToClear);
                    triadsCleared++;
                }
            }
            
            var bidirectionals = Enum.GetValues(typeof(BidirectionalDirection));
            foreach (var bidirectional in bidirectionals)
            {
                var dir = (BidirectionalDirection)bidirectional;
                if (boardChecker.AreNeightboorsMatched(board, row, column, dir, 1, piece))
                {
                    var coordinatesToClear =
                        boardChecker.GetCoordinateSet(board, row, column, dir, 1);
                    coordinatesToClear.Add(new Vector2Int(row, column));
                    ClearCoordinates(board, coordinatesToClear);
                    triadsCleared++;
                }
            }

            return triadsCleared;
        }

        private void ClearCoordinates(IBoard board, List<Vector2Int> coordinates)
        {
            foreach (var coordinate in coordinates)
            {
                if (board.IsOutOfRange(coordinate.x, coordinate.y)) continue;
                board.ClearPiece(coordinate.x, coordinate.y);
            }
        }
    }
}