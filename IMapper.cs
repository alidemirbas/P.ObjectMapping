using System;

namespace P.ObjectMapping
{
    public interface IMapper
    {
        object Map(object source, Type targetType);
    }
}
