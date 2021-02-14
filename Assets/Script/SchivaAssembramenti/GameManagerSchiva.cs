using UnityEngine;

public class GameManagerSchiva : GameManager {

    public AudioClip jumpSound;

    override public void Start() {
        base.Start();  
    }
    override public void Update() {
        base.Update();
    }


    public void playJumpDone(){
        if (soundOn) {
            AudioSource.PlayClipAtPoint(jumpSound, GameObject.Find("Main Camera").transform.position, 1F);
        }
    }
}