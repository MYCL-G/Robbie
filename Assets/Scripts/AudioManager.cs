using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    static AudioManager current;
    public AudioMixerGroup ambeintgp,muscigp,stingsgp,playergp,voicesgp,trapsgp;

    [Header("环境声音")]
    public AudioClip ambientclip;
    public AudioClip musicclip;
    [Header("Robbie声音")]
    public AudioClip[] walkstepclips;
    public AudioClip[] crouchstepclips;
    public AudioClip jumpclip;
    public AudioClip deathclip;
    public AudioClip jumpvoiceclip;
    public AudioClip deathvoiceclip;
    public AudioClip orbclip;
    public AudioClip coinclip;
    [Header("fx声音")]
    public AudioClip deathfxclip;
    public AudioClip dooropenclip;
    public AudioClip winclip;
    public AudioClip starclip;
    AudioSource ambientSource;
    AudioSource musicSource;
    AudioSource fxSource;
    AudioSource playerSource;
    AudioSource voiceSource;

    private void Awake()
    {
        if(current!=null){
            Destroy(gameObject);
            return; 
        }
        current=this;
        DontDestroyOnLoad(gameObject);
        ambientSource=gameObject.AddComponent<AudioSource>();
        musicSource=gameObject.AddComponent<AudioSource>();
        fxSource=gameObject.AddComponent<AudioSource>();
        playerSource=gameObject.AddComponent<AudioSource>();
        voiceSource=gameObject.AddComponent<AudioSource>();
        startlevelaudio();

        ambientSource.outputAudioMixerGroup=ambeintgp;
        musicSource.outputAudioMixerGroup=muscigp;
        fxSource.outputAudioMixerGroup=stingsgp;
        playerSource.outputAudioMixerGroup=playergp;
        voiceSource.outputAudioMixerGroup=voicesgp;
    }
    void startlevelaudio(){
        current.ambientSource.clip=current.ambientclip;
        current.ambientSource.loop=true;
        current.ambientSource.Play();

        current.musicSource.clip=current.musicclip;
        current.musicSource.loop=true;
        current.musicSource.Play();
    }
    public static void playfootstepaudio(){
        int index=Random.Range(0,current.walkstepclips.Length);
        current.playerSource.clip=current.walkstepclips[index];
        current.playerSource.Play();
    }
    public static void playopenddooraudio(){
        current.voiceSource.clip=current.dooropenclip;
        current.voiceSource.PlayDelayed(1);
    }

    public static void playcrouchstepaudio(){
        int index=Random.Range(0,current.crouchstepclips.Length);
        current.playerSource.clip=current.crouchstepclips[index];
        current.playerSource.Play();
    }

    public static void playjumpaudio(){
        current.playerSource.clip=current.jumpclip;
        current.playerSource.Play();

        current.voiceSource.clip=current.jumpvoiceclip;
        current.voiceSource.Play();
    }
    public static void plaudeathaudio(){
        current.playerSource.clip=current.deathclip;
        current.playerSource.Play();

        current.voiceSource.clip=current.deathvoiceclip;
        current.voiceSource.Play();

        current.fxSource.clip=current.deathfxclip;
        current.fxSource.Play();
    }
    public static void playorbaudio(){
        current.playerSource.clip=current.orbclip;
        current.playerSource.Play();

        current.voiceSource.clip=current.coinclip;
        current.voiceSource.Play();
    }
    public static void playwinaudio(){
        current.voiceSource.clip=current.winclip;
        current.voiceSource.Play();
    }
    public static void playstartaudio(){
        current.voiceSource.clip=current.starclip;
        current.voiceSource.Play();
    }
}
