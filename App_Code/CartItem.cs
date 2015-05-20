using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CartItem
/// </summary>
public class CartItem : IEquatable<CartItem>
{
    #region Properties

    // A place to store the quantity in the cart
    // This property has an implicit getter and setter.
    public int Quantity { get; set; }

    private int _productId;
    public int ProductId
    {
        get { return _productId; }
        set
        {
            // To ensure that the Prod object will be re-created
            _product = null;
            _productId = value;
        }
    }

    private Product _product = null;
    public Product Prod
    {
        get
        {
            //Object is created only when required
            if (_product == null)
            {
                _product = new Product(ProductId);
            }
            return _product;
        }
    }

    public string Code
    {
        get { return Prod.ProductCode; }
    }

    public decimal UnitPrice
    {
        get { return Prod.Price; }
    }

    public int Inventory
    {
        get { return Prod.Inventory; }
    }
    public decimal Price
    {
        get
        {
            decimal UP = 0;
            if (Prod.Pricing != null)
            {
                if (Prod.Pricing.Count > 1)
                {
                    foreach (KeyValuePair<string, decimal> p in Prod.Pricing)
                    {
                        string[] arrQtyLimit = Convert.ToString(p.Key).Split('-');
                        if (arrQtyLimit.Length > 1)
                        {
                            if (this.Quantity >= Convert.ToInt32(arrQtyLimit[0]) && this.Quantity <= Convert.ToInt32(arrQtyLimit[1]))
                            {
                                UP = Convert.ToDecimal(p.Value);
                                break;
                            }
                        }
                        else if (this.Quantity >= Convert.ToInt32(arrQtyLimit[0]))
                        {
                            UP = Convert.ToDecimal(p.Value);
                            break;
                        }
                    }
                }
                else
                {
                    UP = Prod.Price;
                }
            }
            else
                UP = Prod.Price;
            return UP;
        }
    }

    public decimal TotalPrice
    {
        get { return Price * Quantity; }
    }

    #endregion

    public CartItem(int productId)
    {
        this.ProductId = productId;
    }

    /**
     * Equals() - Needed to implement the IEquatable interface
     *    Tests whether or not this item is equal to the parameter
     *    This method is called by the Contains() method in the List class
     *    We used this Contains() method in the ShoppingCart AddItem() method
     */
    public bool Equals(CartItem item)
    {
        return item.ProductId == this.ProductId;
    }

    //public decimal ComputeUnitPrice()
    //{
    //    decimal UP = 0;
    //    if (Pricing.Count > 1)
    //    {
    //        foreach (KeyValuePair<string, decimal> p in Pricing)
    //        {
    //            string[] arrQtyLimit = Convert.ToString(p.Key).Split('|');
    //            if (this.Quantity >= Convert.ToInt32(arrQtyLimit[0]) && this.Quantity <= Convert.ToInt32(arrQtyLimit[1]))
    //            {
    //                UP = p.Value;
    //                break;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        UP = Prod.Price;
    //    }

    //    return UP;
    //}
}