using System;

namespace CST350_Minesweeper.Models
{
    public class GameBoard
    {
        public int Rows { get; }
        public int Columns { get; }
        public Cell[][] Cells { get; }
        public bool GameOver { get; set; }

        public GameBoard(int rows, int columns, int numberOfBombs)
        {
            Rows = rows;
            Columns = columns;
            Cells = new Cell[rows][];

            // Initialize cells
            for (int r = 0; r < rows; r++)
            {
                Cells[r] = new Cell[columns];
                for (int c = 0; c < columns; c++)
                {
                    Cells[r][c] = new Cell();
                }
            }

            // Place bombs and calculate neighboring bombs
            PlaceBombs(numberOfBombs);
            GameOver = false;
        }

        private void PlaceBombs(int numberOfBombs)
        {
            Random rand = new Random();
            int placedBombs = 0;

            while (placedBombs < numberOfBombs)
            {
                int row = rand.Next(Rows);
                int col = rand.Next(Columns);

                if (!Cells[row][col].IsBomb)
                {
                    Cells[row][col].IsBomb = true;
                    placedBombs++;
                }
            }

            // Calculate neighboring bombs
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    if (!Cells[r][c].IsBomb)
                    {
                        Cells[r][c].NeighboringBombs = CountNeighboringBombs(r, c);
                    }
                }
            }
        }

        private int CountNeighboringBombs(int row, int column)
        {
            int count = 0;

            for (int r = Math.Max(0, row - 1); r <= Math.Min(Rows - 1, row + 1); r++)
            {
                for (int c = Math.Max(0, column - 1); c <= Math.Min(Columns - 1, column + 1); c++)
                {
                    if (Cells[r][c].IsBomb)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        // Add this method to check if the game is over
        public void CheckGameOver()
        {
            
            foreach (var row in Cells)
            {
                foreach (var cell in row)
                {
                    if (!cell.IsBomb && !cell.IsRevealed)
                    {
                        return; 
                    }
                }
            }
            GameOver = true;
        }
    }

    public class Cell
    {
        public bool IsBomb { get; set; }
        public bool IsRevealed { get; set; }
        public bool IsFlagged { get; set; } 
        public int NeighboringBombs { get; set; }
    }
}
