using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    [SerializeField] public AudioSource Source;

    public void PlaySound()
    {
        PlaySound(Random.Range(0, clips.Length));
    }

    public void PlaySound(int index)
    {
        Source.PlayOneShot(clips[index]);
    }
}