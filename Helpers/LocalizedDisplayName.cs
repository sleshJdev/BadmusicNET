﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Reflection;

namespace MvcSimpleMusicSite_CourseProject.Helpers
{
    public class LocalizedDisplayName : DisplayNameAttribute
    {

        private PropertyInfo _nameProperty;
        private Type _resourceType;

        public LocalizedDisplayName(string displayNameKey)
            : base(displayNameKey)
        {

        }

        public Type NameResourceType
        {
            get
            {
                return _resourceType;
            }
            set
            {
                _resourceType = value;
                //инициализация nameProperty, когда тип свойства устанавливается set'ром
                _nameProperty = _resourceType.GetProperty(base.DisplayName, BindingFlags.Static | BindingFlags.Public);
            }
        }

        public override string DisplayName
        {
            get
            {
                //проверяет,nameProperty null и возвращает исходный значения отображаемого имени
                if (_nameProperty == null)
                {
                    return base.DisplayName;
                }

                return (string)_nameProperty.GetValue(_nameProperty.DeclaringType, null);
            }
        }

    }
}