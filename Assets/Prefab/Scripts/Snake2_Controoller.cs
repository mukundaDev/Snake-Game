using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake2_Controoller : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    private List<Transform> _body = new List<Transform>();
    public Transform bodyPrefab;
    private Vector2 _direction = Vector2.left;
    public GameObject Top, Buttom, Right, Left;
    [SerializeField]
    public int _initialSnake = 5;



    private void Start()
    {
        ResetPosition();
    }

    void ResetPosition()
    {
        for (int i = 1; i < _body.Count; i++)
        {
            Destroy(_body[i].gameObject);
        }
        _body.Clear();
        _body.Add(this.transform);

        for (int i = 1; i < this._initialSnake; i++)
        {
            _body.Add(Instantiate(this.bodyPrefab));
        }
        this.transform.position = Vector3.zero;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            _direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _direction = Vector2.right;
        }
    }
    private void FixedUpdate()
    {
        for (int i = _body.Count - 1; i > 0; i--)
        {
            _body[i].position = _body[i - 1].position;
        }   
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f);
    }
    void CalculateMoment()
    {
        float horizontalInput = transform.position.x + _direction.x * _speed;
        float verticalInput = transform.position.y + _direction.y * _speed;
        transform.position = new Vector2(horizontalInput, verticalInput);

       
    }
    private void ChangeMovement()
    {
            if (_direction == Vector2.left)
            {
                this.transform.position = new Vector3(Mathf.Round(Right.transform.position.x) + _direction.x,
                                                      Mathf.Round(this.transform.position.y) + _direction.y,
                                                      0.0f);
            }
            if (_direction == Vector2.right)
            {
                this.transform.position = new Vector3(Mathf.Round(Left.transform.position.x) + _direction.x,
                                                     Mathf.Round(this.transform.position.y) + _direction.y,
                                                     0.0f);

            }
            if (_direction == Vector2.up)
            {
                this.transform.position = new Vector3(Mathf.Round(this.transform.position.x) + _direction.x,
                                                     Mathf.Round(Buttom.transform.position.y) + _direction.y,
                                                     0.0f);

            }
            if (_direction == Vector2.down)
            {
                this.transform.position = new Vector3(Mathf.Round(this.transform.position.x) + _direction.x,
                                                     Mathf.Round(Top.transform.position.y) + _direction.y,
                                                     0.0f);
            }
        }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
       if(other.tag == "Food")
        {
            GrowSnake();
        }
        else if (other.tag == "Obstacle")
        {
            ResetPosition();
        }
        else if (other.tag == "Walls")
        {
            ChangeMovement();
        }
    }
    void GrowSnake()
    {
        Transform segment = Instantiate(this.bodyPrefab);
        segment.position = _body[_body.Count - 1].position;

        _body.Add(segment);
    }
}
