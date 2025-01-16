using System.Data.SqlClient;
using SQL.Repo;
var songRepo = new SQLSongRepos();
int userChoise;
bool isCorrectChoise;
bool isEnd = false;
string artist;
string title;
string album;
int year;
do
{
    do
    {
        UserQuestion();
        isCorrectChoise = int.TryParse(Console.ReadLine(), out userChoise) && userChoise > 0 && userChoise < 7;
        if (isCorrectChoise)
        {
            switch (userChoise)
            {
                case 1:
                    AddNewSong();
                    break;
                case 2:
                    DeleteOneSong();
                    break;
                case 3:
                    UpdateOneSong();
                    break;
                case 4:
                    ShowAllSongs();
                    break;
                case 5:
                    ShowAllArtistSong();
                    break;
                case 6:
                    isEnd = true;
                    break;
            }
        }
        else
        {
            Console.WriteLine("Wrong Value. Try one more time.");
        }
        Console.WriteLine("Press any key...");
        Console.ReadKey();
        Console.Clear();
    } while (!isCorrectChoise);
} while (!isEnd);

void UserQuestion()
{
    Console.WriteLine("Choose an option:");
    Console.WriteLine("1. Add song");
    Console.WriteLine("2. Delete song");
    Console.WriteLine("3. Update song");
    Console.WriteLine("4. Show all songs");
    Console.WriteLine("5. Show all songs of the artist");
    Console.WriteLine("6.Quit application");
}

void ShowAllSongs()
{
    var songs = songRepo.ReadAll();
    foreach (var song in songs)
    {
        Console.WriteLine(song);
    }
}

void ShowAllArtistSong()
{
    Console.WriteLine("Insert artist name");
    string filter = Console.ReadLine();
    var songs = songRepo.ReadAllByArtist(filter);
    foreach (var song in songs)
    {
        Console.WriteLine(song);
    }
}

void AddNewSong()
{
    Console.WriteLine("Insert title");
    title = DumbTester(true);
    Console.WriteLine("Insert album");
    album = DumbTester(true);
    Console.WriteLine("Insert year");
    year = int.Parse(DumbTester(false));
    Console.WriteLine("Insert artist");
    artist = DumbTester(true);
    var song = new Song(0, title, album, year, artist);
    songRepo.AddSong(song);
    Console.WriteLine("Song has been added");
}

void DeleteOneSong()
{
    Console.WriteLine("Insert title");
    title = DumbTester(true);
    Console.WriteLine("Insert album");
    album = DumbTester(true);
    Console.WriteLine("Insert year");
    year = int.Parse(DumbTester(false));
    Console.WriteLine("Insert artist");
    artist = DumbTester(true);
    var song = new Song(0, title, album, year, artist);
    songRepo.DeleteSong(song);
    Console.WriteLine("Song has been deleted");
}

void UpdateOneSong()
{
    Console.WriteLine("Insert song info");
    Console.WriteLine("Insert title");
    title = DumbTester(true);
    Console.WriteLine("Insert album");
    album = DumbTester(true);
    Console.WriteLine("Insert year");
    year = int.Parse(DumbTester(false));
    Console.WriteLine("Insert artist");
    artist = DumbTester(true);
    var song = new Song(0, title, album, year, artist);
    Console.WriteLine("Insert changed values");
    Console.WriteLine("Insert new title");
    title = DumbTester(true);
    Console.WriteLine("Insert new album");
    album = DumbTester(true);
    Console.WriteLine("Insert new year");
    year = int.Parse(DumbTester(false));
    Console.WriteLine("Insert new artist");
    artist = DumbTester(true);
    var newSong = new Song(0, title, album, year, artist);
    songRepo.Update(song, newSong);
    Console.WriteLine("Song has been updated");
}


string DumbTester(bool isString)
{
    string result;
    bool isCorrect = false;
    do
    {
        result = Console.ReadLine();

        switch (isString)
        {
            case true:
                if (result.Length > 3)
                {
                    isCorrect = true;
                }
                else
                    Console.WriteLine("Incorrect value!");
                break;

            case false:
                int intResult;
                bool isParsing = false;
                do
                {
                    isParsing = int.TryParse(result, out intResult);
                    if(!isParsing)
                    result = Console.ReadLine();
                } while (!isParsing);

                if (intResult < DateTime.Now.Year && intResult > 1990)
                {
                    isCorrect = true;
                    result = intResult.ToString();
                }
                else
                    Console.WriteLine("Incorrect value!");
                break;
        }
    } while (!isCorrect);
    return result;
}