using Dapper;
using GDL.Sampark.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;

namespace GDL.Sampark.DataAccess
{
    public class MembersDAL
    {
        //OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=‪D:\AccessDB\P11311.MDB;Persist Security Info = False;");

        public List<Members> GetAllP10711()
        {
            using (IDbConnection db = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\WARD WISE DETAILS\1\P10711.mdb;Persist Security Info=False;"))
            {
                return db.Query<Members>("Select * from A00000").ToList();
            }
        }

        public List<Members> GetAllP10712()
        {
            using (IDbConnection db = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\WARD WISE DETAILS\1\P10712.mdb;Persist Security Info=False;"))
            {
                return db.Query<Members>("Select * from A00000").ToList();
            }
        }

        public List<Members> GetAllP10713()
        {
            using (IDbConnection db = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\WARD WISE DETAILS\1\P10713.mdb;Persist Security Info=False;"))
            {
                return db.Query<Members>("Select * from A00000").ToList();
            }
        }

        public List<Members> GetAllP10714()
        {
            using (IDbConnection db = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\WARD WISE DETAILS\1\P10714.mdb;Persist Security Info=False;"))
            {
                return db.Query<Members>("Select * from A00000").ToList();
            }
        }

        public List<Members> GetAllP10715()
        {
            using (IDbConnection db = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\WARD WISE DETAILS\1\P10715.mdb;Persist Security Info=False;"))
            {
                return db.Query<Members>("Select * from A00000").ToList();
            }
        }

        public List<Members> GetAllP10716()
        {
            using (IDbConnection db = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\WARD WISE DETAILS\1\P10716.mdb;Persist Security Info=False;"))
            {
                return db.Query<Members>("Select * from A00000").ToList();
            }
        }

        public List<Members> GetAllP10717()
        {
            using (IDbConnection db = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\WARD WISE DETAILS\1\P10717.mdb;Persist Security Info=False;"))
            {
                return db.Query<Members>("Select * from A00000").ToList();
            }
        }

        public List<Members> GetAllP10718()
        {
            using (IDbConnection db = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\WARD WISE DETAILS\1\P10718.mdb;Persist Security Info=False;"))
            {
                return db.Query<Members>("Select * from A00000").ToList();
            }
        }

        public List<Members> GetAllP10729()
        {
            using (IDbConnection db = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\WARD WISE DETAILS\2\P10729.mdb;Persist Security Info=False;"))
            {
                return db.Query<Members>("Select * from A00000").ToList();
            }
        }

        public List<Members> GetAllP106218()
        {
            using (IDbConnection db = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\WARD WISE DETAILS\2\P106218.mdb;Persist Security Info=False;"))
            {
                return db.Query<Members>("Select * from A00000").ToList();
            }
        }

        public List<Members> GetAllP106219()
        {
            using (IDbConnection db = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\WARD WISE DETAILS\2\P106219.mdb;Persist Security Info=False;"))
            {
                return db.Query<Members>("Select * from A00000").ToList();
            }
        }

        public List<Members> GetAllP107210()
        {
            using (IDbConnection db = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\WARD WISE DETAILS\2\P107210.mdb;Persist Security Info=False;"))
            {
                return db.Query<Members>("Select * from A00000").ToList();
            }
        }

        public List<Members> GetAllP107211()
        {
            using (IDbConnection db = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\WARD WISE DETAILS\2\P107211.mdb;Persist Security Info=False;"))
            {
                return db.Query<Members>("Select * from A00000").ToList();
            }
        }

        public List<Members> GetAllP107212()
        {
            using (IDbConnection db = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\WARD WISE DETAILS\2\P107212.mdb;Persist Security Info=False;"))
            {
                return db.Query<Members>("Select * from A00000").ToList();
            }
        }

        public List<Members> GetAllP107213()
        {
            using (IDbConnection db = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\WARD WISE DETAILS\2\P107213.mdb;Persist Security Info=False;"))
            {
                return db.Query<Members>("Select * from A00000").ToList();
            }
        }

        public List<Members> GetAllP107214()
        {
            using (IDbConnection db = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\WARD WISE DETAILS\2\P107214.mdb;Persist Security Info=False;"))
            {
                return db.Query<Members>("Select * from A00000").ToList();
            }
        }

        public List<Members> GetAllP107215()
        {
            using (IDbConnection db = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\WARD WISE DETAILS\2\P107215.mdb;Persist Security Info=False;"))
            {
                return db.Query<Members>("Select * from A00000").ToList();
            }
        }

        public List<Members> GetAllP107216()
        {
            using (IDbConnection db = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\WARD WISE DETAILS\2\P107216.mdb;Persist Security Info=False;"))
            {
                return db.Query<Members>("Select * from A00000").ToList();
            }
        }

        public List<Members> GetAllP107217()
        {
            using (IDbConnection db = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\WARD WISE DETAILS\2\P107217.mdb;Persist Security Info=False;"))
            {
                return db.Query<Members>("Select * from A00000").ToList();
            }
        }

        public List<Members> GetAllP107320()
        {
            using (IDbConnection db = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\WARD WISE DETAILS\3\P107320.mdb;Persist Security Info=False;"))
            {
                return db.Query<Members>("Select * from A00000").ToList();
            }
        }

        public List<Members> GetAllP107321()
        {
            using (IDbConnection db = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\WARD WISE DETAILS\3\P107321.mdb;Persist Security Info=False;"))
            {
                return db.Query<Members>("Select * from A00000").ToList();
            }
        }

        public List<Members> GetAllP107322()
        {
            using (IDbConnection db = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\WARD WISE DETAILS\3\P107322.mdb;Persist Security Info=False;"))
            {
                return db.Query<Members>("Select * from A00000").ToList();
            }
        }

        public List<Members> GetAllP107323()
        {
            using (IDbConnection db = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\WARD WISE DETAILS\3\P107323.mdb;Persist Security Info=False;"))
            {
                return db.Query<Members>("Select * from A00000").ToList();
            }
        }

        public List<Members> GetAllP107324()
        {
            using (IDbConnection db = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\WARD WISE DETAILS\3\P107324.mdb;Persist Security Info=False;"))
            {
                return db.Query<Members>("Select * from A00000").ToList();
            }
        }

        public List<Members> GetAllP107325()
        {
            using (IDbConnection db = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\WARD WISE DETAILS\3\P107325.mdb;Persist Security Info=False;"))
            {
                return db.Query<Members>("Select * from A00000").ToList();
            }
        }
    }
}