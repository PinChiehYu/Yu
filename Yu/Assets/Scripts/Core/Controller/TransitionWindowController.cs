using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionWindowController : MonoBehaviour
{
    public GameObject TransitionImage;
    private Animator _animator;

    private Action _fadeInCallback;
    private Action _fadeOutCallback;


    void Awake()
    {
        _animator = TransitionImage.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo animatorInfo;

        animatorInfo = _animator.GetCurrentAnimatorStateInfo(0);

        //  "roar" 是该动画的名字

        if ((animatorInfo.normalizedTime > 1.0f) && (animatorInfo.IsName("fadeIn")))//normalizedTime: 范围0 -- 1,  0是动作开始，1是动作结束  
        {
            _animator.SetTrigger("fadeOut");
            if (_fadeInCallback != null)
            {
                _fadeInCallback();
                _fadeInCallback = null;
            }
        }else if ((animatorInfo.normalizedTime > 1.0f) && (animatorInfo.IsName("fadeOut")))//normalizedTime: 范围0 -- 1,  0是动作开始，1是动作结束  
        {
            _animator.Play("none");
            if (_fadeOutCallback != null)
            {
                _fadeOutCallback();
                _fadeOutCallback = null;
                TransitionImage.SetActive(false);
            }
        }
    }

    public void StartTransition()
    {
        TransitionImage.SetActive(true);
        _animator.SetTrigger("fadeIn");
    }

    public void StartTransition(Action fadeInCallback, Action fadeOutCallback)
    {
        TransitionImage.SetActive(true);
        if (fadeInCallback != null)
        {
            _fadeInCallback = fadeInCallback;
        }
        if (fadeOutCallback != null)
        {
            _fadeOutCallback = fadeOutCallback;
        }
        _animator.SetTrigger("fadeIn");
    }

    //private IEnumerator WaitAnimationEnd()
    //{
    //    yield return new WaitForSeconds(time);
    //    EndAnimation();
    //}

    //private void EndAnimation()
    //{
    //    // 动作播放结束
    //}
}
