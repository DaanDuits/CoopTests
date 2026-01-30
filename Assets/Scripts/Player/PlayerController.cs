using UnityEngine;
using UnityEngine.InputSystem;
using FishNet.Object;
using DaanBanaan.Input;


namespace DaanBanaan.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : NetworkBehaviour, CustomInput.IPlayerActions
    {
        [SerializeField] private float speed;

        private CustomInput _actions;

        private CharacterController _controller;

        private float _horizontalInput, _verticalInput;
        private Vector3 _velocity;

        private InputAction _onMove;

        public override void OnStartNetwork()
        {
            _controller = GetComponent<CharacterController>();

            TimeManager.OnTick += TimeManagerTick;
        }

        public override void OnStopNetwork()
        {
            TimeManager.OnTick -= TimeManagerTick;
        }

        private void OnEnable()
        {
            _actions = new CustomInput();
            _actions.Enable();
            _actions.Player.SetCallbacks(this);
        }

        private void OnDisable()
        {
            _actions.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (!IsOwner) return;

            Vector2 moveValue = context.ReadValue<Vector2>();

            _horizontalInput = moveValue.x;
            _verticalInput = moveValue.y;
        }

        private void TimeManagerTick()
        {
            float tickDelta = (float)TimeManager.TickDelta;

            Vector3 desiredVelocity = (transform.right * _horizontalInput + transform.forward * _verticalInput) * speed;
            desiredVelocity = Vector3.ClampMagnitude(desiredVelocity, speed);

            _velocity.x = desiredVelocity.x;
            _velocity.z = desiredVelocity.z;
            
            if (!IsOwner) return;
            _controller.Move(_velocity * tickDelta);
        }
    }
}