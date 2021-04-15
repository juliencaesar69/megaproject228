using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SoundEdit : MonoBehaviour
{
    public AudioMixerGroup SoundMixer;
    public float _SoundF = 1;
    private int OldSound;
    public Image[] SoundsSprites;


    public AudioMixerGroup EffectMixer;
    public float _EffF = 1;
    private int OldEffF;
    public Image[] EffectSprites;

    public Sprite _false;
    public Sprite _true;


    public Slider SoundSl;
    public Slider EffSl;





    void Start()
    {
        SoundSl.value = _SoundF;
        EffSl.value = _EffF;
    }

    // Update is called once per frame
    void Update()
    {

        int ScolcoPointSound = (int)Mathf.Round(_SoundF * 10);
    
        if (ScolcoPointSound != OldSound)
        {
            for (int i = 0; i < 10; i++)
            {
                SoundsSprites[i].sprite = _false;
            }
            
            
            for(int i = 0 ; i < ScolcoPointSound ; i++)
            {
                SoundsSprites[i].sprite = _true;
            }

            OldSound = ScolcoPointSound;
        }


        int ScolcoPointEff = (int)Mathf.Round(_EffF * 10);

        if (ScolcoPointEff != OldEffF)
        {
            for (int i = 0; i < 10; i++)
            {
                EffectSprites[i].sprite = _false;
            }
            
            
            for(int i = 0 ; i < ScolcoPointEff ; i++)
            {
                EffectSprites[i].sprite = _true;
            }

            OldEffF = ScolcoPointEff;
        }




        
        

        
    }

    public void SoundVol(float SoundF)
    {
        SoundMixer.audioMixer.SetFloat("SoundVolume", Mathf.Lerp(-80, 0, SoundF));
        _SoundF = SoundF;
    }

    public void EffectVol(float EffF)
    {
        EffectMixer.audioMixer.SetFloat("EffVolume", Mathf.Lerp(-80, 0, EffF));
        _EffF = EffF;
    }
}
