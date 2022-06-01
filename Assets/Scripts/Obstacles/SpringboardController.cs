using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringboardController : MonoBehaviour
{
    [SerializeField] private float _springForce;

    private SpriteRenderer _renderer;
    private float _cooldown = 0.2f;
    private float _cooldownTimer;
    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) Launch(collision.gameObject);
    }

    private void Launch(GameObject player)
    {
        if (_cooldownTimer <= 0)
        {
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.GetComponent<Rigidbody2D>().AddForce(transform.up * _springForce, ForceMode2D.Force);
            _cooldownTimer = _cooldown;
            _renderer.color = Color.red;
        }
    }

    private void Update()
    {
        if (_cooldownTimer > 0)
            _cooldownTimer -= Time.deltaTime;
        else
            _renderer.color = Color.yellow;
    }
}
