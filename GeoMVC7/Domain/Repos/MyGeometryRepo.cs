using System;
using System.Collections.Generic;
using GeoMVC7.Domain.Entities.GeoEntitie;
using GeoMVC7.Domain.Repos.Base;
using GeoMVC7.Domain.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace GeoMVC7.Domain.Repos
{
    public class MyGeometryRepo : BaseRepo<MyGeometry>, IMyGeometryRepo
    {
        public MyGeometryRepo(ApplicationContext context) : base(context){}
        public override IEnumerable<MyGeometry> GetAll()
            => Table.Include(m => m.MyPage).ToList();
        public override MyGeometry? Find(int? id)
            => Table.IgnoreQueryFilters().Where(x => x.Id == id).Include(m => m.MyPage).FirstOrDefault();
        //public override int Update(MyGeometry entity, bool persist = true)
        //{
            
        //    Table.Update(entity);
        //    return persist ? SaveChanges() : 0;
        //}
    }
}
