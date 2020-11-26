using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnyStateAnimator : MonoBehaviour
{
    public RuntimeAnimatorController[] controllers;

    private Animator animator;

    private Dictionary<string, bool> animationBools = new Dictionary<string, bool>();

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Animate();
    }

    public void AddAnimation(params string[] animations)
    {
        for (int i = 0; i < animations.Length; i++)
        {
            animationBools.Add(animations[i], false);
        }
    
    }

    public void ChangeController(int index)
    {
        animator.runtimeAnimatorController = controllers[index];
    }

    private void Animate()
    {
        foreach (string key in animationBools.Keys)
        {
            animator.SetBool(key, animationBools[key]);
        }
    }


    public void TryPlayAnimation(string animation, params string[] higherPrio)
    {
        bool startAnimation = true;

        if (higherPrio == null)
        {
            StartAnimation();
        }
        else
        {
            foreach (string animName in higherPrio)
            {
                if (animationBools[animName] == true)
                {
                    startAnimation = false;
                    break;
                }

            }
            if (startAnimation)
            {
                StartAnimation();
            }
        }

        void StartAnimation()
        {
            foreach (string animName in animationBools.Keys.ToList())
            {
                animationBools[animName] = false;
            }

            animationBools[animation] = true;
        }

    }
    public void OnAnimationDone(string animation)
    {
        //Debug.Log(animation);
        animationBools[animation] = false;
    }

    public bool IsAnimationActive(string animName)
    {
        return animationBools[animName];
    }

    public void ActivateLayer(int index)
    {
        for (int i = 0; i < animator.layerCount; i++)
        {
            animator.SetLayerWeight(i, 0);
        }


        animator.SetLayerWeight(index, 1);

    }


}
