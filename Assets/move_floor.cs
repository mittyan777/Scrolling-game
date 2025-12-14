using UnityEngine;

public class move_floor : MonoBehaviour
{
    [SerializeField] bool Axis_x;


    Vector3 startPos;//’è‹`
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Axis_x == false)
        {
            float posY = startPos.y + Mathf.Sin(Time.time) * 2;

            transform.position = new Vector3(transform.position.x, posY, transform.position.z);
        }
        else
        {
            float posX = startPos.x + Mathf.Sin(Time.time) * 2;

            transform.position = new Vector3(posX, transform.position.y, transform.position.z);
        }
    }
}
