using UnityEngine;
using UnityEngine.Tilemaps;

public class PlacementSystem : MonoBehaviour {
    [Header("References")]
    [SerializeField] private GameObject cellIndicator;
    [SerializeField] private Grid grid;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject placementSystemPanel;
    [SerializeField] private PlaceableObjectsDB placeableObjectsDB;
    [SerializeField] private Tilemap pathTilemap;

    private SpriteRenderer indicatorSpriteRenderer;
    private PlaceableObject selectedObject = null;
    private bool isBuilderActive = false;
    private GridData gridData;

    private void Awake() {
        UpdateBuilderState(false);
        indicatorSpriteRenderer = cellIndicator.GetComponent<SpriteRenderer>();
        Vector2 indicatorDimensions = indicatorSpriteRenderer.bounds.size;
        cellIndicator.transform.localScale = TransformHelpers.GetScaleFromDimensions(indicatorDimensions, grid.cellSize);
        inputManager.ActivateBuilder += StopPlacement;
    }


    private bool CanPlaceObjectOnTile(Vector3 mousePosition) {
        Vector3Int pathTilemapCellPosition = pathTilemap.WorldToCell(mousePosition);
        if (pathTilemap.GetTile(pathTilemapCellPosition) == null) {
            return true;
        }
        return false;
    }

    private void UpdateBuilderState(bool newBuilderState) {
        isBuilderActive = newBuilderState;
        cellIndicator.SetActive(newBuilderState);
        placementSystemPanel.SetActive(newBuilderState);
    }

    private void StopPlacement() {
        UpdateBuilderState(!isBuilderActive);
        selectedObject = null;
        inputManager.OnClicked -= PlaceGameObject;
    }

    private Vector2 GetMousePosition() {
        Vector3 mousePositionPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2(mousePositionPoint.x, mousePositionPoint.y);
    }

    private void UpdateCellIndicatorPosition() {
        Vector2 mousePosition = GetMousePosition();
        Vector3Int cellPosition = grid.WorldToCell(mousePosition);
        cellIndicator.transform.position = grid.GetCellCenterWorld(cellPosition);
        indicatorSpriteRenderer.color = CanPlaceObjectOnTile(mousePosition) ? Color.white : Color.red;

    }

    public void StartPlacement(int placeableObjectID) {
        selectedObject = placeableObjectsDB.PlaceableObjects.Find(placeableObject => placeableObject.ID == placeableObjectID);
        if (selectedObject == null) {
            Debug.LogError($"Unable to find object with id {placeableObjectID}");
            return;
        }
        inputManager.OnClicked += PlaceGameObject;
    }

    public void PlaceGameObject() {
        if (selectedObject == null || inputManager.IsPointerOverUI()) {
            return;
        }
        Vector2 mousePosition = GetMousePosition();
        Vector3Int cellPosition = grid.WorldToCell(mousePosition);
        GameObject placedGameObject = Instantiate(selectedObject.Prefab);
        placedGameObject.transform.position = cellPosition;
    }

    private void Update() {
        if (isBuilderActive) {
            UpdateCellIndicatorPosition();
        }

    }
}
