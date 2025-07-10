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

    public Transform moonVisual;
    public float moonDistance = 100f;



    // Update is called once per frame
    void Update()
    {
    timeOfDay += (Time.deltaTime / (dayDurationInMinutes * 60f)) * 24f;
    timeOfDay %= 24f;

    // Rotate around X axis only (sunAngle = 0 at sunrise)
    float sunAngle = (timeOfDay / 24f) * 360f - 90f;

    Quaternion sunRotation = Quaternion.Euler(sunAngle, 0f, 0f);
    sun.transform.rotation = sunRotation;

    // Moon is opposite the sun
    Quaternion moonRotation = sunRotation * Quaternion.Euler(180f, 0f, 0f);
    moon.transform.rotation = moonRotation;

    // Place moon visual opposite the sun
    Vector3 moonDir = moon.transform.forward * -1f;
    moonVisual.position = Vector3.zero + moonDir * moonDistance;
    moonVisual.LookAt(Camera.main.transform);

    // Day/night toggle
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
