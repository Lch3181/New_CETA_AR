using UnityEngine;

public class PlayAnimationOnEnable : MonoBehaviour {

    public Animation anim;
    public BoxCollider baseCollider;
    public bool animationPlayed;

    private void Update()
    {
        if(Screen.orientation != ScreenOrientation.Portrait)
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }

        if(baseCollider.enabled && !animationPlayed)
        {
            anim.Play(PlayMode.StopAll);
            animationPlayed = true;
        }

        if(!baseCollider.enabled && animationPlayed)
        {
            animationPlayed = false;
        }
    }
}
