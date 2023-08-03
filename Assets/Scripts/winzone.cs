using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winzone : MonoBehaviour
{
    int playerlayer;
    // Start is called before the first frame update
    void Start()
    {
        playerlayer=LayerMask.NameToLayer("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer==playerlayer){
            GameManager.playerwin();
        }
    }
}
