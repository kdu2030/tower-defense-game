using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CellIndicator : MonoBehaviour {
    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap pathTilemap;

    private SpriteRenderer spriteRenderer;
    public bool IsValid { get; set; } = true;
    private bool onTerrainObject = false;

    // TODO: Need Script to attach the following to each terrain object
    /*
     * 1. Attach Rigidbody2D and Box Collider 2D to each terrain object
     * 2. Modify the tag to be Terrain
     */

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetCellIndicatorScale();
    }

    private void SetCellIndicatorScale() {
        Vector2 indicatorDimensions = spriteRenderer.bounds.size;
        transform.localScale = TransformHelpers.GetScaleFromDimensions(indicatorDimensions, grid.cellSize);
    }

    public bool CanPlaceObjectOnTile(Vector3 mousePosition) {
        if (onTerrainObject) return false;

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
        onTerrainObject = CollidedWithTerrainObject(collision);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        onTerrainObject = false;
    }

    private void Update() {
        IsValid = CanPlaceObjectOnTile(transform.position);
        spriteRenderer.color = IsValid ? Color.white : Color.red;
    }
}
