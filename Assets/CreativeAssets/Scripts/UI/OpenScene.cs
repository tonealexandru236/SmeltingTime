using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenScene : MonoBehaviour
{
    public string scene_name;
    public void on_click()
    {
        AudioManager.instance.PlaySound("buttonClick");
        SceneManager.LoadScene(scene_name);
    }
}
