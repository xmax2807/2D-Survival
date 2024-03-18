using System;
using System.Linq;

namespace Project.Utils.SerializeType{
    public static class TypeExtension{
        /// <summary>
        /// Check if type is inherited from baseType
        /// </summary>
        /// <param name="type">need check type</param>
        /// <param name="baseType"></param>
        /// <returns></returns>
        public static bool IsInheritedOrImplementedFrom(this Type type, Type baseType){
            type = ResolveGenericType(type);
            baseType = ResolveGenericType(baseType);

            while(type != typeof(object)){
                if(baseType == type || type.IsImpletemenedFrom(baseType)){
                    return true;
                }

                type = ResolveGenericType(type.BaseType);

                if(type == null){
                    return false;
                }
            }

            return false;
        } 
        
        static Type ResolveGenericType(Type type){
            if(type is not {IsGenericType: true}){
                return type;
            }

            Type genericType = type.GetGenericTypeDefinition();
            return genericType != type ? genericType : type;
        }

        static bool IsImpletemenedFrom(this Type type, Type interfaceType){
            return type.GetInterfaces().Any(i => ResolveGenericType(i) == interfaceType);
        }
    }
}