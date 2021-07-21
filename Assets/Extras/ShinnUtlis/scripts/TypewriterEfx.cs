using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEfx : MonoBehaviour
{
    public Text txtDisplay;
    public AudioSource audioSource;
    public AudioClip[] typeSound;
    public float delaytimeOnStart = 1f;
    
    private float letterPause = 0.2f;
    private string words;
    private string displayText;

    private void Start()
    {
        Init();
        StartCoroutine(Display(displayText));
    }

    public void Init()
    {
        displayText = txtDisplay.text;
        txtDisplay.text = string.Empty;
    }

    public IEnumerator Display(string displayStr)
    {
        words = displayStr;
        displayText = "";
        yield return new WaitForSeconds(delaytimeOnStart);
        StartCoroutine(TypeText());
    }
    
    private IEnumerator TypeText()
    {
        foreach (var word in words)
        {
            if (audioSource != null)
            {
                int randomIndex = Random.Range(0, typeSound.Length);
                audioSource.PlayOneShot(typeSound[randomIndex]);
            }

            txtDisplay.text += word;
            yield return new WaitForSeconds(letterPause);
        }

    }
}
