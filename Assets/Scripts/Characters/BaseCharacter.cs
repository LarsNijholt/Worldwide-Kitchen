using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public class BaseCharacter : MonoBehaviour
    {
        [Header("Base values")]
        [SerializeField] protected float Speed = 10;
        [SerializeField] protected float JumpForce = 10f;
        [SerializeField] protected float _maxInventoryCount = 10;
        protected bool HasJumped = false;


        [Header("Outfits")]
        [SerializeField] protected Sprite Outfit1;
        [SerializeField] protected Sprite Outfit2;
        [SerializeField] protected Sprite Outfit3;
        [SerializeField] protected Sprite Outfit4;

        [Header("Inventory")]
        [SerializeField] protected List<GameObject> _inventory;

        protected CharacterState characterState = new CharacterState();

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            SwitchOutfit(collision);
            if (collision.gameObject.CompareTag("Food") && _inventory.Count < _maxInventoryCount) PickUpItem(collision.gameObject);
        }
        protected void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Floor")) HasJumped = false; print("Hello");
            if (collision.gameObject.CompareTag("Platform"))
            {
                this.transform.SetParent(collision.gameObject.transform);
                HasJumped = false;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            Rigidbody2D rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
            if (collision.gameObject.CompareTag("Floor") && rigidbody.velocity.y >= 1) HasJumped = true;
            print(rigidbody.velocity.y);
            if (collision.gameObject.CompareTag("Platform")) this.transform.SetParent(null);
        }

        /// <summary>
        /// Handles outfit switching based on which location the player is in.
        /// </summary>
        private void SwitchOutfit(Collider2D collision)
        {
            if (collision.CompareTag("Asia"))
            {
                characterState = CharacterState.Asia;
                CheckLocation(this.gameObject);
                return;
            }
            if (collision.CompareTag("Africa"))
            {
                characterState = CharacterState.Africa;
                CheckLocation(this.gameObject);
                return;
            }
            if (collision.CompareTag("Europe"))
            {
                characterState = CharacterState.Europe;
                CheckLocation(this.gameObject);
                return;
            }
            if (collision.CompareTag("Oceania"))
            {
                characterState = CharacterState.Oceania;
                CheckLocation(this.gameObject);
                return;
            }
        }

        /// <summary>
        /// Handles character movement
        /// </summary>
        protected void MoveCharacter(GameObject CharacterToMove)
        {
            MoveSideways(CharacterToMove);
            Jump(CharacterToMove);
        }

        /// <summary>
        /// Checks location for specified character to see what outfit to switch.
        /// </summary>
        protected void CheckLocation(GameObject CharacterToLocate)
        {
            switch (characterState)
            {
                case CharacterState.Asia:
                    CharacterToLocate.GetComponent<SpriteRenderer>().sprite = Outfit1;
                    print("outfit 1");
                    break;
                case CharacterState.Oceania:
                    CharacterToLocate.GetComponent<SpriteRenderer>().sprite = Outfit2;
                    print("outfit 2");
                    break;
                case CharacterState.Europe:
                    CharacterToLocate.GetComponent<SpriteRenderer>().sprite = Outfit3;
                    print("outfit 3");
                    break;
                case CharacterState.Africa:
                    CharacterToLocate.GetComponent<SpriteRenderer>().sprite = Outfit4;
                    print("outfit 4");
                    break;
                default:
                    CharacterToLocate.GetComponent<SpriteRenderer>().sprite = Outfit1;
                    break;
            }
        }

        /// <summary>
        /// Handles jumping.
        /// </summary>
        private void Jump(GameObject CharacterToMove)
        {
            Rigidbody2D RigidBody = CharacterToMove.GetComponent<Rigidbody2D>();
            if (RigidBody.velocity.y <= 0 && Input.GetKeyDown(KeyCode.W) && !HasJumped)
            {
                HasJumped = true;
                RigidBody.AddForce(transform.up * JumpForce, ForceMode2D.Force);
            }
        }
        /// <summary>
        /// Handles movement on the x axis.
        /// </summary>
        private void MoveSideways(GameObject CharacterToMove)
        {
            if (Input.GetKey(KeyCode.D)) CharacterToMove.transform.Translate(new Vector3(Speed, 0) * Time.deltaTime);
            if (Input.GetKey(KeyCode.A)) CharacterToMove.transform.Translate(new Vector3(-Speed, 0) * Time.deltaTime);
        }

        /// <summary>
        /// Handles picking up items and adding it to a list that serves as inventory.
        /// </summary>
        protected void PickUpItem(GameObject Item)
        {
            if (Item == null) return;

            _inventory.Add(Item);
            Item.SetActive(false);
        }

        public enum CharacterState
        {
            Asia = 0,
            Oceania = 1,
            Europe = 2,
            Africa = 3,
        }
    }
}
