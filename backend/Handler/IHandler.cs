using System;
using System.Collections.Generic;
using System.Text;

namespace Handler
{
    public interface IHandler<TRequest, TResponse>
    {
        TResponse Execute(TRequest request);
    }
}
