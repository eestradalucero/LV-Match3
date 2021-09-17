using Match3.Scripts.API.Match3;
using Match3.Scripts.API.Match3.Data;
using UnityEngine;

namespace Match3.Scripts.Match3.Data
{
    [CreateAssetMenu(fileName = "PieceRandomizer", menuName = "Match3/PieceRandomizer")]
    public class PieceTypeRandomizerScriptableObject : ScriptableObject, IPieceTypeRandomizer
    {
        [SerializeField] private PieceProbabilitiesMockup[] pieceProbabilities;
        public PieceType GetRandomPieceType()
        {
            var randomValue = Random.Range(0f, 100f);
            foreach (var pieceProbability in pieceProbabilities)
            {
                if (pieceProbability.IsInPercentage(randomValue)) return pieceProbability.PieceType;
            }
            Debug.LogError("Wrong piecetype probability");
            return PieceType.None;
        }

        [System.Serializable]
        private class PieceProbabilitiesMockup
        {
#pragma warning disable 649
            [SerializeField] private PieceType pieceType;
            [SerializeField] private float minPercentageInclusive;
            [SerializeField] private float maxPercentageExclusive;
#pragma warning restore 649
            public PieceType PieceType => pieceType;

            public bool IsInPercentage(float percentage) => percentage >= minPercentageInclusive && percentage < maxPercentageExclusive;
        }
    }
}

