
using UnityEngine;
[System.Serializable]

public class CameraRig : MonoBehaviour
{

    /*
    Triggers Assignment - Camera Rig script the causes smooth interpolation based on player movement, referenced from the tutorials. Also handles cutscenes.

    Josh Bellyk - 100526009
    Owen Meier  - 100538643    
    */

    public Transform target;

    public float positionSpeed = 5.0f;
    public float rotationSpeed = 5.0f;

    public Camera mainCam;
    public Vector4[] CutscenePositions;
    public Vector3[] CutsceneRotations;
    public PlayerController player;

    private int cutsceneTrig;
    private float deltaT = 0.0f;
    private Vector3 pos;
    private Quaternion rot;
    private int cCounter;

    public Animator Ccage;
    public ParticleSystem pt;
    public string triggerName;

    void Start()
    {
        cCounter = 0;
        pt.Stop();
    }

    void FixedUpdate()
    {
        if (player.score == player.scoreTrigger && cutsceneTrig == 0)
        {
            cutsceneTrig = 1;
            player.plrControl = false;
            pt.Play();
        }

        if (cutsceneTrig == 1)
        {
            Ccage.SetTrigger(triggerName);

            if (CutscenePositions.Length != CutsceneRotations.Length)
            {
                print("Array size mismatch, make sure you have the same number of rotations set up as your do for positions");
            }

            if (deltaT < CutscenePositions[cCounter].w)
            {
                deltaT += Time.deltaTime;
                pos = new Vector3(CutscenePositions[cCounter].x, CutscenePositions[cCounter].y, CutscenePositions[cCounter].z);
                rot = new Quaternion(CutsceneRotations[cCounter].x, CutsceneRotations[cCounter].y, CutsceneRotations[cCounter].z, 1.0f);

                transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime / CutscenePositions[cCounter].w);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime / CutscenePositions[cCounter].w);
            }
            else
            {
                cCounter++;
                deltaT = 0.0f;
            }

            if(cCounter == CutscenePositions.Length)
            {
                cCounter = 0;
                deltaT = 0.0f;
                cutsceneTrig = 2;
                player.plrControl = true;

                Vector3 defaultPos = new Vector3(player.transform.position.x, player.transform.position.y + 2, player.transform.position.z - 2);
                transform.position = defaultPos;
                transform.rotation = new Quaternion(0, 0, 0, 1);
            }

        }

        if (cutsceneTrig == 0 || cutsceneTrig == 2)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * positionSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime * rotationSpeed);
        }
    }
}