using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Signaling : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _volumeChangeStep;
    [SerializeField] private float _volumeChangeInterval;
    [SerializeField] private float _minVolume;
    [SerializeField] private float _maxVolume;

    private int _swipersIn = 0;
    private float _volumeTarget;
    private WaitForSeconds _waitForVolumeChange;

    private Coroutine _volumeChangeCoroutine;

    private void Awake()
    {
        _waitForVolumeChange = new WaitForSeconds(_volumeChangeInterval);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_swipersIn == 0)
            StartSignalingGradually();

        _swipersIn++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _swipersIn--;

        if (_swipersIn == 0)
            StopSignalingGradually();
    }

    private void StartSignalingGradually()
    {
        _volumeTarget = _maxVolume;
        StartPlayingSound();
        StartVolumeChangeCoroutine();
    }

    private void StopSignalingGradually()
    {
        _volumeTarget = _minVolume;
        StartVolumeChangeCoroutine();
    }

    private void StartPlayingSound()
    {
        if (_audioSource.isPlaying == false)
            _audioSource.Play();
    }

    private void StartVolumeChangeCoroutine()
    {
        if (_volumeChangeCoroutine == null)
            _volumeChangeCoroutine = StartCoroutine(ChangeVolumeCoroutine());
    }

    private IEnumerator ChangeVolumeCoroutine()
    {
        while (Mathf.Approximately(_audioSource.volume, _volumeTarget) == false)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _volumeTarget, _volumeChangeStep);

            yield return _waitForVolumeChange;
        }

        _volumeChangeCoroutine = null;
    }
}
