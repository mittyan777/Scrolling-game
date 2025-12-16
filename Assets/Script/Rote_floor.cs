using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Rote_floor : MonoBehaviour
{
    float rotationSpeed = 30;
    Rigidbody2D rb;
    [SerializeField] GameObject target;
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
      
        
            transform.Rotate(Vector3.forward * -rotationSpeed * Time.deltaTime);
        
        transform.RotateAround(target.transform.position, Vector3.forward, 30 * Time.deltaTime);

    }


}
