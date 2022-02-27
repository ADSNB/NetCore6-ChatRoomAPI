using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Repository.UnitOfWork
{
    public static class UnitOfWorkExtension
    {
        public static void InitializeRepository(this IUnitOfWork unitOfWork, DbContext dbContext)
        {
            foreach (var property in unitOfWork.GetType().GetProperties())
            {
                try
                {
                    var implementation = Assembly.GetExecutingAssembly()
                        .DefinedTypes
                        .FirstOrDefault(p => property.PropertyType.IsAssignableFrom(p) && !p.IsInterface);
                    if (implementation != null)
                    {
                        var type = unitOfWork.GetType();

                        var backingField = type
                          .GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                          .FirstOrDefault(field =>
                            field.Attributes.HasFlag(FieldAttributes.Private) &&
                            field.Attributes.HasFlag(FieldAttributes.InitOnly) &&
                            field.CustomAttributes.Any(attr => attr.AttributeType == typeof(CompilerGeneratedAttribute)) &&
                            (field.DeclaringType == property.DeclaringType) &&
                            field.FieldType.IsAssignableFrom(property.PropertyType) &&
                            field.Name.StartsWith($"<{property.Name}>")
                          );
                        backingField.SetValue(unitOfWork, Activator.CreateInstance(implementation, new object[] { dbContext }));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Erro ao criar a instância da propriedade {property.Name}", ex);
                }
            }
        }
    }
}