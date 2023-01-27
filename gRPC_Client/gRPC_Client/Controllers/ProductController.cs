using Grpc.Net.Client;
using gRPC_Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static gRPC_Client.ProductSrv;

namespace gRPC_Client.Controllers
{
    public class ProductController : Controller
    {
        private readonly string GrpcChannelURL = "https://localhost:7189";
        // GET: ProductController
        public ActionResult Index()
        {
            using var channel = GrpcChannel.ForAddress(GrpcChannelURL);
            var client = new ProductSrvClient(channel);
            var products = client.GetAll(new Empty { });

            return View(products);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product)
        {
            try
            {
                using var channel = GrpcChannel.ForAddress(GrpcChannelURL);
                var client = new ProductSrvClient(channel);
                await client.PostAsync(product);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var channel = GrpcChannel.ForAddress(GrpcChannelURL);
            var client = new ProductSrvClient(channel);
            var product = await client.GetByIdAsync(new ProductRowIdFilter { ProductRowId = id });
            return View(product);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Product product)
        {
            try
            {
                var channel = GrpcChannel.ForAddress(GrpcChannelURL);
                var client = new ProductSrvClient(channel);
                await client.PutAsync(product);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var channel = GrpcChannel.ForAddress(GrpcChannelURL);
            var client = new ProductSrvClient(channel);
            var product = await client.GetByIdAsync(new ProductRowIdFilter { ProductRowId = id });
            return View(product);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var channel = GrpcChannel.ForAddress(GrpcChannelURL);
                var client = new ProductSrvClient(channel);
                client.DeleteAsync(new ProductRowIdFilter { ProductRowId = id });
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

