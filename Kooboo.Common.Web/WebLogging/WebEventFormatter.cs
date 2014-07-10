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
    public class WebEventFormatter
    {
        #region Fields
        // Fields
        private int _level = 0;
        private StringBuilder _sb = new StringBuilder();
        private int _tabSize = 4;
        #endregion

        #region .ctor

        // Methods
        internal WebEventFormatter()
        {
        }

        #endregion

        #region Methods

        private void AddTab()
        {
            for (int i = this._level; i > 0; i--)
            {
                this._sb.Append(' ', this._tabSize);
            }
        }

        public void AppendLine(string s)
        {
            this.AddTab();
            this._sb.Append(s);
            this._sb.Append('\n');
        }

        public override string ToString()
        {
            return this._sb.ToString();
        }
        #endregion

        #region Properties
        // Properties
        public int IndentationLevel
        {
            get
            {
                return this._level;
            }
            set
            {
                this._level = Math.Max(value, 0);
            }
        }

        public int TabSize
        {
            get
            {
                return this._tabSize;
            }
            set
            {
                this._tabSize = Math.Max(value, 0);
            }
        }
        #endregion
    }
}
