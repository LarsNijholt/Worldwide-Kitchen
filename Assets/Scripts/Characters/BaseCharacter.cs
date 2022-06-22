using Assets.Food;
using Assets.Inventory;
using Assets.World;
using System.Collections.Generic;
using Assets.UI;
using UnityEngine;

namespace Characters
{
    public class BaseCharacter : MonoBehaviour
    {
        [Header("Horizontal movement")]
        [SerializeField] private float _accelerationSpeed = 30;
        [SerializeField] private Vector2 _direction;
        [SerializeField] private Collider2D _otherCollider;
        private bool _facingRight = true;

        [Header("Verical movement")]
        [SerializeField] private float _jumpSpeed = 15f;
        [SerializeField] private float _jumpDelay = 0.25f;
        private float _jumpTimer;

        [Header("Components")]
        [SerializeField] private Rigidbody2D _rigidBody;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _charachterHolder;

        [Header("Sprites")]
        [SerializeField] protected Sprite EuropeSprite;
        [SerializeField] protected Sprite OceaniaSprite;
        [SerializeField] protected Sprite AsiaSprite;
        [SerializeField] protected Sprite AfricaSprite;

        [Header("Physics")]
        [SerializeField] private float _maxSpeed = 7f;
        [SerializeField] private float _linearDrag = 4f;
        [SerializeField] private float _gravity = 1f;
        [SerializeField] private float _fallMultiplier = 5f;

        [Header("Collision")]
        [SerializeField] private bool _onGround = false;
        [SerializeField] private float _groundLength = 0.6f;
        [SerializeField] private Vector3 _colliderOffset;

        [Header("Inventory")]
        [SerializeField] private InventorySystem _inventory;
        [SerializeField] ShowInventory _inventoryui;

        [Header("World References")]
        [SerializeField] private ChangeBackground _changeBackground;

        private void Awake()
        {
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), _otherCollider);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            SwitchBackGround(collision);
            if (collision.gameObject.CompareTag("Food"))
            {
                _inventory.AddToInventory(collision.gameObject.GetComponent<BaseIngredient>());
                _inventoryui.AddToUi(collision.gameObject.GetComponent<SpriteRenderer>().sprite);

            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Platform"))
            {
                this.transform.SetParent(collision.gameObject.transform);
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            Rigidbody2D rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
            if (collision.gameObject.CompareTag("Platform"))
            {
                this.transform.SetParent(null);
            }
        }

        /// <summary>
        /// Movement for update.
        /// </summary>
        protected void UpdateMovement(KeyCode jumpkey, string horizontalAxis, string VerticalAxis)
        {
            _onGround = Physics2D.Raycast(transform.position + _colliderOffset, Vector2.down, _groundLength, _groundLayer) ||
                Physics2D.Raycast(transform.position - _colliderOffset, Vector2.down, _groundLength, _groundLayer);

            if (Input.GetKey(jumpkey))
            {
                _jumpTimer = Time.time + _jumpDelay;
            }
            // _animator.SetBool("onGround", _onGround);
            _direction = new Vector2(Input.GetAxisRaw(horizontalAxis), Input.GetAxisRaw(VerticalAxis));
        }

        /// <summary>
        /// Movement for fixed update.
        /// </summary>
        protected void FixedMovement(KeyCode jump)
        {
            MoveCharacter(_direction.x);
            if (_jumpTimer > Time.time && _onGround) Jump();
            ModifyPhysics(jump);
        }

        /// <summary>
        /// Handles movement for character.
        /// </summary>
        private void MoveCharacter(float horizontal)
        {
            _rigidBody.AddForce(Vector2.right * horizontal * _accelerationSpeed);

            if ((horizontal > 0 && !_facingRight) || (horizontal < 0 && _facingRight)) Flip();
            if (Mathf.Abs(_rigidBody.velocity.x) > _maxSpeed)
            {
                _rigidBody.velocity = new Vector2(Mathf.Sign(_rigidBody.velocity.x) * _maxSpeed, _rigidBody.velocity.y);
            }
            //_animator.SetFloat("horizontal", Mathf.Abs(_rigidBody.velocity.x));
            //_animator.SetFloat("vertical", _rigidBody.velocity.y);
        }

        /// <summary>
        /// Handles jumping.
        /// </summary>
        private void Jump()
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, 0);
            _rigidBody.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
            _jumpTimer = 0;
        }

        /// <summary>
        /// Changes physics for drag and direction
        /// </summary>
        private void ModifyPhysics(KeyCode jump)
        {
            bool changingDirection = (_direction.x > 0 && _rigidBody.velocity.x < 0) || (_direction.x < 0 && _rigidBody.velocity.x > 0);

            if (_onGround)
            {
                if (Mathf.Abs(_direction.x) < 0.4f || changingDirection) _rigidBody.drag = _linearDrag;
                else
                {
                    _rigidBody.drag = 0f;
                }
                _rigidBody.gravityScale = 0;
            }
            else
            {
                _rigidBody.gravityScale = _gravity;
                _rigidBody.drag = _linearDrag * 0.5f;
                if (_rigidBody.velocity.y < 0) _rigidBody.gravityScale = _gravity * _fallMultiplier;
                else if (_rigidBody.velocity.y > 0 && Input.GetKey(jump))
                {
                    _rigidBody.gravityScale = _gravity * (_fallMultiplier / 2);
                }
            }
        }

        /// <summary>
        /// Flips the character.
        /// </summary>
        private void Flip()
        {
            _facingRight = !_facingRight;
            transform.rotation = Quaternion.Euler(0, _facingRight ? 0 : 180, 0);
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

        protected void SwitchCharacter(BaseCharacter switchedCharacter)
        {

            Character character = (Character)PlayerPrefs.GetInt("Selection");
            print(character);
            switch (character)
            {
                case Character.europe:
                    {
                        switchedCharacter.gameObject.GetComponent<SpriteRenderer>().sprite = EuropeSprite;
                        break;
                    }
                case Character.Oceania:
                    {
                        switchedCharacter.gameObject.GetComponent<SpriteRenderer>().sprite = OceaniaSprite;
                        break;
                    }
                case Character.Africa:
                    {
                        switchedCharacter.gameObject.GetComponent<SpriteRenderer>().sprite = AfricaSprite;
                        break;
                    }
                case Character.Asia:
                    {
                        switchedCharacter.gameObject.GetComponent<SpriteRenderer>().sprite = AsiaSprite;
                        break;
                    }
            }
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

        public enum Character
        {
            europe = 1,
            Oceania = 2,
            Africa = 3,
            Asia = 4
        }
    }
}
