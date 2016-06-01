﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gurukul.Business;
using Gurukul.DAL.Abstract;
using StageDocs.DAL.Abstract;

namespace Gurukul.Web.Controllers
{
    public class GurukulBaseController : Controller
    {
        /// <summary>
        /// Unit of work should not be able to be set from outside code. This will be set by via constructor DI.
        /// </summary>
        protected IUoW UoW { get; private set; }

        public GurukulBaseController(IUoW uow)
        {
            if (uow == null)
            {
                throw new ArgumentNullException("Unit of Work object cannot be null");
            }

            this.UoW = uow;
        }

        protected virtual IRepository<T> GetRepository<T>() where T : class,IModel
        {
            var repository = this.UoW.GetRepository<T>();
            return repository;
        }
    }
}