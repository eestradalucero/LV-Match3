namespace Match3.Scripts.API.Match3.Data.AI
{
    public interface IPiecesValuesContainer
    {
        int GetDupletValue(PieceType type);
        int GetTripletValue(PieceType type);
    }
}
