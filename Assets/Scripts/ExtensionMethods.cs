using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class ExtensionMethods
    {
        public static void Scale(this Transform t, float f, bool relative = false) => t.localScale = relative ? t.localScale += new Vector3(f, f, f) : new Vector3(f, f, f);
    }
}