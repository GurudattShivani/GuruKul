﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.UI;
using Gurukul.Business;
using Gurukul.DAL;
using Gurukul.Web.Infrastructure;
using Gurukul.Web.ViewModels;
using Gurukul.Web.ViewModels.Student;
using StageDocs.DAL.Abstract;
using WebGrease.Css.Extensions;
using System.Linq.Dynamic;


namespace Gurukul.Web.Controllers
{

    public class StudentsController : GurukulBaseController
    {
        public StudentsController(IUoW uow)
            : base(uow)
        {
        }


        /*
   * TODO:
   * 1. Include the view in side a  panel
   * 2. Use bootstrap tables with the correct admin lte styles with paging.
   * 3. Edit/Details/Delete should be shown as buttons with proper icons
   * 4. Search functionality should be there (AJAX), with paging support
   * */
        //[OutputCache(Duration = 60, Location = OutputCacheLocation.Client)]
        public ActionResult Index()
        {
            var students = this.UoW.Get<Student>(); ;

            students = students.OrderBy(x => x.FirstName);

            return View(students);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var repository = this.GetRepository<Student>();
            if (repository == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }


            var student = repository.GetAll().Where(x => x.Id == id).Include(x => x.Enrollments.Select(y => y.Course)).FirstOrDefault();
            if (student == null)
            {
                return HttpNotFound();
            }

            var viewModel = student.ToViewModel();
            viewModel.Enrollments = student.Enrollments.Select(x => x.ToViewModel());

            return View(viewModel);
        }

        public ActionResult Create()
        {
            var viewModel = new StudentViewModel
            {
                EnrolledDate = DateTime.Now
            };
            return View("Edit", viewModel);
        }

        //
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,EnrolledDate")] Student student)
        {
            try
            {
                var repository = this.GetRepository<Student>();
                if (repository == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }

                UoW.Commit(() =>
                {
                    repository.Add(student);
                });

                return RedirectToAction("Index");

            }
            catch (Exception exception)
            {


            }

            return View(student);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var repository = this.GetRepository<Student>();
            if (repository == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            var student = repository.GetById(id.Value);

            if (student == null)
            {
                return HttpNotFound();
            }

            var viewModel = student.ToViewModel();
            return View(viewModel);
        }

        //
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var repository = this.GetRepository<Student>();
            if (repository == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            var studentToUpdate = repository.GetById(id.Value);

            if (TryUpdateModel(studentToUpdate, new[] { "FirstName", "LastName", "EnrolledDate" }))
            {
                try
                {
                    this.UoW.Commit();

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("Update failed, try again", exception);

                }
            }

            return View(studentToUpdate);

        }

        public ActionResult Delete(int? id, bool showError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (showError)
            {
                ViewBag.ErrorMessage = "Delete failed, try again";
            }

            var repository = this.GetRepository<Student>();
            if (repository == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            Student student = repository.GetById(id.Value);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                //
                // This way of deleting the student will involve sending 2 queries to the database (one to fetch, and one to delete the entity)
                //
                //Student student = db.Students.Find(id);
                //db.Students.Remove(student);

                var repository = this.GetRepository<Student>();
                if (repository == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound);
                }

                UoW.Commit(() =>
                {
                    repository.Delete(id);
                });

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                //return RedirectToAction("Delete", new { id, showError = true });
            }

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetStudents(DataTableInfo dataTableInfo, string search = "")
        {
            if (dataTableInfo == null)
            {
                return Json(new
                {
                    draw = 0,
                    recordsTotal = 0,
                    recordsFiltered = 0,
                    data = new object[0],
                    error = "Incorrect request"
                }, JsonRequestBehavior.AllowGet);
            }

            var allStudents = this.UoW.Get<Student>();

            if (allStudents == null)
            {
                return Json(new
                {
                    draw = dataTableInfo.Draw,
                    recordsTotal = 0,
                    recordsFiltered = 0,
                    data = new object[0],
                    error = "There are no students"
                }, JsonRequestBehavior.AllowGet);
            }
            
            var filter = string.IsNullOrEmpty(search) ? dataTableInfo.Search : search;

            var filteredResults = dataTableInfo.ToDataSource(allStudents, student => student.FirstName.Contains(filter) || student.LastName.Contains(filter));
            
            return Json(new
            {
                draw = dataTableInfo.Draw,
                recordsTotal = allStudents.Count(),
                recordsFiltered = filteredResults.Count(),
                data = filteredResults

            }, JsonRequestBehavior.AllowGet);
        }


        //
        // Cache the search results in the client side
        //
        //[OutputCache(Duration = 60, VaryByParam = "search", Location = OutputCacheLocation.Client)]
        //public ActionResult SearchStudents(string search, int page = 1)
        //{
        //    IQueryable<Student> students = null;

        //    if (string.IsNullOrEmpty(search))
        //    {
        //        students = this.UoW.Get<Student>();
        //    }
        //    else
        //    {
        //        students = this.UoW.Get<Student>(x => x.FirstName.Contains(search) ||
        //                                              x.LastName.Contains(search));
        //    }

        //    page = page <= 0 ? 1 : page;

        //    var totalStudents = students.Count();


        //    var viewModel = new StudentListViewModel
        //    {
        //        Search = search,
        //        Students = students.OrderBy(x => x.FirstName).Skip((page - 1) * this.PageSize).Take(this.PageSize).ToList(),
        //        TotalPages = (totalStudents / this.PageSize) == 0 ? 1 : totalStudents / this.PageSize
        //    };

        //    return PartialView("_studentList", viewModel.Students);
        //}
    }

    public class OrderExpression<T> where T : class
    {
        public ColumnSortOrder ColumnOrder { get; set; }
        public Func<T, object> Func { get; set; }
    }
}
