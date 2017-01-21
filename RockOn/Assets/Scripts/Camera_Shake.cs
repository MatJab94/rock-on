using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Shake : MonoBehaviour
{
    private Transform _tf;
    private Vector3 _originalPosition;

    // needed for smooth damp function
    private Vector3 _speed = Vector3.one;

    void Start()
    {
        _tf = GetComponent<Transform>();
        _originalPosition = _tf.localPosition;
    }

    public void shakeCamera()
    {
        StartCoroutine(cameraShake(0.25f));
    }

    IEnumerator cameraShake(float time)
    {
        for (float f = time; f > 0; f -= Time.deltaTime)
        {
            Vector2 shake = PerlinShake(20.0f, 0.2f);
            _tf.localPosition = new Vector3(shake.x, shake.y, _originalPosition.z);
            yield return null;
        }
        for (float f = 0.1f; f > 0; f -= Time.deltaTime)
        {
            _tf.localPosition = Vector3.SmoothDamp(_tf.localPosition, _originalPosition, ref _speed, 0.1f);
            yield return null;
        }

        _tf.localPosition = _originalPosition;
    }

    public Vector2 PerlinShake(float frequency, float magnitude)
    {
        Vector2 result;
        float seed = Time.time * frequency;
        result.x = Mathf.Clamp01(Mathf.PerlinNoise(seed, 0f)) - 0.5f;
        result.y = Mathf.Clamp01(Mathf.PerlinNoise(0f, seed)) - 0.5f;
        result = result * magnitude;
        return result;
    }
}
