using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    /// <summary>
    /// ������ 0 - ��������� ���� (Start Menu)
    /// ������ 1 - ������� ���� (Play Menu)
    /// ������ 2 - ���� ����� ����  (Game Over)
    /// </summary>
    public Canvas[] canvasMenu; //������ ����

    public float timeShowingAnnounceMenu = 1f; //����� ������ ���� ����������

    //public AudioSource startMenuFonSound; //AudioResource ��� ���������� ����

    //public AudioSource gameOverFonSound; //AudioResource ��� ���� ��������� ����

    public GameObject player; //������ �� ������ Player

    public GameObject AudioManager;

    private AudioControl _audioControl;

    void Start()
    {
        _audioControl = AudioManager.GetComponent<AudioControl>();
        //��������� ��������� ���� 
        canvasMenu[0].gameObject.SetActive(true);
        //����������� �������� ����
        canvasMenu[1].gameObject.SetActive(false);
        //����������� ���� GameOver
        canvasMenu[2].gameObject.SetActive(false);
        player.SetActive(false);
    }



    public void ShowGameOverMenu(bool value)
    {
        //��������� ���� Game Over
        canvasMenu[2].gameObject.SetActive(true);
        ShowGameOverAnnounce(value);
    }

    private void ShowGameOverAnnounce(bool value)
    {
        GetComponent<UIShowGameOverText>().SetMessageForGameOverText(value);
    }

    /// <summary>
    /// ��������� ������� �� ������ �����
    /// </summary>
    public void BAStartButton()
    {
        //���������� ������� ������ � ��������� ����
        _audioControl.StopAudioSourse(0);
        //����������� ��������� ���� 
        canvasMenu[0].gameObject.SetActive(false);
        //��������� �������� ����
        canvasMenu[1].gameObject.SetActive(true);
        //��������� ������
        player.SetActive(true);
        //��������� ������� ������ � ����
        _audioControl.PlayAudioSourse(1);
    }

    /// <summary>
    /// ��������� ������� �� ������ �����
    /// </summary>
    public void BAExitButton()
    {
        Application.Quit();
    }

    /// <summary>
    /// ��������� ������� �� ������ �������
    /// </summary>
    public void BARestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
