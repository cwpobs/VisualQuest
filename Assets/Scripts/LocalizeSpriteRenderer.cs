using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class LocalizeSpriteRenderer : LocalizedAssetEvent<Texture, LocalizedTexture, UnityEventTexture>
{
    private SpriteRenderer spriteRenderer;

    protected SpriteRenderer SpriteRenderer
    {
        get
        {
            if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
            return spriteRenderer;
        }
    }


    protected override void UpdateAsset(Texture localizedAsset)
    {
        base.UpdateAsset(localizedAsset);

        SpriteRenderer.sprite = Sprite.Create((Texture2D)localizedAsset,
            new Rect(0, 0, localizedAsset.width, localizedAsset.width), new Vector2(0.5f, 0.5f));
    }
}