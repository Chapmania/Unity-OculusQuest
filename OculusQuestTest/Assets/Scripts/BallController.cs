using UnityEngine;

public class BallController : MonoBehaviour {
    public AudioSource source;

    void Start() {
        source = GetComponent<AudioSource>();
    }

    void Update() {
    }

    private void OnCollisionEnter(Collision other) {
        Debug.Log("Boing!");
        source.Play();
    }
}