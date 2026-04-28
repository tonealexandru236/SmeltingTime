using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Weather : MonoBehaviour
{
    public ParticleSystem rain;
    public ParticleSystem snow;

    public GameObject rain_overlay;

    static public float weather_debuff;
    void Start()
    {
        rain.Stop();

        weather_debuff = 1;


        if (SceneManager.GetActiveScene().name.ToLower() != "tutorial")
            StartCoroutine(wait_for_weather());
    }

    private Animator rainAnimator;

    IEnumerator wait_for_weather()
    {
        float start_time = Random.Range(1f, 2f);
        yield return new WaitForSeconds(start_time);


        if(rain_overlay.GetComponent<Animator>() != null )
            rainAnimator = rain_overlay.GetComponent<Animator>();

        while (true)
        {
            int weather_chance = Random.Range(1, 100);

            if (weather_chance <100) // Snow starts
            {
                float snow_time = Random.Range(14f, 28f);
                float intensity = Random.Range(50f, 120f);

                rain_overlay.GetComponent<Image>().color = new Color32(255, 255, 255, 0);

                weather_debuff = 1.2f;

                if (snow != null)
                {
                    var em = snow.emission;
                    em.rateOverTime = intensity;

                    snow.Play();
                }

                yield return new WaitForSeconds(2f);

                rainAnimator.SetFloat("Speed", 1);
                rainAnimator.Play("weather", 0, 0);

                yield return new WaitForSeconds(snow_time);

                if (snow != null)
                    snow.Stop();

                yield return new WaitForSeconds(2.5f);

                rainAnimator.SetFloat("Speed", -1);
                rainAnimator.Play("weather", 0, 1);

                // Snowops

                weather_debuff = 1f;

                yield return new WaitForSeconds(snow_time / 4);
            }
            else if (weather_chance <= 8) // Rain starts
            {
                float rain_time = Random.Range(14f, 36f);
                float intensity = Random.Range(80f, 100f);

                rain_overlay.GetComponent<Image>().color = new Color32(0, 97, 255, 0);

                weather_debuff = 1.1f;

                if (rain != null)
                {
                    var em = rain.emission;
                    em.rateOverTime = intensity;

                    rain.Play();
                }

                /*var emg = rain.main;
                var emgg = emg.gravityModifier;

                emgg.constantMax = intensity / 50 + 1;*/

                yield return new WaitForSeconds(0.0f);

                rainAnimator.SetFloat("Speed", 1);
                rainAnimator.Play("weather", 0, 0);

                if(AudioManager.instance != null) AudioManager.instance.PlayWeather("Rain", 1);

                yield return new WaitForSeconds(rain_time);

                if (rain != null)
                    rain.Stop();

                yield return new WaitForSeconds(0.5f);

                rainAnimator.SetFloat("Speed", -1);
                rainAnimator.Play("weather", 0, 1);

                // Rain stops

                weather_debuff = 1f;

                if (AudioManager.instance != null) AudioManager.instance.PlayWeather("Rain", 0);

                yield return new WaitForSeconds(rain_time/4);
            }

            float wait_time = Random.Range(7f, 8.2f);
            yield return new WaitForSeconds(wait_time);
        }
    }
}
