using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    /// <summary>
    /// Индекс 0 - стартовое меню (Start Menu)
    /// Индекс 1 - игровое меню (Play Menu)
    /// Индекс 2 - меню конца игры  (Game Over)
    /// </summary>
    public Canvas[] canvasMenu; //Массив меню

    public float timeShowingAnnounceMenu = 1f; //Время показа меню объявления

    //public AudioSource startMenuFonSound; //AudioResource для стартового меню

    //public AudioSource gameOverFonSound; //AudioResource для меню окончания игры

    public GameObject player; //Ссылка на объект Player

    public GameObject AudioManager;

    private AudioControl _audioControl;

    void Start()
    {
        _audioControl = AudioManager.GetComponent<AudioControl>();
        //Активация стртового меню 
        canvasMenu[0].gameObject.SetActive(true);
        //Деактивация игрового меню
        canvasMenu[1].gameObject.SetActive(false);
        //Деактивация меню GameOver
        canvasMenu[2].gameObject.SetActive(false);
        player.SetActive(false);
    }



    public void ShowGameOverMenu(bool value)
    {
        //Активация меню Game Over
        canvasMenu[2].gameObject.SetActive(true);
        ShowGameOverAnnounce(value);
    }

    private void ShowGameOverAnnounce(bool value)
    {
        GetComponent<UIShowGameOverText>().SetMessageForGameOverText(value);
    }

    /// <summary>
    /// Обработка нажатия на кнопку старт
    /// </summary>
    public void BAStartButton()
    {
        //Отключение фоновой музыки в стартовом меню
        _audioControl.StopAudioSourse(0);
        //Деактивация стртового меню 
        canvasMenu[0].gameObject.SetActive(false);
        //Активация игрового меню
        canvasMenu[1].gameObject.SetActive(true);
        //Активация игрока
        player.SetActive(true);
        //Включение фоновой музыки в игре
        _audioControl.PlayAudioSourse(1);
    }

    /// <summary>
    /// Обратотка нажатия на кнопку выход
    /// </summary>
    public void BAExitButton()
    {
        Application.Quit();
    }

    /// <summary>
    /// Обработка нажатия на кнопку рестарт
    /// </summary>
    public void BARestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
