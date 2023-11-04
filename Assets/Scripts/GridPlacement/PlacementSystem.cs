using UnityEngine;

public class PlacementSystem : MonoBehaviour {
    [Header("References")]
    [SerializeField] private GameObject cellIndicator;
    [SerializeField] private Grid grid;

    private SpriteRenderer indicatorSpriteRenderer;

    private Vector2 GetMousePosition() {
        Vector3 mousePositionPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2(mousePositionPoint.x, mousePositionPoint.y);
    }

    private void Start() {
        indicatorSpriteRenderer = cellIndicator.GetComponent<SpriteRenderer>();

        Vector2 indicatorDimensions = indicatorSpriteRenderer.bounds.size;
        cellIndicator.transform.localScale = TransformHelpers.GetScaleFromDimensions(indicatorDimensions, grid.cellSize);
    }

    private void Update() {
        Vector2 mousePosition = GetMousePosition();
        Vector3Int cellPosition = grid.WorldToCell(mousePosition);
        cellIndicator.transform.position = grid.CellToWorld(cellPosition);
    }
}
