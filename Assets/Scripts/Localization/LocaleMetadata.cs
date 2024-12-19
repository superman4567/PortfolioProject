using UnityEngine;
using UnityEngine.Localization.Metadata;

[System.Serializable]
[Metadata(AllowedTypes = MetadataType.Locale)]
public class LocaleMetadata : IMetadata
{
    public Sprite Flag; // Flag image for the locale
}