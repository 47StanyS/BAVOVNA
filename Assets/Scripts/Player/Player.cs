using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float _currentHealth;
    private float _maxHealth = 100f;
    [SerializeField] private HeathBarPlayer _heathBarPlayer;
    [Space]
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
    private AudioSource _audioSource;

    
    [SerializeField] AudioClip[] _jumpClip;
    [SerializeField] AudioClip _damagClip;
    [SerializeField] AudioClip _blockClip;


    [SerializeField]private bool _isGrounded;
    [SerializeField]private float _groundCheckDistance;
    public LayerMask _groundLayer;

    public bool _isBlocking = false;

    private Vector3 _position;

    private Transform _spriteTransform;
    private void Start()
    {
        _currentHealth = _maxHealth;
        _heathBarPlayer.SetMaxHealth(_maxHealth);

        _spriteTransform = transform.GetChild(0);

        _audioSource = GetComponent<AudioSource>();
        _sprite = _spriteTransform.GetComponent<SpriteRenderer>();
        _animator = _spriteTransform.GetComponent<Animator>();
        _rb2D = GetComponent<Rigidbody2D>();
        
        _startSpeed = _speedMoove;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            _animator.SetBool("_isBlock", true);
            _animator.Play("Block");
            StartBlocking();
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            _animator.SetBool("_isBlock", false);
            StopBlocking();
        }

        if (_currentHealth <= 0)
        {
            Debug.Log("Oj, Aj, AUCH BEBEBEBE! .!. ");
            SceneManager.LoadScene(2);
            //Destroy(gameObject);
        }
      

        if( Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {

            PlayRandomJumpSound();
            _audioSource.volume = Random.Range(0.4f, 1f);
            _animator.SetTrigger("TakeOff");
            //_animator.CrossFade("takeOff",0.1f);
            _rb2D.AddForce(transform.up * _jampForse, ForceMode2D.Impulse);
        }
        if (_isGrounded)
        {
            _animator.SetBool("_isJumping", false);
            //anim.SetBool("isJumpimg", false);
        }
        else
        {
            _animator.SetBool("_isJumping", true);
            //anim.SetBool("isJumpimg",true);
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

        _isGrounded = Physics2D.Raycast(_rb2D.position, Vector2.down, _groundCheckDistance, _groundLayer);
        Debug.DrawLine(_rb2D.position, _rb2D.position + Vector2.down * _groundCheckDistance, Color.yellow);

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

            _spriteTransform.localScale = new Vector3(1,1,1);
        }

        if (Input.GetKey(KeyCode.A))
        {
            _position.x -= _speedMoove;

            _spriteTransform.localScale = new Vector3(-1, 1, 1);
        }

        transform.position = _position;
    }

    public void StartBlocking()
    {
        _audioSource.clip = _blockClip;
        if(!_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
        
        _isBlocking = true;
        Debug.Log("isBlocking");
    }

    public void StopBlocking() 
    { 
        _audioSource.Stop();
        _isBlocking = false;
        Debug.Log("falseBlocking");
    }
    public void TakeDamage(float damage)
    {
        if (_isBlocking == false) 
        {
            _audioSource.clip = _damagClip;
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
            //_audioSource.Stop();
            _animator.Play("Damage");
            _currentHealth -= damage;
            _heathBarPlayer.SetHealth(_currentHealth);
        }
        else
        {
            
            damage = 0;
        }

        
    }
    private void PlayRandomJumpSound()
    {
        if (_jumpClip.Length == 0)
        {
            Debug.LogWarning("Масив jumpClips порожній! Додайте звуки в інспекторі.");
            return;
        }

        int randomIndex = Random.Range(0, _jumpClip.Length);
        AudioClip randomClip = _jumpClip[randomIndex];

        _audioSource.PlayOneShot(randomClip);
    }

    // public bool isGrounded()
    // {
    //     if (Physics2D.BoxCast(transform.position, _boxSide, 0, -transform.up, _castDistens, _groundLayer))
    //     {
    //         return true;
    //     }
    //     else
    //     {
    //         return false;
    //     }
    // }
    //  private void OnDrawGizmos()
    //  {
    //      Gizmos.DrawWireCube(transform.position - transform.up * _castDistens, _boxSide);
    //  }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Ground"))
    //     {
    //         _animator.SetTrigger("Land");  
    //         _isGrounded = true;
    //     }
    //     
    // }
    // private void OnTriggerExit2D(Collider2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Ground"))
    //     {
    //         _isGrounded = false;
    //     }
    // }
}
