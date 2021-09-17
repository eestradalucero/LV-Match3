using Match3.Scripts.API.Match3;
using Match3.Scripts.API.Match3.Data.AI;
using UnityEngine;

namespace Match3.Scripts.Match3.Data.AI
{
    [System.Serializable]
    public class PieceAiValue : IPieceAiValue
    {
        #pragma warning disable 649
        [SerializeField] private PieceType pieceType;
        [SerializeField] private int possibleTripleValue;
        [SerializeField] private int possibleDupletValue;
#pragma warning restore 649
        
        public PieceType PieceType => pieceType;
        public int PossibleTripleValue => possibleTripleValue;
        public int PossibleDupletValue => possibleDupletValue;
    }
}
