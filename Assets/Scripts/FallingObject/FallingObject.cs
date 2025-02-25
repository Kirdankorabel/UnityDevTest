using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FallingObject : MonoBehaviour, IReleased
{
    [SerializeField] private MeshRenderer _meshRenderer;

    private Collider _collider;
    protected float _minX;
    protected float _minY;
    protected float _velocityY;
    protected float _velocityX;
    protected FallingObjectData _ballData;

    public event Action OnReleased;

    public float VeclocityMultipler { get; set; } = 1f;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    #region builders
    public FallingObject SetPosition(Vector3 position)
    {
        transform.position = position;
        return this;
    }

    public FallingObject SetMinPosition(float minY)
    {
        _minY = minY;
        return this;
    }

    public FallingObject SetWalls(float posX)
    {
        _minX = -posX;
        return this;
    }

    public FallingObject SetFallingObjectData(FallingObjectData ballData)
    {
        _ballData = ballData;
        transform.localScale = Vector3.one * ballData.Size;
        _meshRenderer.material.color = ballData.Color;
        return this;
    }
    #endregion

    public void StartMoving()
    {
        StopAllCoroutines();

        _velocityY = _ballData._speed;
        _velocityX = 0;
        StartCoroutine(FallCoroutine());
        _collider.enabled = true;
    }

    public void Fall()
    {
        _collider.enabled = false;
    }

    public virtual void PlayerColisionAction(float velocityX) { }

    private IEnumerator FallCoroutine()
    {
        Vector3 targetPsoition;
        while (true)
        {
            if (!GameSettings.IsPaused)
            {
                _velocityY += _ballData.Gravity * Time.deltaTime;
                _velocityY = Math.Min(_velocityY, _ballData.MaxSpeed);
                targetPsoition = transform.position;
                targetPsoition += Vector3.down * _velocityY * VeclocityMultipler * Time.deltaTime;
                targetPsoition += Vector3.right * _velocityX * VeclocityMultipler * Time.deltaTime;

                transform.position = targetPsoition;
                CheckPosiiton();
            }
            yield return null;
        }
    }

    protected virtual void CheckPosiiton()
    {
        if (transform.position.y <= _minY)
        {
            ReleseObject();
        }
    }

    public virtual void ReleseObject()
    {
        OnReleased?.Invoke();
    }
}
