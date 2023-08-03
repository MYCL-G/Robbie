using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    Animator anim;
    int openid;
    void Start()
    {
        anim=GetComponent<Animator>();
        openid=Animator.StringToHash("Open");
        GameManager.redoor(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void opendoor(){
        anim.SetTrigger(openid);
        AudioManager.playopenddooraudio();
    }
}
