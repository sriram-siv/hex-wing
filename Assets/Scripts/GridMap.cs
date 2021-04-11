using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMap : MonoBehaviour {
  
  [SerializeField]
  int gridWidth = 5;
  [SerializeField]
  int gridHeight = 5;
  GridCell[][] grid; // [width][height]
  bool oddRowsClipped = true;

  [SerializeField]
  GameObject cellProto;

  List<GridCell> _activeCells = new List<GridCell>();
  public List<GridCell> activeCells { get => _activeCells; }

  void Start() {
    grid = InitGrid();
  }

  public Vector3 GetCellPosition(int x, int y) {
    Vector3 position = new Vector3(x, y * -0.75f);
    if (y % 2 == 1) {
      position.x += 0.5f;
    }
    return position;
  }

  public void SetActiveCell(GridCell cell, bool state) {
    if (state) {
      _activeCells.Add(cell);
    } else {
      GridCell cellRef = _activeCells.Find(item => item == cell);
      _activeCells.Remove(cellRef);
    }
  }

  public void DeactiveAllCells() {
    foreach (GridCell cell in _activeCells.ToArray()) {
      cell.SetActive(false);
    }
  }


  GridCell InstantiateCell(int x, int y) {
    GameObject newCell = Instantiate(
      cellProto,
      transform.position,
      transform.rotation
    );

    newCell
      .GetComponent<GridCell>()
      .Initialise(x, y, this);

    return newCell.GetComponent<GridCell>();
  }

  public bool IsValidGridIndex(int[] index) {

    int rowEnd = gridWidth;
    if (oddRowsClipped && index[1] % 2 == 1) {
      rowEnd--;
    }

    return index[0] >= 0 && index[0] < rowEnd
      && index[1] >= 0 && index[1] < gridHeight;
  }

  public GridCell GetCellAt(int[] index) {
    return grid[index[0]][index[1]];
  }

  GridCell[][] InitGrid() {
    GridCell[][] grid = new GridCell[gridWidth][];

    for (int i = 0; i < gridWidth; i++) {
      grid[i] = new GridCell[gridHeight];

      for (int j = 0; j < gridHeight; j++) {

        if (oddRowsClipped && j % 2 == 1 && i + 1 == gridWidth) {
          continue;
        }
        grid[i][j] = InstantiateCell(i, j);
      }
    }
    return grid;
  }

  public int[][] GetCellNeighbours(int x, int y) {
    bool isEvenRow = y % 2 == 0;
    int diagLeft = isEvenRow ? x - 1 : x;
    int diagRight = isEvenRow ? x : x + 1;
    int[][] neighbours = new int[6][] {
      // Top right
      new int[] { diagRight, y - 1 },
      // Right
      new int[] { x + 1, y },
      // Bottom right
      new int[] { diagRight, y + 1 },
      // Bottom left
      new int[] { diagLeft, y + 1 },
      // Left
      new int[] { x - 1, y },
      // Top left
      new int[] { diagLeft, y - 1 },
    };

    return neighbours;
  }

  bool IndexIsInGrid(int[] index) {
    return
      index[0] >= 0
      && index[0] < gridWidth
      && index[1] >= 0
      && index[1] < gridHeight;
  }

}
