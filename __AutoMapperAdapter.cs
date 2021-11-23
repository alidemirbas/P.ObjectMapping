//using System;

//namespace P.ObjectMapping
//{
//    //note adapter tasarim deseni ornek
//    public class AutoMapperAdapter : IMapper
//    {
//        public object Map(object source, Type targetType)
//        {
//            AutoMapper.MapperConfiguration config = new AutoMapper.MapperConfiguration(cfg =>
//            {
//                cfg.CreateMissingTypeMaps = true;
//                // other configurations
//            });
//            AutoMapper.IMapper mapper = config.CreateMapper();

//            return mapper.Map(source,source.GetType(),targetType);
//        }
//    }
//}
