using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playeranimation : MonoBehaviour
{
    [SerializeField] movment Player;
    Animator anime ;
    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(movment.Instance.speed != 0){
            
            if(Player.PlayerHasSomthing()){
                anime.SetBool("IsWalking",false);
                anime.SetBool("carry",true);
                
                if(Player.walking()){
                    anime.SetBool("Iscarrying",true);
                }
                else{
                    anime.SetBool("Iscarrying",false);
                }
            }
            else{
                anime.SetBool("Iscarrying",false);
                anime.SetBool("carry",false);
                if(Player.walking()){
                    anime.SetBool("IsWalking",true);
                }
                else{
                    anime.SetBool("Iscarrying",false);
                    anime.SetBool("IsWalking",false);
                }
            }
            
            transform.forward = Player.transform.forward;
            transform.localPosition = Vector3.zero;
            transform.position = Player.transform.position;
        }
    }
}
