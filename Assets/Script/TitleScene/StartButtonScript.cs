using UnityEngine;
using UnityEngine.SceneManagement;


///
/// attach to StartButtonObject in Title Scene
///
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
