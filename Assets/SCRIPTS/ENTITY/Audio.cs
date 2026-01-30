using UnityEngine;

public class Audio : MonoBehaviour
{
    ENTITY entity;
    public virtual void Play(string sound)
    {
        GetComponent<AudioSource>().PlayOneShot((AudioClip)MGR.audio.assetAliases["audio"][sound]);
    }

    public virtual void Play(AudioClip clip)
    {
        GetComponent<AudioSource>().PlayOneShot(clip);
    }
}
