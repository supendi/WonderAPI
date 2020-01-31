using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WonderAPI.Pkg
{
    /// <summary>
    /// Base interface for repository, so that every concrete class which implements it must implement dispose method
    /// </summary>
    public interface IRepository : IDisposable
    {
    }
}
