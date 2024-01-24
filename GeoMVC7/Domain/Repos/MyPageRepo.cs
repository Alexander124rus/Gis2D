using GeoMVC7.Domain.Entities.GeoEntitie;
using GeoMVC7.Domain.Repos.Base;
using GeoMVC7.Domain.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GeoMVC7.Domain.Repos
{
    public class MyPageRepo : BaseRepo<MyPage>, IMyPageRepo
    {
        public MyPageRepo(ApplicationContext context) : base(context)
        {
        }

        //internal MyPageRepo(DbContextOptions<ApplicationContext> options) : base(options)
        //{
        //}
        public override IEnumerable<MyPage> GetAll()
            => Table.Include(m => m.MyGeometry).ToList();

    }
}
