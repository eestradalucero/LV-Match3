using Match3.Scripts.API.Match3;
using Match3.Scripts.API.Match3.Data.AI;
using UnityEngine;

namespace Match3.Scripts.Match3.Data.AI
{
    [CreateAssetMenu(fileName = "PieceAiValue", menuName = "Match3/PieceAiValue")]
    public class PiecesValueScriptableObject : ScriptableObject, IPiecesValuesContainer
    {
        [SerializeField] private PieceAiValue[] piecesValues;

        public int GetDupletValue(PieceType type)
        {
            for (int i = 0; i < piecesValues.Length; ++i)
            {
                if (piecesValues[i].PieceType == type)
                    return piecesValues[i].PossibleDupletValue;
            }
            return 0;
        }

        public int GetTripletValue(PieceType type)
        {
            for (int i = 0; i < piecesValues.Length; ++i)
            {
                if (piecesValues[i].PieceType == type)
                    return piecesValues[i].PossibleTripleValue;
            }
            return 0;
        }
    }
}
