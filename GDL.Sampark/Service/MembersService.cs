using GDL.Sampark.DataAccess;
using GDL.Sampark.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GDL.Sampark.Service
{
    public class MembersService
    {
        private List<Members> memberList = new List<Members>();

        public async Task<List<Members>> GetPagedListAsync()
        {
            MembersDAL membersDal = new MembersDAL();

            var memberListP11311 = membersDal.GetAllP10711();

            var memberListP11312 = membersDal.GetAllP10712();

            var memberListP11313 = membersDal.GetAllP10713();

            var memberListP11314 = membersDal.GetAllP10714();

            var memberListP11315 = membersDal.GetAllP10715();

            var memberListP11316 = membersDal.GetAllP10716();

            var memberListP11317 = membersDal.GetAllP10717();

            var memberListP11318 = membersDal.GetAllP10718();

            var memberListP10729 = membersDal.GetAllP10729();

            var memberListP106218 = membersDal.GetAllP106218();

            var memberListP106219 = membersDal.GetAllP106219();

            var memberListP106210 = membersDal.GetAllP107210();

            var memberListP106211 = membersDal.GetAllP107211();

            var memberListP106212 = membersDal.GetAllP107212();

            var memberListP106213 = membersDal.GetAllP107213();

            var memberListP106214 = membersDal.GetAllP107214();

            var memberListP106215 = membersDal.GetAllP107215();

            var memberListP106216 = membersDal.GetAllP107216();

            var memberListP106217 = membersDal.GetAllP107217();

            var memberListP107320 = membersDal.GetAllP107320();

            var memberListP107321 = membersDal.GetAllP107321();

            var memberListP107322 = membersDal.GetAllP107322();

            var memberListP107323 = membersDal.GetAllP107323();

            var memberListP107324 = membersDal.GetAllP107324();

            var memberListP107325 = membersDal.GetAllP107325();

            //memberList = memberListP11311.ToList();

            //memberList = memberListP11311.Concat(memberListP11312)
            //    .Concat(memberListP11313)
            //    .Concat(memberListP11314)
            //    .Concat(memberListP11315)
            //    .Concat(memberListP11316)
            //    .Concat(memberListP11317)
            //    .Concat(memberListP11318)
            //    .Concat(memberListP10729)
            //    .Concat(memberListP106218)
            //    .Concat(memberListP106219)
            //    .Concat(memberListP106211)
            //    .Concat(memberListP106212)
            //    .Concat(memberListP106213)
            //    .Concat(memberListP106214)
            //    .Concat(memberListP106215)
            //    .Concat(memberListP106216)
            //    .Concat(memberListP106217)
            //    .ToList();

            memberList = memberListP11311.Concat(memberListP11312)
                                        //.Concat(memberListP11313)
                                        //.Concat(memberListP11314)
                                        //.Concat(memberListP11315)
                                        //.Concat(memberListP11316)
                                        //.Concat(memberListP11317)
                                        //.Concat(memberListP11318)
                                        //.Concat(memberListP10729)
                                        //.Concat(memberListP106218)
                                        //.Concat(memberListP106219)
                                        //.Concat(memberListP106211)
                                        //.Concat(memberListP106212)
                                        //.Concat(memberListP106213)
                                        //.Concat(memberListP106214)
                                        //.Concat(memberListP106215)
                                        //.Concat(memberListP106216)
                                        //.Concat(memberListP106217)
                                        //.Concat(memberListP107320)
                                        //.Concat(memberListP107321)
                                        //.Concat(memberListP107322)
                                        //.Concat(memberListP107323)
                                        //.Concat(memberListP107324)
                                        //.Concat(memberListP107325)
                                        .ToList();

            return await Task.Factory.StartNew(() => memberList);
        }

        public async Task<List<Members>> GetAllMembers()
        {
            memberList = await GetPagedListAsync();
            return await Task.Factory.StartNew(() => memberList.ToList());
        }

        public async Task<List<Members>> GetMembersByAlphabeticalWise()
        {
            memberList = await GetPagedListAsync();
            return await Task.Factory.StartNew(() => memberList.OrderBy(x => x.FM_NAME).ToList());
        }

        public async Task<List<Members>> GetMembersByAgeWise()
        {
            memberList = await GetPagedListAsync();
            return await Task.Factory.StartNew(() => memberList.OrderByDescending(x => x.Age).ToList());
        }

        public async Task<List<Members>> GetMembersByHouseWise()
        {
            memberList = await GetPagedListAsync();
            return await Task.Factory.StartNew(() => memberList.OrderBy(x => x.HOUSE_NO).ToList());
        }

        public async Task<List<Members>> GetMembersByHeadOfTheFamily()
        {
            memberList = await GetPagedListAsync();
            return await Task.Factory.StartNew(() => memberList.GroupBy(x => x.HOUSE_NO).Select(r => r.OrderByDescending(x => x.Age).First()).ToList());
        }

        public async Task<List<Members>> GetMembersByMarriedWomenWise()
        {
            memberList = await GetPagedListAsync();
            return await Task.Factory.StartNew(() => memberList.Where(x => x.Sex == "F" && x.STATUSTYPE == "M").ToList());
        }

        public async Task<List<Members>> GetMembersByAddressWise()
        {
            memberList = await GetPagedListAsync();
            return await Task.Factory.StartNew(() => memberList.OrderBy(x => x.AREAID).ToList());
        }

        public async Task<List<Members>> GetMembersDoubleName()
        {
            memberList = await GetPagedListAsync();
            return await Task.Factory.StartNew(() => memberList.GroupBy(x => new { x.FM_NAME }).Where(g => g.Count() > 1).Select(g => g.First()).ToList());
        }

        public async Task<List<Members>> GetMembersSurName()
        {
            memberList = await GetPagedListAsync();
            return await Task.Factory.StartNew(() => memberList.OrderBy(x => x.LastName).ToList());
        }

        public async Task<List<Members>> GetMembersDetials(string houseNo)
        {
            memberList = await GetPagedListAsync();
            return await Task.Factory.StartNew(() => memberList.Where(x => x.HOUSE_NO.Contains(houseNo)).ToList());
        }
    }
}