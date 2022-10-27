using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class WordList : MonoBehaviour
{
    List<string> easyWords = new List<string>();
    List<string> mediumWords = new List<string>();
    List<string> hardWords = new List<string>();

    [SerializeField] Transform contentWindow;
    TextMeshProUGUI promptText;

    public string currentWord;
    public GameObject recallTextObject;
    // Start is called before the first frame update
    void Awake()
    {
        LoadDictionaries();
        promptText = contentWindow.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    async void LoadDictionaries()
    {
        string baseStr = "/Words/";
        easyWords = await GetWordsAsync(baseStr + "easy.txt");
        mediumWords = await GetWordsAsync(baseStr + "medium.txt");
        hardWords = await GetWordsAsync(baseStr + "hard.txt");
    }

    async Task<List<string>> GetWordsAsync(string path)
    {
        UnityWebRequest request = UnityWebRequest.Get(Application.streamingAssetsPath + path);
        UnityWebRequestAsyncOperation op = request.SendWebRequest();
        while (op.isDone == false)
        {
            await Task.Delay(30);
        }

        return request.downloadHandler.text.Split(new string[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.None).ToList();
    }

    public void ChangePrompt()
    {
        StartCoroutine(LoadWords());
    }

    IEnumerator LoadWords()
    {
        while (hardWords.Count == 0 || easyWords.Count == 0 || mediumWords.Count == 0)
        {
            yield return null;
        }
        if (UIManager.singleton != null)
        {
            if (UIManager.singleton.difficulty == 0)
            {
                Debug.Log("easy");
                int randomIndex = Random.Range(0, hardWords.Count);
                currentWord = easyWords[randomIndex];

                easyWords.Remove(hardWords[randomIndex]);
            }
            else if (UIManager.singleton.difficulty == 1)
            {
                Debug.Log("medium");
                int randomIndex = Random.Range(0, hardWords.Count);
                currentWord = mediumWords[randomIndex];

                mediumWords.Remove(hardWords[randomIndex]);
            }
            else if (UIManager.singleton.difficulty == 2)
            {
                Debug.Log("hard");
                int randomIndex = Random.Range(0, hardWords.Count);
                currentWord = hardWords[randomIndex];
                hardWords.Remove(hardWords[randomIndex]);
            }
        }
        else
        {
            Debug.Log("null");
            int randomIndex = Random.Range(0, hardWords.Count);
            currentWord = hardWords[randomIndex];

            hardWords.Remove(hardWords[randomIndex]);
        }
    }

    public void ChangeText()
    {
        promptText.text = currentWord;
    }
    public void ChangeTextToPrompt()
    {
        promptText.text = "PROMPT";
    }
}