using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Transform cameraParent;
    private Transform mainCamera;

    public float rotationSpeed;

    public Vector3 moveBounds;
    public float minMoveSpeed;
    public float maxMoveSpeed;
    private Vector3 startPosition;

    public float minZoom;
    public float maxZoom;
    public float zoomSpeed;

    private void Start() {
        mainCamera = cameraParent.GetChild(0).GetComponent<Transform>();
        startPosition = mainCamera.localPosition;
    }

    private void Update() {
        Zoom();

        if (Input.GetKey(KeyCode.LeftShift)) Rotate();
        else if (Input.GetMouseButton(0)) Pan();
        else Cursor.visible = true;
    }

    private void Zoom() {
        float zoom = Input.mouseScrollDelta.y * zoomSpeed * Time.deltaTime;
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + zoom, minZoom, maxZoom);
    }

    private void Rotate() {
        cameraParent.Rotate(Vector3.up * Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime);
        Cursor.visible = false;
    }

    private void Pan() {
        float zoomRatio = (Camera.main.orthographicSize - minZoom) / (maxZoom - minZoom);
        float _speed = (maxMoveSpeed - minMoveSpeed) * zoomRatio + minMoveSpeed;
        Vector3 minMoveBounds = startPosition - moveBounds * (1 - zoomRatio);
        Vector3 maxMoveBounds = startPosition + moveBounds * (1 - zoomRatio);

        mainCamera.position += (mainCamera.right * Input.GetAxis("Mouse X") + mainCamera.up * Input.GetAxis("Mouse Y")) * _speed * Time.deltaTime;
        mainCamera.localPosition = new Vector3(
            Mathf.Clamp(mainCamera.localPosition.x, minMoveBounds.x, maxMoveBounds.x),
            Mathf.Clamp(mainCamera.localPosition.y, minMoveBounds.y, maxMoveBounds.y),
            Mathf.Clamp(mainCamera.localPosition.z, minMoveBounds.z, maxMoveBounds.z)
        );

        Cursor.visible = false;
    }
}
