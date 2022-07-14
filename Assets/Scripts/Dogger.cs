using System.Collections;
using UnityEngine;

public class Dogger : MonoBehaviour
{
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
        transform.position += direction;
        Vector3 destination = transform.position + direction;
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
