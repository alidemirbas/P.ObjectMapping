using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace P.ObjectMapping
{
    public class SimpleMapper : IMapper
    {
        private static readonly Type _ienumerable = typeof(IEnumerable);
        private static readonly Type _string = typeof(string);
        
        //todo iyilestir
        public object Map(object source, Type targetType)
        {
            if (source == null)
                return null;

            var sourceType = source.GetType();

            if (sourceType.IsValueType)//nullavfjsdkghkghdfjk;sghsdfk;hgdfjkghghghghfjk;ddgdjgdfhshfsdjkhgf
            {
                var underlyingType = Nullable.GetUnderlyingType(targetType);
                if (underlyingType != null)//demek ki nullable
                {
                    if (!underlyingType.IsValueType) //orn hata: source int, target datetine gibi
                        throw new Exception();//todo

                    targetType = underlyingType;
                }

                return Convert.ChangeType(source, targetType);
            }

            if (sourceType == _string)
            {
                if (targetType != _string)
                    throw new Exception();//todo

                return source;
            }

            if (_ienumerable.IsAssignableFrom(sourceType))
            {
                if (!_ienumerable.IsAssignableFrom(targetType))
                    throw new Exception();//todo

                IEnumerable srcCollection = (IEnumerable)source;
                IList targetCollection;

                if (targetType.IsArray)
                {
                    var elementType = targetType.GetElementType();
                    int i = 0;
                    var enumerator = srcCollection.GetEnumerator();

                    while (enumerator.MoveNext())
                        i++;

                    targetCollection = Array.CreateInstance(elementType, i);

                    i = 0;

                    foreach (var srcItem in srcCollection)
                    {
                        targetCollection[i++] = Map(srcItem, elementType);
                        //i++;
                    }
                }
                else
                {
                    var trgArgType = targetType.GetGenericArguments()[0];
                    var listType = typeof(List<>);
                    var trgListType = listType.MakeGenericType(trgArgType);
                    targetCollection = (IList)Activator.CreateInstance(trgListType);

                    foreach (var srcItem in srcCollection)
                    {
                        targetCollection.Add(Map(srcItem, trgArgType));
                    }
                }

                return targetCollection;
            }

            var target = Activator.CreateInstance(targetType);

            var targetProps = targetType.GetProperties();
            var sourceProps = sourceType.GetProperties();

            foreach (var srcProp in sourceProps)
            {
                foreach (var trgProp in targetProps)
                {
                    if (srcProp.Name == trgProp.Name)
                    {
                        try
                        {
                            // yanisi en basta verilen TargetType'in property'lerinin de property'lerinin type'lari (en bastaki a.nin icindeki b prop'un prop'larindan c olan property'sinin type'i)
                            var trgPropProperties = (trgProp.PropertyType != _string && _ienumerable.IsAssignableFrom(trgProp.PropertyType)) ? (trgProp.PropertyType.IsArray ? trgProp.PropertyType.GetElementType() : trgProp.PropertyType.GetGenericArguments()[0]).GetProperties() : trgProp.PropertyType.GetProperties();

                            var cyclingPropName = trgPropProperties.SingleOrDefault(x => x.Name == targetType.Name && x.PropertyType == targetType)?.Name;//yani cycling var mi?(a.nin icindeki b prop'un prop'larindan c olan property'sinin type'i a'ya esitse bu durum stackoverflow exp sebep olur

                            trgProp.SetValue(target, Map(srcProp.GetValue(source), trgProp.PropertyType));

                            break;
                        }
                        catch (Exception ex)
                        {

                            //todo nav prop'lardan sonsuzluğa dolayısıyla stackıverflıw ılıyo
                            //boyle gecici cozum
                            return target;
                        }
                    }

                }
            }

            return target;

        }
    }

}
