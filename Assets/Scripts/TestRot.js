#pragma strict

function Start () {
	MoveObject(this.transform, this.transform.position, Vector3(0,0,0), 0.5);
}

function Update () {

}


function MoveObject (thisTransform : Transform, startPos : Vector3, endPos : Vector3, time : float) {
    var i = 0.0;
    var rate = 1.0/time;
    while (i < 1.0) {
        i += Time.deltaTime * rate;
        thisTransform.position = Vector3.Lerp(startPos, endPos, i);
        yield; 
    }
}