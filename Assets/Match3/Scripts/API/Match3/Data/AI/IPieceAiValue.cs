namespace Match3.Scripts.API.Match3.Data.AI
{
    public interface IPieceAiValue
    {
        PieceType PieceType { get; }
        int PossibleTripleValue { get; }
        int PossibleDupletValue { get; }
    }
}
