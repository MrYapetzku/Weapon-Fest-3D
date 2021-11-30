using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] [Min(0)] private float _speed;
    [SerializeField] [Min(0)] private float _roadWidth;
    [SerializeField] [Range(0, 0.01f)] private float _sensetivity;

    private PlayerInput _input;
    private float _screenTouchDeltaX;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _input = new PlayerInput();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _input.Player.Enable();
        _input.Player.MoveX.performed += ctx => OnMoveX();

        _rigidbody.velocity = Vector3.forward * _speed;
    }

    private void OnDisable()
    {
        _input.Player.Disable();
        _input.Player.MoveX.performed -= ctx => OnMoveX();

        _rigidbody.velocity = Vector3.zero;
    }

    private void Update()
    {
        _screenTouchDeltaX = _input.Player.MoveX.ReadValue<float>();
    }

    public void OnMoveX()
    {
        float targetPositionX = Mathf.Clamp(transform.position.x + (_screenTouchDeltaX * _sensetivity), -_roadWidth / 2, _roadWidth / 2);

        transform.position = new Vector3(targetPositionX, 0, transform.position.z);
    }
}
