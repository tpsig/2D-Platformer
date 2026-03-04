using UnityEngine;

public class EnemyPatrol : MonoBehaviour {
    public float moveSpeed = 2f;
    public float patrolDistance = 3f;

    private Vector3 startPosition;
    private int direction = 1; // 1 = right, -1 = left

    void Start() {
        // Save starting position
        startPosition = transform.position;
    }

    void Update() {
        // Move enemy horizontally
        transform.position += new Vector3(
            direction * moveSpeed * Time.deltaTime,
            0,
            0
        );

        // Check distance from starting point
        float distanceFromStart =
            Vector3.Distance(startPosition, transform.position);

        // Reverse direction if too far
        if (distanceFromStart > patrolDistance) {
            direction *= -1;
        }
    }
}