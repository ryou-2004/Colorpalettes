using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource au;
    public AudioSource changeAu;
    public Animator ani;
    public AudioClip walkAu;
    public AudioClip runAu;
    
    private bool walk;
    private bool run;
    private bool push;
    private bool jump;
    private bool setWalk;
    private bool setRun;
    private bool setPush;
    
    void Update()
    {
        walk = ani.GetBool("walkOn");
        run = ani.GetBool("dashOn");
        push = ani.GetBool("pushOn");
        jump = ani.GetBool("jumpOn");
        if (walk && !setWalk&&!jump)
        {
            au.clip = walkAu;
            setWalk = true;
            setRun = false;
            au.Play();
        }
        else if (run && !setRun&&!jump)
        {
            au.clip = runAu;
            setRun = true;
            setWalk = false;
            au.Play();
        }
        else if (push&&!setPush)
        {

        }
        if (jump)
        {
            PlayerSeStop();
        }
        else if (!walk && !run && !push)
        {
            PlayerSeStop();
        }
    }
    private void PlayerSeStop()
    {
        au.Stop();
        au.clip = null;
        setWalk = setRun = setPush = false;
    }
    public void BackChangeSE()
    {
        changeAu.Play();
    }
}
