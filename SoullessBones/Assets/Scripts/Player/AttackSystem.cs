using System.Collections;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{

    public GameObject player;
    //for prefabs
    public GameObject SlashLeft;
    public GameObject SlashRight;
    public GameObject SlashUpRight;
    public GameObject SlashUpLeft;

    private GameObject Slash;
    private GameObject LastObject;
    public Transform pointOfSlash;
    public Transform pointOfSlashUp;

    private bool isSlashRight = true;
    public bool CanAttack = true;
    public bool onWall = false;
    public float cooldown;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && CanAttack && !onWall)
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (!GetComponent<MovementController>().facingRight && isSlashRight)
                {
                    isSlashRight = false;
                }
                else if (GetComponent<MovementController>().facingRight && !isSlashRight)
                {
                    isSlashRight = true;
                }
                Slash = isSlashRight ? SlashUpRight : SlashUpLeft;
                LastObject = Instantiate(Slash, pointOfSlashUp.position, pointOfSlash.rotation);
            }
            else
            {
                if (!GetComponent<MovementController>().facingRight && isSlashRight)
                {
                    isSlashRight = false;
                }
                else if (GetComponent<MovementController>().facingRight && !isSlashRight)
                {
                    isSlashRight = true;
                }
                Slash = isSlashRight ? SlashRight : SlashLeft;
                LastObject = Instantiate(Slash, pointOfSlash.position, pointOfSlash.rotation);
            }
            LastObject.transform.SetParent(pointOfSlash);
            CanAttack = false;
            StartCoroutine(DeleteSlash());
        }
    }

    private IEnumerator DeleteSlash()
    {
        yield return new WaitForSeconds(0.05f);
        if (LastObject != null)
            Destroy(LastObject);
        yield return new WaitForSeconds(cooldown);
        CanAttack = true;
    }

    public void PreDelete()
    {
        if (!(LastObject == null  && (LastObject == SlashLeft || LastObject == SlashRight)))
            Destroy(LastObject);
    }
}
