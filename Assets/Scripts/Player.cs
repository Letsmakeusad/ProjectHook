using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour, IAnchorUser
{
    Rigidbody2D m_rigidbody;
    AnchorSystem m_anchorSystem;
    Animator playerAnim;
    Transform playerTransform;
    public float impulseForce;
    public GameManager gameManager;
    public PauseMenu pauseMenu;
    public bool isDead;

    private bool facingRight;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_anchorSystem = AnchorSystem.GetInstance();
        playerAnim = GetComponent<Animator>();
        playerTransform = GetComponent<Transform>();
        gameManager = FindObjectOfType<GameManager>();
        pauseMenu = FindObjectOfType<PauseMenu>();
        isDead = false;
    }

    Rigidbody2D IAnchorUser.GetRigidbody2D()
    {
        return m_rigidbody;
    }

    void FixedUpdate()
    {
        Flip(m_rigidbody.velocity.x);
        m_anchorSystem.FindNearestAnchorComponent(m_rigidbody.position);
        if (m_rigidbody.velocity.x >= 15f || m_rigidbody.velocity.x <= -15f)
        {
            m_rigidbody.velocity = m_rigidbody.velocity.normalized * 15f;
        }
    }

    void Update()
    {
        if (!pauseMenu.isPaused && !isDead)
        {

          
            if (Input.GetMouseButtonDown(0))
            {
                m_anchorSystem.AttachToNearestAnchor(this);
                m_rigidbody.AddForce(new Vector3(m_rigidbody.transform.localScale.x * impulseForce, 0, 0), ForceMode2D.Force);
                playerAnim.SetBool("isSlinging", true);

            }
            else if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("INPUT");
                playerAnim.SetBool("isSlinging", false);
                m_rigidbody.AddForce(new Vector3(m_rigidbody.transform.localScale.x * impulseForce, 0, 0), ForceMode2D.Impulse);
                m_anchorSystem.Detach(this);
            }
        }
        
    }

    void Flip(float velocity)
    {
        if(velocity < 0 && !facingRight || velocity > 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector2 theScale = playerTransform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            playerAnim.SetTrigger("jump");
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("coin"))
        {
            gameManager.CollectCoin();

        }
        if (collision.gameObject.CompareTag("death"))
        {
            isDead = true;

        }
    }
 



}