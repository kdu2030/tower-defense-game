using UnityEngine;

public class PlacementSystem : MonoBehaviour {
    [Header("References")]
    [SerializeField] private GameObject cellIndicator;
    [SerializeField] private Grid grid;

    private Vector2 GetMousePosition() {
        Vector3 mousePositionPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2(mousePositionPoint.x, mousePositionPoint.y);
    }

    private void Start() {

    }

    private void Update() {
        Vector2 mousePosition = GetMousePosition();
        Vector3Int cellPosition = grid.WorldToCell(mousePosition);
        cellIndicator.transform.position = grid.CellToWorld(cellPosition);
    }
}
