using System.Reflection;

namespace TemplateCA.SharedKernel.Extensions;

public static class AssemblyExtensions
{
    public static List<TypeInfo> GetTypesAssignableTo(this Assembly assembly, Type compareType)
    {
        List<TypeInfo>? typeInfoList = assembly.DefinedTypes.Where(x => x.IsClass
                                                                        && !x.IsAbstract
                                                                        && x != compareType
                                                                        && x.GetInterfaces()
                                                                            .Any(i => i.IsGenericType
                                                                                && i.GetGenericTypeDefinition() ==
                                                                                compareType)).ToList();

        return typeInfoList ?? throw new InvalidProgramException();
    }
}