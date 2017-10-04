#pragma strict
@script ExecuteInEditMode

	public var projectionScreen : GameObject;
	public var estimateViewFrustum : boolean = true;
	public var isDebugAssistanceRequired : boolean = true;

	public var rayColor : Color;
	
	function LateUpdate () {
	if(projectionScreen != null){
		var cam : Camera = camera;
		
		
		var pa : Vector3 = projectionScreen.transform.TransformPoint(Vector3(-5.0f,  0, -5.0f)); //lower left
		var pb : Vector3 = projectionScreen.transform.TransformPoint(Vector3( 5.0f,  0, -5.0f)); //lower right
		var pc : Vector3 = projectionScreen.transform.TransformPoint(Vector3(-5.0f,  0,  5.0f)); //upper left
		
	    var pe : Vector3 = transform.position;
      	var n : float = cam.nearClipPlane;
      	var f : float = cam.farClipPlane;
      	
      	var va : Vector3; // from pe to pa
      	var vb : Vector3; // from pe to pb
      	var vc : Vector3; // from pe to pc
      	var vr : Vector3; // right axis of screen
      	var vu : Vector3; // up axis of screen
      	var vn : Vector3; // normal vector of screen
 
      	var l : float; // distance to left screen edge
      	var r : float; // distance to right screen edge
      	var b : float; // distance to bottom screen edge
      	var t : float; // distance to top screen edge
      	var d : float; // distance from eye to screen
      	
      	if (isDebugAssistanceRequired){
      		Debug.DrawLine(transform.position, projectionScreen.transform.TransformPoint(Vector3(-5.0f,  0,  5.0f)), rayColor);
			Debug.DrawLine(transform.position, projectionScreen.transform.TransformPoint(Vector3( 5.0f,  0,  5.0f)), rayColor);
			Debug.DrawLine(transform.position, projectionScreen.transform.TransformPoint(Vector3( 5.0f,  0, -5.0f)), rayColor);
			Debug.DrawLine(transform.position, projectionScreen.transform.TransformPoint(Vector3(-5.0f,  0, -5.0f)), rayColor);
      	}
      	vr = pb - pa;
      	vu = pc - pa;
      	vr.Normalize();
      	vu.Normalize();
      	vn = -Vector3.Cross(vr, vu); 
      	vn.Normalize();
 
      	va = pa - pe;
      	vb = pb - pe;
      	vc = pc - pe;
 
      	d = -Vector3.Dot(va, vn);
      	l = Vector3.Dot(vr, va) * n / d;
      	r = Vector3.Dot(vr, vb) * n / d;
      	b = Vector3.Dot(vu, va) * n / d;
      	t = Vector3.Dot(vu, vc) * n / d;
	
		var m : Matrix4x4 = PerspectiveOffCenter(
			l, r, b, t,
			n, f );
			
	  var rm : Matrix4x4; // rotation matrix;
      rm[0,0] = vr.x; 
      rm[0,1] = vr.y; 
      rm[0,2] = vr.z; 
      rm[0,3] = 0.0;    
 
      rm[1,0] = vu.x; 
      rm[1,1] = vu.y; 
      rm[1,2] = vu.z; 
      rm[1,3] = 0.0;    
 
      rm[2,0] = vn.x; 
      rm[2,1] = vn.y; 
      rm[2,2] = vn.z; 
      rm[2,3] = 0.0;    
 
      rm[3,0] = 0.0;  
      rm[3,1] = 0.0;  
      rm[3,2] = 0.0;  
      rm[3,3] = 1.0;            
 
      var tm : Matrix4x4; // translation matrix;
      tm[0,0] = 1.0; 
      tm[0,1] = 0.0; 
      tm[0,2] = 0.0; 
      tm[0,3] = -pe.x;
 
      tm[1,0] = 0.0; 
      tm[1,1] = 1.0; 
      tm[1,2] = 0.0; 
      tm[1,3] = -pe.y;  
 
      tm[2,0] = 0.0; 
      tm[2,1] = 0.0; 
      tm[2,2] = 1.0; 
      tm[2,3] = -pe.z;  
 
      tm[3,0] = 0.0; 
      tm[3,1] = 0.0; 
      tm[3,2] = 0.0; 
      tm[3,3] = 1.0;
      
	  cam.projectionMatrix = m;
	  cam.worldToCameraMatrix = rm * tm;
	}
}
	static function PerspectiveOffCenter(
		left : float, right : float,
		bottom : float, top : float,
		near : float, far : float ) : Matrix4x4
	{        
		var x : float =  (2.0 * near) / (right - left);
		var y : float =  (2.0 * near) / (top - bottom);
		var a : float =  (right + left) / (right - left);
		var b : float =  (top + bottom) / (top - bottom);
		var c : float = -(far + near) / (far - near);
		var d : float = -(2.0 * far * near) / (far - near);
		var e : float = -1.0;
		var m : Matrix4x4 = new Matrix4x4();
		m[0,0] = x;  m[0,1] = 0;  m[0,2] = a;  m[0,3] = 0;
		m[1,0] = 0;  m[1,1] = y;  m[1,2] = b;  m[1,3] = 0;
		m[2,0] = 0;  m[2,1] = 0;  m[2,2] = c;  m[2,3] = d;
		m[3,0] = 0;  m[3,1] = 0;  m[3,2] = e;  m[3,3] = 0;
		return m;
	}