using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL.Repo
{
    public record Song(int Id, string Title, string Album, int Year, string Artist);

    //public class Song
    //{
    // public Song(int id, string title, string album, int year, string artist)
    // {
    // Id = id;
    // Title = title;
    // Album = album;
    // Year = year;
    // Artist = artist;
    // }

    // public int Id { get; }
    // public string Title{ get; }
    // public string Album{ get; }
    // public int Year { get; ; }
    // public string Artist { get; }
    // public override string ToString()
    // {
    // return $&quot;Id {Id} Title {Title} Album {Album} Year {Year} Artist {Artist}&quot;;
    // }
    //}

}