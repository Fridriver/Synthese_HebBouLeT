using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIntensity : MonoBehaviour
{
    [SerializeField] private Light alarmLight;
    [SerializeField] private float intensityChangeSpeed = 1.0f;

    void Start()
    {
        StartCoroutine(AlarmRoutine());
    }

    IEnumerator AlarmRoutine()
    {
        while (true)
        {
            float temps = 0f;
            while (temps < 1.0f)
            {
                temps += Time.deltaTime * intensityChangeSpeed;
                alarmLight.intensity = Mathf.Lerp(0f, 1f, temps);
                yield return null;
            }

            yield return new WaitForSeconds(1.0f);

            temps = 0f;
            while (temps < 1.0f)
            {
                temps += Time.deltaTime * intensityChangeSpeed;
                alarmLight.intensity = Mathf.Lerp(1f, 0f, temps);
                yield return null;
            }

            yield return new WaitForSeconds(1.0f);
        }
    }
}
