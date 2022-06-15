using Assets.Food;
using Assets.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public class BaseCharacter : MonoBehaviour
    {
        [Header("Horizontal movement")]
        [SerializeField] protected float moveSpeed = 10;
        [SerializeField] protected Vector2 direction;
        [SerializeField] protected Collider2D _otherCollider;
        private bool facingRight = true;
        protected bool HasJumped = false;

        [Header("Verical movement")]
        [SerializeField] protected float jumpSpeed = 15f;
        [SerializeField] protected float jumpDelay = 0.25f;
        private float jumpTimer;

        [Header("Components")]
        [SerializeField] protected Rigidbody2D rigidBody;
        [SerializeField] protected LayerMask groundLayer;
        [SerializeField] protected Animator animator;
        [SerializeField] protected GameObject charachterHolder;

        [Header("Physics")]
        [SerializeField] protected float maxSpeed = 7f;
        [SerializeField] protected float linearDrag = 4f;
        [SerializeField] protected float gravity = 1f;
        [SerializeField] protected float fallMultiplier = 5f;

        [Header("Collision")]
        [SerializeField] protected bool onGround = false;
        [SerializeField] protected float groundLength = 0.6f;
        [SerializeField] protected Vector3 colliderOffset;

       

        [Header("Inventory")]
        [SerializeField] private InventorySystem _inventory;

        [Header("World References")]
        [SerializeField] private ChangeBackground _changeBackground;

        private void Awake()
        {
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), _otherCollider);
        }


        protected void OnTriggerEnter2D(Collider2D collision)
        {
            SwitchBackGround(collision);
            if (collision.gameObject.CompareTag("Food")) _inventory.AddToInventory(collision.gameObject.GetComponent<BaseIngredient>());
           
        }
        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Platform"))
            {
                this.transform.SetParent(collision.gameObject.transform);
                HasJumped = false;
            }
        }

        protected void UpdateMovement(KeyCode jumpkey)
        {
            bool wasOnGround = onGround;
            onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundLayer) ||
                Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLength, groundLayer);
            if(!wasOnGround && onGround)
            {
                StartCoroutine(JumpSqueeze(1.25f, 0.8f, 0.05f));
            }
            if(Input.GetKey(jumpkey))
            {
                jumpTimer = Time.time + jumpDelay;
            }
            animator.SetBool("onGround", onGround);
        }

        protected void FixUpdateMovement()
        {

        }

        IEnumerator JumpSqueeze(float xSqueeze, float ySqueeze, float seconds)
        {
            Vector3 originalSize = Vector3.one;
            Vector3 newSize = new Vector3(xSqueeze, ySqueeze, originalSize.z);

            float t = 0f;
            while(t < 1.0)
            {
                t += Time.deltaTime / seconds;
                charachterHolder.transform.localScale = Vector3.Lerp(originalSize, newSize, t);
                yield return null;
            }
            t = 0f;
            while(t < 1.0)
            {
                t += Time.deltaTime / seconds;
                charachterHolder.transform.localScale = Vector3.Lerp(newSize, originalSize, t);
                yield return null;
            }

        }


        /// <summary>
        /// Handles movement on the x axis.
        /// </summary>
        protected void MoveSideways(GameObject CharacterToMove, KeyCode left, KeyCode right)
        {
            MoveCharacter(direction.x);
        }

        private void MoveCharacter(float horizontal)
        {
            rigidBody.AddForce(Vector2.right * horizontal * moveSpeed);

            if (Mathf.Abs(rigidBody.velocity.x) > maxSpeed)
            {
                rigidBody.velocity = new Vector2(Mathf.Sign(rigidBody.velocity.x) * maxSpeed, rigidBody.velocity.y);
            }
            animator.SetFloat("horizontal", Mathf.Abs(rigidBody.velocity.x));
            animator.SetFloat("vertical", rigidBody.velocity.y);
        }


        /// <summary>
        /// Handles jumping.
        /// </summary>
        protected void Jump(GameObject CharacterToMove, KeyCode up)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 0);
            rigidBody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            jumpTimer = 0;
            StartCoroutine(JumpSqueeze(0.5f, 1.2f, 0.1f));
        }

        protected void ModifyPhysics()
        {
            bool changingDirection = (direction.x > 0 && rigidBody.velocity.x < 0) || (direction.x < 0 && rigidBody.velocity.x > 0);

            if(onGround)
            {
                if(Mathf.Abs(direction.x) < 0.04f || changingDirection) rigidBody.drag = linearDrag;
                else
                {
                    rigidBody.drag = 0f;
                }
                rigidBody.gravityScale = gravity;
                rigidBody.drag = linearDrag * 0.15f;
                if (rigidBody.velocity.y < 0) rigidBody.gravityScale = gravity * fallMultiplier;
                else if()
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            Rigidbody2D rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
            if (rigidbody.velocity.y <= -0.1) HasJumped = true;
            if (collision.gameObject.CompareTag("Platform"))
            {
                this.transform.SetParent(null);
                HasJumped = true;
            }
        }

        /// <summary>
        /// Switches out the background.
        /// </summary>
        private void SwitchBackGround(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Europe")) _changeBackground.UpdateBackGround(0);
            if (collision.gameObject.CompareTag("Africa")) _changeBackground.UpdateBackGround(1);
            if (collision.gameObject.CompareTag("Oceania")) _changeBackground.UpdateBackGround(2);
            if (collision.gameObject.CompareTag("Asia")) _changeBackground.UpdateBackGround(3);
            if (collision.gameObject.CompareTag("Default")) _changeBackground.UpdateBackGround(4);
        }

        


        public bool GameEnded()
        {
            // This can be placed elsewhere if neccessary, i just don't have a game manager script right now.
            //placeholder
            return false;
            if (true) return true;
            return false;
            
        }

        public void StartCooking()
        {

        }
    }
}
