using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCycle : MonoBehaviour
{
    public Vector2 direction = Vector2.right;
    public int size = 1;
    public float speed = 1f;

    // detect when object is outside of screen
    private Vector3 leftEdge;
    private Vector3 rightEdge;
    private void Start()
    {
        leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        rightEdge= Camera.main.ViewportToWorldPoint(Vector3.right);
    }

    private void Update()
    {
        // if entire object off screen, move to other side
        if (direction.x > 0 && (transform.position.x - size) > rightEdge.x)
        {
            Vector3 position = transform.position;
            position.x = leftEdge.x - size;
            transform.position = position;
        }
        else if (direction.x < 0 && (transform.position.x - size) < leftEdge.x)
        {
            Vector3 position = transform.position;
            position.x = rightEdge.x + size;
            transform.position = position;
        }
        else
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}
