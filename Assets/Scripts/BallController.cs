using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{
    public Vector2 Direction;
    public float Speed;

    public GameObject LoserButton;

    private bool RespawningOk = true;

    private bool RespawnDecider = true;

    private Vector3 ReplaceBall;

    public GameObject PaddleRight;

    public GameObject PaddleLeft;

    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        Util.GetComponentIfNull<SpriteRenderer>(this, ref spriteRenderer); 
        rb2D = GetComponent<Rigidbody2D>();//Check for null?
    }

    // Use this for initialization
    void Start()
    {
        ReplaceBall = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Respaqn();


    }

    private void Respaqn()
    {
        if (this.gameObject.transform.position.y < (PaddleRight.transform.position.y - .8) && RespawningOk == true)
        {
            ScoreManager.Lives -= 1;
            RespawningOk = false;
            if (RespawnDecider == true)
            {
                this.gameObject.transform.position = new Vector3(PaddleLeft.transform.position.x, ReplaceBall.y, 0);
                RespawnDecider = false;
                RespawningOk = true;
            }
            else
            {
                this.gameObject.transform.position = new Vector3(PaddleRight.transform.position.x, ReplaceBall.y, 0);
                RespawnDecider = true;
                RespawningOk = true;
            }

            if (ScoreManager.Lives <= 0)
            {
                
                LoserButton.SetActive(true);
                this.gameObject.SetActive(false);
            }
            

        }
    }



    /// <summary>
    /// FixedUpdate uses physics
    /// </summary>
    void FixedUpdate()
    {
        if (rb2D != null)
        {
            //Keep on screen
            this.rb2D.position = Util.BounceOffWalls(this.transform.position,
                spriteRenderer.bounds.size.x - 1,
                spriteRenderer.bounds.size.y - 1, ref this.Direction);
        }

        rb2D.MovePosition(rb2D.position + Direction * Speed * Time.fixedDeltaTime);
    }

    /// <summary>
    /// Adds a bit of randomness to the ball bounce collision
    /// </summary>
    public void UpdateBallCollisionRandomFuness()
    {
        /// 
        /// Adds a bit of entropy to bounce nothing should be perfect
        /// 
        /// 
        
        
            this.Direction.y *= GetReflectEntropy();
    }
    

    private float GetReflectEntropy()
    {
        return -1 + ((Random.Range(0, 3) - 1) * 0.1f); //return -.9, -1 or -1.1
    }

    public void RelfectY()
    {
        this.Direction.y *= -1;
    }

    public void RelfectX()
    {
        this.Direction.x *= -1;
    }


}
