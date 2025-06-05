using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    public class VFXHandler : MonoBehaviour
    {
        [SerializeField] protected List<UnLoopVFXInObject> unLoopVFXsInObject;

        public VFXController GetAudio(VFXID id){
            foreach(var vfxInObject in unLoopVFXsInObject)
                if(vfxInObject.ID.Equals(id)) return vfxInObject.Controller;
            return null;
        }

        public VFXController PlayUnLoopVFX(VFXID id){
            if(unLoopVFXsInObject.Count == 0) return null;
            foreach (var vfxInObject in unLoopVFXsInObject)
            {
                if (!vfxInObject.ID.Equals(id)) continue;

                vfxInObject.Controller.gameObject.SetActive(true);
                return vfxInObject.Controller;
            }
            
            return null;
        }
    }
}