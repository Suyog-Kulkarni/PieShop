using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PieShop.Models
{
    public class PieRepository : IPieRepository
    {
        private readonly BethanysPieShopDbContext _context;

        public PieRepository(BethanysPieShopDbContext context)
        {
            _context = context;
        }
        
        public IEnumerable<Pie> AllPies
        {
            get
            {
                return _context.Pies.Include(p => p.Category);// inculde is used to include the related data from other table in the same query 
            }
        }
        public IEnumerable<Pie> PiesOfTheWeek
        {
            /*The most common extension methods are the LINQ standard query operators that add query functionality 
            to the existing System.Collections.IEnumerable and System.Collections.Generic.IEnumerable<T> types.
            Extension methods are defined as static methods*/
            get
            {
                /*var list = _context.Pies.Where(p => EF.Functions.Like(p.LongDescription, "cm")).ToList();*/
                /*return (IEnumerable<Pie>)_context.Categories.Where(p=>p.CategoryId == 1);*/

                return _context.Pies.Include(p => p.Category).Where(p => p.IsPieOfTheWeek /*&& p.InStock*/);
                // can also use && and || operator
               
            }
        }
        public Pie? GetPieById(int pieId)
        {
            
            return _context.Pies.FirstOrDefault(p => p.PieId == pieId);// firstordefault return first or null

            /*_context.Pies
             * .OrderByDescending(p => p.FirstName)
             * .FirstOrDefault(p => p.LastName == "some name"); // this return first occurrence of the name order in descinding base on their first name*/
        }
       /* public IEnumerable<Pie> mufun
        {
            get
            {
                var me = _context.Pies.Where(p => p.PieId == 5).ToList();
                return me;

                // where return Ienumerable<t> so return type must be Ienum. 
                // ForD return single value so return type can anything

            }
        }*/


        /*public Pie? Myfun()
        {
            return _context.Pies.SingleOrDefault(p => p.PieId == 0);
        }*/
    }
}
