using UnityEngine;
using UnityEngine.UI;

public class UITimerControl : MonoBehaviour
{
    [SerializeField] private float _downHillTime = 0f;
    [SerializeField] private float _upHillTime = 0f;
    [SerializeField] private bool _isPlayerAlive = false;
    [SerializeField] private bool _isDownhiil = false;

    public GameObject uphillText;

    public Text downHilltxt;
    public Text upHilltxt;

    void Start()
    {
        downHilltxt.text = "";
        upHilltxt.text = "";
        _downHillTime = 0f;
        _upHillTime = 0f;
        _isDownhiil = false;
        uphillText.SetActive(false);
    }

    void Update()
    {
        if (_isPlayerAlive)
        {
            if (_isDownhiil)
            {
                SetValueForDownhiilTime(_downHillTime += Time.deltaTime);
            }
            else
            {
                SetValueForUphiilTime(_upHillTime += Time.deltaTime);
            } 
        }
    }

    public void SetValueForDownhiilTime(float currentTime)
    {
        downHilltxt.text = GetTime(currentTime).ToString();
    }

    public void SetValueForUphiilTime(float currentTime)
    {
        upHilltxt.text = GetTime(currentTime).ToString();
    }

    public void StartTimerDownhill()
    {
        _isPlayerAlive = true;
        _isDownhiil = true;
    }

    public void StartTimerUphill()
    {
        uphillText.SetActive(true);
        _isDownhiil = false;
    }

    public float GetTime(float currentTime)
    {
        return Mathf.Floor(currentTime);
    }

    public void StopTimer()
    {
        _isPlayerAlive = false;
    }

    public float GetTotalTime()
    {
        return GetTime(_downHillTime) + GetTime(_upHillTime);
    }
}
