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

namespace Kooboo.Common
{
    public interface ITimeZoneHelper
    {
        TimeZoneInfo GetCurrentTimeZone();

        TimeZoneInfo FindTimeZoneById(string id);

        IEnumerable<TimeZoneInfo> GetTimeZones();

        DateTime ConvertToUtcTime(DateTime dt);

        DateTime ConvertToLocalTime(DateTime dt, TimeZoneInfo sourceTimeZone);

    }
}
