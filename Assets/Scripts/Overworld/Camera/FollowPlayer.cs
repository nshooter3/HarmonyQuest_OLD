using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    public GameObject player;
    private PlayerMovementOverworld pMov;

    public enum CameraState { Default, Cutscene };
    public CameraState cameraState;

    GameObject boundHolder;
    BoxCollider2D cameraBounds;
    float xMin, xMax, yMin, yMax;
    float camWidth, camHeight;

    public bool follow = true;

    //How many pixels are in a Unity unit. used for clamping camera to pixels
    //Not currently being used
    float pixelsPerUnits = 32f;

    RaycastHit2D result;

	// Use this for initialization
	void Start () {
        camHeight = GetComponent<Camera>().orthographicSize;
        camWidth = camHeight * (800f/600f);

        boundHolder = GameObject.FindGameObjectWithTag("CameraBounds");
        if (boundHolder != null)
        {
            cameraBounds = boundHolder.GetComponent<BoxCollider2D>();
            if (cameraBounds != null)
            {
                xMax = boundHolder.transform.position.x + cameraBounds.size.x / 2f - camWidth;
                xMin = boundHolder.transform.position.x - cameraBounds.size.x / 2f + camWidth;
                yMax = boundHolder.transform.position.y + cameraBounds.size.y / 2f - camHeight;
                yMin = boundHolder.transform.position.y - cameraBounds.size.y / 2f + camHeight;
            }
        }
        cameraState = CameraState.Default;
        player = PlayerMovementOverworld.instance.gameObject;
        pMov = player.GetComponent<PlayerMovementOverworld>();
    }

    void Awake()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (follow)
        {
            if (cameraState == CameraState.Default)
            {
                CamFollowPlayer();
            }
            if (pMov.cameraDefaultFlag)
            {
                SetCameraDefault();
            }
            if (pMov.cameraCutsceneFlag)
            {
                SetCameraCutscene();
            }
        }
    }

    void CamFollowPlayer()
    {
        Vector3 v3 = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        v3.x = Mathf.Clamp(v3.x, xMin, xMax);
        v3.y = Mathf.Clamp(v3.y, yMin, yMax);
        transform.position = v3;
    }

    public void SetCameraDefault()
    {
        cameraState = CameraState.Default;
        pMov.cameraDefaultFlag = false;

    }

    public void SetCameraCutscene()
    {
        cameraState = CameraState.Cutscene;
        pMov.cameraCutsceneFlag = false;
    }

}
