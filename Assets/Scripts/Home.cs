using UnityEngine;

public class Home : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private bool _hasInvasion = false;
    private float _volume = 0;
    private float _stepVolume = 0.001f;
    private float _maxVolume = 1f;
    private float _minVolume = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief))
        {
            _audioSource?.Play();
            _hasInvasion = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief))
        {
            _hasInvasion = false;
        }
    }

    private void Update()
    {
        if (_hasInvasion)
        {
            if (_volume < _maxVolume)
            {
                _volume += _stepVolume;

                if (_volume > _maxVolume)
                    _volume = _maxVolume;

                _audioSource.volume = _volume;
            }
        }
        else
        {
            if (_volume > _minVolume)
            {
                _volume -= _stepVolume;

                if (_volume < _minVolume)
                    _volume = _minVolume;

                _audioSource.volume = _volume;

                if (_volume == _minVolume)
                    _audioSource.Pause();
            }
        }
    }
}