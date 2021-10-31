using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _roadWidth;
    [SerializeField][Range(0, 0.01f)] private float _sensetivity;

    private PlayerInput _input;
    private float _screenTouchDeltaX;

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
        _screenTouchDeltaX = _input.Player.MoveX.ReadValue<float>();
    }

    public void OnMoveX()
    {
        float targetPositionX = Mathf.Clamp(transform.position.x + (_screenTouchDeltaX * _sensetivity), -_roadWidth / 2, _roadWidth / 2);

        transform.position = new Vector3(targetPositionX, 0, transform.position.z);
    }
}
