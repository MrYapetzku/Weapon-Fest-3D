using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _roadWidth;

    private PlayerInput _input;
    private float _screenTouchPositionX;

    private void Awake()
    {
        _input = new PlayerInput();
    }

    private void OnEnable()
    {
        _input.Player.Enable();
        _input.Player.MoveX.performed += ctx => OnMoveX();
    }

    private void OnDisable()
    {
        _input.Player.Disable();
        _input.Player.MoveX.performed -= ctx => OnMoveX();
    }

    private void Update()
    {
        transform.position += Vector3.forward * _speed * Time.deltaTime;
        _screenTouchPositionX = _input.Player.MoveX.ReadValue<float>();
    }

    public void OnMoveX()
    {
        var screenTouchPositionXNormalized = Mathf.Clamp(_screenTouchPositionX, 0, Camera.main.scaledPixelWidth) / Camera.main.scaledPixelWidth;

        transform.position = new Vector3((screenTouchPositionXNormalized - 0.5f) * _roadWidth, 0, transform.position.z);
    }
}
