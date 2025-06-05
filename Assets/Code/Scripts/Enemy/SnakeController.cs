using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class SnakeController : MonoBehaviour, ISPContext<SnakeStateID>
    {
        private List<BaseSPState<SnakeStateID>> currentStates;
        private Dictionary<SnakeStateID, BaseSPState<SnakeStateID>> states;

        // Internal Depedencies
        [field:SerializeField] public Animator Animator { get; private set; }
        [field:SerializeField] public Rigidbody2D Rb2d { get; private set; }
        [field:SerializeField] public ObjectDetector PlayerDetector { get; private set; }
        [field:SerializeField] public ObjectChangingDirection ChangingDirection { get; private set; }
        [field:SerializeField] public ObjectMovement Moving { get; private set; }
        [field:SerializeField] public ObjectMovement Chasing { get; private set; }
        [field:SerializeField] public ObjectAttacking Attacking { get; private set; }
        [field: SerializeField] public ObjectCollision Collision { get; private set; }
        [field: SerializeField] public ObjectDespawning Despawning { get; private set; }
        [field: SerializeField] public VFXHandler VFXHandler { get; private set; }

        // External Depedencies
        public Rigidbody2D TargetRb2d { get; private set; }
        
        List<BaseSPState<SnakeStateID>> ISPContext<SnakeStateID>.CurrentStates { get => currentStates; set => currentStates = value; }
        Dictionary<SnakeStateID, BaseSPState<SnakeStateID>> ISPContext<SnakeStateID>.States { get => states; set => states = value; }

        protected virtual void Awake()
        {
            LoadComponents();
            SetTargetRb2d();

            InitializeStates();
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
            if (PlayerDetector == null) PlayerDetector = GetComponentInChildren<ObjectDetector>();
            if (ChangingDirection == null) ChangingDirection = GetComponentInChildren<ObjectChangingDirection>();
            if (Moving == null) Moving = GetComponentInChildren<ObjectMovement>();
            if (Chasing == null) Chasing = GetComponentInChildren<ObjectMovement>();
            if (Attacking == null) Attacking = GetComponentInChildren<ObjectAttacking>();
            if (Collision == null) Collision = GetComponentInChildren<ObjectCollision>();
            if (VFXHandler == null) VFXHandler = GetComponentInChildren<VFXHandler>();
        }

        protected virtual void SetTargetRb2d()
        {
            if (TargetRb2d == null) TargetRb2d = PlayerController.Instance.Rb2d;
        }

        public void InitializeStates()
        {
            currentStates = new();
            states = new();

            var idelState = new SnakeIdelState(this, this);
            var moveState = new SnakeMoveState(this, this);
            var chaseState = new SnakeChaseState(this, this);
            var attackState = new SnakeAttackState(this, this);
            var deathState = new SnakeDeathState(this, this);

            states.Add(idelState.ID, idelState);
            states.Add(moveState.ID, moveState);
            states.Add(chaseState.ID, chaseState);
            states.Add(attackState.ID, attackState);
            states.Add(deathState.ID, deathState);

            SetDefaultState();
        }

        public void SetDefaultState()
        {
            ((ISPContext<SnakeStateID>)this).SetCurrentState(SnakeStateID.Idel);
        }

        protected virtual void OnTriggerEnter2D(Collider2D collider)
        {
            Collision.HandleTrigger(collider);
        }

        protected virtual void OnCollisionEnter2D(Collision2D collision) {
            Collision.HandleCollision(collision);
        }
    }   
}