using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataServices
{

    public class DBBase
    {
        
            
        public MySqlConnection conn = new MySqlConnection("Server=SV;Database=DB;Uid=USR;Pwd=pwd;Port=3306;charset=latin5;");
        

    }
}
