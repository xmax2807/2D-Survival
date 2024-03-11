using System;

namespace Project.Utils.SerializeType
{
    public class TypeFilterAttribute : UnityEngine.PropertyAttribute {
        public Func<Type, bool> Filter { get; }

        public TypeFilterAttribute(Type filterType) {
            Filter = type => !type.IsAbstract && 
                            !type.IsInterface &&
                            !type.IsGenericType &&
                            type.IsInheritedOrImplementedFrom(filterType);
        }
    }
}