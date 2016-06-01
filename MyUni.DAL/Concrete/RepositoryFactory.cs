﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using Gurukul.Business;
using Gurukul.DAL.Abstract;
using Ninject;
using StageDocs.DAL.Abstract;

namespace Gurukul.DAL.Concrete
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly DbContext context;

        [Inject]
        public Dictionary<Type, object> CustomRepositoriesMappedByType { get; set; }

        public RepositoryFactory(DbContext context)
        {
            this.context = context;
            Debug.WriteLine("RepositoryFactory created...");
        }

        public void SetCustomRepo<T>(IRepository<T> repository) where T : class, IModel
        {
            CustomRepositoriesMappedByType = CustomRepositoriesMappedByType ?? new Dictionary<Type, object>();

            var specializedType = typeof (T);
            if (CustomRepositoriesMappedByType.ContainsKey(specializedType))
            {
                CustomRepositoriesMappedByType[specializedType] = repository;
            }
            else
            {
                CustomRepositoriesMappedByType.Add(specializedType, repository);
            }
        }

        public IRepository<T> GetRepository<T>() where T : class, IModel
        {
            //
            // Check whether this is a special repository
            //
            if (this.CustomRepositoriesMappedByType != null)
            {
                object repository;
                if (this.CustomRepositoriesMappedByType.TryGetValue(typeof (T), out repository))
                {
                    return (IRepository<T>)repository;
                }
            }

            return new GenericRepository<T>(this.context);
        }
    }
}