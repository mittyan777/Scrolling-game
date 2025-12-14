using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Livestext;
    [SerializeField] float Start_time;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("Gamestart", Start_time);
        
    }

    // Update is called once per frame
    void Update()
    {
        Livestext.GetComponent<TextMeshProUGUI>().outlineColor = new Color(255, 255, 255, 255);
        Livestext.text = ($"Å~{Player_controller.HP}");
    }
    void Gamestart()
    {
        if (Player_controller.HP <= 0)
        {
            camera_control.Title = true;
            Player_controller.HP = 3;
        }
        SceneManager.LoadScene("stage1-1");
    }
}
