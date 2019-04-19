using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Sphaera.Web.Server.Helpers;

namespace Sphaera.Web.Services.Basics
{
    public abstract class SeviceBase<T> where T: class, new()
    {
        #region Private Fields

        private readonly TimeSpan _cacheTimeSpan = TimeSpan.FromSeconds(10d);
        private DateTime _dt;
        private SpinLock _spinlock = new SpinLock();

        [NotNull]
        private readonly Dictionary<object, T> _cache = new Dictionary<object, T>();

        #endregion

        #region Constructor

        protected SeviceBase([NotNull] IConfiguration config)
        {
            SvcUrl = string.Empty;

            var strInterval = config["LoadChanges_Interval"];
            if (int.TryParse(strInterval, out var interval))
                _cacheTimeSpan = TimeSpan.FromSeconds(interval);
        }

        protected SeviceBase(TimeSpan cacheTimeSpan)
        {
            SvcUrl = string.Empty;
            _cacheTimeSpan = cacheTimeSpan;
        }

        #endregion

        #region Protected Properties

        [NotNull]
        protected string SvcUrl { get; set; }

        #endregion 

        #region Protected Methods

        protected async Task<T[]> GetList(string url, Action<Dictionary<object, T>, T> setter)
        {
            var lockTaken = false;

            try
            {
                _spinlock.Enter(ref lockTaken);
                var dt = DateTime.Now;
                TimeSpan interval = dt - _dt;

                if (interval < _cacheTimeSpan)
                    return _cache.Values.ToArray();

                _dt = dt;
            }
            finally
            {
                if (lockTaken) _spinlock.Exit(false);
            }

            var proxy = new WebApiProxy(SvcUrl);
            var data = await proxy.GetResultAsync<T[]>(url);
            lockTaken = false;
            try
            {
                _spinlock.Enter(ref lockTaken);

                _cache.Clear();
                foreach (var obj in data)
                    setter(_cache, obj);

                return _cache.Values.ToArray();
            }
            finally
            {
                if (lockTaken) _spinlock.Exit(false);
            }
        }

        protected async Task<T> Get(object key)
        {
            var lockTaken = false;

            try
            {
                _spinlock.Enter(ref lockTaken);

                return await Task.FromResult(_cache.TryGetValue(key, out var obj) ? obj : null);
            }
            finally
            {
                if (lockTaken) _spinlock.Exit(false);
            }
        }

        protected async Task<T> Get(string url, object key)
        {
            var proxy = new WebApiProxy(SvcUrl);
            var obj = await proxy.GetResultAsync<T>(url);

            var lockTaken = false;

            try
            {
                _spinlock.Enter(ref lockTaken);

                _cache[key] = obj;

                return obj;
            }
            finally
            {
                if (lockTaken) _spinlock.Exit(false);
            }
        }

        protected async Task Update(string url, object key, T obj)
        {
            var proxy = new WebApiProxy(SvcUrl);
            await proxy.PutRequestAsync(url, obj);

            var lockTaken = false;

            try
            {
                _spinlock.Enter(ref lockTaken);

                _cache[key] = obj;
            }
            finally
            {
                if (lockTaken) _spinlock.Exit(false);
            }
        }

        protected async Task Delete(string url, object key)
        {
            var proxy = new WebApiProxy(SvcUrl);
            await proxy.DeleteRequestAsync(url);

            var lockTaken = false;

            try
            {
                _spinlock.Enter(ref lockTaken);

                _cache.Remove(key);
            }
            finally
            {
                if (lockTaken) _spinlock.Exit(false);
            }
        }

        #endregion
    }
}