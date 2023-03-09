using System.Collections.Generic;
using System.Xml.Linq;

namespace PieShop.Models
{
    public class MockPieRepository : IPieRepository
    {
        private readonly ICategoryRepository _categoryRepository = new MockCategoryRepository();// 
        public IEnumerable<Pie> PiesOfTheWeek {
            
            get
            { 

                return AllPies.Where(p => p.IsPieOfTheWeek);// there must one accessor while doing this
            }
         }

        public IEnumerable<Pie> AllPies => new List<Pie>
        {
            new Pie{PieId = 1,Name="Strawberry Pie",Price=16.3M,ShortDescription="Lorem Ipsum",
                LongDescription="A very good Strawaberry Pie",Category = _categoryRepository.AllCategories.ToList()[0],
                ImageUrl="https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.freepik.com%2Ffree-photos-vectors%2Fstrawberry-pie&psig=AOvVaw0uJ6V7Z0Wx_hXPu3g8VaL4&ust=1678466973806000&source=images&cd=vfe&ved=0CA8QjRxqFwoTCNCzxe-mz_0CFQAAAAAdAAAAABAE",
                ImageThumbnailUrl="",InStock=true,IsPieOfTheWeek=false},

            new Pie{PieId = 2,Name="Cheese cake",Price=18.9M,ShortDescription="Cheese burst",
                LongDescription="A very good cheese cake",Category = _categoryRepository.AllCategories.ToList()[1],
                ImageUrl="https://thumbs.dreamstime.com/b/creamy-mascarpone-cheese-cake-strawberry-winter-berries-new-york-cheesecake-close-up-christmas-dessert-healthy-78995578.jpg",
                ImageThumbnailUrl="",InStock=true,IsPieOfTheWeek=false},

            new Pie {PieId = 3,Name="Seasonal pies",Price=20M,ShortDescription="Works in season",
                LongDescription="A very good Seasonal pies",Category = _categoryRepository.AllCategories.ToList()[2],
                ImageUrl="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRfyI_Qw0QtqdwCoYjuzxijyUGVclqSC_Q-vHBLoPnyDQ&s",
                ImageThumbnailUrl="",InStock=true,IsPieOfTheWeek=false}
        };

        public Pie? GetPieById(int pieId)
        {
           return AllPies.FirstOrDefault(p=>p.PieId == pieId);
        }
    }
}
