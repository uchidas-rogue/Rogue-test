using UnityEngine;
using UnityEngine.SceneManagement;
public class StartButtonScript : MonoBehaviour
{
    //button pressed first
    private bool First = true;
    public void GameStart()
    {
        if (First)
        {
            SceneManager.LoadScene("DungeonSceneA");
            First = false;
        }
    }
}
