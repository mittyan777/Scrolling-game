using UnityEngine;
using System.Collections;
public class camera_control : MonoBehaviour
{
    [SerializeField] GameObject Player;
    static public bool Title = true;
    bool start = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Title == false)
        {
            if (start == false)
            {
                StartCoroutine(SlowLoop());
                StartCoroutine(SlowLoop2());

                // transform.position = new Vector3(0, 0, -10);
                start = true;
            }
            if (GetComponent<Camera>().orthographicSize >= 5)
            {
                if (Player.transform.position.x >= 0 && Player.transform.position.x <= 128)
                {
                    transform.position = new Vector3(Player.transform.position.x, 0, -10);
                }
            }
        }
        else
        {
                GetComponent<Camera>().orthographicSize = 2;
            transform.position = new Vector3(-5.24f, -3.3f, -10);
        }
     

    }
    IEnumerator SlowLoop()
    {
        while (GetComponent<Camera>().orthographicSize <= 5)
        {
            
                GetComponent<Camera>().orthographicSize += 3.5f * Time.deltaTime;
            

            // 0.5•b‘Ò‚Â
            yield return new WaitForSeconds(0.01f);
        }

    }
    IEnumerator SlowLoop2()
    {
        while (transform.position != new Vector3(0, 0, -10))
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, -10), 10 * Time.deltaTime);
            yield return new WaitForSeconds(0.01f);
        }

    }

}
