using System.Collections.Generic;

namespace P.ObjectMapping
{
    public class SimpleMapperTest
    {
        public static void Test1()
        {
            Source source = new Source
            {
                Foos = new List<Foo>()
                {
                    new Foo {
                        Id =1,
                        Name ="foo1" ,
                        SubFoos =new List<FooSub>() {
                            new FooSub
                            {
                                SubId=1,
                                SubName="foo1,subFoo1"
                            },
                             new FooSub
                            {
                                SubId=2,
                                SubName="foo1,subFoo2"
                            }
                        }
                    },
                     new Foo {
                        Id =2,
                        Name ="foo2" ,
                        SubFoos =new List<FooSub>() {
                            new FooSub
                            {
                                SubId=1,
                                SubName="foo2,subFoo1"
                            },
                             new FooSub
                            {
                                SubId=2,
                                SubName="foo2,subFoo2"
                            }
                        }
                    },
                },
                Strings = new string[] { "a", "b" },
                Ints = new int[] { 1, 2, 3 }
            };

            Destination destination = source.As<Destination>();

        }

        public static void Test2()
        {


            ComplexSource<FooSub, FooSub> cs = new ComplexSource<FooSub, FooSub>
            {
                T1Prop = new FooSub { SubId = 1, SubName = "fooSubName" },
                T2Prop = new FooSub { SubId = 2, SubName = "fooSubName" }
            };
            ComplexDestination<FooSub2, FooSub2> cd = cs.As<ComplexDestination<FooSub2, FooSub2>>();

            //bu da oluyo merak etme
            //ComplexSource<Source, Source> cs = new ComplexSource<Source,Source>();
            //cs.T1Prop = source;
            //cs.T2Prop = source;
            //ComplexDestination<Destination, Destination> cd = cs.As<ComplexDestination<Destination, Destination>>();
        }
    }

    public class Source
    {
        public ICollection<Foo> Foos { get; set; }
        public string[] Strings { get; set; }
        public int[] Ints { get; set; }
    }

    public class Destination
    {
        public ICollection<Foo> Foos { get; set; }
        public string[] Strings { get; set; }
        public int[] Ints { get; set; }
    }

    public class Foo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<FooSub> SubFoos { get; set; }

    }

    public class FooSub
    {
        public int SubId { get; set; }
        public string SubName { get; set; }

    }

    public class FooSub2
    {
        public int SubId { get; set; }
        public string SubName { get; set; }

    }

    public class ComplexSource<T1, T2>
    {
        public T1 T1Prop { get; set; }
        public T2 T2Prop { get; set; }
    }

    public class ComplexDestination<T1, T2>
    {
        public T1 T1Prop { get; set; }
        public T2 T2Prop { get; set; }
    }

}
