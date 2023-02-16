using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFade : MonoBehaviour
{
    public AnimationClip animationFade;
    public GameObject[] gameObjects;

    int objectVisible = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject go in gameObjects){
            go.AddComponent<Animation>();
            go.GetComponent<Animation>().wrapMode = WrapMode.Once;
            go.GetComponent<Animation>().AddClip(animationFade,"AnimationFade");
            go.GetComponent<Animation>()["AnimationFade"].time = animationFade.length;
            go.GetComponent<Animation>().AddClip(animationFade, "AnimationUnfade");
            go.GetComponent<Animation>()["AnimationUnfade"].normalizedTime = 1f;
            go.GetComponent<Animation>()["AnimationUnfade"].speed = -1f;
        }
    }

    public void ChangeVisibility(int n)
    {
        Debug.Log(n);

        gameObjects[objectVisible].GetComponent<Animation>().CrossFade("AnimationFade",1);


        gameObjects[n].GetComponent<Animation>().CrossFade("AnimationUnfade",2);

        objectVisible = n;
    }

}
