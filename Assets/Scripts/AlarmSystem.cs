using System.Collections;
using UnityEngine;

public class AlarmSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _stepVolume = 0.1f;

    private float _maxVolume = 1f;
    private float _minVolume = 0;
    private float _timeStep = 1f;
    private WaitForSeconds _sleepTime;
    private Coroutine _coroutineInceaseVolume;

    private void Start()
    {
        _sleepTime = new WaitForSeconds(_timeStep);
    }

    public void On()
    {
        _audioSource.Play();
        _coroutineInceaseVolume = StartCoroutine(IncreaseVolume());
    }

    public void Off()
    {
        if (_coroutineInceaseVolume != null)
            StopCoroutine(_coroutineInceaseVolume);

        StartCoroutine(TurnDownVolume());
    }

    private IEnumerator IncreaseVolume()
    {
        while (_audioSource.volume < _maxVolume)
        {
            _audioSource.volume += _stepVolume;
            yield return _sleepTime;
        }
    }

    private IEnumerator TurnDownVolume()
    {
        while (_audioSource.volume > _minVolume)
        {
            _audioSource.volume -= _stepVolume;

            if (_audioSource.volume <= _minVolume)
                _audioSource.Pause();

            yield return _sleepTime;
        }
    }
}
