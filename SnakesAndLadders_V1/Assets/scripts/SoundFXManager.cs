using UnityEngine;

public class SoundFXManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static SoundFXManager instance; // singleton

    [SerializeField] private AudioSource soundFXObject;

     private void Awake()
     {
        if (instance == null) {
            instance = this;
        }
     }

     public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
     {
        // spawn
        AudioSource audioSource = Instantiate(soundFXObject, spawnTransform.position, Quaternion.identity);

        // assign clip & volume
        audioSource.clip = audioClip;
        audioSource.volume = volume;

        // play
        audioSource.Play();

        // get length
        float clipLength = audioSource.clip.length;

        // destroy
        Destroy(audioSource.gameObject, clipLength);
     }
}
