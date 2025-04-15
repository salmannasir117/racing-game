using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitten_Spawner : MonoBehaviour
{
    private Transform kit_parent;
    public GameObject kit;

    // Start is called before the first frame update
    void Start()
    {
        kit.transform.localScale = 15 * (new Vector3(1,1,1));
        kit_parent = gameObject.transform;

        NewKitten(0, new Vector3(48f, 0.2f, 91f));
    }

    private void NewKitten(int id, Vector3 pos)
    {
        GameObject kitten = Instantiate(kit, pos, Quaternion.identity, kit_parent);
        kitten.name = "Kitty" + id.ToString();
        print(kitten.name + " spawned");
    }
}
