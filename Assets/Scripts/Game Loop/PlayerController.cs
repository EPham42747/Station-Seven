using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Transform cameraParent;
    private Transform mainCamera;

    [Header("Rotate")]
    public float rotationSpeed;

    [Header("Pan")]
    public Vector2 moveBounds;
    public float minMoveSpeed;
    public float maxMoveSpeed;
    private Vector3 startPos;
    private Vector3 offset = Vector3.zero;

    [Header("Zoom")]
    public float minZoom;
    public float maxZoom;
    public float zoomSpeed;

    private void Start() {
        mainCamera = cameraParent.GetChild(0).GetChild(0).GetComponent<Transform>();
        startPos = mainCamera.localPosition;
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

        Vector2 lowBounds = -moveBounds * (1 - zoomRatio);
        Vector2 highBounds = moveBounds * (1 - zoomRatio);

        offset -= (Vector3.right * Input.GetAxis("Mouse X") + Vector3.up * Input.GetAxis("Mouse Y")) * _speed * Time.deltaTime;
        offset = new Vector3(
            Mathf.Clamp(offset.x, lowBounds.x, highBounds.x),
            Mathf.Clamp(offset.y, lowBounds.y, highBounds.y),
            offset.z
        );

        mainCamera.localPosition = startPos + offset;
        Cursor.visible = false;
    }
}
