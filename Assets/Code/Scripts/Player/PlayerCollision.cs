using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D{
    public class PlayerCollision : ObjectCollision
    {
        protected Dictionary<HittingObjectType, Action<Collider2D>> triggerExitHandles;

        protected PlayerController playerController;

        protected override void Awake()
        {
            base.Awake();

            InitializeTriggerExitHandles();
            SetPlayerController();
        }

        protected virtual void SetPlayerController() => playerController = GetComponentInParent<PlayerController>();

        protected override void InitializeCollisionHandles()
        {
            collisionHandles = new()
            {
                { HittingObjectType.Enemy, (collision) => OnCollideEnemy(collision) },
                { HittingObjectType.DeathZone, (collision) => OnCollideDeathZone(collision) },
                { HittingObjectType.NextDoor, (collision) => OnCollideNextDoor(collision) },
            };
        }

        protected override void InitializeTriggerHandles()
        {
            triggerHandles = new()
            {
                { HittingObjectType.Enemy, (collider) => OnTriggerEnemy(collider) },
                { HittingObjectType.ClimbingTree, (collider) => StartCoroutine(OnTriggerClibingTree(collider)) },
                { HittingObjectType.Key, (collider) => OnTriggerKey(collider) },
            };
        }

        protected virtual void InitializeTriggerExitHandles()
        {
            triggerExitHandles = new()
            {
                { HittingObjectType.ClimbingTree, (collider) => OnTriggerClibingTreeExit(collider) },
            };
        }

        public virtual void HandleTriggerExit(Collider2D collider)
        {
            if (!collider.transform.TryGetComponent<ObjectCollision>(out ObjectCollision hittedObject)) return;

            if (triggerExitHandles.TryGetValue(hittedObject.ObjectType, out Action<Collider2D> actionHandleTriggerExit))
                actionHandleTriggerExit(collider);
        }

        protected virtual void OnTriggerEnemy(Collider2D collider)
        {
            playerController.Despawning.InitializeDespawn();
        }

        protected virtual void OnCollideEnemy(Collision2D collision)
        {
            ContactPoint2D contact = collision.GetContact(0);
            Vector2 colDir = (contact.point - playerController.Rb2d.position).normalized;

            if (Vector2.Dot(colDir, Vector2.up) < -0.5f)
                playerController.Jumping.Jump(true);
            else
                playerController.Despawning.InitializeDespawn();
        }

        protected virtual IEnumerator OnTriggerClibingTree(Collider2D collider)
        {
            while (playerController.Attacking.IsAttacking)
                yield return new WaitForFixedUpdate();
            ((ISPContext<PlayerStateID>)playerController).SetCurrentState(PlayerStateID.Climb);
        }

        protected virtual void OnTriggerClibingTreeExit(Collider2D collider)
        {
            ((ISPContext<PlayerStateID>)playerController).SetDefaultState();
            //Thêm 1 tí offset pos để thoát hẳn khỏi Collider
            playerController.Rb2d.position += new Vector2(0, 0.1f);
        }

        protected virtual void OnCollideDeathZone(Collision2D collision)
        {
            playerController.Despawning.InitializeDespawn();
        }

        protected virtual void OnTriggerKey(Collider2D collider)
        {
            LevelManager.Instance.AddCurrentKey();
        }

        protected virtual void OnCollideNextDoor(Collision2D collision)
        {
            LevelManager.Instance.LoadNextLevel();
        }
    }
}