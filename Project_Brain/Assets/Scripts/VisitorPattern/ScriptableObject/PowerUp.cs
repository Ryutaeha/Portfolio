using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp", menuName = "PowerUp")]
public class PowerUp : ScriptableObject, IVisitor
{
    #region Fields
    // 파워업의 이름을 저장합니다.
    public string powerupName;

    // 파워업의 프리팹을 저장합니다.
    public GameObject powerupPrefab;

    // 파워업의 설명을 저장합니다.
    public string powerupDescription;

    // 이 값이 참이면, 캐릭터의 실드를 완전히 회복시킵니다.
    [Tooltip("Fully heal shield")]
    public bool healShield;

    // 캐릭터의 속도를 증가시킬 값을 설정합니다. 0에서 50 사이의 값이어야 합니다.
    [Range(0f, 50f)]
    [Tooltip("Speed up setting up to 200")]
    public float Boost;

    // 무기의 사거리를 증가시킬 값을 설정합니다. 0에서 25 사이의 값이어야 합니다.
    [Range(0f, 25f)]
    [Tooltip("Weapon range in increments of up to 25 units")]
    public int weaponRange;

    // 무기의 공격력을 증가시킬 값을 설정합니다. 0에서 50 사이의 값이어야 합니다.
    [Range(0f, 50f)]
    [Tooltip("Weapon strength in increments of up to 50%")]
    public float weaponStrength;
    #endregion

    #region Methods
    // CharacterShield 객체를 방문하여 실드의 체력을 회복합니다.
    public void Visit(CharacterShield characterShield)
    {
        if (healShield) characterShield.health = 100f;
    }

    // CharacterSpeed 객체를 방문하여 속도를 증가시킵니다.
    public void Visit(CharacterSpeed characterSpeed)
    {
        float speed = characterSpeed.Boost += Boost;

        if (speed < 0f) characterSpeed.Boost = 0.0f;

        if (speed >= characterSpeed.maxBoost) characterSpeed.Boost = characterSpeed.maxBoost;
    }

    // CharacterWeapon 객체를 방문하여 무기의 사거리와 공격력을 증가시킵니다.
    public void Visit(CharacterWeapon characterWeapon)
    {
        int range = characterWeapon.range += weaponRange;

        if (range >= characterWeapon.maxRange) characterWeapon.range = characterWeapon.maxRange;
        else characterWeapon.range = range;

        float strength = characterWeapon.strength += Mathf.Round(characterWeapon.strength * weaponStrength / 100);

        if (strength >= characterWeapon.maxStrength) characterWeapon.strength = characterWeapon.maxStrength;
        else characterWeapon.strength = strength;
    }
    #endregion
}
