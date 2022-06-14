using Assets.Food;
using Assets.World;
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
        [SerializeField] protected Collider2D _otherCollider;
        protected bool HasJumped = false;


        [Header("Outfits")]
        [SerializeField] protected Sprite Outfit1;
        [SerializeField] protected Sprite Outfit2;
        [SerializeField] protected Sprite Outfit3;
        [SerializeField] protected Sprite Outfit4;

        [Header("Inventory")]
        [SerializeField] private InventorySystem _inventory;

        [Header("World References")]
        [SerializeField] private ChangeBackground _changeBackground;

        protected CharacterState characterState;

        private void Awake()
        {
            characterState  = new CharacterState();
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), _otherCollider);
        }


        protected void OnTriggerEnter2D(Collider2D collision)
        {
            SwitchOutfit(collision);
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
        protected void Jump(GameObject CharacterToMove, KeyCode up)
        {
            Rigidbody2D RigidBody = CharacterToMove.GetComponent<Rigidbody2D>();
            if (RigidBody.velocity.y <= 0 && RigidBody.velocity.y > -0.01) HasJumped = false;
            if (RigidBody.velocity.y <= 0 && Input.GetKeyDown(up) && !HasJumped)
            {
                HasJumped = true;
                RigidBody.AddForce(transform.up * JumpForce, ForceMode2D.Force);
            }
        }

        /// <summary>
        /// Handles movement on the x axis.
        /// </summary>
        protected void MoveSideways(GameObject CharacterToMove, KeyCode left, KeyCode right)
        {
            if (Input.GetKey(right)) CharacterToMove.transform.Translate(new Vector3(Speed, 0) * Time.deltaTime);
            if (Input.GetKey(left)) CharacterToMove.transform.Translate(new Vector3(-Speed, 0) * Time.deltaTime);
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
       
        public enum CharacterState
        {
            Asia = 0,
            Oceania = 1,
            Europe = 2,
            Africa = 3,
        }
    }
}
