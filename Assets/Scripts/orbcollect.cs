using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orbcollect : MonoBehaviour
{
    int Playerlayer;
    public GameObject orbfx;
    void Start()
    {
        Playerlayer=LayerMask.NameToLayer("Player");
        GameManager.Registerorb(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer==Playerlayer){
            Instantiate(orbfx,transform.position,transform.rotation);
            gameObject.SetActive(false);
            AudioManager.playorbaudio();
            GameManager.playergetorb(this);
        }
    }
}
