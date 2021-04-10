using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMap : MonoBehaviour {
  
  int gridWidth = 5;
  int gridHeight = 5;
  Cell[][] grid;

  void Start() {
    grid = initGrid();
  }

  void Update() {
    
  }

  public Cell getCellAt(int[] index) {
    return grid[index[0]][index[1]];
  }

  public void setCellState(int[] index, bool state) {
    grid[index[0]][index[1]].isEmpty = state;
  }

  Cell[][] initGrid() {
    Cell[][] grid = new Cell[gridHeight][];
    for (int i = 0; i < grid.Length; i++)
    {
      grid[i] = new Cell[gridWidth];
      for (int j = 0; j < gridWidth; j++)
      {
        int[] index = new int[] { i, j };
        grid[i][j] = new Cell(index, getCellNeighbours(index));
      }
    }
    return grid;
  }

  int[][] getCellNeighbours(int[] position) {
    bool isEvenRow = position[0] % 2 == 0;
    int diagLeft = isEvenRow ? position[1] : position[1] - 1;
    int diagRight = isEvenRow ? position[1] + 1 : position[1];
    int[][] neighbours = new int[6][] {
      // Top right
      new int[] { position[0] - 1, diagRight },
      // Right
      new int[] { position[0], position[1] + 1 },
      // Bottom right
      new int[] { position[0] + 1, diagRight },
      // Bottom left
      new int[] { position[0] + 1, diagLeft },
      // Left
      new int[] { position[0], position[1] - 1 },
      // Top left
      new int[] { position[0] - 1, diagLeft },
    };

    return neighbours;
  }

  bool indexIsInGrid(int[] index) {
    return
      index[0] >= 0
      && index[0] < gridHeight
      && index[1] >= 0
      && index[1] < gridWidth;
  }

}

public class Cell {
  public int[] index;
  public int[][] neighbours;
  public bool isEmpty;

  public Cell(int[] _index, int[][] _neighbours) {
    index = _index;
    neighbours = _neighbours;
    isEmpty = false;
  }
}
