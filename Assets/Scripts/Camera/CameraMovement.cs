using System.Collections;
using System.Collections.Generic;
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

    private void Start() {


    }

    private void ClampCameraMovement(Vector3 cameraMovement) {
        float halfScreenHeight = Camera.main.orthographicSize;
        float halfScreenWidth = halfScreenHeight * Camera.main.aspect;

        float topCamEdgeY = currentCamPosition.y + halfScreenHeight;
        float bottomCamEdgeY = currentCamPosition.y - halfScreenHeight;
        float rightCamEdgeX = currentCamPosition.x + halfScreenWidth;
        float leftCamEdgeX = currentCamPosition.x - halfScreenWidth;

        float topGridEdgeY = topLeftGridPoint.position.y;
        float bottomGridEdgeY = bottomRightGridPoint.position.y;
        float rightGridEdgeX = bottomRightGridPoint.position.x;
        float leftGridEdgeX = topLeftGridPoint.position.x;
    }


    private void Update() {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        currentCamPosition = Camera.main.transform.position;

        float newPosX = currentCamPosition.x + xInput * cameraSpeedX;
        float newPosY = currentCamPosition.y + yInput * cameraSpeedY;

        Camera.main.transform.position = new Vector3(newPosX, newPosY, currentCamPosition.z);

        // TODO Add check to make sure that the camera doesn't scroll off the screen

        ClampCameraMovement(new Vector3(newPosX, newPosY, currentCamPosition.z));

    }
}
