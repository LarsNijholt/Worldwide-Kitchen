using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public class BaseCharacter : MonoBehaviour
    {
        [Header("Base values")]
        [SerializeField] protected float _health = 100;
        [SerializeField] protected float _stamina = 100;
        [SerializeField] protected float _speed = 10;

        protected void CheckHealth()
        {
            int maxHealth = 100;
            if (_health >= maxHealth) _health = maxHealth;
        }

        protected void TakeDamage(float Damage)
        {
            _health -= Damage;
        }

        protected bool isDead(GameObject CharacterThatDied)
        {
            if (_health <= 0)
            {
                CharacterThatDied.SetActive(false);
                return true;
            }
            return false;
        }
        protected void MoveSideways(GameObject CharacterToMove)
        {
            if(Input.GetKey(KeyCode.D)) CharacterToMove.transform.Translate(new Vector3(_speed, 0) * Time.deltaTime);
            if (Input.GetKey(KeyCode.A)) CharacterToMove.transform.Translate(new Vector3(-_speed, 0) * Time.deltaTime);
            if (Input.GetKey(KeyCode.W)) CharacterToMove.transform.Translate(new Vector3(0, _speed) * Time.deltaTime);
        }
    }
}
