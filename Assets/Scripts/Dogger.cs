using System;
using System.Collections;
using UnityEngine;

public class Dogger : MonoBehaviour
{
    public Sprite deadSprite;
    public Sprite idleSprite;
    private SpriteRenderer spriteRenderer;

    private Vector3 spawnPosition;

    // get initial position for respawning
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spawnPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            Move(Vector3.up);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            transform.rotation = Quaternion.Euler(0f,0f, 180f);
            Move(Vector3.down);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            Move(Vector3.left);
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            transform.rotation = Quaternion.Euler(0f, 0f, -90f);
            Move(Vector3.right);
        }
    }

    private void Move(Vector3 direction)
    {
        Vector3 destination = transform.position + direction;
        
        Collider2D barrier = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Barrier"));
        Collider2D platform = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Platform"));
        Collider2D obstacle = Physics2D.OverlapBox(destination, Vector2.zero, 0f, LayerMask.GetMask("Obstacle"));

        // don't move if barrier is in the way
        if (barrier != null) {
            return;
        }

        // if moving onto platform, attach frogger to platform so he moves with the platform
        if (platform != null) {
            transform.SetParent(platform.transform);
        } else {
            transform.SetParent(null);
        }

        // lose life
        if (obstacle != null && platform == null) 
        {
            transform.position = destination;
            Death();
        } else {
            transform.position += direction;
        }
    }

    private void Death()
    {
        transform.rotation = Quaternion.identity;
        spriteRenderer.sprite = deadSprite;
        enabled = false;
        Invoke(nameof(Respawn), 1f);
    }

    public void Respawn()
    {
        transform.rotation = Quaternion.identity;
        transform.position = spawnPosition;
        spriteRenderer.sprite = idleSprite;
        gameObject.SetActive(true);
        enabled = true;
    }
    // detect when another collider enters trigger zone
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (enabled && collision.gameObject.layer == LayerMask.NameToLayer("Obstacle") && transform.parent == null) {
            Death();
        }
    }

    // this block has been commented out because it caused issues with frogger movement along the x/y axis. TODO: fix this so frogger still only moves in increments of 1
    //    StartCoroutine(Leap(destination));
    //}
    //private IEnumerator Leap(Vector3 destination)
    //{
    //    Vector3 startPosition = transform.position;

    //    float elapsed = 0f;
    //    float duration = 0.125f;

    //    while (elapsed < duration)
    //    {
    //        float t = elapsed / duration;
    //        transform.position = Vector3.Lerp(startPosition, destination, t);
    //        elapsed += Time.deltaTime;
    //        yield return null;
    //    }

    // safeguard to assure destination is reached
    //transform.position = destination;
    //}
}
