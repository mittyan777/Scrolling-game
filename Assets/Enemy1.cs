using Unity.VisualScripting;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    [SerializeField] float signedDistanceX;
    [SerializeField] GameObject Player;
    float ray_distance = 0.5f;
    Rigidbody2D rigidbody2D;
    bool jump = true;
    [SerializeField] float Speed;
    [SerializeField]int ray_controller;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (camera_control.Title == false)
        {
            Vector3 diff = transform.position - Player.transform.position;

            signedDistanceX = diff.x;

            if (signedDistanceX >= 8)
            {
                transform.eulerAngles = new Vector2(0, 180);
            }
            if (signedDistanceX <= -8)
            {
                transform.eulerAngles = new Vector2(0, 0);

            }

            if (signedDistanceX <= 15)
            {
                transform.position += transform.right * Speed * Time.deltaTime;
            }


            SpriteRenderer sr = GetComponent<SpriteRenderer>();



            int layerMask = ~LayerMask.GetMask("Enemy");

            RaycastHit2D hit = Physics2D.Raycast(
                new Vector2(transform.position.x - ray_controller, transform.position.y),
                transform.right,
                ray_distance,
                layerMask
            );

            // ê‘ÇæÇØï\é¶
            Debug.DrawRay(new Vector2(transform.position.x - ray_controller, transform.position.y), transform.right * ray_distance, Color.red);

            if (hit.collider != null)
            {
                Debug.Log("ìñÇΩÇ¡ÇΩ: " + hit.collider.name);
                if (hit.collider.name == "Tilemap")
                {
                    if (jump == true)
                    {
                        rigidbody2D.AddForce(new Vector2(0f, 600f));
                        jump = false;
                    }
                }
            }
        }
    }
    //void OnDrawGizmos()
    //{
    //    SpriteRenderer sr = GetComponent<SpriteRenderer>();
    //    if (sr == null) return;
    //
    //    
    //
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(
    //        transform.position,
    //        transform.position + (Vector3)(transform.right * ray_distance)
    //    );
    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Tilemap")
        {
            jump = true;
        }
    }
}
