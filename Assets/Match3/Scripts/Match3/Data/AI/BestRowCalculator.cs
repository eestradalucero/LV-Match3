using System;
using System.Collections.Generic;
using Match3.Scripts.API.Match3;
using Match3.Scripts.API.Match3.Control;
using Match3.Scripts.API.Match3.Data.AI;
using Match3.Scripts.Match3.Control;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Match3.Scripts.Match3.Data.AI
{
    public class BestRowCalculator : IBestRowCalculator
    {
        private IBoardChecker boardChecker;
        private IPiecesValuesContainer piecesValuesContainer;
        private int[] rowScores;
        public BestRowCalculator(IPiecesValuesContainer piecesValuesContainer)
        {
            boardChecker = new BoardChecker();
            this.piecesValuesContainer = piecesValuesContainer;
        }

        private const int defaultScore = 2000;
        public int CalculateBestRow(IBoard board, PieceType nextPieceType)
        {
            rowScores = new int[board.Rows];
            resetRowScores();
            int? bestRow = GetBestTripletSpace(board, nextPieceType);
            if (bestRow.HasValue) return bestRow.Value;
            
            resetRowScores();
            bestRow = GetBestDupletSpace(board, nextPieceType);
            if (bestRow.HasValue && bestRow.Value != defaultScore) return bestRow.Value;

            return GetRandomRow(board);
        }
        
        private void resetRowScores()
        {
            for (int i = 0; i < rowScores.Length; ++i)
            {
                rowScores[i] = defaultScore;
            }
        }

        private int? GetBestTripletSpace(IBoard board, PieceType nextPieceType)
        {
            for (int i = 0; i < board.Rows; ++i)
            {
                var col = board.GetUppermostColumnIndex(i);
                calculateTripleScore(board, i,col, nextPieceType);
            }
            Debug.Log("<color=red>TripletSpace</color>");
            return areAllRowsAtDefaultScore() ? null : getHighestScoringRow();
        }

        
        private void calculateTripleScore(IBoard board, int row, int column, PieceType nextPieceType, bool positive = true)
        {
            var unidirectionals = Enum.GetValues(typeof(UnidirectionalDirection));
            foreach (var unidirection in unidirectionals)
            {
                var dir = (UnidirectionalDirection)unidirection;
                if (boardChecker.AreNeightboorsMatched(board, row, column, dir, 2, nextPieceType))
                {
                    var multiplier = positive ? 1 : -1;
                    rowScores[row] += piecesValuesContainer.GetTripletValue(nextPieceType) * multiplier;
                }
            }
            
            var bidirectionals = Enum.GetValues(typeof(BidirectionalDirection));
            foreach (var bidirectional in bidirectionals)
            {
                var dir = (BidirectionalDirection)bidirectional;
                if (boardChecker.AreNeightboorsMatched(board, row, column, dir, 1, nextPieceType))
                {
                    var multiplier = positive ? 1 : -1;
                    rowScores[row] += piecesValuesContainer.GetTripletValue(nextPieceType) * multiplier;
                }
            }
        }

        private int? getHighestScoringRow()
        {
            var highScore = getRowsHighestScore();
            Debug.Log($"High score {highScore}");
            List<int> highestRows = new List<int>();
            for (int i = 0; i < rowScores.Length; ++i)
            {
                if (rowScores[i] == highScore)
                {
                    
                    highestRows.Add(i);
                }
            }

            if (highestRows.Count > 0)
            {
                return highestRows[Random.Range(0, highestRows.Count)];
            }

            return null;
        }

        private int getRowsHighestScore()
        {
            int highScore = int.MinValue;
            for (int i = 0; i < rowScores.Length; ++i)
            {
                if (rowScores[i] > highScore)
                {
                    highScore = rowScores[i];
                }
            }
            return highScore;
        }

        private int? GetBestDupletSpace(IBoard board, PieceType nextPieceType)
        {
            Debug.Log($"<color=Cyan> CALCULATING DUPLET SCORE for {nextPieceType}: </color> ");
            for (int i = 0; i < board.Rows; ++i)
            {
                var col = board.GetUppermostColumnIndex(i);
                Debug.Log($"Checking ROW: {i},{col}");
                calculateDupletScore(board, i,col, nextPieceType);
                calculateUnmatchingScores(board, i, col, nextPieceType);
                calculateHeightPenalty(board, i, col);
            }

            if (areAllRowsAtDefaultScore())
            {
                Debug.Log("ALL ROWS AT DEFAULT");
            }
            else
            {
                DebugRowsScores();
            }
            
            return areAllRowsAtDefaultScore() ? null : getHighestScoringRow();
        }

        private void DebugRowsScores()
        {
            string debugString = "Scores, ";
            for (int i = 0; i < rowScores.Length; ++i)
            {
                debugString += $"Row {i}: {rowScores[i]}";
                if (i != rowScores.Length - 1)
                {
                    debugString += ", ";
                }
            }
            Debug.Log(debugString);
        }
        
        private void calculateDupletScore(IBoard board, int row, int column, PieceType nextPieceType, bool isPositiveCheck = true)
        {
            var unidirectionals = Enum.GetValues(typeof(UnidirectionalDirection));
            foreach (var unidirection in unidirectionals)
            {
                var dir = (UnidirectionalDirection)unidirection;
                if (boardChecker.AreNeightboorsMatched(board, row, column, dir, 1, nextPieceType))
                {
                    var multiplier = getMultiplier(isPositiveCheck, dir);
                    rowScores[row] += (int)(piecesValuesContainer.GetDupletValue(nextPieceType) * multiplier);
                    Debug.Log($"({row},{column}) {nextPieceType} score {(isPositiveCheck?"<color=cyan>Increased</color>":"<color=red>Reduced</color>")} " +
                              $"to {rowScores[row]} - on {unidirection} - ");
                }
            }
        }

        private float getMultiplier(bool isPositiveCheck, UnidirectionalDirection direction) => isPositiveCheck ? 1f : -.4f;

        private void calculateUnmatchingScores(IBoard board, int row, int col, PieceType nextPieceType)
        {
            var pieceTypes = Enum.GetValues(typeof(PieceType));
            foreach (var pieceType in pieceTypes)
            {
                var pieceToCheck = (PieceType)pieceType;
                if(pieceToCheck == nextPieceType || pieceToCheck == PieceType.None) continue; //Do not check same type

                calculateTripleScore(board, row,col, pieceToCheck, false);
                calculateDupletScore(board, row,col, pieceToCheck, false);
            }
        }

        private void calculateHeightPenalty(IBoard board, int row, int col)
        {

            int heightPenalty = (board.Columns - col) * 50;
            rowScores[row] -= heightPenalty;
            Debug.Log($"{row}, {col} height penalty: {heightPenalty}:");
        }

        private bool areAllRowsAtDefaultScore() => getRowsHighestScore() == defaultScore;


        public int GetRandomRow(IBoard board) => Random.Range(0, board.Rows);
    }
}
