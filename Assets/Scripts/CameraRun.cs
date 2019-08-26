using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRun : MonoBehaviour
{
    public Camera cameraOrho;
    public Camera cameraIso;
    public float sensibilite = 0.1f;
    public float sensibiliteZoom = 1f;
    public float zoomMax = 10;
    public float zoomMin = 1;

    // Start is called before the first frame update
    void Start()
    {
        GameManage.DonnerInstance.CameraType = GameManage.CAMERA_TYPE.CAMERA_TYPE_ISO;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManage.DonnerInstance.Role == GameManage.ROLE.ROLE_AUCUN)
            return;

        if(GameManage.DonnerInstance.CameraType == GameManage.CAMERA_TYPE.CAMERA_TYPE_ISO)
        {
            float mvt = Input.GetAxis("Horizontal");
            if (mvt != 0)
                cameraIso.transform.Translate(new Vector3(mvt*sensibilite, 0, 0));

            mvt = Input.GetAxis("Vertical");
            if (mvt != 0)
                cameraIso.transform.Translate(new Vector3(0, mvt * sensibilite, 0));


            mvt = Input.GetAxis("Mouse ScrollWheel");
            if (mvt < 0)
            {
                if (zoomMax <= cameraIso.orthographicSize + mvt)
                    cameraIso.orthographicSize = zoomMax;
                else
                    cameraIso.orthographicSize += (-1 * (mvt * sensibiliteZoom));
            }
            else
            {
                if (zoomMin < cameraIso.orthographicSize - mvt)
                    cameraIso.orthographicSize += (-1 * (mvt * sensibiliteZoom));
                else
                    cameraIso.orthographicSize = zoomMin;
            }
        }
        else if(GameManage.DonnerInstance.CameraType == GameManage.CAMERA_TYPE.CAMERA_TYPE_ORTHO)
        {
            float mvt = Input.GetAxis("Horizontal");
            if (mvt != 0)
                cameraOrho.transform.Translate(new Vector3(mvt * sensibilite, 0, 0));

            mvt = Input.GetAxis("Vertical");
            if (mvt != 0)
                cameraOrho.transform.Translate(new Vector3(0, mvt * sensibilite, 0));

            mvt = Input.GetAxis("Mouse ScrollWheel");
            if (mvt < 0)
            {
                if (zoomMax <= cameraIso.orthographicSize + mvt)
                    cameraIso.orthographicSize = zoomMax;
                else
                    cameraIso.orthographicSize += (-1 * (mvt * sensibiliteZoom));
            }
            else
            {
                if (zoomMin < cameraIso.orthographicSize - mvt)
                    cameraIso.orthographicSize += (-1 * (mvt * sensibiliteZoom));
                else
                    cameraIso.orthographicSize = zoomMin;
            }
        }
    }
}
