using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
using SearchNest.Model;

namespace SearchNest
{
    public class QueryDescriptor
    {
        public List<Func<QueryContainerDescriptor<Document>, QueryContainer>> mustFuncList;
        public List<Func<QueryContainerDescriptor<Document>, QueryContainer>> mustNotFuncList;
        public List<Func<QueryContainerDescriptor<Document>, QueryContainer>> shouldFuncList;
    }
}