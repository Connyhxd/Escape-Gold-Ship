using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource music;
    public AudioSource sfx;

    public AudioClip keys;
    public AudioClip openDoor;
    public AudioClip wrong;
    public AudioClip right;
    public AudioClip throwObj;
    public AudioClip spray;
    public AudioClip walk;
    public AudioClip run;
    public AudioClip lighter;
    public AudioClip locker;
    public AudioClip click;
    public AudioClip locked;
    public AudioClip lose;
    public AudioClip grab;

    public AudioClip bg;
    public AudioClip chase;
    public AudioClip die;
    public AudioClip end;

    private void Start()
    {
        music.clip = bg;
        music.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfx.PlayOneShot(clip);
    }
    public void PlayChaseMusic()
    {
        music.clip = chase;
        music.loop = true;
        music.Play();
    }

    public void PlayNormalMusic()
    {
        music.clip = bg;
        music.loop = true;
        music.Play();
    }
}

