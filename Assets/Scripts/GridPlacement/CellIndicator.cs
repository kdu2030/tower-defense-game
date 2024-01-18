using UnityEngine;

public class CellIndicator : MonoBehaviour {
    [SerializeField] private Grid grid;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        SetCellIndicatorScale();
    }

    private void SetCellIndicatorScale() {
        Vector2 indicatorDimensions = spriteRenderer.bounds.size;
        transform.localScale = TransformHelpers.GetScaleFromDimensions(indicatorDimensions, grid.cellSize);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // NOTE: This only gets triggered when another collider enters the sprite collider, so if there is already overlap, it won't trigger again
        Debug.Log(collision.gameObject.name);
    }
}
