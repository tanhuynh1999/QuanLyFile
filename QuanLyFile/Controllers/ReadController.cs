using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.DAO;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace QuanLyFile.Controllers
{
    public class ReadController : Controller
    {
        DATAOCREntities db = new DATAOCREntities();
        // GET: Read
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(int ?id)
        {
            Session["key"] = null;
            return View(db.FileMains.Find(id));
        }
        public ActionResult TestAnh()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TestAnh(HttpPostedFileBase file)
        {
            var code = Guid.NewGuid().ToString();
            var fileimg = Path.GetFileName(file.FileName);

            var pa = Path.Combine(Server.MapPath("~/IMG/Img"), code + fileimg);


           


            return RedirectToAction("TestAnh");
        }
        public bool SaveResizeImage(Image img, string path, int xm0, int ym0, int xm1, int ym1)
        {
            try
            {

                // toa do -- tu lay roi gan vao
                int x0 = xm0 - 5; // toa do cat + tang do rong them 5
                int y0 = ym0 - 5;
                int x1 = xm1 + 5;
                int y1 = ym1 + 5;


                // lấy chiều rộng và chiều cao ban đầu của ảnh
                int originalW = img.Width;
                int originalH = img.Height;

                RectangleF srcRect = new RectangleF(x0, y0, x1 - x0, y1 - y0);
                GraphicsUnit units = GraphicsUnit.Pixel;


                Bitmap b = new Bitmap(originalW, originalH);
                Graphics g = Graphics.FromImage((Image)b);
                g.InterpolationMode = InterpolationMode.Bicubic;    // Specify here
                g.DrawImage(img, x0, y0, srcRect, units);
                g.Dispose();
                b.Save(path);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        //Loading
        public ActionResult Loading()
        {
            var code = Session["key"];
            FileMain file = db.FileMains.SingleOrDefault(n => n.file_key == code.ToString());

            return View(db.FileMains.Find(file.file_id));
        }
        //Hàm lưu item
        public JsonResult CreateItem(string[] price, string [] confidence, int[] x0, int[] y0, int[] x1, int[] y1)
        {
            var code = Session["key"];
            FileMain file = db.FileMains.SingleOrDefault(n => n.file_key == code.ToString());

            Image img = Image.FromFile(Request.MapPath("~/IMG/img/" + file.file_img));
            

            for (var i = 0; i < price.Length - 1; i++)
            {
                var codekey = Guid.NewGuid().ToString();
                var pa = Path.Combine(Server.MapPath("~/IMG/Img"), codekey + ".png");
                SaveResizeImage(img, pa, x0[i], y0[i], x1[i], y1[i]);
                ItemMain itemMain = new ItemMain
                {
                    item_mvo = price[i],
                    item_pro = float.Parse(confidence[i]),
                    item_datecreate = DateTime.Now,
                    file_id = file.file_id,
                    item_x0 = x0[i],
                    item_y0 = y0[i],
                    item_x1 = x1[i],
                    item_y1 = y1[i],
                    table_id = 1,
                    item_watched = false,
                    notSee = false,
                    item_mvi = price[i],
                    item_img = codekey + ".png"
                };
                db.ItemMains.Add(itemMain);
            }
            db.SaveChanges();
            return Json(null);
        }
        //Đọc file
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostSubmit(HttpPostedFileBase file, FileMain fileMain)
        {
            if (file == null)
            {
                TempData["check"] = "Vui lòng bỏ văn bản vào để tiến hành";
                return Redirect("/");
            }
            else
            {

                var code = Guid.NewGuid().ToString();

                var fileimg = Path.GetFileName(file.FileName);
                var pa = Path.Combine(Server.MapPath("~/IMG/Img"), code + fileimg);

                file.SaveAs(pa);

                fileMain.file_key = code;
                fileMain.file_status = 1;
                fileMain.file_datecreate = DateTime.Now;
                fileMain.file_img = code + file.FileName;


                var dao = new FilesDAO();
                var status = dao.create(fileMain);
                switch (status)
                {
                    case 1:
                        Session["key"] = code;
                        return RedirectToAction("Loading");
                    default:
                        return Redirect("/");
                }
            }
        }
        public JsonResult IndexDetails(int ? id)
        {
            var list = from item in db.ItemMains
                                  where item.file_id == id
                                  select new
                                  {
                                      target = item.item_name,
                                      code = item.item_code,
                                      V1 = item.item_mvi,
                                      V0 = item.item_mvo,
                                      color = item.item_pro,
                                      watched = item.item_watched,
                                      notSee = item.notSee,
                                      filecir = item.FileMain.file_circular,
                                      form = item.FileMain.file_form,
                                      table = item.table_id,
                                      img = "/IMG/img/" + item.item_img
                                  };
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}