using System;

namespace Platformer2D
{
    public enum VFXID
    {
        None = 0,
        Object_Death,
    }

    [Serializable]
    public struct UnLoopVFXInObject{
        public VFXID ID;
        public UnLoopVFXController Controller;
    }
}