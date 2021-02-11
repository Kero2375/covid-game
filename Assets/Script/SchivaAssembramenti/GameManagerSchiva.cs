using UnityEngine;

public class GameManagerSchiva : GameManager {

    public AudioClip coinSound;
    public AudioClip jumpSound;

    override public void Start() {
        base.Start();  
    }
    override public void Update() {
        base.Update();
    }


    public void playCoinTaken(){
        if (soundOn) {
            AudioSource.PlayClipAtPoint(coinSound, GameObject.Find("Main Camera").transform.position, 0.1F);
        }
    }


    public void playJumpDone(){
        if (soundOn) {
            AudioSource.PlayClipAtPoint(jumpSound, GameObject.Find("Main Camera").transform.position, 1F);
        }
    }
}