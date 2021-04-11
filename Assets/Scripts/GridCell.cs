using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
  [Header("Highlight Colors")]
  [SerializeField]
  Color32 activeColor;
  [SerializeField]
  Color32 neighbourColor;

  int x;
  int y;
  int[][] neighbours;
  bool isEmpty = false;
  bool isActive = false;
  GridMap grid;

  enum Colors {
    clear, active, neighbour
  }

  public void Initialise(int _x, int _y, GridMap _grid) {
    x = _x;
    y = _y;
    grid = _grid;

    neighbours = grid.GetCellNeighbours(x, y);

    transform.SetParent(grid.gameObject.transform);
    transform.position += grid.GetCellPosition(x, y);
  }

  public void SetActive(bool state) {
    isActive = state;
    grid.SetActiveCell(this, state);
    HighlightCell(state ? Colors.active : Colors.clear);

    foreach (int[] neighbourIndex in neighbours) {
      if (!grid.IsValidGridIndex(neighbourIndex)) continue;

      grid
        .GetCellAt(neighbourIndex)
        .HighlightCell(state ? Colors.neighbour : Colors.clear);
    }
  }

  void HighlightCell(Colors state) {

    Color32 cellColor = new Color32(255, 255, 255, 255);

    if (state == Colors.active) cellColor = activeColor;
    if (state == Colors.neighbour) cellColor = neighbourColor;

    GetComponent<SpriteRenderer>().color = cellColor;
  }

  void OnMouseDown() {

    bool wasActive = isActive;

    if (!KeyRegister.Instance.ShiftActive()) {
      grid.DeactiveAllCells();
    }

    SetActive(!wasActive);
  }
}
