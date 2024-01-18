using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CellIndicator : MonoBehaviour {
    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap pathTilemap;

    private SpriteRenderer spriteRenderer;
    public bool IsValid { get; set; } = true;

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
        if (!IsValid) return false;

        Vector3Int pathTilemapCellPosition = pathTilemap.WorldToCell(mousePosition);
        if (pathTilemap.GetTile(pathTilemapCellPosition) == null) {
            return true;
        }
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == TagConstants.Terrain) {
            IsValid = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == TagConstants.Terrain) {
            IsValid = true;
        }
    }

    private void Update() {
        spriteRenderer.color = IsValid ? Color.white : Color.red;
    }
}
