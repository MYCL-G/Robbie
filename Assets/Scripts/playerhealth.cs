using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerhealth : MonoBehaviour
{
    public GameObject deathyz;
    int trapslayer;
    void Start()
    {
        trapslayer=LayerMask.NameToLayer("traps");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer==trapslayer){
            // Instantiate(deathyz,transform.position,transform.rotation);
            Instantiate(deathyz,transform.position,Quaternion.Euler(0,0,Random.Range(-60,60)));
            gameObject.SetActive(false);
            AudioManager.plaudeathaudio();
            GameManager.playerdead();
        }
    }
}
