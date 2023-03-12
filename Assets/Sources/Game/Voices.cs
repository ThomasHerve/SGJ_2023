using System.Collections;
using UnityEngine;

public class Voices : MonoBehaviour
{
    [SerializeField]
    public AudioClip[] voiceClips; // tableau des clips audio � jouer
    public float minInterval = 6f; // intervalle minimum entre deux voix
    public float maxInterval = 12f; // intervalle maximum entre deux voix

    private AudioSource audioSource;
    private float nextVoiceTime;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        nextVoiceTime = Time.time + Random.Range(minInterval, maxInterval); // on initialise le temps de la prochaine voix
    }

    private void Update()
    {
        if (Time.time >= nextVoiceTime) // si le temps de la prochaine voix est atteint
        {
            PlayRandomVoice(); // on joue une voix al�atoire
            nextVoiceTime = Time.time + Random.Range(minInterval, maxInterval); // on calcule le temps de la prochaine voix
        }
    }

    private void PlayRandomVoice()
    {
        if (voiceClips.Length == 0) return; // si le tableau est vide, on ne fait rien

        int index = Random.Range(0, voiceClips.Length); // on tire un index al�atoire dans le tableau
        audioSource.PlayOneShot(voiceClips[index]); // on joue le clip audio correspondant
    }
}