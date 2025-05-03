using UnityEngine;

public interface IAnimateable
{
    public bool isAnimationTrue { get; set; }

    public void AnimationMode(bool turnOn = true)
    {
        isAnimationTrue = turnOn;
    }
}
