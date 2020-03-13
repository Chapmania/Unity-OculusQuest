using UnityEngine;

public class MapDisplay : MonoBehaviour {
    public Renderer textureRenderer;

    public void DrawTexture(Texture texture) {
        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }
}