using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CellIndicator : MonoBehaviour {
    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap pathTilemap;

    private SpriteRenderer spriteRenderer;
    public bool IsValid { get; set; } = true;
    public GridData GameGridData { get; set; }
    private List<GameObject> placedGameObjects = new List<GameObject>();

    private GameObject overlappingTerrainObject = null;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetCellIndicatorScale();
        GameGridData = new GridData();
    }

    private void SetCellIndicatorScale() {
        Vector2 indicatorDimensions = spriteRenderer.bounds.size;
        transform.localScale = TransformHelpers.GetScaleFromDimensions(indicatorDimensions, grid.cellSize);
    }

    public void UpdateGridData(PlaceableObject placeableObjectTemplate, GameObject createdGameObject) {
        // We need the top left of the current cell indicator position
        Vector2Int gridPosition = (Vector2Int)grid.WorldToCell(transform.position);
        GameGridData.AddObjectAt(placeableObjectTemplate.ID, placedGameObjects.Count + 1, gridPosition, placeableObjectTemplate.Size);
        placedGameObjects.Add(createdGameObject);
    }

    public bool CanPlaceObjectOnTile(Vector2 mousePosition, PlaceableObject selectedObject) {
        if (overlappingTerrainObject != null) return false;

        Vector3Int pathTilemapCellPosition = pathTilemap.WorldToCell(mousePosition);
        if (pathTilemap.GetTile(pathTilemapCellPosition) != null) {
            return false;
        }

        if (selectedObject != null) {
            Vector2Int gridPosition = (Vector2Int)grid.WorldToCell(mousePosition);
            return GameGridData.CanPlaceAt(gridPosition, selectedObject.Size);
        }

        return true;
    }

    private bool CollidedWithTerrainObject(Collider2D collision) {
        Transform parentGameObjectTransform = collision.gameObject.GetComponentInParent<Transform>();
        GameObject parentGameObject = parentGameObjectTransform.gameObject;
        return parentGameObject.tag.Equals(TagConstants.Terrain);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (CollidedWithTerrainObject(collision)) {
            overlappingTerrainObject = collision.gameObject;
        };
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (overlappingTerrainObject != null && collision.gameObject.GetHashCode() == overlappingTerrainObject.GetHashCode()) {
            overlappingTerrainObject = null;
        }
    }

    public void UpdateCellIndicatorPosition(Vector2 mousePosition) {
        Vector3Int cellPosition = grid.WorldToCell(mousePosition);
        transform.position = grid.GetCellCenterWorld(cellPosition);
    }

    private void Update() {
        spriteRenderer.color = IsValid ? Color.white : Color.red;
    }
}
