using System;
using System.Collections.Generic;

namespace Extend.DTAnimation.Editor
{
    public class CombineTemplate
    {
        public CombineType combineType = CombineType.Concerrence;
        public string editorName = "";
        public List<Type> animations = new List<Type>();
    }

    public class BaseTemplate
    {
        public List<CombineTemplate> combines = new List<CombineTemplate>();
    }
}