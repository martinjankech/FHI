using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Xml.Linq;

namespace skusam_xml
{
    

    public class CustomJsonWriter : JsonTextWriter
    {
        public CustomJsonWriter(TextWriter writer) : base(writer) { }

        public override void WritePropertyName(string name)
        {
            if (name.StartsWith("@") || name.StartsWith("#"))
            {
                base.WritePropertyName(name.Substring(1));
            }
            else
            {
                base.WritePropertyName(name);
            }
        }
    }
}