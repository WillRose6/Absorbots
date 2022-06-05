﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private float duration;
    [SerializeField]
    private float magnitude;

    public void CreateShake()
    {
        StartCoroutine(Shake(duration, magnitude));
    }

    public void CreateShake(float _duration, float _magnitude)
    {
        StartCoroutine(Shake(_duration, _magnitude));
    }
    private IEnumerator Shake (float _duration, float _magnitude)
    {
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0f;
        while (elapsed < _duration)
        {
            float x = Random.Range(-1f, 1f) * _magnitude;
            float y = Random.Range(-1f, 1f) * _magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
