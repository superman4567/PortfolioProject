using UnityEngine;
using DG.Tweening;

public class RandomUIMovement : MonoBehaviour
{
    public float maxXDistance = 50f; // Max X distance for movement
    public float maxYDistance = 50f; // Max Y distance for movement
    public float maxRotation = 5f;   // Max rotation in degrees
    public float maxScale = 5f;      // Max scale factor
    public float moveDuration = 1f;  // Duration for movement to new position

    private RectTransform rectTransform;
    private Vector2 startingPosition;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startingPosition = rectTransform.anchoredPosition;

        // Start the continuous movement and animation loop
        MoveAndAnimate();
    }

    void MoveAndAnimate()
    {
        // Move to a random nearby position
        Vector2 randomPosition = startingPosition + new Vector2(
            Random.Range(-maxXDistance, maxXDistance),
            Random.Range(-maxYDistance, maxYDistance)
        );

        // Rotate randomly within the range
        float randomRotationZ = Random.Range(-maxRotation, maxRotation);

        // Scale randomly within the range
        float randomScale = Random.Range(1f, maxScale);

        // Animate position, rotation, and scale simultaneously
        DOTween.Sequence()
            .Append(rectTransform.DOAnchorPos(randomPosition, moveDuration))
            .Join(rectTransform.DORotate(new Vector3(0, 0, randomRotationZ), moveDuration))
            .Join(rectTransform.DOScale(randomScale, moveDuration))
            .OnComplete(() =>
            {
                // Immediately start the next movement after the current one finishes
                MoveAndAnimate();
            });
    }
}
