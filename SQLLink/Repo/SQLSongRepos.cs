
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections;

namespace SQL.Repo
{
    public class SQLSongRepos
    {
        private string _connectionString = "Server=localhost;Database=Yauhen;Integrated Security=true;";

        public void AddSong(Song song)
        {
            //insert into Song values(0,"Yapadapadu","Album bum bum",2003,"Flinstone");
            var query = $"insert into Song values('{song.Title}','{song.Album}',{song.Year},'{song.Artist}');";
            Execute(query);
        }
        public void DeleteSong(Song song)
        {
            //insert into Song values(0,"Yapadapadu","Album bum bum",2003,"Flinstone");
            var query = $"delete from Song where Title='{song.Title} ' and Album ='{song.Album} ' and [Year] = {song.Year} and Artist ='{song.Artist}';";
            Execute(query);
        }
        public void Update(Song song, Song newSong)
        {
            var query = $"update song set Title='{newSong.Title}', Artist='{newSong.Artist} ', Album = '{newSong.Album}' , [Year] = {newSong.Year} where Title='{song.Title} ' and Album ='{song.Album} ' and [Year] = {song.Year} and Artist ='{song.Artist}';";
            Execute(query);
        }
        private void Execute(string query)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
        public Song[] ReadAll()
        {
            var query = "Select * from Song;";
            return ReadDB(query);
        }
        public Song[] ReadAllByArtist(string filter)
        {
            var query = $"select * from Song where Artist like @Filter";
            return ReadDBBetter(query, filter);
        }
        private Song[] ReadDB(string query)
        {
            var list = new List<Song>();
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    var reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        var id = int.Parse(reader["Id"].ToString());
                        var title = reader["Title"].ToString();
                        var album = reader["Album"].ToString();
                        var year = int.Parse(reader["Year"].ToString());
                        var artist = reader["Artist"].ToString();
                        var song = new Song(id, title, album, year, artist);
                        list.Add(song);
                    }
                }
            }
            return list.ToArray();
        }
        private Song[] ReadDBBetter(string query, string filter)
        {
            var list = new List<Song>();
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@Filter", filter + "%");
                    var reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        var id = int.Parse(reader["Id"].ToString());
                        var title = reader["Title"].ToString();
                        var album = reader["Album"].ToString();
                        var year = int.Parse(reader["Year"].ToString());
                        var artist = reader["Artist"].ToString();
                        var song = new Song(id, title, album, year, artist);
                        list.Add(song);
                    }
                }
            }
            return list.ToArray();
        }
    }
}