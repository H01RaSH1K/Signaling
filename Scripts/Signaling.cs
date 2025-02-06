using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signaling : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private float _volumeChangeStep;
    [SerializeField] private float _volumeChangeInterval;
    [SerializeField] private float _minVolume;
    [SerializeField] private float _maxVolume;

    private WaitForSeconds _waitForVolumeChange;
    private Coroutine _volumeChangeCoroutine;

    private void Awake()
    {
        _waitForVolumeChange = new WaitForSeconds(_volumeChangeInterval);
    }

    public void StartSignalingGradually()
    {
        StartPlayingSound();
        StartVolumeChangeCoroutine(_maxVolume);
    }

    public void StopSignalingGradually()
    {
        StartVolumeChangeCoroutine(_minVolume);
    }

    private void StartPlayingSound()
    {
        if (_audioSource.isPlaying == false)
            _audioSource.Play();
    }

    private void StartVolumeChangeCoroutine(float volumeTarget)
    {
        if (_volumeChangeCoroutine != null)
            StopCoroutine(_volumeChangeCoroutine);

        _volumeChangeCoroutine = StartCoroutine(ChangeVolumeCoroutine(volumeTarget));
    }

    private IEnumerator ChangeVolumeCoroutine(float volumeTarget)
    {
        while (Mathf.Approximately(_audioSource.volume, volumeTarget) == false)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, volumeTarget, _volumeChangeStep);

            yield return _waitForVolumeChange;
        }

        if (_audioSource.isPlaying && _audioSource.volume <= 0)
            _audioSource.Stop();

        _volumeChangeCoroutine = null;
    }
}
