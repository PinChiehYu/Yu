using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    private new Collider2D collider2D;
    private bool moveAllowed;

    // Start is called before the first frame update
    void Start()
    {
        collider2D = GetComponent<Collider2D>();
        moveAllowed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                Collider2D touchCollider = Physics2D.OverlapPoint(touchPos);
                if (collider2D == touchCollider)
                {
                    moveAllowed = true;
                }
            }

            if (touch.phase == TouchPhase.Moved)
            {
                if (moveAllowed)
                {
                    transform.position = new Vector2(touchPos.x, touchPos.y);
                }
            }

            if (touch.phase == TouchPhase.Ended)
            {
                moveAllowed = false;
            }
        }
    }
}
