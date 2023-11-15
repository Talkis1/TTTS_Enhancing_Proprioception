//This file or parts of this code are base on the project
//
// https://github.com/j1930021/Stackable-Decorator
//
//MIT License
//
//Copyright (c) 2018 Kinwailo
//
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.
//
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.




using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HapticGUI
{
    public class NotNullAttribute : ValidateObjectAttribute
    {
#if UNITY_EDITOR
#endif
        public NotNullAttribute()
        {
#if UNITY_EDITOR
            m_Message = "%1 should not be none.";
#endif
        }

        public NotNullAttribute(string message)
        {
#if UNITY_EDITOR
            m_Message = message;
#endif
        }
#if UNITY_EDITOR
        protected override bool OnCheckObject(UnityEngine.Object obj)
        {
            return obj != null;
        }
#endif
    }
}