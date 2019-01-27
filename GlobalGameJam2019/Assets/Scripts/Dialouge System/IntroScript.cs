using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
    private bool pushed = false;
    public void buttonPushed(){
        pushed = true;
    }
    private void Update(){
        if(DialougeManager.inConversation == false){
            if(pushed == true){
                SceneManager.LoadScene("Level_01");
            }

        }
    }
}
