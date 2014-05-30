using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kooboo.Common.ObjectContainer.MEF
{
    public class NotSupportedFeaturesException : NotSupportedException
    {
        const string MSG = @"避免小项目为了使用Kooboo.Common而引入比较复杂的第三方IoC容器，减少对第三方DLL的依赖。ObjectContainer默认实现了MEF的适配器，作为简单的对象容器。MEF的限制，以下功能在MEF里面没有被支持：
            IResolvingObserver,InThreadScope/InRequestScope生命周期,泛型注册,InjectProperties,params Parameter[] parameters,AddComponentInstance";
        public NotSupportedFeaturesException() :
            base(MSG)
        {

        }
    }
}
