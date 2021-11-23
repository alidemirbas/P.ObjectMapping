using System;
using System.Collections.Generic;
using System.Text;

namespace P.ObjectMapping
{
    public static class ObjectExtension
    {
        public static TReturn As<TReturn>(this object value)
            where TReturn : class
        {
            IMapper mapper = new SimpleMapper();//new AutoMapperAdapter();

            return mapper.Map(value, typeof(TReturn)) as TReturn;
        }
    }
}
