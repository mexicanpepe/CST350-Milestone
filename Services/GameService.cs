using CST350_Minesweeper.Models;

public class GameService
{
    public GameBoard CreateGameBoard(int rows, int columns, int numberOfBombs)
    {
        return new GameBoard(rows, columns, numberOfBombs);
    }

    public void RevealCell(GameBoard gameBoard, int row, int column)
    {
        if (gameBoard.Cells[row][column].IsBomb)
        {
            gameBoard.GameOver = true;
            RevealAllBombs(gameBoard);
        }
        else
        {
            RevealCellRecursive(gameBoard, row, column);
            gameBoard.CheckGameOver();
        }
    }

    public void ToggleFlag(GameBoard gameBoard, int row, int column)
    {
        var cell = gameBoard.Cells[row][column];
        cell.IsFlagged = !cell.IsFlagged;
    }

    private void RevealCellRecursive(GameBoard gameBoard, int row, int column)
    {
        if (row < 0 || row >= gameBoard.Rows || column < 0 || column >= gameBoard.Columns)
            return;

        var cell = gameBoard.Cells[row][column];
        if (cell.IsRevealed || cell.IsFlagged || cell.IsBomb)
            return;

        cell.IsRevealed = true;
        if (cell.NeighboringBombs == 0)
        {
            for (int r = Math.Max(0, row - 1); r <= Math.Min(gameBoard.Rows - 1, row + 1); r++)
            {
                for (int c = Math.Max(0, column - 1); c <= Math.Min(gameBoard.Columns - 1, column + 1); c++)
                {
                    RevealCellRecursive(gameBoard, r, c);
                }
            }
        }
    }

    private void RevealAllBombs(GameBoard gameBoard)
    {
        foreach (var row in gameBoard.Cells)
        {
            foreach (var cell in row)
            {
                if (cell.IsBomb)
                {
                    cell.IsRevealed = true;
                }
            }
        }
    }
}
