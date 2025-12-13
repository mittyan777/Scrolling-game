using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Livestext;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("Gamestart",1);
    }

    // Update is called once per frame
    void Update()
    {
        Livestext.text = ($"Å~{Player_controller.HP}");
    }
    void Gamestart()
    {
        SceneManager.LoadScene("stage1-1");
    }
}
