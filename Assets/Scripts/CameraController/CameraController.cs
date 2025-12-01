using Unity.Cinemachine;
using UnityEngine;

namespace CameraController
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float rotationSpeed = 100f;
        [SerializeField] private float zoomSpeed = 0.05f;
        [SerializeField] private CinemachineCamera cinemachineCamera;

        private float _zoom = 0.5f;
        private CinemachinePositionComposer _positionComposer;

        private const float CAMERA_DISTANCE_MIN = 1f;
        private const float CAMERA_DISTANCE_MAX = 13f;
        private const float TARGET_OFFSET_MIN = 1.2f;
        private const float TARGET_OFFSET_MAX = 4.8f;

        private void Awake()
        {
            _positionComposer = cinemachineCamera.GetComponent<CinemachinePositionComposer>();
        }

        private void Update()
        {
            Vector3 inputMoveDir = new Vector3(0, 0, 0);
            Vector3 inputRotation = new Vector3(0, 0, 0);

            if(Input.GetKey(KeyCode.W))
            {
                inputMoveDir.z = +1f;
            }
            if(Input.GetKey(KeyCode.S))
            {
                inputMoveDir.z = -1f;
            }
            if(Input.GetKey(KeyCode.A))
            {
                inputMoveDir.x = -1f;
            }
            if(Input.GetKey(KeyCode.D))
            {
                inputMoveDir.x = +1f;
            }
            if(Input.GetKey(KeyCode.Q))
            {
                inputRotation.y = +1f;
            }
            if(Input.GetKey(KeyCode.E))
            {
                inputRotation.y = -1f;
            }
            if(Input.mouseScrollDelta.y != 0)
            {
                _zoom -= Input.mouseScrollDelta.y * zoomSpeed;
                _zoom = Mathf.Clamp01(_zoom);
            }

            Vector3 moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
            transform.position += moveVector * (moveSpeed * Time.deltaTime);
            transform.eulerAngles += inputRotation * (rotationSpeed * Time.deltaTime);

            _positionComposer.CameraDistance = Mathf.Lerp(CAMERA_DISTANCE_MIN, CAMERA_DISTANCE_MAX, _zoom);
            _positionComposer.TargetOffset.y = Mathf.Lerp(TARGET_OFFSET_MIN, TARGET_OFFSET_MAX, _zoom);
        }
    }
}
