using System.Collections.Generic;
using System.Web.Mvc;
using NCommons.Rules;
using RulesEngineExample.Domain;
using RulesEngineExample.Models;

namespace RulesEngineExample.Controllers
{
    public class ProductController : Controller
    {
        readonly IProductRepository _productRepository;
        readonly IRulesEngine _rulesEngine;

        public ProductController(IRulesEngine rulesEngine, IProductRepository productRepository)
        {
            _rulesEngine = rulesEngine;
            _productRepository = productRepository;
        }

        public ActionResult Index(ProductInput productInput)
        {
            IEnumerable<Product> products = _productRepository.GetAll();
            return View(products);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductInput productInput)
        {
            ProcessResult results = _rulesEngine.Process(productInput);

            if (!results.Successful)
            {
                CopyValidationErrors(results);
                return View(productInput);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            _productRepository.Delete(id);
            return RedirectToAction("Index");
        }

        void CopyValidationErrors(ProcessResult results)
        {
            foreach (RuleValidationFailure failure in results.ValidationFailures)
            {
                ModelState.AddModelError(failure.PropertyName, failure.Message);
            }
        }
    }
}