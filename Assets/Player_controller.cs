using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_controller : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody2D rigidbody2D;
    [SerializeField] float distance;
    float ray_distance = 0.5f;
    public static int HP = 3;
    Animator animator;
    bool control = true;
    bool jump_flag = false;
    [SerializeField] GameObject goal_text;
    [SerializeField] GameObject camera;
    [SerializeField] TextMeshProUGUI time_text;
    [SerializeField] TextMeshProUGUI score_text;
    [SerializeField] GameObject Explosion;
    float time = 200;
    float score = 0;
    [SerializeField] GameObject Title_Reload;

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
            if (camera_control.Title == false && camera.GetComponent<Camera>().orthographicSize >= 5)
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
            }
            else if(camera_control.Title == true)
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
            }

            if (Input.GetKeyDown(KeyCode.Space) && jump_flag == false)
            {
                jump_flag = true;
                animator.SetBool("jump", true);
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

        if (camera_control.Title == false)
        {
            if (GameObject.FindWithTag("start_trigger") != null)
            {
                GameObject.FindWithTag("start_trigger").GetComponent<BoxCollider2D>().enabled = false;
            }
        }

        if (speed == 0)
        {
            animator.SetBool("Run", false);
        }

        if (transform.position.y <= -10)
        {
            if (HP >= 1)
            {
                HP -= 1;
                SceneManager.LoadScene("Lives");
            }
            if(HP == 0)
            {
               Debug.Log("GameOver");
                SceneManager.LoadScene("gameover");
            }
        }
        if (camera_control.Title == false)
        {
            time -= Time.deltaTime;
        }
        time_text.text = "Time: " + ((int)time).ToString();
        score_text.text = "SCORE: " + ((int)score).ToString();
        time_text.GetComponent<TextMeshProUGUI>().outlineColor = new Color(255, 255, 255, 255);
        score_text.GetComponent<TextMeshProUGUI>().outlineColor = new Color(255, 255, 255, 255);
        


        transform.position += transform.right * speed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        transform.localScale = new Vector3(1,1,1);
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
        if (collision.gameObject.tag == "Floor")
        {
            jump_flag = false;
            animator.SetBool("jump", false);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            control = false;
            speed = 0; 

            Transform firstChild = collision.transform.GetChild(0);
            Destroy(firstChild.gameObject);
            if (jump_flag == false)
            {
                Vector3 diff = transform.position - collision.transform.position;
                distance = diff.x;
                rigidbody2D.AddForce(new Vector2(distance * 200, 400f));
            }
            GetComponent<BoxCollider2D>().enabled = false;
        }

        if (collision.gameObject.tag == "hit")
        {
            if (camera_control.Title == false)
            {
                score += Random.Range(10, 30);
            }
            Instantiate(Explosion, collision.gameObject.transform.position, Quaternion.identity);
            rigidbody2D.AddForce(new Vector2(0f, 600f));
            
            Destroy(collision.transform.parent.gameObject);
           
        }
        if (collision.gameObject.tag == "Move_Floor")
        {
            jump_flag = false;
            animator.SetBool("jump", false);
            this.gameObject.transform.parent = collision.gameObject.transform;
        }

       
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Move_Floor")
        {
            this.gameObject.transform.parent = null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "goal")
        {
            goal_text.SetActive(true);
            Title_Reload.SetActive(true);
        }
        if (collision.gameObject.tag == "start_trigger")
        {
            speed = 0;
            collision.GetComponent<BoxCollider2D>().enabled = false;
            camera_control.Title = false;
        }
        if (collision.gameObject.tag == "score_item")
        {
            if (camera_control.Title == false)
            {
                score += Random.Range(30, 60);
                Destroy(collision.gameObject);
            }
        }
    }
    public void Restart()
    {
        camera_control.Title = true;
        HP = 3;
        SceneManager.LoadScene("stage1-1");
    }
}
