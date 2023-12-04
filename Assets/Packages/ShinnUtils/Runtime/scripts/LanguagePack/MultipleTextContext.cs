using Cysharp.Threading.Tasks;
using Shinn;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MultipleTextContext : MonoBehaviour
{
    [SerializeField] string id;
    [SerializeField] ContentSizeFitter targetContentSizeFitter;

    Text myText;
    TMP_Text myTextMesh;

    void Start()
    {
        if (id == string.Empty)
            return;

        MultipleTextManager.instance.UpdateMTC += UpdateText;

        myText = GetComponent<Text>();
        myTextMesh = GetComponent<TMP_Text>();

        SetText(myText, id, MultipleTextManager.instance.currentLanguageIndex);
        SetText(myTextMesh, id, MultipleTextManager.instance.currentLanguageIndex);
    }

    void OnDestroy()
    {
        MultipleTextManager.instance.UpdateMTC -= UpdateText;
    }

    void UpdateText(int _languageIndex)
    {
        SetText(myText, id, _languageIndex);
        SetText(myTextMesh, id, _languageIndex);
        UpdateSize();
    }

    void SetText<T>(T element, string _id, int _languageIndex) where T : UnityEngine.Object
    {
        if (element != null)
        {
            if (element is TMP_Text textMesh)
                textMesh.text = MultipleTextManager.instance.FindText(_id, _languageIndex);
            else if (element is Text text)
                text.text = MultipleTextManager.instance.FindText(_id, _languageIndex);
        }
    }

    public async void UpdateSize()
    {
        if (targetContentSizeFitter == null)
            return;
        targetContentSizeFitter.enabled = false;
        await UniTask.Delay(250);
        targetContentSizeFitter.enabled = true;
    }
}
