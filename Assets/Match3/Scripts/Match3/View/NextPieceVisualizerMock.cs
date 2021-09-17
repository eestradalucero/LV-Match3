using System;
using Match3.Scripts.Match3.Control;
using Match3.Scripts.Match3.Data;
using UnityEngine;

namespace Match3.Scripts.Match3.View
{
    public class NextPieceVisualizerMock : MonoBehaviour
    {
        #pragma warning disable 649
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private Match3Controller match3Controller;
        [SerializeField] private PiecesColorDataScriptableObject piecesColor;
        #pragma warning restore 649

        private void Update()
        {
            sprite.color = piecesColor.GetPieceColor(match3Controller.NextPiece);
        }
    }
}
