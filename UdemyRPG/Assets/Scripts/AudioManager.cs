using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] sfx;
    public AudioSource[] bgm;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void PlaySFX(int soundToPlay){
        //Verifica se som existe
        if(soundToPlay < sfx.Length){
            //Toca o SFX
            sfx[soundToPlay].Play();
        } 
    }

    public void PlayBgm(int musicToPlay){
        //Verifica se a musica ja está tocando
        if(!bgm[musicToPlay].isPlaying){
            //Para todas as Musicas que estejam tocando
            StopMusic();
            //Verifica se som existe
            if(musicToPlay < bgm.Length){
                //Toca a Musica
                bgm[musicToPlay].Play();
            }   
        }  
    }

    public void StopMusic(){
        //Percorre todas as musicas
        for(int i = 0; i < bgm.Length; i++){
            //Para a musica
            bgm[i].Stop();
        }
    }
}
