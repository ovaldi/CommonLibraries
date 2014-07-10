#region License
// 
// Copyright (c) 2013, Kooboo team
// 
// Licensed under the BSD License
// See the file LICENSE.txt for details.
// 
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Common.Web.WebLogging
{
    internal class WebEventFieldData
    {
        // Fields
        private string _data;
        private string _name;
        private WebEventFieldType _type;

        // Methods
        public WebEventFieldData(string name, string data, WebEventFieldType type)
        {
            this._name = name;
            this._data = data;
            this._type = type;
        }

        // Properties
        public string Data { get { return _data; } }
        public string Name { get { return _name; } }
        public WebEventFieldType Type { get { return _type; } }
    }
}
