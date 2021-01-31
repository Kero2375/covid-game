using UnityEngine;

public class GameManagerSchiva : GameManager {

    public AudioClip coinSound;
    public AudioClip damageSound;
    public AudioClip jumpSound;

    override public void Start() {
        base.Start();  
    }
    override public void Update() {
        base.Update();
    }


    public void playCoinTaken(){
        AudioSource.PlayClipAtPoint(coinSound, GameObject.Find("Main Camera").transform.position,0.1F);
    }

    public void playDamageTaken(){
        AudioSource.PlayClipAtPoint(damageSound, GameObject.Find("Main Camera").transform.position,0.5F);
    }

    public void playJumpDone(){
        AudioSource.PlayClipAtPoint(jumpSound, GameObject.Find("Main Camera").transform.position, 0.5F);
    }
}