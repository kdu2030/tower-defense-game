using UnityEngine;
using UnityEngine.Tilemaps;

public class PlacementSystem : MonoBehaviour {
    [Header("References")]
    [SerializeField] private GameObject cellIndicator;
    [SerializeField] private Grid grid;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject placementSystemPanel;
    [SerializeField] private PlaceableObjectsDB placeableObjectsDB;

    private PlaceableObject selectedObject = null;
    private bool isBuilderActive = false;
    private GridData gridData;

    private CellIndicator cellIndicatorHandler;

    private void Awake() {
        UpdateBuilderState(false);
        inputManager.ActivateBuilder += StopPlacement;
    }

    private void Start() {
        cellIndicatorHandler = cellIndicator.GetComponent<CellIndicator>();
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
        cellIndicatorHandler.UpdateCellIndicatorPosition(GetMousePosition());
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
        if (selectedObject == null || inputManager.IsPointerOverUI() || !cellIndicatorHandler.IsValid) {
            return;
        }

        GameObject placedGameObject = Instantiate(selectedObject.Prefab);
        placedGameObject.transform.position = cellIndicator.transform.position;
    }

    private void Update() {
        if (isBuilderActive) {
            UpdateCellIndicatorPosition();
            cellIndicatorHandler.IsValid = cellIndicatorHandler.CanPlaceObjectOnTile(GetMousePosition(), selectedObject);
        }

    }
}
