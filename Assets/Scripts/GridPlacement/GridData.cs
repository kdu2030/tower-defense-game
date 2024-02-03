using System.Collections.Generic;
using UnityEngine;

public class GridData {
    private Dictionary<Vector2Int, PlacementData> gridPlacementData = new Dictionary<Vector2Int, PlacementData>();

    public List<Vector2Int> CalculateOccupiedPositions(Vector2Int position, Vector2Int size) {
        List<Vector2Int> occupiedPositions = new List<Vector2Int>();

        for (int x = 0; x < size.x; x++) {
            for (int y = 0; y < size.y; y++) {
                occupiedPositions.Add(new Vector2Int(position.x + x, position.y + y));
            }
        }
        return occupiedPositions;
    }

    public void AddObjectAt(int placeableObjectId, int placedObjectIndex, Vector2Int position, Vector2Int size) {
        List<Vector2Int> positionsToOccupy = CalculateOccupiedPositions(position, size);

        foreach (Vector2Int positionToOccupy in positionsToOccupy) {
            if (gridPlacementData.ContainsKey(positionToOccupy)) {
                throw new System.Exception($"Grid Placement Data already contains an object at position (${positionToOccupy.x}, ${positionToOccupy.y})");
            }

            PlacementData placementData = new PlacementData(positionsToOccupy, placeableObjectId, placedObjectIndex);
            gridPlacementData[positionToOccupy] = placementData;
        }
    }

    public bool CanPlaceAt(Vector2Int position, Vector2Int size) {
        List<Vector2Int> positionsToOccupy = CalculateOccupiedPositions(position, size);

        foreach (Vector2Int positionToOccupy in positionsToOccupy) {
            if (gridPlacementData.ContainsKey(positionToOccupy)) {
                return false;
            }
        }

        return true;
    }

}
