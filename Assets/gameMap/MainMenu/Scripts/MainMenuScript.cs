using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class MainMenuScript : MonoBehaviour
{

    public void GameButton()
    {
        Debug.Log("Scene Loaded!"); 
        SceneManager.LoadScene("Game");
        
    }
    public void GameQuit()
    {
        Application.Quit();
        Debug.Log("Game has been quit!"); 
    }

}
