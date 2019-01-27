using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialougeManager : MonoBehaviour{

    public Text dialougeName;
    public Text dialougeText;

    public Animator animator;

    public static bool inConversation = false;

    private Queue<string> sentences;
    private Queue<string> names;

    private int nameCount;
    private int sentenceCount;

    void Start(){
        sentences = new Queue<string>();
        names = new Queue<string>();
    }

    public void StartDialouge(Dialouge dialouge) {
        Debug.Log("Starting Dialouge");
        sentences.Clear();
        names.Clear();
        inConversation = true;
        animator.SetBool("inDialouge", true);

        foreach (string sentence in dialouge.sentences)
        {
            sentences.Enqueue(sentence);
            sentenceCount++;
        }

        foreach(string name in dialouge.name){
            names.Enqueue(name);
            nameCount++;
        }

        if(nameCount != sentenceCount){
            Debug.Log("OH DANG! There are "+ nameCount + "names and " + sentenceCount + "sentences! If those numbers ain't adding up, FIX IT!");
        }

        DisplayNextSentence();
    }

        public void DisplayNextSentence(){
            if(sentences.Count == 0){
                EndDialouge();
                return;
            }
        string sentence = sentences.Dequeue();
        string name = names.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        dialougeName.text = name;
        Debug.Log(sentence);
        }

    IEnumerator TypeSentence(string sentence){
        dialougeText.text = "";
        foreach (char letter in sentence.ToCharArray()){
            dialougeText.text += letter;
            yield return null;
        }
    }

        void EndDialouge(){
            Debug.Log("End of conversation.");
        inConversation = false;
        animator.SetBool("inDialouge", false);
    }

    void Update(){
        if (inConversation){
            if (Input.GetKeyDown(KeyCode.Space)){
                DisplayNextSentence();
            }
        }
    }
}

