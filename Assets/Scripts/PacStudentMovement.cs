using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PacStudentMovement : MonoBehaviour
{
    public float speed = 5f;

    private Vector2[] pathPoints;
    private int currentPoint = 0;
    public AudioSource walkingAudioSource;
    // For future audio clips that are dependant on factors such as dying add another AudioSource public variable and drag audio source component to it in inspector
    private Animator animator;

    private void Start()
    {
        // Getting components
        animator = GetComponent<Animator>();
 

        // Setup the path 
        pathPoints = new Vector2[]
        {
            new Vector2(-27, 13),
            new Vector2(-22, 13),
            new Vector2(-22, 9),
            new Vector2(-27, 9)
        };

        // Play the walking sound
        walkingAudioSource.Play();

        
 
        // Start PacStudent movement
        StartCoroutine(MovePacStudent());
    }

    private IEnumerator MovePacStudent()
    {
        Vector2 currentTarget = pathPoints[currentPoint];

        while (true)
        {
            transform.position = Vector2.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

            if ((Vector2)transform.position == currentTarget)
            {
                currentPoint = (currentPoint + 1) % pathPoints.Length;  // Cycle through pathPoints
                currentTarget = pathPoints[currentPoint];

                UpdateAnimation((Vector2)transform.position, currentTarget);
            }

            yield return null;
        }
    }

    private void UpdateAnimation(Vector2 currentPos, Vector2 targetPos)
    {
        Vector2 direction = (targetPos - currentPos).normalized;

        

        animator.ResetTrigger("PacWalkRight");
        animator.ResetTrigger("PacWalkUp");
        animator.ResetTrigger("PacWalkLeft");
        animator.ResetTrigger("PacWalkDown");

        if (direction == Vector2.right)
            animator.SetTrigger("PacWalkRight");
        else if (direction == Vector2.left)
            animator.SetTrigger("PacWalkLeft");
        else if (direction == Vector2.up)
            animator.SetTrigger("PacWalkUp");
        else if (direction == Vector2.down)
            animator.SetTrigger("PacWalkDown");
    }
}
