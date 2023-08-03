using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playeranim : MonoBehaviour
{
    Animator anim;
    playermove pm;
    int fallid;
    void Start()
    {
        anim=GetComponent<Animator>();
        pm=GetComponentInParent<playermove>();
        fallid=Animator.StringToHash("verticalVelocity");
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("speed",Mathf.Abs(pm.nowxspeed));
        anim.SetBool("isCrouching",pm.squatzt);
        anim.SetBool("isHanging",pm.hanging);
        anim.SetBool("isJumping",pm.sky);
        anim.SetBool("isOnGround",pm.ground);
        anim.SetFloat(fallid,pm.nowyspeed);
    }
    
    public void StepAudio(){
        AudioManager.playfootstepaudio();
    }

    public void CrouchStepAudio(){
        AudioManager.playcrouchstepaudio();
    }
}
