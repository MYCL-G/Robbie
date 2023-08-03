using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFader : MonoBehaviour
{
    Animator anim;
    int faderid;
    private void Start() {
        anim=GetComponent<Animator>();
        faderid=Animator.StringToHash("Fade");
        GameManager.resscenefader(this);
    }
    public void FadeOut(){
        anim.SetTrigger(faderid);
    }
}
