using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_controller : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody2D rigidbody2D;
    [SerializeField] float distance;
    float ray_distance = 0.5f;
    public static int HP = 3;
    Animator animator;
    bool control = true;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Debug.Log(HP);

        if (control == true)
        {
            if (Input.GetKey("d"))
            {
                animator.SetBool("Run", true);
                GetComponent<SpriteRenderer>().flipX = false;
                if (speed <= 5)
                {
                    speed += 8 * Time.deltaTime;
                }
            }
            else if (Input.GetKey("a"))
            {
                animator.SetBool("Run", true);
                GetComponent<SpriteRenderer>().flipX = true;
                if (speed >= -5)
                {
                    speed -= 8 * Time.deltaTime;
                }
            }
            else
            {
                if (speed > 0.1f)
                {
                    speed -= 8 * Time.deltaTime;
                }
                else if (speed < -0.1f)
                {
                    speed += 8 * Time.deltaTime;
                }
                else
                {
                    speed = 0;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigidbody2D.AddForce(new Vector2(0f, 600f));
            }

            
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            Vector2 rayDir = sr.flipX ? Vector2.left : Vector2.right;

          
            int layerMask = ~LayerMask.GetMask("Player"); 

            RaycastHit2D hit = Physics2D.Raycast(
                transform.position,
                rayDir,
                ray_distance,
                layerMask
            );

           
            Debug.DrawRay(
                transform.position,
                rayDir * ray_distance,
                Color.red
            );

            if (hit.collider != null)
            {
                Debug.Log("“–‚½‚Á‚½: " + hit.collider.name);
                if(hit.collider.name == "Tilemap")
                {
                    speed = 0;
                }
            }
           
        }

        if (speed == 0)
        {
            animator.SetBool("Run", false);
        }

        if (transform.position.y <= -10)
        {
            if (HP > 0)
            {
                HP -= 1;
                SceneManager.LoadScene("Lives");
            }
            else
            {
                SceneManager.LoadScene("Lives");
            }
        }

        transform.position += transform.right * speed * Time.deltaTime;
    }

    void OnDrawGizmos()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        Vector2 rayDir = sr.flipX ? Vector2.left : Vector2.right;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(
            transform.position,
            transform.position + (Vector3)(rayDir * ray_distance)
        );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            control = false;
            speed = 0;
            Vector3 diff = transform.position - collision.transform.position;
            distance = diff.x;
            rigidbody2D.AddForce(new Vector2(distance * 200, 400f));
            GetComponent<BoxCollider2D>().enabled = false;
        }

        if (collision.gameObject.tag == "hit")
        {
            rigidbody2D.AddForce(new Vector2(0f, 300f));
            Destroy(collision.transform.parent.gameObject);
        }
    }
}
