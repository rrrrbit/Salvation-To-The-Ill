using UnityEngine;

public class MGR_audio : MonoBehaviour
{
    public AssetAliases assetAliases;

    public void PlaySoundOmnipresent(AudioClip clip)
    {
        GetComponent<AudioSource>().PlayOneShot(clip);
    }

    public void PlayAtPos(AudioClip clip, Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(clip, pos);
    }
}
