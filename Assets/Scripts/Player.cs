using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float _health;
    [Space]
    [SerializeField] private float _speedMoove;
    [SerializeField] private float _speedRun;
    [SerializeField] private float _speedSneaking;
    [SerializeField] private float _jampForse;
    private float _startSpeed;
    [Space]
    [SerializeField] SpriteRenderer _sprite;
    private Rigidbody2D _rb2D;
    private Animator _animator;

    private bool _isGrounded;

    private Vector3 _position;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rb2D = GetComponent<Rigidbody2D>();
        _startSpeed = _speedMoove;
    }
    private void Update()
    {
        if(_health == 0)
        {
            Debug.Log("Oj, Aj, AUCH BEBEBEBE! .!. ");
            SceneManager.LoadScene(2);
            //Destroy(gameObject);
        }
      

        if( Input.GetKeyDown(KeyCode.Space) && _isGrounded == true)
        {
            _rb2D.AddForce(transform.up * _jampForse, ForceMode2D.Impulse);
        }

        if ( Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            _animator.SetInteger("State", 1);
        }
        else
        {
            _animator.SetInteger("State", 0);
        }
        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
        {
            _animator.SetInteger("State", 2);
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.D))
        {
            _animator.SetInteger("State",3);
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.A))
        {
            _animator.SetInteger("State", 2);
        }
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.A))
        {
            _animator.SetInteger("State", 3);
        }

    }
    private void FixedUpdate()
    {
        _position = transform.position;

        if ( Input.GetKey(KeyCode.LeftShift) && _isGrounded == true )
        {
            _speedMoove = _speedRun;
        } else if (Input.GetKey(KeyCode.LeftControl) )
        {
            _speedMoove = _speedSneaking;
        } else
        {
            _speedMoove =  _startSpeed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            _position.x += _speedMoove;

            _sprite.flipX = false;
        }

        if (Input.GetKey(KeyCode.A))
        {
            _position.x -= _speedMoove;

            _sprite.flipX = true;
        }

        transform.position = _position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }
}
