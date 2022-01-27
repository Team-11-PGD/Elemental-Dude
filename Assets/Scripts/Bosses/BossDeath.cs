using UnityEngine;

public class BossDeath : FireBossState
{
    [SerializeField]
    GameObject fracturedModel;
    [SerializeField]
    Renderer renderer;
    [SerializeField]
    float explosionForce = 1f;
    [SerializeField]
    GameObject[] portals;

    public override void Enter(int previousStateId)
    {
        AudioManager.instance.StopSoundFromWorld(AudioManager.instance.AmbianceSounds, "BossMusic");
        AudioManager.instance.PlaySoundFromObject(AudioManager.instance.MonsterSounds, gameObject, "BossDeath");

        // Spawn death model
        renderer.enabled = false;
        GameObject instance = Instantiate(fracturedModel, context.transform.position, context.transform.rotation, context.transform);
        foreach (Rigidbody rigidbody in instance.GetComponentsInChildren<Rigidbody>())
        {
            rigidbody.AddForce(Vector3.up * explosionForce);
        }
        foreach (Renderer childRenderer in instance.GetComponentsInChildren<Renderer>())
        {
            childRenderer.material = renderer.material;
        }

        // Activate portals
        foreach (GameObject portal in portals)
        {
            portal.SetActive(true);
        }
    }
}
