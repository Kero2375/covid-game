using UnityEngine;

public class GameManagerSchiva : GameManager {

    public AudioClip coinSound;
    override public void Start() {
        base.Start();  
    }
    override public void Update() {
        base.Update();
    }

    public void coinTaken() {
        AudioSource.PlayClipAtPoint(coinSound, GameObject.Find("Main Camera").transform.position);
    }
}