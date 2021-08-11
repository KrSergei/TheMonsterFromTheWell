using UnityEngine;
using UnityEngine.UI;

public class UIShowGameOverText : MonoBehaviour
{
    public Text gameOverText;

    [SerializeField]
    private string _messageForLose = "YOU LOSE!!";
    [SerializeField]
    private string _messageForWin = "YOU WIN!! You're total time : ";

    public void SetMessageForGameOverText(bool value)
    {
        if(value)        
            gameOverText.text = _messageForWin + GetComponent<UITimerControl>().GetTotalTime();
        else
            gameOverText.text = _messageForLose;
    }
}
