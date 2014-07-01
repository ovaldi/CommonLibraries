#region License
// 
// Copyright (c) 2013, Kooboo team
// 
// Licensed under the BSD License
// See the file LICENSE.txt for details.
// 
#endregion
using Kooboo.Common.Web.SelectList;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Kooboo.Common.Web.Metadata
{
    public class KoobooModelMetadata : DataAnnotationsModelMetadata
    {
        private string _description;

        public KoobooModelMetadata(DataAnnotationsModelMetadataProvider provider, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName, DisplayColumnAttribute displayColumnAttribute, IEnumerable<Attribute> attributes)
            : base(provider, containerType, modelAccessor, modelType, propertyName, displayColumnAttribute)
        {
            var descAttr = attributes.OfType<DescriptionAttribute>().SingleOrDefault();
            _description = descAttr != null ? descAttr.Description : "";

            DataSourceAttribute = attributes.OfType<DataSourceAttribute>().SingleOrDefault();

            var enumAttribute = attributes.OfType<EnumDataTypeAttribute>().SingleOrDefault();
            if (enumAttribute != null)
            {
                DataSource = new EnumTypeSelectListDataSource(enumAttribute.EnumType);
            }

            Attributes = attributes;

            var defaultValueAttr = attributes.OfType<DefaultValueAttribute>().SingleOrDefault();

            DefaultValue = defaultValueAttr != null ? defaultValueAttr.Value : this.ModelType.GetDefaultValue();

            this.AdditionalValues["DefaultValue"] = DefaultValue;
        }

        public virtual object DefaultValue { get; set; }

        // here's the really important part
        public override string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        public DataSourceAttribute DataSourceAttribute { get; private set; }

        public ISelectListDataSource DataSource { get; private set; }

        public IEnumerable<Attribute> Attributes { get; private set; }
    }
}
