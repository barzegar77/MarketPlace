namespace ConsoleApp
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }

        public int SellerId { get; set; }
    }

    public class Seller
    {
        public int SellerId { get; set; }
        public string DisplayName { get; set; }
    }

    public class ProductServices
    {
        static List<Seller> Sellers = new List<Seller>
        {
            new Seller
            {
                SellerId = 1,
                DisplayName = "Abolfazl"
            },
            new Seller
            {
                SellerId = 2,
                DisplayName = "Ali"
            },
            new Seller
            {
                SellerId = 3,
                DisplayName = "Sara"
            },
        };

        static List<Product> Products = new List<Product>
        {
            new Product
            {
                ProductId = 1,
                Price =  124.999,
                Title = "Galaxy S23 Ultra 5G",
                Category = "Samsung",
                SellerId = 1
            },
            new Product
            {
                ProductId = 2,
                Price =  22.999,
                Title = "POCO X5 Pro",
                Category = "Xiaomi",
                SellerId = 3
            },
            new Product
            {
                ProductId = 3,
                Price = 56.999,
                Title = "OnePlus 11",
                Category = "OnePlus",
                SellerId = 2
            },
            new Product
            {
                ProductId = 4,
                Price =  29.989,
                Title = "OPPO Reno 8 5G",
                Category = "OPPO",
                SellerId = 2
            },
            new Product
            {
                ProductId = 5,
                Price =  18.999,
                Title = "OnePlus Nord CE 2 Lite 5G",
                Category = "OnePlus",
                SellerId = 2
            },
            new Product
            {
                ProductId = 6,
                Price = 24.980,
                Title = "Redmi Note 12 Pro 5G",
                Category = "Xiaomi",
                SellerId = 1
            },
            new Product
            {
                ProductId = 7,
                Price =  29.999,
                Title = "Galaxy S20 FE 5G",
                Category = "Samsung",
                SellerId = 3
            },
            new Product
            {
                ProductId = 8,
                Price =  61.499,
                Title = "iPhone 13",
                Category = "Apple",
                SellerId = 1
            },
            new Product
            {
                ProductId = 9,
                Price =  132.990,
                Title = "iPhone 14 Pro Max",
                Category = "Apple",
                SellerId = 1
            },
            new Product
            {
                ProductId = 10,
                Price =  15.999,
                Title = "Galaxy M33 5G",
                Category = "Samsung",
                SellerId = 3
            },

        };




        #region Filter (محدود کردن)

        public static List<Product> FilterProductsByMinPrice(int minPrice)
        {
            return Products.Where(x => x.Price >= minPrice).ToList();
        }

        //todo
        public static List<Product> FilterProductsByMaxPrice(int maxPrice)
        {
            return null;
        }

        public static IEnumerable<Product> SearchProductsByTitle(string searchString)
        {
            return Products.Where(x => x.Title.Contains(searchString));
        }

        //todo
        public static List<Product> SearchProductsByCategory(string searchString)
        {
            return null;
        }

        #endregion




        #region Order By (مرتب سازی)

        public static List<Product> SortProductsPriceAscending()
        {
            return Products.OrderBy(x => x.Price).ToList();
        }

        // todo (OrderByDescending)
        public static List<Product> SortProductsPriceDescending()
        {
            return null;
        }

        //how to use two order by (then by)
        public static List<Product> SortProductsWithCategoryAndPrice()
        {
            return Products.OrderBy(x => x.Category).ThenBy(x => x.Price).ToList();
        }

        #endregion






        #region Group by (گروه بندی)

        public static IEnumerable<IEnumerable<Product>> GroupProductsByCategory()
        {
            return Products.GroupBy(x => x.Category).ToList();
        }


        #endregion




        #region Join (ادغام کردن)

        // Inner Join
        public static IEnumerable<string> GetInnerSellersProducts()
        {
            var res = Sellers.Join(Products, s => s.SellerId, p => p.SellerId, (s, p) =>
            new
            {
                s.DisplayName,
                p.Title,
                p.Price
            }
            );

            List<string> outPut = new List<string>();

            foreach (var item in res)
            {
                outPut.Add($"{item.DisplayName} : {item.Title}");
            }

            return outPut;
        }


        //Group Join

        public static IEnumerable<string> GetGroupSellersProducts()
        {
            var res = Sellers.GroupJoin(Products, s => s.SellerId, p => p.SellerId, (s, p) =>
            new
            {
                s.DisplayName,
                products = p,
            }
            );

            List<string> outPut = new List<string>();

            foreach (var item in res)
            {
                outPut.Add($"{item.DisplayName} : {item.products.Count()}");
            }

            return outPut;
        }


        #endregion




        // skip & take

        public static List<Product> Pagination(int pageIndex = 0)
        {
            int pageCount = Products.Count() / 3;

            var res = Products.OrderBy(x => x.ProductId).Skip(pageCount * pageIndex).Take(3).ToList();

            return res;

        }


        //todo 
        // متدی بنویسید که محصولات را بر اساس عنوان و دسته فیلتر کند و همینطور 4 محصول را در هر صفحه نمایش دهد  
        // صفحه بندی الزامیست





        // Aggregate Functions
        public static string GetProductsAggregate()
        {
            int count = Products.Count();
            double min = Products.Min(x => x.Price);
            double max = Products.Max(x => x.Price);
            double sum = Products.Sum(x => x.Price);
            double average = Products.Average(x => x.Price);

            return $"Count:{count} , Min:{min} , max:{max} , sum:{sum} : average:{average}";
        }





    }
}
