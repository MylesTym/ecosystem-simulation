using UnityEngine;

public class CelestialController : MonoBehaviour
{
    [SerializeField, Range(0, 24)]
    private float timeOfDay = 12;
    public float dayDurationInMinutes = 1f;

    public Light sun;
    public Light moon;

    public event System.Action<float> OnTimeChanged;
    public event System.Action<bool> OnDayStateChanged;
    private bool wasDaytime;

    public float CurrentTimeOfDay => timeOfDay;
    public bool IsDayTime => wasDaytime;



    // Update is called once per frame
    void Update()
    {
        timeOfDay += (Time.deltaTime / (dayDurationInMinutes * 60f)) * 24f;
        timeOfDay %= 24f;

        float sunAngle = (timeOfDay / 24f) * 360f - 90f;
        sun.transform.rotation = Quaternion.Euler(sunAngle, 170f, 0f);
        moon.transform.rotation = Quaternion.Euler(sunAngle + 180f, 170f, 0f);

        bool isDayTime = timeOfDay >= 6f && timeOfDay <= 18f;

        if (isDayTime != wasDaytime)
        {
            wasDaytime = isDayTime;
            OnDayStateChanged?.Invoke(isDayTime);
        }
        sun.enabled = isDayTime;
        moon.enabled = !isDayTime;

        OnTimeChanged?.Invoke(timeOfDay);


    }
}
