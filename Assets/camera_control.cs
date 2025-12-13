using UnityEngine;

public class camera_control : MonoBehaviour
{
    [SerializeField] GameObject Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.x >= 0 && Player.transform.position.x <= 100)
        {
            transform.position = new Vector3(Player.transform.position.x, 0, -10);
        }
     

    }
}
