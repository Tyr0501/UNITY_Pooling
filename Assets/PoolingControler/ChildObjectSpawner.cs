using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildObjectSpawner : MonoBehaviour
{
    private string _Tag;
    private float _LimitedDistance;
    private bool _isCamera, _isTag;
    private bool _Y, _X;
    private float _maxBoundsY, _maxBoundsX, _minBoundsY, _minBoundsX;
    private Vector2 _newPos;
    public void SetTag(string tag)
    {
        _isTag = true;
        _Tag = tag;
    }
    public void SetLimitedDistance(float limited, bool isY, bool isX)
    {
        _isTag = false;
        _Y = isY;
        _X = isX;
        _LimitedDistance = limited;
    }

    public void SetLimitedCamera(float maxX, float minX, float maxY, float minY)
    {
        _isCamera = true;
        _maxBoundsY = maxY;
        _maxBoundsX = maxX;
        _minBoundsY = minY;
        _minBoundsX = minX;
    }
    private void FixedUpdate()
    {
        SetDespawn();
    }


    private void SetDespawn()
    {
        if (_isCamera)
        {
            CheckLimitedCamera();
        }
        else
        {
            if (!_isTag)
            {
                CheckLimitedDistance();
            }
        }
    }

    private void CheckLimitedCamera()
    {
        _newPos.y = Mathf.Clamp(transform.position.y, _minBoundsY, _maxBoundsY);
        _newPos.x = Mathf.Clamp(transform.position.x, _minBoundsX, _maxBoundsX);

        if (_newPos.y == _maxBoundsY || _newPos.x == _maxBoundsX || _newPos.y == _minBoundsY || _newPos.x == _minBoundsX)
        {
            gameObject.SetActive(false);
        }
    }

    private void CheckLimitedDistance()
    {
        if (_LimitedDistance > 0)
        {
            if (_Y && _X)
            {
                if (transform.position.y > _LimitedDistance || transform.position.x > _LimitedDistance)
                {
                    gameObject.SetActive(false);
                }
            }
            else if (_Y)
            {
                if (transform.position.y > _LimitedDistance)
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                if (transform.position.x > _LimitedDistance)
                {
                    gameObject.SetActive(false);
                }
            }
        }
        else
        {
            if (_Y && _X)
            {
                if (transform.position.y < _LimitedDistance || transform.position.x < _LimitedDistance)
                {
                    gameObject.SetActive(false);
                }
            }
            else if (_Y)
            {
                if (transform.position.y < _LimitedDistance)
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                if (transform.position.x < _LimitedDistance)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_Tag == null) { return; }
        if (collision.transform.gameObject.tag.Equals(_Tag))
        {
            gameObject.SetActive(false);
        }
    }
}
