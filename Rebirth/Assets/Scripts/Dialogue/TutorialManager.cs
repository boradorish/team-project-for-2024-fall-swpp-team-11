using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class TutorialManager : MonoBehaviour
{
    private static string logPrefix = "[TutorialManager] ";

    public TextMeshProUGUI tutorialText; 
    [TextArea(3, 5)]
    public string[] tutorials; 
    [TextArea(3, 5)]
    public string[] randomDialogues; 
    private int currentTutorialIndex = 0;

    public GameObject tutorialModal;
    private bool isOutGame;

    void Start()
    {
       isOutGame = SceneManager.GetActiveScene().name == "MainMenu"
                || SceneManager.GetActiveScene().name == "Narration"
                || SceneManager.GetActiveScene().name == "EndingScene";

        if(isOutGame){
            return; 
        }

        if (tutorialModal != null)
        {
            tutorialModal.SetActive(false);
            if(CharacterStatusManager.Instance.PlayerState ==  PlayerState.Default){
                tutorialModal.SetActive(true);
            } 
        }

        if (tutorials.Length > 0)
        {
            tutorialText.text = tutorials[0];
        }
    }

    void Update()
    {
        if(isOutGame) {
            return; 
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if(CharacterStatusManager.Instance.PlayerState ==  PlayerState.Default){
                ShowNextDialogue();      
            } 
            else {
                if(tutorialModal.activeSelf){
                    tutorialModal.SetActive(false);
                }
                else {
                    tutorialModal.SetActive(true);
                    ShowRandomDialogue();
                }
                
            }
            
        }
    }

    void ShowNextDialogue()
    {
        if (currentTutorialIndex < tutorials.Length - 1)
        {
            currentTutorialIndex++;
            tutorialText.text = tutorials[currentTutorialIndex];
        }
        else
        {
            Debug.Log(logPrefix + "튜토리얼이 끝났습니다.");
            tutorialText.text = "";
            if (tutorialModal != null)
            {
                tutorialModal.SetActive(false);
            }
        }
    }

    void ShowRandomDialogue()
    {
        if (randomDialogues.Length > 0)
        {
            string randomDialogue = randomDialogues[Random.Range(0, randomDialogues.Length)]; 
            if (tutorialModal != null)
            {
                tutorialModal.SetActive(true); 
            }
            tutorialText.text = randomDialogue;
        }
        else
        {
            Debug.Log(logPrefix + "랜덤 대사가 없습니다.");
        }
    }
}
