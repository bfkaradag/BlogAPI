using Dapper;
using System;
using Models;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Collections.Generic;

namespace DataServices
{
    public class AdminService : DBBase
    {
        Helpers helpers = new Helpers();

        public UserCred GetUser(string username)
        {
            try
            {
                UserCred usr = conn.QueryFirstOrDefault<UserCred>("SELECT name, username, password FROM users WHERE username = @usrnm", new { usrnm = username });
                if(usr != null)
                {
                    usr.password = helpers.DecryptPassword(usr.password);
                    return usr;
                }
                return null;

            }
            catch (Exception e)
            {

                return null;
                throw e;
            }
        }
        public int AddPost(BlogPost post)
        {
            try
            {
                int result = conn.Execute("INSERT INTO posts (title, imgurl, content, author, readCount, cd)" +
                                           "VALUES (@ttl, @imgurl, @cnt, @author, @rc,CURRENT_TIMESTAMP())",
                                           new
                                           {
                                               ttl = post.title,
                                               imgurl = post.imgurl,
                                               cnt = post.content,
                                               author = post.author,
                                               rc = post.readCount
                                           });
                int lastId = conn.QueryFirstOrDefault<int>("SELECT MAX(id) FROM posts;");

                foreach (string item in post.tags)
                {
                    AddTag(item, post.id);
                }
                return result;
            }
            catch (Exception e)
            {
                return -1;
                throw e;
            }
        }
        public void AddTag(string tag, int postId)
        {
            try
            {
               
                conn.Execute("INSERT INTO posttags (tagname, postid) VALUES(@tag, @postId)", new { tag = tag, postId = postId });                   
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public int UpdatePost(BlogPost post)
        {
            try
            {
                int result = conn.Execute("UPDATE posts SET " +
                                          "title = @title," +
                                          "imgurl = @imgurl," +
                                          "content = @content" +
                                          " WHERE id = @id", new {
                                          title = post.title,
                                          imgurl = post.imgurl,
                                          content = post.content,
                                          id = post.id
                                          });

                UpdateTags(post.tags, post.id);

                return result;
            }
            catch (Exception e)
            {
                return -1;
                throw e;
            }
        }
        public void UpdateTags(string[] tags, int postId)
        {
            try
            {
                List<string> postTags = conn.Query<string>("SELECT tagname FROM posttags WHERE postid = " + postId + ";").ToList();
                foreach (string x in postTags)
                {
                    if (!tags.Contains(x))
                        RemoveTag(x, postId);
                }   

                foreach(string i in tags)
                {
                    if (!postTags.Contains(i))
                        AddTag(i, postId);
                }
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void RemoveTag(string tagName, int postId)
        {
            try
            {
               int result = conn.Execute("DELETE FROM posttags WHERE tagname = @tag AND postid = @pid", new { tag = tagName, pid = postId });
            }
            catch (Exception e)
            {

                throw;
            }
        }
        public int DeletePost(int postId)
        {
            try
            {
                int result = conn.Execute("DELETE FROM posts WHERE id = " + postId + ";");
                return result;
            }
            catch (Exception e)
            {
                return -1;
                throw;
            }
        }
    }
}
