using UnityEngine;

public class PlacementSystem : MonoBehaviour {
    [Header("References")]
    [SerializeField] private GameObject cellIndicator;
    [SerializeField] private Grid grid;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject placementSystemPanel;

    private SpriteRenderer indicatorSpriteRenderer;
    private bool isBuilderActive = false;

    private void Awake() {
        UpdateBuilderState(false);
        indicatorSpriteRenderer = cellIndicator.GetComponent<SpriteRenderer>();
        Vector2 indicatorDimensions = indicatorSpriteRenderer.bounds.size;
        cellIndicator.transform.localScale = TransformHelpers.GetScaleFromDimensions(indicatorDimensions, grid.cellSize);
        inputManager.ActivateBuilder += FlipBuilderState;
    }

    private void UpdateBuilderState(bool newBuilderState) {
        isBuilderActive = newBuilderState;
        cellIndicator.SetActive(newBuilderState);
        placementSystemPanel.SetActive(newBuilderState);
    }

    private void FlipBuilderState() {
        UpdateBuilderState(!isBuilderActive);
    }

    private Vector2 GetMousePosition() {
        Vector3 mousePositionPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2(mousePositionPoint.x, mousePositionPoint.y);
    }

    private void UpdateCellIndicatorPosition() {
        Vector2 mousePosition = GetMousePosition();
        Vector3Int cellPosition = grid.WorldToCell(mousePosition);
        cellIndicator.transform.position = grid.GetCellCenterWorld(cellPosition);
    }

    private void Update() {
        if (isBuilderActive) {
            UpdateCellIndicatorPosition();
        }

    }
}
