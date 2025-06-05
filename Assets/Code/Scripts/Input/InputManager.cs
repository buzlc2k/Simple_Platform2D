using System.Collections;
using UnityEngine;

namespace Platformer2D
{
    public class InputManager : Singleton<InputManager>
    {
        private InputSystem_Actions inputActions;

        protected override void Awake()
        {
            inputActions = new();
            inputActions.Enable();
            
            SwitchToHorizontalMap();
        }

        private void Update()
        {
            if (inputActions.Horizontal.enabled)
            {
                HandleMoveInput();
                HandleJumpInput();
                HandleAttackInput();
            }
            else
                HandleClimbInput();
        }

        public void SwitchToHorizontalMap()
        {
            inputActions.Horizontal.Enable();
            inputActions.Vertical.Disable();
        }

        public void SwitchToVerticalMap()
        {
            inputActions.Horizontal.Disable();
            inputActions.Vertical.Enable();
        }


        private Vector2 climbValue = Vector2.zero;
        public Vector2 ClimbValue { get => climbValue; }

        private void HandleClimbInput()
        {
            climbValue = inputActions.Vertical.Climb.ReadValue<Vector2>();
        }

        private float moveValue = 0;
        public float MoveValue { get => moveValue; }

        private void HandleMoveInput()
        {
            moveValue = inputActions.Horizontal.Move.ReadValue<float>();
        }

        private bool jumpTriggered = false;
        public bool JumpTriggered { get => jumpTriggered; }
        private void HandleJumpInput()
        {
            if (jumpTriggered || !inputActions.Horizontal.Jump.triggered) return;

            jumpTriggered = true;
            StartCoroutine(ResetJumpTriggered());
        }

        private IEnumerator ResetJumpTriggered()
        {
            yield return new WaitForFixedUpdate();
            jumpTriggered = false;
        }

        private int numAttackTriggered = 0;
        [SerializeField] private int numToTriggerSuperAttack = 3;
        private bool attackTriggered = false;
        private bool superAttackTriggered = false;
        public bool AttackTriggered { get => attackTriggered; }
        public bool SuperAttackTriggered { get => superAttackTriggered; }
        private void HandleAttackInput()
        {
            if (attackTriggered || superAttackTriggered || !inputActions.Horizontal.Attack.triggered) return;
            numAttackTriggered++;

            if (numAttackTriggered < numToTriggerSuperAttack)
                attackTriggered = true;
            else
                superAttackTriggered = true;

            StartCoroutine(ResetAttackTriggered());
        }

        private IEnumerator ResetAttackTriggered()
        {
            yield return new WaitForFixedUpdate();

            if (superAttackTriggered)
            {
                superAttackTriggered = false;
                numAttackTriggered = 0;
            }

            else
                attackTriggered = false;
            
        }
    }   
}