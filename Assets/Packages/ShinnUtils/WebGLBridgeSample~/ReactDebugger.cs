using UnityEngine;
using System.Text;
using TMPro;

public class ReactDebugger : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textConsole;

    private StringBuilder stringBuilder = new();

    public void ClearConsole()
    {
        stringBuilder.Clear();
        textConsole.text = stringBuilder.ToString();
    }

    public void UpdateText(string str)
    {
        stringBuilder.AppendLine(str);
        textConsole.text = stringBuilder.ToString();
    }
}
