using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CellIndicator : MonoBehaviour {
    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap pathTilemap;

    private SpriteRenderer spriteRenderer;
    public bool IsValid { get; set; } = true;
    private GameObject overlappingTerrainObject = null;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetCellIndicatorScale();
    }

    private void SetCellIndicatorScale() {
        Vector2 indicatorDimensions = spriteRenderer.bounds.size;
        transform.localScale = TransformHelpers.GetScaleFromDimensions(indicatorDimensions, grid.cellSize);
    }

    public bool CanPlaceObjectOnTile(Vector3 mousePosition) {
        if (overlappingTerrainObject != null) return false;

        Vector3Int pathTilemapCellPosition = pathTilemap.WorldToCell(mousePosition);
        if (pathTilemap.GetTile(pathTilemapCellPosition) != null) {
            return false;
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
        IsValid = CanPlaceObjectOnTile(transform.position);
        spriteRenderer.color = IsValid ? Color.white : Color.red;
    }
}
