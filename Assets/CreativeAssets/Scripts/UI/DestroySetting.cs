using UnityEngine;
using static UnityEngine.ParticleSystem;

public class DestroySetting : MonoBehaviour
{
    public string setting_name;
    private int state;
    void Start()
    {
        if(setting_name == "fps")
            state = PlayerPrefs.GetInt("s_fps", 0);
        else if (setting_name == "weather")
            state = PlayerPrefs.GetInt("s_weather", 1);
        else if (setting_name == "particles")
            state = PlayerPrefs.GetInt("s_particles", 1);

        if (state == 0)
            Destroy(gameObject);
    }
}
