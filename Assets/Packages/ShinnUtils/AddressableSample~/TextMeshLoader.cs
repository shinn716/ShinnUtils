using TMPro;
using UnityEngine;

public class TextMeshLoader : MonoBehaviour
{
    private TMP_Text textMeshPro;

    private void Awake() => textMeshPro = GetComponent<TMP_Text>();

    private void Start()
    {
        Main.instance.FinishLoadText += GetFontAssets;
        if (textMeshPro.font == null)
            textMeshPro.font = Main.instance.FinalText;
    }

    private void OnDestroy() => Main.instance.FinishLoadText -= GetFontAssets;

    private void GetFontAssets(TMP_FontAsset fontAsset) => textMeshPro.font = fontAsset;
}
