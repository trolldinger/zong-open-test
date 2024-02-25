using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveTransitionManager : MonoBehaviour
{
    [SerializeField] private float _timeBeforeTransition=3f;
    [SerializeField] private Image fadeImg;

    private bool _isTransitioning;
    private float _time;
    private string _transitionAnim;
    private Animator _anim;

    private void Start() {
        _anim = GetComponent<Animator>();
    }

    private void Update() 
    {
        if(!_isTransitioning)
            return;
        if(Time.time-_time >= _timeBeforeTransition)
        {
            _anim.Play(_transitionAnim);
            _isTransitioning = false;
        }
    }

    public void BeginTransition(string transitionName)
    {
        _isTransitioning = true;
        _transitionAnim = transitionName;
        _time = Time.time;
    }

    public void BackToInitialPos()
    {
        StartCoroutine(BackToPosSequence());
    }

    IEnumerator BackToPosSequence()
    {
        float fadeTransparency =0f;
        while(fadeTransparency < 1f)
        {
            fadeTransparency+=Time.deltaTime*5f;
            fadeImg.color = new Color(fadeImg.color.r,fadeImg.color.g,fadeImg.color.b,fadeTransparency);
            yield return new WaitForEndOfFrame();
        }
        fadeTransparency=1f;
        _anim.Play("BackToBeginning");
        while(fadeTransparency > 0f)
        {
            fadeTransparency-=Time.deltaTime*5f;
            fadeImg.color = new Color(fadeImg.color.r,fadeImg.color.g,fadeImg.color.b,fadeTransparency);
            yield return new WaitForEndOfFrame();
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
