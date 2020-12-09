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

    public Writer writer;

    private int activeLineIndex = 0;

    private bool hasDecision;
    private bool hasNarration;
    private int isEndOfChoice;
    int choice, dealsTaken;
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

        dealsTaken = PlayerPrefs.GetInt("dealsTaken", 0);

        switch(SceneManager.GetActiveScene().name)
        {
            case "Intro":
                dealsTaken = 0;
                PlayerPrefs.DeleteAll();
                choice = 1;
                break;
            case "talkGreed":
                choice = 2;
                break;
            case "talkLust":
                choice = 3;
                break;
            case "talkWrath":
                choice = 4;
                break;
        }
        AdvanceConversation();
    }

    void onDestroy()
    {
        PlayerPrefs.SetInt("dealsTaken", dealsTaken);
    }

    
    void Update()
    {
    
        //Advance through converations until you have a decision element.
        if (Input.GetKeyDown("space") && !hasDecision)
        {
            AdvanceConversation();
        }
        
    }

    public void goodChange()
    {
        AdvanceConversation();
        hasDecision = false;         
        goodButton.gameObject.SetActive(false);
        dealsTaken += 1;
        badButton.gameObject.SetActive(false);
    }
     public void badChange()
    {
        
        switch(choice)
        {
            case 1:
                activeLineIndex += 9;
                break;
            case 2:
                activeLineIndex += 23;
                break;
            case 3:
                activeLineIndex += 20;
                break;
            case 4:
                activeLineIndex += 13;
                break;
        }   
        AdvanceConversation();
        hasDecision = false;
        goodButton.gameObject.SetActive(false);

        badButton.gameObject.SetActive(false);
    }

    //Function that responds to a key press to proceed with conversation
    void AdvanceConversation()
    {
        if(activeLineIndex < conversation.lines.Length)
        {
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
            if(choice != 4)
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
            //choice = line.endOfChoice;

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
            writer.enabled = true;
            writer.buildWriter(activeSpeakerUI, text, 0.03f, false);
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
            writer.enabled = true;
            writer.buildWriter(narrationScreen.GetComponentInChildren<Text>(), conversation.lines[activeLineIndex - 1].text, 0.03f, true);
            hasNarration = false;
        }

        if(activeLineIndex < conversation.lines.Length && isEndOfChoice != 0)
        {
            switch(isEndOfChoice)
            {
                case 1:
                    SceneManager.LoadScene("Main Menu");
                    break;
                case 2:
                    activeLineIndex += 15;
                    break;
                case 3:
                    activeLineIndex += 18;
                    break;
                case 4:
                    if(dealsTaken == 3)
                        SceneManager.LoadScene("talkAlt");
                    else
                        SceneManager.LoadScene("talkEnd");
                    break;
            }
           // activeLineIndex += 7;
           // AdvanceConversation();
        }

        //Debug.Log("Index is:" + activeLineIndex);
        //Debug.Log("Decision =" + hasDecision);
    }  
}
