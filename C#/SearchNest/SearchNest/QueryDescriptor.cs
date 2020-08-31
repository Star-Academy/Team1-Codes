using System;
using System.Collections.Generic;
using Nest;
using SearchNest.Model;

namespace SearchNest
{
    public class QueryDescriptor
    {
        public List<Func<QueryContainerDescriptor<Document>, QueryContainer>> MustFuncList;
        public List<Func<QueryContainerDescriptor<Document>, QueryContainer>> MustNotFuncList;
        public List<Func<QueryContainerDescriptor<Document>, QueryContainer>> ShouldFuncList;
    }
}