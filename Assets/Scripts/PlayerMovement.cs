using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float limitValue = 1.0f;
    private Animator animator;

    bool isSliding = false;
    bool isJumping = false;
    Vector2 startTouchPosition;
    Vector2 currentTouchPosition;
    bool isSwiping = false;

    void Start()
    {
        // Find the Animator component in the child object 'character1'
        animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the child object 'character1'");
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MovePlayer();
        }

        HandleTouchInput();
    }

    private void MovePlayer()
    {
        float halfScreen = Screen.width / 2;
        float xPos = (Input.mousePosition.x - halfScreen) / halfScreen;
        float finalXPos = Mathf.Clamp(xPos * limitValue, -limitValue, limitValue);

        playerTransform.localPosition = new Vector3(finalXPos, 0, 0);
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    isSwiping = true;
                    break;

                case TouchPhase.Moved:
                    currentTouchPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    if (isSwiping)
                    {
                        float swipeDistanceY = currentTouchPosition.y - startTouchPosition.y;
                        // float swipeDistanceX = currentTouchPosition.x - startTouchPosition.x;

                        if (swipeDistanceY < -50) // Arbitrary threshold for swipe down
                        {
                            StartCoroutine(Sliding());
                        }
                        else if (swipeDistanceY > 50) // Arbitrary threshold for swipe up
                        {
                            if (!isJumping)
                            {
                                StartCoroutine(Jump());
                            }
                        }
                    }
                    isSwiping = false;
                    break;
            }
        }
    }

    private IEnumerator Jump()
    {
        if (!isJumping && animator != null)
        {
            isJumping = true;
            animator.SetBool("jumping", true);
            yield return new WaitForSeconds(1.1f);
            animator.SetBool("jumping", false);
            isJumping = false;
        }
    }

    IEnumerator Sliding()
    {
        if (!isSliding && animator != null)
        {
            isSliding = true;
            animator.SetBool("sliding", true);
            yield return new WaitForSeconds(1.5f);
            animator.SetBool("sliding", false);
            isSliding = false;
        }
    }
}




/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float limitValue = 1.0f;
    private Animator animator;

    bool isSliding = false;
    Vector2 startTouchPosition;
    Vector2 currentTouchPosition;
    bool isSwiping = false;

    void Start()
    {
        *//*animator = GetComponent<Animator>();*//*
        animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the child object 'character1'");
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MovePlayer();
        }

        HandleTouchInput();
    }

    private void MovePlayer()
    {
        float halfScreen = Screen.width / 2;
        float xPos = (Input.mousePosition.x - halfScreen) / halfScreen;
        float finalXPos = Mathf.Clamp(xPos * limitValue, -limitValue, limitValue);

        playerTransform.localPosition = new Vector3(finalXPos, 0, 0);
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    isSwiping = true;
                    break;

                case TouchPhase.Moved:
                    currentTouchPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    if (isSwiping)
                    {
                        float swipeDistance = currentTouchPosition.y - startTouchPosition.y;
                        if (swipeDistance < -50) // Arbitrary threshold for swipe down
                        {
                            StartCoroutine(Sliding());
                        }
                    }
                    isSwiping = false;
                    break;
            }
        }
    }

    IEnumerator Sliding()
    {
        if (!isSliding)
        {
            isSliding = true;
            animator.SetBool("sliding", true);
            yield return new WaitForSeconds(1.5f);
            animator.SetBool("sliding", false);
            isSliding = false;
        }
    }
}*/


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float limitValue = 1.0f;
    Animator animator;

    bool isSliding = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        float halfScreen = Screen.width / 2;    // 540px
        // 540-540/540 = 0 middle of the screen
        // 0-540/540 = -1 left edge of the screen
        // 1080-540/540 = 1 right edge of the screen
        float xPos = (Input.mousePosition.x - halfScreen) / halfScreen;
        float fianlXPos = Mathf.Clamp(xPos * limitValue, -limitValue, limitValue);

        playerTransform.localPosition = new Vector3(fianlXPos, 0, 0);
    }

    IEnumerator Sliding()
    {
        isSliding = true;
        animator.SetBool("sliding", true);
        yield return new WaitForSeconds(1.5f);
        isSliding = false;
    }
}*/
