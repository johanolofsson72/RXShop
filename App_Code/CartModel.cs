using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Runtime.Serialization;

/// <summary>
/// Contains the data for a cart.
/// </summary>
public class CartModel// : ISerializable
{
    /// <summary>
    /// Product to be stored in cart.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Stores data to access the data behind the corresponding product module. 
        /// </summary>
        public class ProductModuleData
        {
            private int _sitId;
            private int _pagId;
            private int _modId;

            public ProductModuleData(int sitId, int pagId, int modId)
            {
                _sitId = sitId;
                _pagId = pagId;
                _modId = modId;
            }

            public int SitId
            {
                get
                {
                    return _sitId;
                }
                set
                {
                    _sitId = value;
                }
            }

            public int PagId
            {
                get
                {
                    return _pagId;
                }
                set
                {
                    _pagId = value;
                }
            }

            public int ModId
            {
                get
                {
                    return _modId;
                }
                set
                {
                    _modId = value;
                }
            }
        }

        private String _productId;
        private String _name;
        private decimal _price;
        private decimal _vat;
        private int _quantity;
        private bool _isInStock;
        private String _choice;
        private ProductModuleData _moduleData;

        /*
         * Default constructor shouldn't be accessible from the outside.
         */
        private Product()
        {}

        public Product(String productId, String name, decimal price, decimal vat, int quantity, bool isInStock, String choice, ProductModuleData moduleDate)
        {
            _productId = productId;
            _name = name;
            _price = price;
            _vat = vat;
            _quantity = quantity;
            _isInStock = isInStock;
            _choice = choice;
            _moduleData = moduleDate;
        }

        public String ProductId
        {
            get
            {
                return _productId;
            }
            set
            {
                _productId = value;
            }
        }

        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public decimal Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
            }
        }

        public decimal Vat
        {
            get
            {
                return _vat;
            }
            set
            {
                _vat = value;
            }
        }

        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = value;
            }
        }

        public bool IsInStock
        {
            get
            {
                return _isInStock;
            }
            set
            {
                _isInStock = value;
            }
        }

        public String Choice
        {
            get
            {
                return _choice;
            }
            set
            {
                _choice = value;
            }
        }

        public ProductModuleData ModuleData
        {
            get
            {
                return _moduleData;
            }
            set
            {
                _moduleData = value;
            }
        }
    }

    private System.Collections.Generic.List<Product> _products;

    /*
     * Default constructor.
     */
    public CartModel()
    {
        _products = new System.Collections.Generic.List<Product>();
    }

    /*
     * Adds product to cart.
     * If it allready exists the quantity will be increased instead.
     */
    public void Add(Product product)
    {
        int pos = Find(product.Name);
        if (pos == -1)
        {
            _products.Add(product);
        }
        else
        {
            _products[pos].Quantity += product.Quantity;
        }
        //RXServer.Web.Cookie.WriteObjectToCookie(this, "cart");
    }

    /*
     * Adds product to cart.
     * If it allready exists the quantity will be increased instead.
     */
    public void Add(String productId, String name, decimal price, decimal vat, int quantity, bool isInStock, String choice, Product.ProductModuleData moduleData)
    {
        Add(new Product(productId, name, price, vat, quantity, isInStock, choice, moduleData));
    }

    /*
     * Delete item at supplied index.
     */
    public void DeleteAt(int index)
    {
        _products.RemoveAt(index);
        //RXServer.Web.Cookie.WriteObjectToCookie(this, "cart");
    }

    /*
     * Empty cart
     */
    public void EmptyCart()
    {
        _products.Clear();
        //RXServer.Web.Cookie.WriteObjectToCookie(this, "cart");
    }

    /*
     * Indexer that returns product at a certain index.
     * Returns null if the index is invalid.
     */
    public Product this[int index]
    {
        get
        {
            if (index > -1 && index < _products.Count)
            {
                return _products[index];
            }
            return null;
        }
    }

    /*
     * Returns the number of products in the cart.
     */
    public int Count()
    {
        return _products.Count;
    }

    /*
     * Returns index of product if names matches.
     * Returns -1 if the product isn't present.
     */
    private int Find(String prdName)
    {
        int position = 0;
        foreach (Product prd in _products)
        {
            if (prd.Name.Equals(prdName))
            {
                return position;
            }
            position++;
        }
        return -1;
    }

    /*
     * Returns the cart for the current session.
     */
    public static CartModel Current
    {
        get
        {
            if (HttpContext.Current.Session["Cart"] == null)
            {
                CartModel cart = null;//(CartModel)RXServer.Web.Cookie.ReadObjectFromCookie("cart");
                if (cart != null)
                {
                    HttpContext.Current.Session["Cart"] = cart;
                }
                else
                {
                    HttpContext.Current.Session["Cart"] = new CartModel();
                }

            }
            return (CartModel)HttpContext.Current.Session["Cart"];
        }
    }
}
