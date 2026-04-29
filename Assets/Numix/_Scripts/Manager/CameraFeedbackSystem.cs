using UnityEngine;
using System.Collections;

public class CameraFeedbackSystem : MonoBehaviour
{
    private IEventBus eventBus;

    private Vector3 originalPosition;
    private bool isShaking = false;

    [SerializeField] private float shakeDuration = 0.2f;
    [SerializeField] private float shakeMagnitude = 0.2f;

    void Awake()
    {
        eventBus = ServiceLocator.Get<IEventBus>();
        originalPosition = transform.localPosition;
    }

    void OnEnable()
    {
        eventBus.Subscribe<Events.OnLevelFailed>(OnLevelLost);
    }

    void OnDisable()
    {
        eventBus.Unsubscribe<Events.OnLevelFailed>(OnLevelLost);
    }

    private void OnLevelLost(Events.OnLevelFailed evt)
    {
        TriggerFeedback();
    }

    private void TriggerFeedback()
    {
        if (!isShaking)
        {
            StartCoroutine(ShakeCamera());
        }

        TriggerVibration();
    }

    private IEnumerator ShakeCamera()
    {
        isShaking = true;

        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.localPosition = originalPosition + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPosition;
        isShaking = false;
    }

    private void TriggerVibration()
    {
#if UNITY_ANDROID || UNITY_IOS
        Handheld.Vibrate();
#endif
    }
}