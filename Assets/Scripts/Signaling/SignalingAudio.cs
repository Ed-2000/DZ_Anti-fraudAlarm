using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Signaling))]
public class SignalingAudio : MonoBehaviour
{
    [SerializeField] private float _speedOfSoundChange = 0.5f;

    private Coroutine _currentCoroutine = null;
    private AudioSource _audioSource;
    private Signaling _signaling;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _signaling = GetComponent<Signaling>();
        _audioSource.volume = 0;
    }

    private void OnEnable()
    {
        _signaling.RogueCameIn += TurnOnSignaling;
        _signaling.RogueIsOut += TurnOffSignaling;
    }

    private void OnDisable()
    {
        _signaling.RogueCameIn -= TurnOnSignaling;
        _signaling.RogueIsOut -= TurnOffSignaling;
    }

    private void TurnOnSignaling()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _currentCoroutine = StartCoroutine(SmoothlyIncreaseVolume());
    }

    private void TurnOffSignaling()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);
        
        _currentCoroutine = StartCoroutine(SmoothlyReduceVolume());
    }

    private IEnumerator SmoothlyIncreaseVolume()
    {
        var wait = new WaitForEndOfFrame();
        int _maxVolume = 1;
        _audioSource.Play();

        while (_audioSource.volume != _maxVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _maxVolume, _speedOfSoundChange * Time.deltaTime);

            yield return wait;
        }
    }

    private IEnumerator SmoothlyReduceVolume()
    {
        var wait = new WaitForEndOfFrame();
        float _minVolume = 0;

        while (_audioSource.volume != _minVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, _minVolume, _speedOfSoundChange * Time.deltaTime);

            yield return wait;
        }

        _audioSource.Stop();
    }
}