using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class fan_control : MonoBehaviour {
	
	public static SerialPort sp = new SerialPort ("COM9", 9600, Parity.None, 8, StopBits.One);
	private int once = 0;
	private string message;
	private float front=0f;
	private float right=0f;
	private float left=0f;
	private float orient;
	private float intensity;

	void Start () {
	}
	
	void Update()
	{
		if (once == 0) {
			if (sp.IsOpen)
				sp.Close ();
		    sp.Open ();  // opens the connection
			sp.ReadTimeout = 50;  // sets the timeout value before reporting error
			once = 1;
		}

		/* Fan Intensity */
		intensity=Mathf.Lerp (0, 100, Mathf.InverseLerp (-10, 10, Camera.main.transform.position.z));

		/* Front Fan */
		orient=Mathf.Cos(3.14159f / 180f * Camera.main.transform.eulerAngles.y);
		if (orient < 0)
		  orient = 0;
		front=Mathf.Lerp (98, 121, Mathf.InverseLerp (0, 100, orient*intensity));

		/* Left & Right Fans */
		orient=Mathf.Sin(3.14159f / 180f * Camera.main.transform.eulerAngles.y);
		if (orient < 0) {
			right=Mathf.Lerp (98, 121, Mathf.InverseLerp (0, 100, orient*intensity*-1f));
		    left = 98f;
		}
		else {
			left=Mathf.Lerp (98, 121, Mathf.InverseLerp (0, 100, orient*intensity));
			right=98f;
		}


		/* Send Message*/ 
		message="a"; //start message
		message += ((char)((int)front)).ToString ();//front fan
		message += ((char)((int)left)).ToString (); //left fan
		message += ((char)((int)right)).ToString (); //right fan
		message += "b"; //back fan
		message += "z"; //end message

		//Debug.Log (message);
		sp.Write (message);

	}

	void OnApplicationQuit() 
	{
		sp.Close();
	}
}

		/*
		pos_prev = pos;
		pos = Camera.main.transform.position;
		vel = pos - pos_prev;
		if (vel.z > 0)
						wind.w = vel.z * 8f;
				else
						wind.w = 0f; */

		/* Generate Debug Message*/
		//message = Camera.main.transform.eulerAngles.ToString ();
		//message += wind.ToString ();

		/* Generate Byte Message = {camera rotation, wind vector} */
		//Start Message
		//byteMessage = "s";
		 // message += byteMessage + ",";

		// Orientation Bytes
		/*singleByte=(int)Mathf.Lerp (0, 255, Mathf.InverseLerp (0, 360, Camera.main.transform.eulerAngles.x));
		  message += singleByte.ToString () + ",";
		  byteMessage += ((char) singleByte).ToString ();
		singleByte=(int)Mathf.Lerp (0, 255, Mathf.InverseLerp (0, 360, Camera.main.transform.eulerAngles.y));
		  message += singleByte.ToString () + ",";
		  byteMessage += ((char) singleByte).ToString ();
		singleByte=(int)Mathf.Lerp (0, 255, Mathf.InverseLerp (0, 360, Camera.main.transform.eulerAngles.z));
		  message += singleByte.ToString () + ",";
		  byteMessage += ((char) singleByte).ToString ();

		// Wind Bytes
		singleByte = (int)Mathf.Lerp (0, 255, Mathf.InverseLerp (0, 360, wind.x));
		  message += singleByte.ToString () + ",";
		  byteMessage += ((char) singleByte).ToString ();
		singleByte = (int)Mathf.Lerp (0, 255, Mathf.InverseLerp (0, 360, wind.y));
		  message += singleByte.ToString () + ",";
		  byteMessage += ((char) singleByte).ToString ();
		singleByte = (int)Mathf.Lerp (0, 255, Mathf.InverseLerp (0, 360, wind.z));
		  message += singleByte.ToString () + ",";
		  byteMessage += ((char) singleByte).ToString ();
		singleByte = (int)Mathf.Lerp (0, 255, Mathf.InverseLerp (0, 1, wind.w));
		  message += singleByte.ToString () + ",";
		  byteMessage += ((char) singleByte).ToString ();
	  
		//End Message
		byteMessage += "e";
		message += "e";

	
		if (frame10 < 5)
			frame10++;
		else {
			Debug.Log (message);
			sp.Write ("s       e");
			frame10=0;
			}
	}*/
	

