using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Weather : MonoBehaviour
{
    public ParticleSystem rain;
    public GameObject rain_overlay;
    void Start()
    {
        rain.Stop();
        
        if(SceneManager.GetActiveScene().name.ToLower() != "tutorial")
            StartCoroutine(wait_for_weather());
    }

    private Animator rainAnimator;

    IEnumerator wait_for_weather()
    {
        float start_time = Random.Range(1f, 2f);
        yield return new WaitForSeconds(start_time);

        rainAnimator = rain_overlay.GetComponent<Animator>();

        while (true)
        {
            int rain_chance = Random.Range(1, 13);

            if(rain_chance == 1)
            {
                float rain_time = Random.Range(14f, 36f);
                float intensity = Random.Range(80f, 100f);

                var em = rain.emission;
                em.rateOverTime = intensity;

                /*var emg = rain.main;
                var emgg = emg.gravityModifier;

                emgg.constantMax = intensity / 50 + 1;*/

                rain.Play();
                rainAnimator.SetFloat("Speed", 1);
                rainAnimator.Play("weather", 0, 0);

                if(AudioManager.instance != null) AudioManager.instance.PlayWeather("Rain", 1);

                yield return new WaitForSeconds(rain_time);

                rain.Stop();
                rainAnimator.SetFloat("Speed", -1);
                rainAnimator.Play("weather", 0, 1);

                if (AudioManager.instance != null) AudioManager.instance.PlayWeather("Rain", 0);

                yield return new WaitForSeconds(rain_time/4);
            }

            float wait_time = Random.Range(7f, 8.2f);
            yield return new WaitForSeconds(wait_time);
        }
    }
}
