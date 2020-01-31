using System;

namespace WonderAPI.Pkg
{
    /// <summary>
    /// Base interface for repository, so that every concrete class which implements it must implement dispose method
    /// </summary>
    public interface IRepository : IDisposable
    {
    }
}
