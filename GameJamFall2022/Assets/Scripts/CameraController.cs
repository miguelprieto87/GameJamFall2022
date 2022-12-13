using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("CameraSettings")]
    [SerializeField] private float cameraSpeed = 2f;

    [Header("Camera Boundaries")]
    [SerializeField] private Transform leftBoundary;
    [SerializeField] private Transform rightBoundary;
    [SerializeField] private Transform upperBoundary;
    [SerializeField] private Transform lowerBoundary;

    [Header("Player Variables")]
    private PlayerController player;
    private Vector3 offset;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        if (player == null)
        {
            Debug.LogError("Error: player not found!");
        }

        GetCameraOffset();
    }
    void LateUpdate()
    {
        FollowPlayer();
    }

    private void GetCameraOffset()
    {
        offset = transform.position - player.transform.position;
    }

    private void FollowPlayer()
    {
        Vector3 smoothPosition = Vector3.Lerp(transform.position, player.transform.position + offset, cameraSpeed);
        transform.position = smoothPosition;
        ClampsCameraToBoundaries();
    }
    private void ClampsCameraToBoundaries()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x,
                leftBoundary?.position.x ?? transform.position.x,
                rightBoundary?.position.x ?? transform.position.x
            ),
            Mathf.Clamp(transform.position.y,
                lowerBoundary?.position.y ?? transform.position.y,
                upperBoundary?.position.y ?? transform.position.y
            ),
            transform.position.z
        );
    }
}
