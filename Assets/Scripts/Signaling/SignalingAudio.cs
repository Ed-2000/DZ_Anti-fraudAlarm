using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(Signaling))]
public class SignalingAudio : MonoBehaviour
{
    [SerializeField] private float _speedOfSoundChange = 0.5f;
    [SerializeField] private float _minVolume = 0;
    [SerializeField] private float _maxVolume = 1;

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
        _audioSource.Play();
        CoroutineSwitcher(_maxVolume);
    }

    private void TurnOffSignaling()
    {
        CoroutineSwitcher(_minVolume);
    }

    private void CoroutineSwitcher(float targetVolume)
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _currentCoroutine = StartCoroutine(SmoothVolumeChange(targetVolume));
    }

    private IEnumerator SmoothVolumeChange(float targetVolume)
    {
        var wait = new WaitForEndOfFrame();

        while (_audioSource.volume != targetVolume)
        {
            _audioSource.volume = Mathf.MoveTowards(_audioSource.volume, targetVolume, _speedOfSoundChange * Time.deltaTime);

            yield return wait;
        }

        if (_audioSource.volume == 0)
            _audioSource.Stop();
    }
}