using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public int difficulty = 1;

    [SerializeField] TextMeshProUGUI difficultyText;

    public static UIManager singleton;

    public WordList wordList;
    private void Awake()
    {
        difficulty = 2;
        if (singleton != null)
        {
            Destroy(this);
        }

        singleton = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        wordList = this.GetComponent<WordList>();
        DontDestroyOnLoad(this);
        //LoadGameScene();
        ChangeWord();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void IncreaseDifficulty()
    {
        if (difficulty >= 2)
        {
            return;
        }
        difficulty++;
        SetDifficultyText();
    }

    public void DecreaseDifficulty()
    {
        if (difficulty <= 0)
        {
            return;
        }
        difficulty--;


        SetDifficultyText();
    }

    void SetDifficultyText()
    {
        if (difficulty == 0)
        {
            difficultyText.text = "EASY";
        }
        if (difficulty == 1)
        {
            difficultyText.text = "MEDIUM";
        }
        if (difficulty == 2)
        {
            difficultyText.text = "HARD";
        }
    }
    public void ChangeWord()
    {
        wordList.ChangePrompt();
    }

}
