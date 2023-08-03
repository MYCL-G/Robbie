using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class uimanager : MonoBehaviour
{
    static uimanager ui;
    public TextMeshProUGUI orbtext,timetext,deadtext,gameovertext;
    private void Awake() {
        if(ui!=null)
        {
            Destroy(gameObject);
            return;
        }
        ui=this;
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void updateorbui(int orbint){
        ui.orbtext.text=orbint.ToString();
    }

    public static void updatedeadui(int deadint){
        ui.deadtext.text=deadint.ToString();
    }

    public static void updatetimeui(float time){
        ui.timetext.text=time.ToString();
        int min=(int)(time/60);
        int seconds=(int)time%60;
        ui.timetext.text=min.ToString("00")+":"+seconds.ToString("00");
    }

    public static void updateoverui(){
        ui.gameovertext.enabled=true;
    }
}
