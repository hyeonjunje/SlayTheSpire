using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField]
    private float _minSpeed = 1f;
    [SerializeField]
    private float _maxSpeed = 3f;
    [SerializeField]
    private float _posX;
    [SerializeField]
    private float _initX;

    private float _speed;

    private void Start()
    {
        _speed = Random.Range(_minSpeed, _maxSpeed);
    }

    void Update()
    {
        transform.position -= transform.right * _speed * Time.deltaTime;

        if(transform.position.x < _initX)
        {
            transform.position = new Vector3(_posX, Random.Range(-5f, 5f), 0);
            _speed = Random.Range(_minSpeed, _maxSpeed);
        }
    }
}
