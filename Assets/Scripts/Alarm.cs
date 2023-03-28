using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _stepVolume = 0.1f;

    private float _maxVolume = 1f;
    private float _minVolume = 0;
    private float _timeStep = 1f;
    private WaitForSeconds _sleepTime;
    private Coroutine _inceaseVolume;
    private Coroutine _turnDownVolume;

    private void Start()
    {
        _sleepTime = new WaitForSeconds(_timeStep);
    }

    public void On()
    {
        TryStopCoroutine(_turnDownVolume);
        _audioSource.Play();
        _inceaseVolume = StartVolumeCoroutine(_inceaseVolume, _maxVolume);
    }

    public void Off()
    {
        TryStopCoroutine(_inceaseVolume);
        _turnDownVolume = StartVolumeCoroutine(_turnDownVolume, _minVolume);
    }

    private Coroutine StartVolumeCoroutine(Coroutine curentCoroutine, float targetVolume)
    {
        TryStopCoroutine(curentCoroutine);
        return StartCoroutine(ChangeVolume(targetVolume));
    }

    private void TryStopCoroutine(Coroutine coroutine)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
    }

    private IEnumerator ChangeVolume(float targetVolume)
    {
        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _stepVolume);

            if (_audioSource.volume <= _minVolume)
                _audioSource.Pause();

            yield return _sleepTime;
        }
    }
}
