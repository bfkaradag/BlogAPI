using System;
using System.Collections.Generic;
using System.Text;
using Models;
using Dapper;
using System.Linq;

namespace DataServices
{
    public class UserService : DBBase
    {
        public List<BlogPost> GetPostsByLazyLoad(int page)
        {
            try
            {
                string query = "SELECT * FROM posts LIMIT " + (page).ToString() + "," + (page + 9);
                List<BlogPost> lst = conn.Query<BlogPost>(query).ToList();
                for(int i = 0; i < lst.Count; i++)
                {
                    string[] tags = conn.Query<string>("SELECT tagname FROM posttags WHERE postid = " + lst[i].id).ToArray();
                    lst[i].tags = tags;
                }
                return lst;
            }
            catch (Exception e)
            {
                return null;
                throw e;
            }
        }

        public int GetPostsLength()
        {
            try
            {
                int result = conn.QueryFirstOrDefault<int>("SELECT COUNT(*) FROM posts");
                return result;
            }
            catch (Exception e)
            {
                return -1;
                throw;
            }
        }

        public List<BlogPost> GetPostBySearchTagName(string txt)
        {
            try
            {
                string getTagsQuery = $"SELECT postid FROM posttags WHERE (tagname like CONCAT('%','{txt}','%') OR tagname like CONCAT('%','{txt}','') OR tagname LIKE CONCAT('','{txt}','%'))";
                int[] tags = conn.Query<int>(getTagsQuery).ToArray();
                string getPostsQuery = "SELECT DISTINCT * FROM posts WHERE id in (" + string.Join(",",tags) + ")";
                List<BlogPost> lst = conn.Query<BlogPost>(getPostsQuery).ToList();
                for (int i = 0; i < lst.Count; i++)
                {
                    string[] tagsX = conn.Query<string>("SELECT tagname FROM posttags WHERE postid = " + lst[i].id).ToArray();
                    lst[i].tags = tagsX;
                }
                return lst;

            }
            catch (Exception e)
            {
                return null;
                throw;
            }
        }
    }
}
