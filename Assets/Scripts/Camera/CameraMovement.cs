using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [SerializeField]
    private float cameraSpeedX = 1f;

    [SerializeField]
    private float cameraSpeedY = 1f;

    [SerializeField]
    private Transform topLeftGridPoint;

    [SerializeField]
    private Transform bottomRightGridPoint;

    private Vector3 currentCamPosition;

    private Vector3 ClampCameraMovement(float propsedCamMovementX, float proposedCamMovementY) {
        float halfScreenHeight = Camera.main.orthographicSize;
        float halfScreenWidth = halfScreenHeight * Camera.main.aspect;

        float topGridEdgeY = topLeftGridPoint.position.y;
        float bottomGridEdgeY = bottomRightGridPoint.position.y;
        float rightGridEdgeX = bottomRightGridPoint.position.x;
        float leftGridEdgeX = topLeftGridPoint.position.x;

        float finalCameraMovementX = Mathf.Clamp(propsedCamMovementX, leftGridEdgeX + halfScreenWidth, rightGridEdgeX - halfScreenWidth);
        float finalCameraMovementY = Mathf.Clamp(proposedCamMovementY, bottomGridEdgeY + halfScreenHeight, topGridEdgeY - halfScreenHeight);

        return new Vector3(finalCameraMovementX, finalCameraMovementY, currentCamPosition.z);
    }


    private void Update() {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        currentCamPosition = Camera.main.transform.position;

        float newPosX = currentCamPosition.x + xInput * cameraSpeedX * Time.deltaTime;
        float newPosY = currentCamPosition.y + yInput * cameraSpeedY * Time.deltaTime;

        Camera.main.transform.position = ClampCameraMovement(newPosX, newPosY);
    }
}
