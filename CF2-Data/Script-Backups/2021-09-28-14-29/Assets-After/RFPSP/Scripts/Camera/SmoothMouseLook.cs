//SmoothMouseLook.cs by Azuline Studios© All Rights Reserved
//Smoothes mouse input, manages angle limits, enables/unlocks cursor on pause, and compensates for non-recovering weapon recoil. 
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SmoothMouseLook : MonoBehaviour {
    public static SmoothMouseLook instance;
	private InputControl InputComponent;
	private GameObject playerObj;
	private FPSPlayer FPSPlayerComponent;

	[Tooltip("Mouse look sensitivity/camera move speed.")]
    public float sensitivity = 4.0f;
	[HideInInspector]
	public float sensitivityAmt = 4.0f;//actual sensitivity modified by IronSights Script

    private float minimumX = -360.0f;
    private float maximumX = 360.0f;

	[Tooltip("Minumum pitch of camera for mouselook.")]
	public float minimumY = -85f;
	[Tooltip("Maximum pitch of camera for mouselook.")]
	public float maximumY = 85f;
	[HideInInspector]
    public float rotationX = 0.0f;
	[HideInInspector]
    public float rotationY = 0.0f;
	[HideInInspector]
	public Quaternion xQuaternion;
	[HideInInspector]   
	public Quaternion yQuaternion;
	[HideInInspector]
    public float inputY = 0.0f;
	[HideInInspector]
	public float horizontalDelta;
   
	[Tooltip("Smooth speed of camera angles for mouse look.")]
	public float smoothSpeed = 0.35f;
	[HideInInspector]
	public float playerMovedTime;
	
	[HideInInspector]
	public Quaternion originalRotation;
	[HideInInspector]
	public Transform myTransform;
	[HideInInspector]
	public float recoilX;//non recovering recoil amount managed by WeaponKick function of WeaponBehavior.cs
	[HideInInspector]
	public float recoilY;//non recovering recoil amount managed by WeaponKick function of WeaponBehavior.cs

	[HideInInspector]
	public bool dzAiming;
	[Tooltip("Reverse vertical input for mouselook.")]
	public bool invertVerticalLook;
	[HideInInspector]
	public bool thirdPerson;
	[HideInInspector]
	public bool tpIdleCamRotate;
    public Rect upperTouchArea = new Rect(0, 0.4f, 1, 0.6f);
    public Rect middleTouchArea = new Rect(0.35f, 0, 0.5f, 1f);
    public Rect lowerTouchArea = new Rect(0.35f, 0, 0.5f, 1f);
    private Vector2 touch;
    private int fingerId = -1;
    public bool canLookAround = true;

   // public Text myText;

    void Start(){
        instance = this;
		playerObj = Camera.main.transform.GetComponent<CameraControl>().playerObj;
		InputComponent = playerObj.GetComponent<InputControl>();
		FPSPlayerComponent = playerObj.GetComponent<FPSPlayer>();


        upperTouchArea = new Rect(upperTouchArea.x * Screen.width, upperTouchArea.y * Screen.height, upperTouchArea.width * Screen.width, upperTouchArea.height * Screen.height);
        middleTouchArea = new Rect(middleTouchArea.x * Screen.width, middleTouchArea.y * Screen.height, middleTouchArea.width * Screen.width, middleTouchArea.height * Screen.height);
        lowerTouchArea = new Rect(lowerTouchArea.x * Screen.width, lowerTouchArea.y * Screen.height, lowerTouchArea.width * Screen.width, lowerTouchArea.height * Screen.height);
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }

       // if (GetComponent<Rigidbody>()){GetComponent<Rigidbody>().freezeRotation = true;}
		
		myTransform = transform;//cache transform for efficiency
		
		//sync the initial rotation of the main camera to the y rotation set in editor
		originalRotation = Quaternion.Euler(myTransform.parent.transform.eulerAngles.x, myTransform.parent.transform.eulerAngles.y, 0.0f);
		
		sensitivityAmt = sensitivity;//initialize sensitivity amount from var set by player
		
		//Hide the cursor
		//Cursor.visible = false;
    }

	#if UNITY_EDITOR || UNITY_WEBPLAYER
	void OnGUI(){//lock cursor - don't use OnGUI in standalone for performance reasons
		if(Time.timeScale > 0.0f && FPSPlayerComponent.pauseHidesCursor){
			ControlFreak2.CFCursor.lockState = CursorLockMode.Locked;
			//Cursor.visible = false;
		}
	}
	#endif

    void Update(){


        //if(sensitivityAmt < sensitivity)
        //{
        //    sensitivityAmt = Mathf.MoveTowards(sensitivityAmt, sensitivity, Time.deltaTime * 2);
        //}

        //if(Time.timeScale > 0.0f && Time.smoothDeltaTime > 0.0f){//allow pausing by setting timescale to 0

        //	//Hide the cursor
        //	//Cursor.lockState = CursorLockMode.Locked;
        //	//Cursor.visible = false;

        ////	horizontalDelta = rotationX;//old rotationX

        //	// Read the mouse input axis
        //	if(!dzAiming){
        //		rotationX += InputComponent.lookX * sensitivityAmt * Time.timeScale;//lower sensitivity at slower time settings
        //		if(!invertVerticalLook){
        //			rotationY += InputComponent.lookY * sensitivityAmt * Time.timeScale;
        //		}else{
        //			rotationY -= InputComponent.lookY * sensitivityAmt * Time.timeScale;
        //		}
        //	}

        //	//reset vertical recoilY value if it would exceed maximumY amount 
        //	//if(maximumY - InputComponent.lookY * sensitivityAmt * Time.timeScale < recoilY){
        //	//	rotationY += recoilY;
        //	//	recoilY = 0.0f;	
        //	//}
        //	////reset horizontal recoilX value if it would exceed maximumX amount 
        //	//if(maximumX - InputComponent.lookX * sensitivityAmt * Time.timeScale < recoilX){
        //	//	rotationX += recoilX;
        //	//	recoilX = 0.0f;	
        //	//}

        //	rotationX = ClampAngle (rotationX, minimumX, maximumX);
        //	rotationY = ClampAngle (rotationY, minimumY - recoilY, maximumY - recoilY);

        //	inputY = rotationY + recoilY;//set public inputY value for use in other scripts

        //	xQuaternion = Quaternion.AngleAxis (rotationX + recoilX, Vector3.up);
        //    yQuaternion = Quaternion.AngleAxis (rotationY + recoilY, -Vector3.right);

        //	horizontalDelta = Mathf.DeltaAngle(horizontalDelta, rotationX);

        //	if(!thirdPerson){
        //		if(playerMovedTime + 0.1f < Time.time){
        //			//smooth the mouse input
        //			myTransform.rotation = Quaternion.Slerp(myTransform.rotation , originalRotation * xQuaternion * yQuaternion, smoothSpeed * Time.smoothDeltaTime * 60.0f / Time.timeScale);
        //		}else{
        //			myTransform.rotation = originalRotation * xQuaternion * yQuaternion;//snap camera instantly to angles with no smoothing
        //		}
        //		//lock mouselook roll to prevent gun rotating with fast mouse movements
        //		myTransform.rotation = Quaternion.Euler(myTransform.rotation.eulerAngles.x, myTransform.rotation.eulerAngles.y, 0.0f);
        //	}

        //}else{
        //	//Show the cursor
        //	Cursor.lockState = CursorLockMode.None;
        //	Cursor.visible = true;
        //}

        if (Time.timeScale > 0.1f && canLookAround)
        {
            //sensitivityAmt = sensitivityAmt * (Screen.width / Screen.height);
            //guiText.text = "Screen : "+ Screen.width+" / "+ Screen.height +", Sensitivity = "+sensitivity;
            //guiText.pixelOffset = new Vector2 (Screen.width * 0.6f, Screen.height * 0.075f);
            // Hide the cursor
            //Screen.lockCursor = true;
#if UNITY_EDITOR
            float xRot = ControlFreak2.CF2Input.GetAxisRaw("Mouse X") * 10f, yRot = ControlFreak2.CF2Input.GetAxisRaw("Mouse Y") * 10f;
            if (ControlFreak2.CF2Input.GetKey(KeyCode.Mouse0))
            {
                xRot = 0;
                yRot = 0;
            }
#elif UNITY_ANDROID
//      Vector2 v = rot1.OnTouchDirection (false);
//      if(v == Vector2.zero){
//          v = rot2.OnTouchDirection (false);
//      }
//      float xRot = v.x, yRot = v.y;
//      v = Vector2.zero;
        
        float xRot = 0, yRot = 0;
        foreach (ControlFreak2.InputRig.Touch t in ControlFreak2.CF2Input.touches) {
            if(upperTouchArea.Contains(t.position))
            {
                if(t.phase == TouchPhase.Began){
                      //  myText.text = "began1";
                        //   sensitivityAmt = 0f;
                        //   smoothSpeed = 0f;
                        xRot = yRot = 0;
                        if (fingerId == -1 || fingerId >= 0){
                        touch= t.position; 
                        fingerId = t.fingerId;
                         //   
                    }
                }
                else if(fingerId == t.fingerId && t.phase == TouchPhase.Moved){
                    Vector2 dir = t.position;
                    Vector2 tmp = dir;
                    dir = dir - touch;
                    touch = tmp;
                    xRot = dir.x;
                    yRot = dir.y;
                     //   myText.text = "moved1";
                        //   sensitivityAmt = sensitivity;
                        //   smoothSpeed = 0.2f;
                    }
            }
            else if(lowerTouchArea.Contains(t.position))
            {
                if(t.phase == TouchPhase.Began){
                        //  sensitivityAmt = 0f;
                        //  smoothSpeed = 0f;
                       // myText.text = "began2";
                        xRot = yRot = 0;
                        if (fingerId == -1 || fingerId >= 0){
                        touch= t.position;
                        fingerId = t.fingerId;
                           // 
                    }
                }
                else if(fingerId == t.fingerId && t.phase == TouchPhase.Moved){
                    Vector2 dir = t.position;
                    Vector2 tmp = dir;
                    dir = dir - touch;
                    touch = tmp;
                    xRot = dir.x;
                    yRot = dir.y;
                       // myText.text = "moved2";
                        //  sensitivityAmt = sensitivity;
                        //  smoothSpeed = 0.2f;
                    }
            }
            else if(middleTouchArea.Contains(t.position))
            {
                if(t.phase == TouchPhase.Began){
                        //   sensitivityAmt = 0f;
                        //  smoothSpeed = 0f;
                      //  myText.text = "began3";
                        xRot = yRot = 0;
                        if (fingerId == -1 || fingerId >= 0){
                        touch= t.position;
                        fingerId = t.fingerId;
                           // 
                        }
                }
                else if(fingerId == t.fingerId && t.phase == TouchPhase.Moved){
                    Vector2 dir = t.position;
                    Vector2 tmp = dir;
                    dir = dir - touch;
                    touch = tmp;
                    xRot = dir.x;
                    yRot = dir.y;
                       // myText.text = "moved3";
                        //  sensitivityAmt = sensitivity;
                        //  smoothSpeed = 0.2f;
                    }
            }
            if(fingerId == t.fingerId && (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)){
                touch = Vector2.zero;
                fingerId = -1;
                   // sensitivityAmt = 0f;
                }
        }
#endif
            //if (xRot == 0 && yRot == 0)
            //return;
            if (Time.timeSinceLevelLoad > 1 && Time.timeScale > 0.1f)
            {
                // Read the mouse input axis
               
                rotationX += xRot * sensitivityAmt * Time.timeScale;//lower sensitivity at slower time settings
                rotationY += yRot * sensitivityAmt * Time.timeScale;

                rotationX = ClampAngle(rotationX, minimumX, maximumX);
                rotationY = ClampAngle(rotationY, minimumY, maximumY);

                Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
                Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
                //smooth the mouse input
               // if (movePlayer.instance != null)
               // {
                 //   if (!movePlayer.instance.startMoving)
                 //   {
                  //      myTransform.rotation = Quaternion.Slerp(myTransform.rotation, originalRotation * xQuaternion * yQuaternion, smoothSpeed * Time.smoothDeltaTime * 30 / Time.timeScale);
                  //  }
               // }
               // else
               // {
                    myTransform.rotation = Quaternion.Slerp(myTransform.rotation, originalRotation * xQuaternion * yQuaternion, smoothSpeed * Time.smoothDeltaTime *500 / Time.timeScale);
               // }
            }
        }

    }

   
    public static float ClampAngle (float angle, float min, float max){
        angle = angle % 360;
        if((angle >= -360.0f) && (angle <= 360.0f)){
            if(angle < -360.0f){
                angle += 360.0f;
            }
            if(angle > 360.0f){
                angle -= 360.0f;
            }         
        }
        return Mathf.Clamp (angle, min, max);
    }
	
}