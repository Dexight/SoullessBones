using System.Collections;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{

    public GameObject player;
    private MovementController movementController;
    //Melee System
    //Slash prefabs
    public GameObject SlashLeft;
    public GameObject SlashRight;
    public GameObject SlashUpRight;
    public GameObject SlashUpLeft;

    private GameObject Slash;
    private GameObject LastObject;
    public Transform pointOfSlash;
    public Transform pointOfSlashUp;

    //Distance system
    //UNLOCK
    public bool distanceUnlock = false;//TODO
    //Tears prefabs
    public GameObject TearsLeft;
    public GameObject TearsRight;
    public GameObject TearsUp;
    public DistanceAttack BottleUI;

    private bool isSlashRight = true;
    public bool CanAttack;
    public bool CanThrow;
    public bool onWall = false;
    public bool inAstral = false;//In Astral
    public bool gameIsPaused = false; //In Pause Menu
    public float cooldown;

    private void Awake()
    {
        movementController= GetComponent<MovementController>();
        BottleUI = SceneLoader.instance.GetComponent<DistanceAttack>();
        CanAttack = true;
        CanThrow = true;
    }

    void Update()
    {
        //Slash
        if (Input.GetMouseButtonDown(0) && CanAttack && !onWall && !gameIsPaused && !inAstral)
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (!movementController.facingRight && isSlashRight)
                {
                    isSlashRight = false;
                }
                else if (movementController.facingRight && !isSlashRight)
                {
                    isSlashRight = true;
                }
                Slash = isSlashRight ? SlashUpRight : SlashUpLeft;
                LastObject = Instantiate(Slash, pointOfSlashUp.position, pointOfSlash.rotation);
            }
            else
            {
                if (!movementController.facingRight && isSlashRight)
                {
                    isSlashRight = false;
                }
                else if (movementController.facingRight && !isSlashRight)
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

        //Tears
        if (Input.GetMouseButton(1) && CanThrow && !onWall && !gameIsPaused && !inAstral)
        {
            if (!Input.GetKey(KeyCode.W))
            {
                if(movementController.facingRight)
                    Instantiate(TearsRight, player.transform.position, player.transform.rotation);
                else Instantiate(TearsLeft, player.transform.position, player.transform.rotation);
            }
            else
            {
                Instantiate(TearsUp, player.transform.position, player.transform.rotation);
            }
            StartCoroutine(ThrowCooldown());
            BottleUI.minusTears(10);
        }
    }

    private IEnumerator ThrowCooldown()
    {
        CanThrow = false;
        yield return new WaitForSeconds(0.4f);
        CanThrow = true;
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
        if (!(LastObject == null && (LastObject == SlashLeft || LastObject == SlashRight)))
            Destroy(LastObject);
    }
}
