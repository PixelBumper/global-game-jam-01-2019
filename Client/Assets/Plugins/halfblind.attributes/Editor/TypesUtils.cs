using System.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace HalfBlind.Attributes
{
    public static class TypesUtils
    {
        public static Type[] GetAllSubtypesOf(Type tType, params string[] assemblyNames)
        {
            List<Assembly> assemblies = new List<Assembly>();
            if (assemblyNames.Length <= 0)
            {
                assemblies.AddRange(AppDomain.CurrentDomain.GetAssemblies());
            }
            else
            {
                for (int i = 0; i < assemblyNames.Length; i++)
                {
                    assemblies.Add(Assembly.Load(new AssemblyName(assemblyNames[i])));
                }
            }

            return (from assembly in assemblies
                    from type in assembly.GetTypes()
                    where type.IsSubclassOf(tType)
                    select type).ToArray();
        }

        public static Type[] GetAllTypesImplementing(Type tType, params string[] assemblyNames)
        {
            List<Assembly> assemblies = new List<Assembly>();
            if (assemblyNames.Length <= 0)
            {
                assemblies.AddRange(AppDomain.CurrentDomain.GetAssemblies());
            }
            else
            {
                for (int i = 0; i < assemblyNames.Length; i++)
                {
                    assemblies.Add(Assembly.Load(new AssemblyName(assemblyNames[i])));
                }
            }

            return (from assembly in assemblies
                    from type in assembly.GetTypes()
                    where type.GetInterfaces().Contains(tType)
                    select type).ToArray();
        }

        public static Type[] GetAllNonAbstractSubtypesOf(Type tType, params string[] assemblyNames)
        {
            return (from t in GetAllSubtypesOf(tType, assemblyNames) where !t.IsAbstract select t).ToArray();
        }

        public static Type[] GetAllSubtypesOf<T>(params string[] assemblyNames) where T : class
        {
            return GetAllSubtypesOf(typeof(T), assemblyNames);
        }

        public static Type[] GetAllTypesImplementing<T>(params string[] assemblyNames) where T : class
        {
            return GetAllTypesImplementing(typeof(T), assemblyNames);
        }

        public static Type[] GetAllNonAbstractSubtypesOf<T>(params string[] assemblyNames) where T : class
        {
            return GetAllNonAbstractSubtypesOf(typeof(T), assemblyNames);
        }
    }
}
