using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PTC.DataAccessLayer.EDM;
using System.Collections.Specialized;
using System.Data.Entity;
namespace PTC.ViewModelLayer
{
  public  class ProductViewModel
    {

        public List<Product> dataCollection { get; set; }
        public string Message { get; set; }
        public Product productData { get; set; }
        public int productId{ get; set; }
        public string Event { get; set; }

        public void Init()
        {
            dataCollection = new List<Product>();
            Message = string.Empty;

        }

        public ProductViewModel() :base()
        {
            Init();

        }
        protected void addProduct()
        {
            try
            {
                using (var db  = new mvvmpattrenEntities())
                {
                    db.Products.Add(productData);
                    db.SaveChanges();
                }

            }
            catch(Exception ex)
            {
                Publish(ex, "Something Went Wrong while Adding Product data");
            }
           
        }

        public void Publish(Exception ex, string message)
        {
            Publish(ex, message, null);
        }
        public void Publish(Exception ex, string message,
                            NameValueCollection nvc)
        {
            // Update view model properties
            Message = message;
            // TODO: Publish exception here	
        }

        protected void BuildCollection()
        {
            try
            {
                mvvmpattrenEntities dc = new mvvmpattrenEntities();
                dataCollection = dc.Products.ToList();
            }
            catch(Exception ex)
            {
                Publish(ex, "Error While Loading Product");
            }

        }
        protected void  EditProduct()
        {
            try
            {
                using(var db = new mvvmpattrenEntities())
                {
                    productData = db.Products.Find(productId);
                }
            }
            catch (Exception ex)
            {

                Publish(ex, "Error While Finding Product");
            }

        }
        protected void UpdateProduct()
        {
            try
            {
                using(var db  = new mvvmpattrenEntities())
                {
                    db.Entry(productData).State = EntityState.Modified;
                    db.SaveChanges();
                }

            }
            catch(Exception ex)
            {

                Publish(ex, "Error While Updating Product");
            }

        }

       public void HandleRequest(string eventname)
        {
            switch (eventname)
            {
                case "Get":
                    BuildCollection();
                    break;
                case "Add":
                    addProduct();
                    break;
                case "Edit":
                    EditProduct();
                    break;
                case "Update":
                    UpdateProduct();
                    break;
                     

                default:
                    break;
                    

            }


           

        }
        public void HandlePostRequest()
        {
            addProduct();
        }
       
    }
}
