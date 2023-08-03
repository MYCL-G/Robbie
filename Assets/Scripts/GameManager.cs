using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gm;
    SceneFader fader;
    List<orbcollect>orbs;
    public int orbnum;
    public int deadnum;
    door lockdoor;
    float gametime;
    bool gameover;
    private void Awake()
    {
        if(gm!=null){
            Destroy(gameObject);
            return; 
        }
        gm=this;
        DontDestroyOnLoad(gameObject);
        orbs=new List<orbcollect>();
    }
    private void Update() {
        if(gameover)return;
        orbnum=gm.orbs.Count;
        gametime+=Time.deltaTime;
        uimanager.updatetimeui(gametime);
    }
    public static void redoor(door door){
        gm.lockdoor=door;
    }
    public static void playergetorb(orbcollect orb){
        if(!gm.orbs.Contains(orb))return;
        gm.orbs.Remove(orb);
        if(gm.orbs.Count==0){
            gm.lockdoor.opendoor();
        }
        uimanager.updateorbui(gm.orbs.Count);
    }
    public static void resscenefader(SceneFader other){
        gm.fader=other;
    }
    public static void Registerorb(orbcollect orb){
        if(gm==null)return;
        if(!gm.orbs.Contains(orb)){
            gm.orbs.Add(orb);
        }
        uimanager.updateorbui(gm.orbs.Count);

    }
    public static void playerdead(){
        gm.fader.FadeOut();
        gm.Invoke("restarscene",1.5f);
        gm.deadnum++;
        uimanager.updatedeadui(gm.deadnum);
    }

    public void restarscene(){
        gm.orbs.Clear();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        AudioManager.playstartaudio();
    }
    public static void playerwin(){
        AudioManager.playwinaudio();
        gm.gameover=true;
        uimanager.updateoverui();
        
    }

    public static bool gamegodie(){
        return gm.gameover;
    }
}
