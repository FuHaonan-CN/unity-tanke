using UnityEngine;

public class Barrier : MonoBehaviour
{
    public AudioClip hitAudio;

    public void PlayAudio()
    {
        // 加入声音
        AudioSource.PlayClipAtPoint(hitAudio,transform.position);
    }
}
