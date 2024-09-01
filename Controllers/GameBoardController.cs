using CST350_Minesweeper.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

public class GameBoardController : Controller
{
    private static GameBoard gameBoard;
    private static bool gameOver = false;
    private static Stopwatch stopwatch = new Stopwatch();

    public IActionResult Index()
    {
        if (gameBoard == null)
        {
            gameBoard = new GameBoard(10, 10, 20); // Initialize a new game board
            gameOver = false; // Reset game over flag
            stopwatch.Reset(); // Reset stopwatch
        }

        // Pass game board and game state to the view
        ViewData["ElapsedTime"] = stopwatch.IsRunning ? stopwatch.Elapsed.ToString(@"mm\:ss") : "00:00";
        ViewData["GameOver"] = gameOver;
        ViewData["CanPlay"] = !gameOver;
        return View(gameBoard);
    }

    [HttpPost]
    public IActionResult StartGame()
    {
        gameBoard = new GameBoard(10, 10, 20); // Reset or initialize a new game board
        gameOver = false; // Reset game
        stopwatch.Restart(); // Start or restart the stopwatch
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult RevealCell(int row, int column)
    {
        if (gameBoard != null && row < gameBoard.Rows && column < gameBoard.Columns && !gameOver)
        {
            var cell = gameBoard.Cells[row][column];

            if (cell.IsBomb)
            {
                gameOver = true; // Set game
                stopwatch.Stop(); // Stop the timer
                RevealAllBombs(); // Reveal all bombs on the board
                return RedirectToAction("GameOver");
            }
            else
            {
                RevealCellRecursive(row, column);
            }
        }
        return View("Index", gameBoard);
    }

    private void RevealCellRecursive(int row, int column)
    {
        if (row < 0 || row >= gameBoard.Rows || column < 0 || column >= gameBoard.Columns)
            return;

        var cell = gameBoard.Cells[row][column];

        if (cell.IsRevealed || cell.IsBomb)
            return;

        cell.IsRevealed = true;

        if (cell.NeighboringBombs == 0)
        {
            for (int r = Math.Max(0, row - 1); r <= Math.Min(gameBoard.Rows - 1, row + 1); r++)
            {
                for (int c = Math.Max(0, column - 1); c <= Math.Min(gameBoard.Columns - 1, column + 1); c++)
                {
                    RevealCellRecursive(r, c);
                }
            }
        }
    }

    private void RevealAllBombs()
    {
        for (int row = 0; row < gameBoard.Rows; row++)
        {
            for (int col = 0; col < gameBoard.Columns; col++)
            {
                var cell = gameBoard.Cells[row][col];
                if (cell.IsBomb)
                {
                    cell.IsRevealed = true; // Reveal all bombs on the board
                }
            }
        }
    }

    public IActionResult GameOver()
    {
        ViewData["GameOver"] = true; // Set game over in ViewData
        ViewData["ElapsedTime"] = stopwatch.Elapsed.ToString(@"mm\:ss"); // Pass the elapsed time to the view
        ViewData["CanPlay"] = false; // Disable playing when game is over
        return View("Index", gameBoard);
    }
}

