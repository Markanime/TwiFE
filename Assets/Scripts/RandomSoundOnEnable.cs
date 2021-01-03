using UnityEngine;

public class RandomSoundOnEnable : MonoBehaviour
{
    public AudioClip[] Clips;
    private void OnEnable()
    {
        GetComponent<AudioSource>().clip = Clips[Random.Range(0, Clips.Length - 1)];
        GetComponent<AudioSource>().Play();
    }
}
