using System.Collections.Generic;
using UnityEngine;

public class PlacementData {

    public int PlaceableObjectId { get; set; }

    public List<Vector2Int> OccupiedPositions { get; set; }

    public int PlacedObjectIndex { get; set; }

    public PlacementData(List<Vector2Int> occupiedPositions, int placeableObjectId, int placedObjectIndex) {
        PlaceableObjectId = placeableObjectId;
        OccupiedPositions = occupiedPositions;
        PlacedObjectIndex = placedObjectIndex;
    }

}