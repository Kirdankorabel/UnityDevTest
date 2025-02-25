using UnityEngine;

public class FallZone : MonoBehaviour
{
    public event System.Action<int> OnBallFalled;

    private void OnTriggerEnter(Collider other)
    {
        var ball = other.gameObject.GetComponent<BallController>();
        if (ball != null)
        {
            OnBallFalled?.Invoke(ball.GetPointCount());
            ball.Fall();
            return;
        }
        var fallingObject = other.gameObject.GetComponent<FallingObject>();
        if (fallingObject != null)
        {
            fallingObject.Fall();
        }
    }
}
