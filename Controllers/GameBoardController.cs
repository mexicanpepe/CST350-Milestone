using CST350_Minesweeper.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

public class GameBoardController : Controller
{
    private static GameBoard gameBoard;
    private static bool gameOver = false;
    private static Stopwatch stopwatch = new Stopwatch();
    private readonly GameService _gameService;

    public GameBoardController(GameService gameService)
    {
        _gameService = gameService;
    }

    public IActionResult Index()
    {
        if (gameBoard == null)
        {
            gameBoard = _gameService.CreateGameBoard(10, 10, 20);
            gameOver = false;
            stopwatch.Reset();
            stopwatch.Start();
        }

        ViewData["ElapsedTime"] = stopwatch.IsRunning ? stopwatch.Elapsed.ToString(@"mm\:ss") : "00:00";
        ViewData["GameOver"] = gameOver;
        ViewData["CanPlay"] = !gameOver;
        return View(gameBoard);
    }

    [HttpPost]
    public IActionResult RevealCell(int row, int column)
    {
        if (!gameOver)
        {
            _gameService.RevealCell(gameBoard, row, column);
            if (gameBoard.GameOver)
            {
                stopwatch.Stop();
                return PartialView("_GameOver", gameBoard);
            }
        }
        return PartialView("_GameBoard", gameBoard);
    }

    [HttpPost]
    public IActionResult ToggleFlag(int row, int column)
    {
        if (!gameOver)
        {
            _gameService.ToggleFlag(gameBoard, row, column);
        }
        return PartialView("_GameBoard", gameBoard);
    }

    [HttpPost]
    public IActionResult StartGame()
    {
        gameBoard = _gameService.CreateGameBoard(10, 10, 20);
        gameOver = false;
        stopwatch.Reset();
        stopwatch.Start();

        return RedirectToAction("Index");
    }
}
