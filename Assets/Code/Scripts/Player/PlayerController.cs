using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class PlayerController : Singleton<PlayerController>, ISPContext<PlayerStateID>
    {
        private List<BaseSPState<PlayerStateID>> currentStates;
        private Dictionary<PlayerStateID, BaseSPState<PlayerStateID>> states;

        // Internal Depedencies
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public Rigidbody2D Rb2d { get; private set; }
        [field: SerializeField] public ObjectDetector GroundDetector { get; private set; }
        [field: SerializeField] public ObjectChangingDirection ChangingDirection { get; private set; }
        [field: SerializeField] public PlayerMovement Movement { get; private set; }
        [field: SerializeField] public PlayerClimbing Climbing { get; private set; }
        [field: SerializeField] public PlayerJumping Jumping { get; private set; }
        [field: SerializeField] public ObjectAttacking Attacking { get; private set; }
        [field: SerializeField] public PlayerCollision Collision { get; private set; }
        [field: SerializeField] public ObjectDespawning Despawning { get; private set; }
        [field: SerializeField] public VFXHandler VFXHandler { get; private set; }
        List<BaseSPState<PlayerStateID>> ISPContext<PlayerStateID>.CurrentStates { get => currentStates; set => currentStates = value; }
        Dictionary<PlayerStateID, BaseSPState<PlayerStateID>> ISPContext<PlayerStateID>.States { get => states; set => states = value; }

        protected override void Awake()
        {
            base.Awake();
            LoadComponents();

            InitializeStates();
            gameObject.SetActive(false);
        }

        protected virtual void OnEnable()
        {
            SetDefaultState();
        }

        private void LoadComponents()
        {
            if (Rb2d == null) Rb2d = GetComponent<Rigidbody2D>();
            if (Animator == null) Animator = GetComponent<Animator>();
            else Animator.keepAnimatorStateOnDisable = true;
            if (GroundDetector == null) GroundDetector = GetComponentInChildren<ObjectDetector>();
            if (ChangingDirection == null) ChangingDirection = GetComponentInChildren<ObjectChangingDirection>();
            if (Movement == null) Movement = GetComponentInChildren<PlayerMovement>();
            if (Climbing == null) Climbing = GetComponentInChildren<PlayerClimbing>();
            if (Jumping == null) Jumping = GetComponentInChildren<PlayerJumping>();
            if (Attacking == null) Attacking = GetComponentInChildren<ObjectAttacking>();
            if (Collision == null) Collision = GetComponentInChildren<PlayerCollision>();
            if (Despawning == null) Despawning = GetComponentInChildren<ObjectDespawning>();
            if (VFXHandler == null) VFXHandler = GetComponentInChildren<VFXHandler>();
        }

        public void InitializeStates()
        {
            states = new();
            currentStates = new();

            var idelState = new PlayerIdelState(this, this);
            var moveState = new PlayerMoveState(this, this);
            var climbState = new PlayerClimbingState(this, this);
            var jumpState = new PlayerJumpState(this, this);
            var attackState = new PlayerAttackState(this, this);
            var deathState = new PlayerDeathState(this, this);

            states.Add(idelState.ID, idelState);
            states.Add(moveState.ID, moveState);
            states.Add(climbState.ID, climbState);
            states.Add(jumpState.ID, jumpState);
            states.Add(attackState.ID, attackState);
            states.Add(deathState.ID, deathState);
        }

        public void SetDefaultState()
        {
            ((ISPContext<PlayerStateID>)this).SetCurrentState(PlayerStateID.Idel);
        }

        protected virtual void OnTriggerEnter2D(Collider2D collider)
        {
            Collision.HandleTrigger(collider);
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision)
        {
            Collision.HandleCollision(collision);
        }

        protected virtual void OnTriggerExit2D(Collider2D collider)
        {
            Collision.HandleTriggerExit(collider);
        }
    }   
}