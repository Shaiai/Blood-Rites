using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class DialogDisplay : MonoBehaviour
{
        public Conversation conversation;
        public GameObject narrationScreen;

        public GameObject speakerLeft;
        public GameObject speakerRight;

        private SpeakerUI speakerUILeft;
        private SpeakerUI speakerUIRight;

        public Button goodButton;
        public Button badButton;

        private int activeLineIndex = 0;

        private bool hasDecision;
        private bool hasNarration;
        private int isEndOfChoice;

        int choice;

        private string good;
        private string bad;
    
    void Start()
    {
        speakerUILeft = speakerLeft.GetComponent<SpeakerUI>();
        speakerUIRight = speakerRight.GetComponent<SpeakerUI>();

        speakerUILeft.Speaker = conversation.speakerLeft;
        speakerUIRight.Speaker = conversation.speakerRight;

        goodButton.gameObject.SetActive(false);
        badButton.gameObject.SetActive(false);
    }

    
    void Update()
    {
    
        //Advance through converations until you have a decision element.
        if (Input.GetKeyDown("space") && !hasDecision)
        {
            AdvanceConversation();
        }
        
    }

    void onDisable()
    {
        PlayerPrefs.SetInt("choice", choice);
    }

    void onEnable()
    {
        choice = PlayerPrefs.GetInt("choice", 0);
    }

    public void goodChange()
    {
        AdvanceConversation();
        hasDecision = false;         
        goodButton.gameObject.SetActive(false);

        badButton.gameObject.SetActive(false);
    }
     public void badChange()
    {
        activeLineIndex += 1;
        AdvanceConversation();
        hasDecision = false;
        goodButton.gameObject.SetActive(false);

        badButton.gameObject.SetActive(false);
    }

    public void callNarration()
    {
        if(hasNarration)
        {
            narrationScreen.SetActive(true);
        }
    }
    

    //Function that responds to a key press to proceed with conversation
    void AdvanceConversation()
    {
        if(activeLineIndex < conversation.lines.Length){
            DisplayLine();
            activeLineIndex += 1;
        }
        else
        {
           speakerUILeft.Hide(); 
           speakerUIRight.Hide();
           activeLineIndex = 0;
        }

        //At the end of text Dialog switch to next scene.
        if(activeLineIndex == conversation.lines.Length)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        }

        //Function that displays text to the correct panels
        void DisplayLine()
        {
            Line line = conversation.lines[activeLineIndex];
            Character character = line.character;
           
           //Checks for decisions mid text-based
            hasDecision = line.decision;

            //Checks for narration trigger
            hasNarration = line.narration;

            isEndOfChoice = line.endOfChoice;

            //Grab text from conversation to impliment on Buttons for good optio and bad option.
            good = line.decision1;
            bad  = line.decision2;
            choice = line.endOfChoice;

            //Check to see who is the speaker, set the text in the panel and hide the other speaker.
            if(speakerUILeft.SpeakerIs(character))
            {
                SetDialog(speakerUILeft, speakerUIRight, line.text);
            }
            else
            {
                SetDialog(speakerUIRight, speakerUILeft, line.text);
            }
        }


        void SetDialog(SpeakerUI activeSpeakerUI, SpeakerUI inactiveSpeakerUI, string text)
        {
            activeSpeakerUI.Dialog = text;
            activeSpeakerUI.Show();
            inactiveSpeakerUI.Hide();
        }

        //If they have a decision then hide both speakers and present buttons.
        if(activeLineIndex < conversation.lines.Length &&  hasDecision)
        {
            speakerUILeft.Hide();
            speakerUIRight.Hide();
            goodButton.gameObject.SetActive(true);
            goodButton.GetComponentInChildren<TextMeshProUGUI>().text = good;

            badButton.gameObject.SetActive(true);
            badButton.GetComponentInChildren<TextMeshProUGUI>().text= bad;
        }
        if(activeLineIndex < conversation.lines.Length && !hasNarration)
        {
            narrationScreen.SetActive(false);
        }
        if(activeLineIndex < conversation.lines.Length &&  hasNarration)
        {
            speakerUILeft.Hide();
            speakerUIRight.Hide();
            narrationScreen.SetActive(true);
            narrationScreen.GetComponentInChildren<Text>().text = conversation.lines[activeLineIndex - 1].text;
            hasNarration = false;
        }

        if(activeLineIndex < conversation.lines.Length && isEndOfChoice == -1)
        {
            activeLineIndex += 7;
            AdvanceConversation();
        }

        Debug.Log("Index is:" + activeLineIndex);
        Debug.Log("Decision =" + hasDecision);
    }  
}
