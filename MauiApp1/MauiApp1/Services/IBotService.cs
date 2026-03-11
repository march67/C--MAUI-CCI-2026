using MauiApp1.Models;

namespace MauiApp1.Services
{
    public interface IBotService
    {
        // Return -1 si aucun coup possible
        int ChooseCell(Board board, string botSymbol);
    }
}
